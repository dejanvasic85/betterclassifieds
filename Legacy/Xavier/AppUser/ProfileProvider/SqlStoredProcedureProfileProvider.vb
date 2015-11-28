'------------------------------------------------------------------------------ 
' <copyright file="SqlProfileProvider.cs" company="Microsoft"> 
' Copyright (c) Microsoft Corporation. All rights reserved. 
' </copyright> 
'------------------------------------------------------------------------------ 

Imports System
Imports System.Web.Profile
Imports System.Web.Configuration
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web.Hosting
Imports System.Security
Imports System.Configuration.Provider

Namespace ProfileProvider
    Public Class SqlStoredProcedureProfileProvider
        Inherits System.Web.Profile.ProfileProvider
        Private _appName As String
        Private _sqlConnectionString As String
        Private _readSproc As String
        Private _setSproc As String
        Private _commandTimeout As Integer
        Private _profileVersion As String

        Public Overloads Overrides Sub Initialize(ByVal name As String, ByVal config As NameValueCollection)

            If config Is Nothing Then
                Throw New ArgumentNullException("config")
            End If
            If [String].IsNullOrEmpty(name) Then
                name = "StoredProcedureDBProfileProvider"
            End If
            If String.IsNullOrEmpty(config("description")) Then
                config.Remove("description")
                config.Add("description", "StoredProcedureDBProfileProvider")
            End If
            MyBase.Initialize(name, config)

            Dim temp As String = config("connectionStringName")
            If [String].IsNullOrEmpty(temp) Then
                Throw New ProviderException("connectionStringName not specified")
            End If
            _sqlConnectionString = GetConnectionString(temp)
            If [String].IsNullOrEmpty(_sqlConnectionString) Then
                Throw New ProviderException("connectionStringName not specified")
            End If

            _appName = config("applicationName")
            If String.IsNullOrEmpty(_appName) Then
                _appName = GetDefaultAppName()
            End If

            If _appName.Length > 256 Then
                Throw New ProviderException("Application name too long")
            End If

            _setSproc = config("setProcedure")
            If [String].IsNullOrEmpty(_setSproc) Then
                Throw New ProviderException("setProcedure not specified")
            End If

            _readSproc = config("readProcedure")
            If [String].IsNullOrEmpty(_readSproc) Then
                Throw New ProviderException("readProcedure not specified")
            End If

            Dim timeout As String = config("commandTimeout")
            If String.IsNullOrEmpty(timeout) OrElse Not Int32.TryParse(timeout, _commandTimeout) Then
                _commandTimeout = 30
            End If

            ' Added the Profile Version code below 
            _profileVersion = config("profileVersion")
            If String.IsNullOrEmpty(_profileVersion) Then
                Throw New ProviderException("No profile version specified")
            End If

            Dim testProfileVersion As Integer

            If Int32.TryParse(_profileVersion, testProfileVersion) = False Then
                Throw New ProviderException("Profile version must be an Integer")
            End If
            ' End Addition 

            config.Remove("commandTimeout")
            config.Remove("connectionStringName")
            config.Remove("applicationName")
            config.Remove("readProcedure")
            config.Remove("setProcedure")
            config.Remove("profileVersion")
            If config.Count > 0 Then
                Dim attribUnrecognized As String = config.GetKey(0)
                If Not [String].IsNullOrEmpty(attribUnrecognized) Then
                    Throw New ProviderException("Unrecognized config attribute:" & attribUnrecognized)
                End If
            End If
        End Sub


        ' Added the property below to obtain the ProfileVersion from the web.config 
        Public Shared ReadOnly Property ProfileVersion() As Int32
            Get
                Dim profileConfig As System.Web.Configuration.ProfileSection = DirectCast(WebConfigurationManager.GetSection("system.web/profile"), System.Web.Configuration.ProfileSection)
                Return Int32.Parse(profileConfig.Providers(1).ElementInformation.Properties("profileVersion").Value.ToString())
            End Get
        End Property
        ' End 


        Friend Shared Function GetDefaultAppName() As String
            Try
                Dim appName As String = HostingEnvironment.ApplicationVirtualPath
                If [String].IsNullOrEmpty(appName) Then
                    appName = System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName
                    Dim indexOfDot As Integer = appName.IndexOf("."c)
                    If indexOfDot <> -1 Then
                        appName = appName.Remove(indexOfDot)
                    End If
                End If

                If [String].IsNullOrEmpty(appName) Then
                    Return "/"
                Else
                    Return appName
                End If
            Catch generatedExceptionName As SecurityException
                Return "/"
            End Try
        End Function

        Friend Shared Function GetConnectionString(ByVal specifiedConnectionString As String) As String
            If [String].IsNullOrEmpty(specifiedConnectionString) Then
                Return Nothing
            End If

            ' Check <connectionStrings> config section for this connection string 
            Dim connObj As ConnectionStringSettings = ConfigurationManager.ConnectionStrings(specifiedConnectionString)
            If connObj IsNot Nothing Then
                Return connObj.ConnectionString
            End If

            Return Nothing
        End Function

        Public Overloads Overrides Property ApplicationName() As String
            Get
                Return _appName
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then
                    Throw New ArgumentNullException("ApplicationName")
                End If
                If value.Length > 256 Then
                    Throw New ProviderException("Application name too long")
                End If
                _appName = value

            End Set
        End Property

        Private ReadOnly Property CommandTimeout() As Integer
            Get
                Return _commandTimeout
            End Get
        End Property

        ' customProviderData = "Varname;SqlDbType;size" 
        Private Sub GetProfileDataFromSproc(ByVal properties As SettingsPropertyCollection, ByVal svc As SettingsPropertyValueCollection, ByVal username As String, ByVal conn As SqlConnection, ByVal userIsAuthenticated As Boolean)

            Dim cmd As SqlCommand = CreateSprocSqlCommand(_readSproc, conn, username, userIsAuthenticated)
            Try
                cmd.Parameters.RemoveAt("@IsUserAnonymous")
                'anonymous flag not needed on get 
                Dim columnData As New List(Of ProfileColumnData)(properties.Count)
                For Each prop As SettingsProperty In properties
                    Dim value As New SettingsPropertyValue(prop)
                    svc.Add(value)

                    Dim persistenceData As String = TryCast(prop.Attributes("CustomProviderData"), String)
                    ' If we can't find the table/column info we will ignore this data 
                    If [String].IsNullOrEmpty(persistenceData) Then
                        ' REVIEW: Perhaps we should throw instead? 
                        Continue For
                    End If
                    Dim chunk As String() = persistenceData.Split(New Char() {";"c})
                    If chunk.Length <> 3 Then
                        ' REVIEW: Perhaps we should throw instead? 
                        Continue For
                    End If
                    Dim varname As String = chunk(0)
                    ' REVIEW: Should we ignore case? 
                    Dim datatype As SqlDbType = DirectCast([Enum].Parse(GetType(SqlDbType), chunk(1), True), SqlDbType)

                    Dim size As Integer = 0
                    If Not Int32.TryParse(chunk(2), size) Then
                        Throw New ArgumentException("Unable to parse as integer: " & chunk(2))
                    End If

                    ' not needed for get 
                    columnData.Add(New ProfileColumnData(varname, value, Nothing, datatype))
                    cmd.Parameters.Add(CreateOutputParam(varname, datatype, size))
                Next

                cmd.ExecuteNonQuery()
                For i As Integer = 0 To columnData.Count - 1
                    Dim colData As ProfileColumnData = columnData(i)
                    Dim val As Object = cmd.Parameters(colData.VariableName).Value
                    Dim propValue As SettingsPropertyValue = colData.PropertyValue

                    'Only initialize a SettingsPropertyValue for non-null values 
                    If Not (TypeOf val Is DBNull OrElse val Is Nothing) Then
                        propValue.PropertyValue = val
                        propValue.IsDirty = False
                        propValue.Deserialized = True
                    End If
                Next
            Finally
                cmd.Dispose()
            End Try
        End Sub

        Public Overloads Overrides Function GetPropertyValues(ByVal context As SettingsContext, ByVal collection As SettingsPropertyCollection) As SettingsPropertyValueCollection
            Dim svc As New SettingsPropertyValueCollection()

            If collection Is Nothing OrElse collection.Count < 1 OrElse context Is Nothing Then
                Return svc
            End If

            Dim username As String = DirectCast(context("UserName"), String)
            Dim userIsAuthenticated As Boolean = CBool(context("IsAuthenticated"))
            If [String].IsNullOrEmpty(username) Then
                Return svc
            End If

            Dim conn As SqlConnection = Nothing
            Try
                conn = New SqlConnection(_sqlConnectionString)
                conn.Open()

                GetProfileDataFromSproc(collection, svc, username, conn, userIsAuthenticated)
            Finally
                If conn IsNot Nothing Then
                    conn.Close()
                End If
            End Try

            Return svc
        End Function

        ' Container struct for use in aggregating columns for queries 
        Private Structure ProfileColumnData
            Public VariableName As String
            Public PropertyValue As SettingsPropertyValue
            Public Value As Object
            Public DataType As SqlDbType

            Public Sub New(ByVal var As String, ByVal pv As SettingsPropertyValue, ByVal val As Object, ByVal type As SqlDbType)
                VariableName = var
                PropertyValue = pv
                Value = val
                DataType = type
            End Sub
        End Structure

        ' Helper that just sets up the usual sproc sqlcommand parameters and adds the applicationname/username 
        Private Function CreateSprocSqlCommand(ByVal sproc As String, ByVal conn As SqlConnection, ByVal username As String, ByVal isAnonymous As Boolean) As SqlCommand
            Dim cmd As New SqlCommand(sproc, conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = CommandTimeout
            cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName)
            cmd.Parameters.AddWithValue("@Username", username)
            cmd.Parameters.AddWithValue("@IsUserAnonymous", isAnonymous)
            Return cmd
        End Function

        Public Overloads Overrides Sub SetPropertyValues(ByVal context As SettingsContext, ByVal collection As SettingsPropertyValueCollection)
            Dim username As String = DirectCast(context("UserName"), String)
            Dim userIsAuthenticated As Boolean = CBool(context("IsAuthenticated"))

            If username Is Nothing OrElse username.Length < 1 OrElse collection.Count < 1 Then
                Exit Sub
            End If

            Dim conn As SqlConnection = Nothing
            Dim cmd As SqlCommand = Nothing
            Try
                Dim anyItemsToSave As Boolean = False

                ' First make sure we have at least one item to save 
                For Each pp As SettingsPropertyValue In collection
                    If pp.IsDirty Then
                        If Not userIsAuthenticated Then
                            Dim allowAnonymous As Boolean = CBool(pp.[Property].Attributes("AllowAnonymous"))
                            If Not allowAnonymous Then
                                Continue For
                            End If
                        End If
                        anyItemsToSave = True
                        Exit For
                    End If
                Next

                If Not anyItemsToSave Then
                    Exit Sub
                End If

                conn = New SqlConnection(_sqlConnectionString)
                conn.Open()

                Dim columnData As New List(Of ProfileColumnData)(collection.Count)

                For Each pp As SettingsPropertyValue In collection
                    If Not userIsAuthenticated Then
                        Dim allowAnonymous As Boolean = CBool(pp.[Property].Attributes("AllowAnonymous"))
                        If Not allowAnonymous Then
                            Continue For
                        End If
                    End If

                    'Unlike the table provider, the sproc provider works against a fixed stored procedure 
                    'signature, and must provide values for each stored procedure parameter 
                    ' 
                    'if (!pp.IsDirty && pp.UsingDefaultValue) // Not fetched from DB and not written to 
                    ' continue; 

                    Dim persistenceData As String = TryCast(pp.[Property].Attributes("CustomProviderData"), String)
                    ' If we can't find the table/column info we will ignore this data 
                    If [String].IsNullOrEmpty(persistenceData) Then
                        ' REVIEW: Perhaps we should throw instead? 
                        Continue For
                    End If
                    Dim chunk As String() = persistenceData.Split(New Char() {";"c})
                    If chunk.Length <> 3 Then
                        ' REVIEW: Perhaps we should throw instead? 
                        Continue For
                    End If
                    Dim varname As String = chunk(0)
                    ' REVIEW: Should we ignore case? 
                    Dim datatype As SqlDbType = DirectCast([Enum].Parse(GetType(SqlDbType), chunk(1), True), SqlDbType)
                    ' chunk[2] = size, which we ignore 

                    Dim value As Object = Nothing

                    If Not pp.IsDirty AndAlso pp.UsingDefaultValue Then
                        ' Not fetched from DB and not written to 
                        value = DBNull.Value
                    ElseIf pp.Deserialized AndAlso pp.PropertyValue Is Nothing Then
                        ' value was explicitly set to null 
                        value = DBNull.Value
                    Else
                        value = pp.PropertyValue
                    End If

                    ' REVIEW: Might be able to ditch datatype 
                    columnData.Add(New ProfileColumnData(varname, pp, value, datatype))
                Next

                cmd = CreateSprocSqlCommand(_setSproc, conn, username, userIsAuthenticated)
                For Each data As ProfileColumnData In columnData
                    cmd.Parameters.AddWithValue(data.VariableName, data.Value)
                    cmd.Parameters(data.VariableName).SqlDbType = data.DataType
                Next
                cmd.ExecuteNonQuery()
            Finally
                If cmd IsNot Nothing Then
                    cmd.Dispose()
                End If
                If conn IsNot Nothing Then
                    conn.Close()
                End If
            End Try
        End Sub


        Private Shared Function CreateOutputParam(ByVal paramName As String, ByVal dbType As SqlDbType, ByVal size As Integer) As SqlParameter
            Dim param As New SqlParameter(paramName, dbType)
            param.Direction = ParameterDirection.Output
            param.Size = size
            Return param
        End Function

        ' Mangement APIs from ProfileProvider class 

        Public Overloads Overrides Function DeleteProfiles(ByVal profiles As ProfileInfoCollection) As Integer
            Throw New NotSupportedException("This method is not supported for this provider.")
        End Function

        Public Overloads Overrides Function DeleteProfiles(ByVal usernames As String()) As Integer
            Throw New NotSupportedException("This method is not supported for this provider.")
        End Function

        Public Overloads Overrides Function DeleteInactiveProfiles(ByVal authenticationOption As ProfileAuthenticationOption, ByVal userInactiveSinceDate As DateTime) As Integer
            Throw New NotSupportedException("This method is not supported for this provider.")
        End Function

        Public Overloads Overrides Function GetNumberOfInactiveProfiles(ByVal authenticationOption As ProfileAuthenticationOption, ByVal userInactiveSinceDate As DateTime) As Integer
            Throw New NotSupportedException("This method is not supported for this provider.")
        End Function

        Public Overloads Overrides Function GetAllProfiles(ByVal authenticationOption As ProfileAuthenticationOption, ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As ProfileInfoCollection
            Throw New NotSupportedException("This method is not supported for this provider.")
        End Function

        Public Overloads Overrides Function GetAllInactiveProfiles(ByVal authenticationOption As ProfileAuthenticationOption, ByVal userInactiveSinceDate As DateTime, ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As ProfileInfoCollection
            Throw New NotSupportedException("This method is not supported for this provider.")
        End Function

        Public Overloads Overrides Function FindProfilesByUserName(ByVal authenticationOption As ProfileAuthenticationOption, ByVal usernameToMatch As String, ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As ProfileInfoCollection
            Throw New NotSupportedException("This method is not supported for this provider.")
        End Function

        Public Overloads Overrides Function FindInactiveProfilesByUserName(ByVal authenticationOption As ProfileAuthenticationOption, ByVal usernameToMatch As String, ByVal userInactiveSinceDate As DateTime, ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As ProfileInfoCollection
            Throw New NotSupportedException("This method is not supported for this provider.")
        End Function
    End Class
End Namespace