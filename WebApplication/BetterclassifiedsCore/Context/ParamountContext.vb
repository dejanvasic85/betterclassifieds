Imports System
Imports System.Globalization
Imports System.Web
Imports BetterclassifiedsCore.Exceptions

Namespace Context
    ''' <summary>
    ''' Base class for the context objects
    ''' </summary>
    <Serializable()> _
Public MustInherit Class ParamountContext
        ''' <summary>
        ''' This is the key, that implemeting class will use to return key to store in a session
        ''' This key will be used by caller to get or set the session value
        ''' </summary>
        ''' <returns></returns>
        Public MustOverride ReadOnly Property ContextKey() As String

        Protected Shared Sub ClearContext(ByVal contextKey As String)
            HttpContext.Current.Session(contextKey) = Nothing
        End Sub

        ''' <summary>
        ''' The set the current context for the control.
        ''' More than one context can be set.
        ''' </summary>
        ''' <param name="currentParamountContext"></param>
        Protected Shared Sub SetContext(ByVal currentParamountContext As ParamountContext)
            HttpContext.Current.Session(currentParamountContext.ContextKey) = currentParamountContext
        End Sub

        ''' <summary>..
        ''' To get the context set by caller.
        ''' Pass the context key name
        ''' </summary>
        ''' <param name="contextKeyName"></param>
        ''' <returns></returns>
        Protected Shared Function GetContext(ByVal contextKeyName As String) As ParamountContext
            If HttpContext.Current.Session IsNot Nothing Then
                Return TryCast(HttpContext.Current.Session(contextKeyName), ParamountContext)
            End If

            Return Nothing
        End Function

        ''' <summary>
        ''' A common function to raise context not set exception
        ''' </summary>
        ''' <param name="contextName"></param>
        Protected Shared Sub ThrowContextNotSetException(ByVal contextName As String)
            Throw New ContextNotSetException(String.Format(CultureInfo.InvariantCulture, "Context {0} has not been set.", contextName))
        End Sub
    End Class
End Namespace