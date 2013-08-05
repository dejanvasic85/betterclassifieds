'------------------------------------------------------------------------------ 
' <copyright file="SqlProfileProvider.cs" company="Microsoft"> 
' Copyright (c) Microsoft Corporation. All rights reserved. 
' </copyright> 
'------------------------------------------------------------------------------ 

Imports System.Web
Imports System
Imports System.Web.Profile
Imports System.Web.Configuration
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports System.Configuration
Imports System.Configuration.Provider

Namespace ProfileProvider
    Public Class SqlTableProfileProvider
        Inherits Profile.ProfileProvider
        Private _appName As String
        Private _appId As Guid
        Private _appIdSet As Boolean
        Private _sqlConnectionString As String
        Private _commandTimeout As Integer
        Private _table As String
        Private _profileVersion As String

        Public Overloads Overrides Sub Initialize(ByVal name As String, ByVal config As NameValueCollection)
            If config Is Nothing Then
                Throw New ArgumentNullException("config")
            End If
            If [String].IsNullOrEmpty(name) Then
                name = "SqlTableProfileProvider"
            End If
            If String.IsNullOrEmpty(config("description")) Then
                config.Remove("description")
                config.Add("description", "SqlTableProfileProvider")
            End If
            MyBase.Initialize(name, config)

            _sqlConnectionString = Paramount.ApplicationBlock.Data.ConfigReader.GetConnectionString("paramount/services", "AppUserConnection")
            If [String].IsNullOrEmpty(_sqlConnectionString) Then
                Throw New ProviderException("connectionStringName not specified")
            End If

            _appName = config("applicationName")
            If String.IsNullOrEmpty(_appName) Then
                _appName = SqlStoredProcedureProfileProvider.GetDefaultAppName()
            End If

            If _appName.Length > 256 Then
                Throw New ProviderException("Application name too long")
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


            _table = config("table")
            If String.IsNullOrEmpty(_table) Then
                Throw New ProviderException("No table specified")
            End If
            EnsureValidTableOrColumnName(_table)

            Dim timeout As String = config("commandTimeout")
            If String.IsNullOrEmpty(timeout) OrElse Not Int32.TryParse(timeout, _commandTimeout) Then
                _commandTimeout = 30
            End If

            config.Remove("commandTimeout")
            config.Remove("connectionStringName")
            config.Remove("applicationName")
            config.Remove("table")
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
                _appIdSet = False
            End Set
        End Property

        Private ReadOnly Property AppId() As Guid
            Get
                If Not _appIdSet Then
                    Dim conn As SqlConnection = Nothing
                    Try
                        conn = New SqlConnection(_sqlConnectionString)
                        conn.Open()

                        Dim cmd As New SqlCommand("dbo.aspnet_Applications_CreateApplication", conn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@applicationname", ApplicationName)
                        cmd.Parameters.Add(CreateOutputParam("@ApplicationId", SqlDbType.UniqueIdentifier, 0))

                        cmd.ExecuteNonQuery()
                        _appId = DirectCast(cmd.Parameters("@ApplicationId").Value, Guid)
                        _appIdSet = True
                    Finally
                        If conn IsNot Nothing Then
                            conn.Close()
                        End If
                    End Try

                End If
                Return _appId
            End Get
        End Property

        Private ReadOnly Property CommandTimeout() As Integer
            Get
                Return _commandTimeout
            End Get
        End Property

        '''///////////////////////////////////////////////////////// 
        '''///////////////////////////////////////////////////////// 
        '''///////////////////////////////////////////////////////// 

        Private Shared s_legalChars As String = "_@#$"
        Private Shared Sub EnsureValidTableOrColumnName(ByVal name As String)
            For i As Integer = 0 To name.Length - 1
                If Not [Char].IsLetterOrDigit(name(i)) AndAlso s_legalChars.IndexOf(name(i)) = -1 Then
                    Throw New ProviderException("Table and column names cannot contain: " & name(i))
                End If
            Next
        End Sub

        Private Sub GetProfileDataFromTable(ByVal properties As SettingsPropertyCollection, ByVal svc As SettingsPropertyValueCollection, ByVal username As String, ByVal conn As SqlConnection)
            Dim columnData As New List(Of ProfileColumnData)(properties.Count)
            Dim commandText As New StringBuilder("SELECT u.UserID")
            Dim cmd As New SqlCommand([String].Empty, conn)

            Dim columnCount As Integer = 0
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
                If chunk.Length <> 2 Then
                    ' REVIEW: Perhaps we should throw instead? 
                    Continue For
                End If
                Dim columnName As String = chunk(0)
                ' REVIEW: Should we ignore case? 
                Dim datatype As SqlDbType = DirectCast([Enum].Parse(GetType(SqlDbType), chunk(1), True), SqlDbType)

                ' not needed for get 
                columnData.Add(New ProfileColumnData(columnName, value, Nothing, datatype))
                commandText.Append(", ")
                commandText.Append("t." & columnName)
                columnCount += 1
            Next

            commandText.Append(" FROM " & _table & " t, vw_aspnet_Users u WHERE u.ApplicationId = '").Append(AppId)
            commandText.Append("' AND u.UserName = LOWER(@Username) AND t.UserID = u.UserID")
            cmd.CommandText = commandText.ToString()
            cmd.CommandType = CommandType.Text
            cmd.Parameters.AddWithValue("@Username", username)
            Dim reader As SqlDataReader = Nothing

            Try
                reader = cmd.ExecuteReader()
                'If no row exists in the database, then the default Profile values 
                'from configuration are used. 
                If reader.Read() Then
                    Dim userId As Guid = reader.GetGuid(0)
                    For i As Integer = 0 To columnData.Count - 1
                        Dim val As Object = reader.GetValue(i + 1)
                        Dim colData As ProfileColumnData = columnData(i)
                        Dim propValue As SettingsPropertyValue = colData.PropertyValue

                        'Only initialize a SettingsPropertyValue for non-null values 
                        If Not (TypeOf val Is DBNull OrElse val Is Nothing) Then
                            propValue.PropertyValue = val
                            propValue.IsDirty = False
                            propValue.Deserialized = True
                        End If
                    Next

                    ' need to close reader before we try to update the user 
                    If reader IsNot Nothing Then
                        reader.Close()
                        reader = Nothing
                    End If

                    UpdateLastActivityDate(conn, userId)
                End If
            Finally
                If reader IsNot Nothing Then
                    reader.Close()
                End If
            End Try
        End Sub

        Private Shared Sub UpdateLastActivityDate(ByVal conn As SqlConnection, ByVal userId As Guid)
            Dim cmd As New SqlCommand(String.Format(Globalization.CultureInfo.CurrentCulture, "UPDATE aspnet_Users SET LastActivityDate = @LastUpdatedDate WHERE UserId = '{0}'", userId), conn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.AddWithValue("@LastUpdatedDate", DateTime.UtcNow)
            Try
                cmd.ExecuteNonQuery()
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
            If [String].IsNullOrEmpty(username) Then
                Return svc
            End If

            Dim conn As SqlConnection = Nothing
            Try
                conn = New SqlConnection(_sqlConnectionString)
                conn.Open()

                GetProfileDataFromTable(collection, svc, username, conn)
            Finally
                If conn IsNot Nothing Then
                    conn.Close()
                End If
            End Try

            Return svc
        End Function

        ' Container struct for use in aggregating columns for queries 
        Private Structure ProfileColumnData
            Public ColumnName As String
            Public PropertyValue As SettingsPropertyValue
            Public Value As Object
            Public DataType As SqlDbType

            Public Sub New(ByVal col As String, ByVal pv As SettingsPropertyValue, ByVal val As Object, ByVal type As SqlDbType)
                EnsureValidTableOrColumnName(col)
                ColumnName = col
                PropertyValue = pv
                Value = val
                DataType = type
            End Sub
        End Structure

        Public Overloads Overrides Sub SetPropertyValues(ByVal context As SettingsContext, ByVal collection As SettingsPropertyValueCollection)
            Dim username As String = DirectCast(context("UserName"), String)
            Dim userIsAuthenticated As Boolean = CBool(context("IsAuthenticated"))

            If username Is Nothing OrElse username.Length < 1 OrElse collection.Count < 1 Then
                Exit Sub
            End If

            Dim conn As SqlConnection = Nothing
            Dim reader As SqlDataReader = Nothing
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

                    'Normal logic for original SQL provider 
                    'if (!pp.IsDirty && pp.UsingDefaultValue) // Not fetched from DB and not written to 

                    'Can eliminate unnecessary updates since we are using a table though 
                    If Not pp.IsDirty Then
                        Continue For
                    End If

                    Dim persistenceData As String = TryCast(pp.[Property].Attributes("CustomProviderData"), String)
                    ' If we can't find the table/column info we will ignore this data 
                    If [String].IsNullOrEmpty(persistenceData) Then
                        ' REVIEW: Perhaps we should throw instead? 
                        Continue For
                    End If
                    Dim chunk As String() = persistenceData.Split(New Char() {";"c})
                    If chunk.Length <> 2 Then
                        ' REVIEW: Perhaps we should throw instead? 
                        Continue For
                    End If
                    Dim columnName As String = chunk(0)
                    ' REVIEW: Should we ignore case? 
                    Dim datatype As SqlDbType = DirectCast([Enum].Parse(GetType(SqlDbType), chunk(1), True), SqlDbType)

                    Dim value As Object = Nothing

                    ' REVIEW: Is this handling null case correctly? 
                    If pp.Deserialized AndAlso pp.PropertyValue Is Nothing Then
                        ' is value null? 
                        value = DBNull.Value
                    Else
                        value = pp.PropertyValue
                    End If

                    ' REVIEW: Might be able to ditch datatype 
                    columnData.Add(New ProfileColumnData(columnName, pp, value, datatype))
                Next

                ' Figure out userid, if we don't find a userid, go ahead and create a user in the aspnetUsers table 
                Dim userId As Guid = Guid.Empty
                cmd = New SqlCommand(String.Format("SELECT u.UserId FROM vw_aspnet_Users u WHERE u.ApplicationId = '{0}' AND u.UserName = LOWER(@Username)", AppId), conn)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.AddWithValue("@Username", username)
                Try
                    reader = cmd.ExecuteReader()
                    If reader.Read() Then
                        userId = reader.GetGuid(0)
                    Else
                        reader.Close()
                        cmd.Dispose()
                        reader = Nothing

                        cmd = New SqlCommand("dbo.aspnet_Users_CreateUser", conn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@ApplicationId", AppId)
                        cmd.Parameters.AddWithValue("@UserName", username)
                        cmd.Parameters.AddWithValue("@IsUserAnonymous", Not userIsAuthenticated)
                        cmd.Parameters.AddWithValue("@LastActivityDate", DateTime.UtcNow)
                        cmd.Parameters.Add(CreateOutputParam("@UserId", SqlDbType.UniqueIdentifier, 16))

                        cmd.ExecuteNonQuery()
                        userId = DirectCast(cmd.Parameters("@userid").Value, Guid)
                    End If
                Finally
                    If reader IsNot Nothing Then
                        reader.Close()
                        reader = Nothing
                    End If
                    cmd.Dispose()
                End Try

                ' Figure out if the row already exists in the table and use appropriate SELECT/UPDATE 
                cmd = New SqlCommand([String].Empty, conn)
                Dim sqlCommand As StringBuilder = New StringBuilder("IF EXISTS (SELECT 1 FROM ").Append(_table)
                sqlCommand.Append(" WHERE UserId = @UserId) ")
                cmd.Parameters.AddWithValue("@UserId", userId)

                ' Build up strings used in the query 
                Dim columnStr As New StringBuilder()
                Dim valueStr As New StringBuilder()
                Dim setStr As New StringBuilder()
                Dim count As Integer = 0
                For Each data As ProfileColumnData In columnData
                    columnStr.Append(", ")
                    valueStr.Append(", ")
                    columnStr.Append(data.ColumnName)
                    Dim valueParam As String = "@Value" & count
                    valueStr.Append(valueParam)
                    cmd.Parameters.AddWithValue(valueParam, data.Value)

                    ' REVIEW: Can't update Timestamps? 
                    If data.DataType <> SqlDbType.Timestamp Then
                        If count > 0 Then
                            setStr.Append(",")
                        End If
                        setStr.Append(data.ColumnName)
                        setStr.Append("=")
                        setStr.Append(valueParam)
                    End If

                    count += 1
                Next
                columnStr.Append(",LastUpdatedDate ")
                valueStr.Append(",@LastUpdatedDate")
                setStr.Append(",LastUpdatedDate=@LastUpdatedDate")
                cmd.Parameters.AddWithValue("@LastUpdatedDate", DateTime.UtcNow)

                sqlCommand.Append("BEGIN UPDATE ").Append(_table).Append(" SET ").Append(setStr.ToString())
                sqlCommand.Append(" WHERE UserId = '").Append(userId).Append("'")

                sqlCommand.Append("END ELSE BEGIN INSERT ").Append(_table).Append(" (UserId").Append(columnStr.ToString())
                sqlCommand.Append(") VALUES ('").Append(userId).Append("'").Append(valueStr.ToString()).Append(") END")

                cmd.CommandText = sqlCommand.ToString()
                cmd.CommandType = CommandType.Text

                cmd.ExecuteNonQuery()

                ' Need to close reader before we try to update 
                If reader IsNot Nothing Then
                    reader.Close()
                    reader = Nothing
                End If

                UpdateLastActivityDate(conn, userId)
            Finally
                If reader IsNot Nothing Then
                    reader.Close()
                End If
                If cmd IsNot Nothing Then
                    cmd.Dispose()
                End If
                If conn IsNot Nothing Then
                    conn.Close()
                End If
            End Try
        End Sub

        '''///////////////////////////////////////////////////////// 
        '''///////////////////////////////////////////////////////// 
        '''///////////////////////////////////////////////////////// 

        Private Shared Function CreateInputParam(ByVal paramName As String, ByVal dbType As SqlDbType, ByVal objValue As Object) As SqlParameter
            Dim param As New SqlParameter(paramName, dbType)
            If objValue Is Nothing Then
                objValue = [String].Empty
            End If
            param.Value = objValue
            Return param
        End Function

        Private Shared Function CreateOutputParam(ByVal paramName As String, ByVal dbType As SqlDbType, ByVal size As Integer) As SqlParameter
            Dim param As New SqlParameter(paramName, dbType)
            param.Direction = ParameterDirection.Output
            param.Size = size
            Return param
        End Function

        ' Mangement APIs from ProfileProvider class 

        Public Overloads Overrides Function DeleteProfiles(ByVal profiles As ProfileInfoCollection) As Integer
            If profiles Is Nothing Then
                Throw New ArgumentNullException("profiles")
            End If

            If profiles.Count < 1 Then
                Throw New ArgumentException("Profiles collection is empty")
            End If

            Dim usernames As String() = New String(profiles.Count - 1) {}

            Dim iter As Integer = 0
            For Each profile As ProfileInfo In profiles
                usernames(System.Math.Max(System.Threading.Interlocked.Increment(iter), iter - 1)) = profile.UserName
            Next

            Return DeleteProfiles(usernames)
        End Function
        '''////////////////////////////////////////////////////////////////////////// 
        '''////////////////////////////////////////////////////////////////////////// 
        Public Overloads Overrides Function DeleteProfiles(ByVal usernames As String()) As Integer
            If usernames Is Nothing OrElse usernames.Length < 1 Then
                Return 0
            End If

            Dim numProfilesDeleted As Integer = 0
            Dim beginTranCalled As Boolean = False
            Try
                Dim conn As SqlConnection = Nothing
                Try
                    conn = New SqlConnection(_sqlConnectionString)
                    conn.Open()

                    Dim cmd As SqlCommand
                    Dim numUsersRemaing As Integer = usernames.Length
                    While numUsersRemaing > 0
                        cmd = New SqlCommand([String].Empty, conn)
                        cmd.Parameters.AddWithValue("@UserName0", usernames(usernames.Length - numUsersRemaing))
                        Dim allUsers As New StringBuilder("@UserName0")
                        numUsersRemaing -= 1

                        Dim userIndex As Integer = 1
                        For iter As Integer = usernames.Length - numUsersRemaing To usernames.Length - 1
                            ' REVIEW: Should we check length of command string instead of parameter lengths? 
                            If allUsers.Length + usernames(iter).Length + 3 >= 4000 Then
                                Exit For
                            End If
                            Dim userNameParam As String = "@UserName" & userIndex
                            allUsers.Append(",")
                            allUsers.Append(userNameParam)
                            cmd.Parameters.AddWithValue(userNameParam, usernames(iter))
                            numUsersRemaing -= 1
                            userIndex += 1
                        Next

                        ' We don't need to start a transaction if we can finish this in one sql command 
                        If Not beginTranCalled AndAlso numUsersRemaing > 0 Then
                            Dim beginCmd As New SqlCommand("BEGIN TRANSACTION", conn)
                            beginCmd.ExecuteNonQuery()
                            beginTranCalled = True
                        End If


                        cmd.CommandText = String.Format("DELETE FROM {0} WHERE UserId IN ( SELECT u.UserId FROM vw_aspnet_Users u WHERE u.ApplicationId = '{1}' AND u.UserName IN ({2}))", _table, AppId, allUsers.ToString())
                        cmd.CommandTimeout = CommandTimeout
                        numProfilesDeleted += cmd.ExecuteNonQuery()
                    End While

                    If beginTranCalled Then
                        cmd = New SqlCommand("COMMIT TRANSACTION", conn)
                        cmd.ExecuteNonQuery()
                        beginTranCalled = False
                    End If
                Catch
                    If beginTranCalled Then
                        Dim cmd As New SqlCommand("ROLLBACK TRANSACTION", conn)
                        cmd.ExecuteNonQuery()
                        beginTranCalled = False
                    End If
                    Throw
                Finally
                    If conn IsNot Nothing Then
                        conn.Close()
                        conn = Nothing
                    End If
                End Try
            Catch
                Throw
            End Try
            Return numProfilesDeleted
        End Function

        Private Function GenerateQuery(ByVal delete As Boolean, ByVal authenticationOption As ProfileAuthenticationOption) As String
            Dim cmdStr As New StringBuilder(200)
            If delete Then
                cmdStr.Append("DELETE FROM ")
            Else
                cmdStr.Append("SELECT COUNT(*) FROM ")
            End If
            cmdStr.Append(_table)
            cmdStr.Append(" WHERE UserId IN (SELECT u.UserId FROM vw_aspnet_Users u WHERE u.ApplicationId = '").Append(AppId)
            cmdStr.Append("' AND (u.LastActivityDate <= @InactiveSinceDate)")
            Select Case authenticationOption
                Case ProfileAuthenticationOption.Anonymous
                    cmdStr.Append(" AND u.IsAnonymous = 1")
                    Exit Select
                Case ProfileAuthenticationOption.Authenticated
                    cmdStr.Append(" AND u.IsAnonymous = 0")
                    Exit Select
                Case ProfileAuthenticationOption.All
                    ' Want to delete all profiles here, so nothing more needed 
                    Exit Select
            End Select
            cmdStr.Append(")")
            Return cmdStr.ToString()
        End Function

        '''////////////////////////////////////////////////////////////////////////// 
        '''////////////////////////////////////////////////////////////////////////// 
        Public Overloads Overrides Function DeleteInactiveProfiles(ByVal authenticationOption As ProfileAuthenticationOption, ByVal userInactiveSinceDate As DateTime) As Integer
            Try
                Dim conn As SqlConnection = Nothing
                Dim cmd As SqlCommand = Nothing
                Try
                    conn = New SqlConnection(_sqlConnectionString)
                    conn.Open()

                    cmd = New SqlCommand(GenerateQuery(True, authenticationOption), conn)
                    cmd.CommandTimeout = CommandTimeout
                    cmd.Parameters.Add(CreateInputParam("@InactiveSinceDate", SqlDbType.DateTime, userInactiveSinceDate.ToUniversalTime()))

                    Return cmd.ExecuteNonQuery()
                Finally
                    If cmd IsNot Nothing Then
                        cmd.Dispose()
                    End If
                    If conn IsNot Nothing Then
                        conn.Close()
                        conn = Nothing
                    End If
                End Try
            Catch
                Throw
            End Try
        End Function

        '''////////////////////////////////////////////////////////////////////////// 
        '''////////////////////////////////////////////////////////////////////////// 
        Public Overloads Overrides Function GetNumberOfInactiveProfiles(ByVal authenticationOption As ProfileAuthenticationOption, ByVal userInactiveSinceDate As DateTime) As Integer
            Dim conn As SqlConnection = Nothing
            Dim cmd As SqlCommand = Nothing
            Try
                conn = New SqlConnection(_sqlConnectionString)
                conn.Open()

                cmd = New SqlCommand(GenerateQuery(False, authenticationOption), conn)
                cmd.CommandTimeout = CommandTimeout
                cmd.Parameters.Add(CreateInputParam("@InactiveSinceDate", SqlDbType.DateTime, userInactiveSinceDate.ToUniversalTime()))

                Dim o As Object = cmd.ExecuteScalar()
                If o Is Nothing OrElse Not (TypeOf o Is Integer) Then
                    Return 0
                End If
                Return CInt(o)
            Finally
                If cmd IsNot Nothing Then
                    cmd.Dispose()
                End If
                If conn IsNot Nothing Then
                    conn.Close()
                    conn = Nothing
                End If
            End Try
        End Function

        ' TODO: Implement size 
        Private Function GenerateTempInsertQueryForGetProfiles(ByVal authenticationOption As ProfileAuthenticationOption) As StringBuilder
            Dim cmdStr As New StringBuilder(200)
            cmdStr.Append("INSERT INTO #PageIndexForProfileUsers (UserId) ")
            cmdStr.Append("SELECT u.UserId FROM vw_aspnet_Users u, ").Append(_table)
            cmdStr.Append(" p WHERE ApplicationId = '").Append(AppId)
            cmdStr.Append("' AND u.UserId = p.UserId")
            Select Case authenticationOption
                Case ProfileAuthenticationOption.Anonymous
                    cmdStr.Append(" AND u.IsAnonymous = 1")
                    Exit Select
                Case ProfileAuthenticationOption.Authenticated
                    cmdStr.Append(" AND u.IsAnonymous = 0")
                    Exit Select
                Case ProfileAuthenticationOption.All
                    ' Want to delete all profiles here, so nothing more needed 
                    Exit Select
            End Select
            Return cmdStr
        End Function

        '''////////////////////////////////////////////////////////////////////////// 
        '''////////////////////////////////////////////////////////////////////////// 
        Public Overloads Overrides Function GetAllProfiles(ByVal authenticationOption As ProfileAuthenticationOption, ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As ProfileInfoCollection
            Dim insertQuery As StringBuilder = GenerateTempInsertQueryForGetProfiles(authenticationOption)
            Return GetProfilesForQuery(Nothing, pageIndex, pageSize, insertQuery, totalRecords)
        End Function

        '''////////////////////////////////////////////////////////////////////////// 
        '''////////////////////////////////////////////////////////////////////////// 
        Public Overloads Overrides Function GetAllInactiveProfiles(ByVal authenticationOption As ProfileAuthenticationOption, ByVal userInactiveSinceDate As DateTime, ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As ProfileInfoCollection
            Dim insertQuery As StringBuilder = GenerateTempInsertQueryForGetProfiles(authenticationOption)
            insertQuery.Append(" AND u.LastActivityDate <= @InactiveSinceDate")
            Dim args As SqlParameter() = New SqlParameter(0) {}
            args(0) = CreateInputParam("@InactiveSinceDate", SqlDbType.DateTime, userInactiveSinceDate.ToUniversalTime())
            Return GetProfilesForQuery(args, pageIndex, pageSize, insertQuery, totalRecords)
        End Function

        '''////////////////////////////////////////////////////////////////////////// 
        '''////////////////////////////////////////////////////////////////////////// 
        Public Overloads Overrides Function FindProfilesByUserName(ByVal authenticationOption As ProfileAuthenticationOption, ByVal usernameToMatch As String, ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As ProfileInfoCollection
            Dim insertQuery As StringBuilder = GenerateTempInsertQueryForGetProfiles(authenticationOption)
            insertQuery.Append(" AND u.UserName LIKE LOWER(@UserName)")
            Dim args As SqlParameter() = New SqlParameter(0) {}
            args(0) = CreateInputParam("@UserName", SqlDbType.NVarChar, usernameToMatch)
            Return GetProfilesForQuery(args, pageIndex, pageSize, insertQuery, totalRecords)
        End Function

        '''////////////////////////////////////////////////////////////////////////// 
        '''////////////////////////////////////////////////////////////////////////// 
        Public Overloads Overrides Function FindInactiveProfilesByUserName(ByVal authenticationOption As ProfileAuthenticationOption, ByVal usernameToMatch As String, ByVal userInactiveSinceDate As DateTime, ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As ProfileInfoCollection
            Dim insertQuery As StringBuilder = GenerateTempInsertQueryForGetProfiles(authenticationOption)
            insertQuery.Append(" AND u.UserName LIKE LOWER(@UserName) AND u.LastActivityDate <= @InactiveSinceDate")
            Dim args As SqlParameter() = New SqlParameter(1) {}
            args(0) = CreateInputParam("@InactiveSinceDate", SqlDbType.DateTime, userInactiveSinceDate.ToUniversalTime())
            args(1) = CreateInputParam("@UserName", SqlDbType.NVarChar, usernameToMatch)
            Return GetProfilesForQuery(args, pageIndex, pageSize, insertQuery, totalRecords)
        End Function

        ' Private methods 

        '''////////////////////////////////////////////////////////////////////////// 
        '''////////////////////////////////////////////////////////////////////////// 
        Private Function GetProfilesForQuery(ByVal insertArgs As SqlParameter(), ByVal pageIndex As Integer, ByVal pageSize As Integer, ByVal insertQuery As StringBuilder, ByRef totalRecords As Integer) As ProfileInfoCollection
            If pageIndex < 0 Then
                Throw New ArgumentException("pageIndex")
            End If
            If pageSize < 1 Then
                Throw New ArgumentException("pageSize")
            End If

            Dim lowerBound As Long = CLng(pageIndex) * pageSize
            Dim upperBound As Long = lowerBound + pageSize - 1
            If upperBound > Int32.MaxValue Then
                Throw New ArgumentException("pageIndex and pageSize")
            End If

            Dim conn As SqlConnection = Nothing
            Dim reader As SqlDataReader = Nothing
            Dim cmd As SqlCommand = Nothing
            Try
                conn = New SqlConnection(_sqlConnectionString)
                conn.Open()

                Dim cmdStr As New StringBuilder(200)
                ' Create a temp table TO store the select results 
                cmd = New SqlCommand("CREATE TABLE #PageIndexForProfileUsers(IndexId int IDENTITY (0, 1) NOT NULL, UserId uniqueidentifier)", conn)
                cmd.CommandTimeout = CommandTimeout
                cmd.ExecuteNonQuery()
                cmd.Dispose()

                insertQuery.Append(" ORDER BY UserName")
                cmd = New SqlCommand(insertQuery.ToString(), conn)
                cmd.CommandTimeout = CommandTimeout
                If insertArgs IsNot Nothing Then
                    For Each arg As SqlParameter In insertArgs
                        cmd.Parameters.Add(arg)
                    Next
                End If

                cmd.ExecuteNonQuery()
                cmd.Dispose()

                cmdStr = New StringBuilder(200)
                cmdStr.Append("SELECT u.UserName, u.IsAnonymous, u.LastActivityDate, p.LastUpdatedDate FROM vw_aspnet_Users u, ").Append(_table)
                cmdStr.Append(" p, #PageIndexForProfileUsers i WHERE u.UserId = p.UserId AND p.UserId = i.UserId AND i.IndexId >= ")
                cmdStr.Append(lowerBound).Append(" AND i.IndexId <= ").Append(upperBound)
                cmd = New SqlCommand(cmdStr.ToString(), conn)
                cmd.CommandTimeout = CommandTimeout

                reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess)
                Dim profiles As New ProfileInfoCollection()
                While reader.Read()
                    Dim username As String
                    Dim dtLastActivity As DateTime, dtLastUpdated As DateTime = DateTime.UtcNow
                    Dim isAnon As Boolean

                    username = reader.GetString(0)
                    isAnon = reader.GetBoolean(1)
                    dtLastActivity = DateTime.SpecifyKind(reader.GetDateTime(2), DateTimeKind.Utc)
                    dtLastUpdated = DateTime.SpecifyKind(reader.GetDateTime(3), DateTimeKind.Utc)
                    profiles.Add(New ProfileInfo(username, isAnon, dtLastActivity, dtLastUpdated, 0))
                End While
                totalRecords = profiles.Count

                If reader IsNot Nothing Then
                    reader.Close()
                    reader = Nothing
                End If

                cmd.Dispose()

                ' Cleanup, REVIEW: should move to finally? 
                cmd = New SqlCommand("DROP TABLE #PageIndexForProfileUsers", conn)
                cmd.ExecuteNonQuery()

                Return profiles
            Finally
                If reader IsNot Nothing Then
                    reader.Close()
                End If

                If cmd IsNot Nothing Then
                    cmd.Dispose()
                End If

                If conn IsNot Nothing Then
                    conn.Close()
                    conn = Nothing
                End If
            End Try
        End Function
    End Class
End Namespace