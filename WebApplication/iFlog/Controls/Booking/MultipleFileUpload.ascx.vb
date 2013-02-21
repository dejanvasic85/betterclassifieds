Public Partial Class MultipleFileUpload
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If _UpperLimit = 0 Then
            lblCaption.Text = "Maximum Files: No Limit"
        Else
            lblCaption.Text = String.Format("Maximum Files: {0}", _UpperLimit)
        End If

        pnlListBox.Attributes("style") = "overflow:auto;"
        pnlListBox.Height = Unit.Pixel(20 * _Rows - 1)

    End Sub

    ' This is Click event defenition for MultipleFileUpload control.
    Public Event Click As MultipleFileUploadClick

    Private _Rows As Integer
    ''' <summary>
    ''' The Number of Visible rows to display
    ''' </summary>
    Public Property Rows() As Integer
        Get
            Return _Rows
        End Get
        Set(ByVal value As Integer)
            _Rows = value
        End Set
    End Property

    Private _UpperLimit As Integer
    ''' <summary>
    ''' The no of maximukm files to upload.
    ''' </summary>
    Public Property UpperLimit() As Integer
        Get
            Return _UpperLimit
        End Get
        Set(ByVal value As Integer)
            _UpperLimit = value
        End Set
    End Property

    ''' <summary>
    ''' Method for Upload Click Event
    ''' </summary>
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click

        Dim eventArgs As New FileCollectionEventArgs(Me.Request)

        RaiseEvent Click(Me, eventArgs)

    End Sub

    Public Sub RegisterScript()
        Page.ClientScript.RegisterStartupScript(GetType(Page), "MyScript", GetJavascript)
    End Sub

    ''' <summary>
    ''' This method is used to generate javascript code for MultipleFileUpload control that execute at client side.
    ''' </summary>
    ''' <returns>Javascript as a string object.</returns>
    ''' <remarks></remarks>
    Private Function GetJavascript() As String
        Dim JavaScript As New StringBuilder

        JavaScript.Append("<script type='text/javascript'>")
        JavaScript.Append("var Id = 0;" + Environment.NewLine)
        JavaScript.AppendFormat("var MAX = {0};" + Environment.NewLine, _UpperLimit)
        JavaScript.AppendFormat("var DivFiles = document.getElementById('{0}');" + Environment.NewLine, pnlFiles.ClientID)
        JavaScript.AppendFormat("var DivListBox = document.getElementById('{0}');" + Environment.NewLine, pnlListBox.ClientID)
        JavaScript.AppendFormat("var BtnAdd = document.getElementById('{0}');" + Environment.NewLine, btnAdd.ClientID)
        JavaScript.Append("function Add()")
        JavaScript.Append("{" + Environment.NewLine)
        JavaScript.Append("var IpFile = GetTopFile();" + Environment.NewLine)
        JavaScript.Append("if(IpFile == null || IpFile.value == null || IpFile.value.length == 0)" + Environment.NewLine)
        JavaScript.Append("{" + Environment.NewLine)
        JavaScript.Append("alert('Please select a file to add.');" + Environment.NewLine)
        JavaScript.Append("return;" + Environment.NewLine)
        JavaScript.Append("}" + Environment.NewLine)
        JavaScript.Append("var NewIpFile = CreateFile();" + Environment.NewLine)
        JavaScript.Append("DivFiles.insertBefore(NewIpFile,IpFile);" + Environment.NewLine)
        JavaScript.Append("if(MAX != 0 && GetTotalFiles() - 1 == MAX)" + Environment.NewLine)
        JavaScript.Append("{" + Environment.NewLine)
        JavaScript.Append("NewIpFile.disabled = true;" + Environment.NewLine)
        JavaScript.Append("BtnAdd.disabled = true;" + Environment.NewLine)
        JavaScript.Append("}" + Environment.NewLine)
        JavaScript.Append("IpFile.style.display = 'none';" + Environment.NewLine)
        JavaScript.Append("DivListBox.appendChild(CreateItem(IpFile));" + Environment.NewLine)
        JavaScript.Append("}" + Environment.NewLine)
        JavaScript.Append("function CreateFile()")
        JavaScript.Append("{" + Environment.NewLine)
        JavaScript.Append("var IpFile = document.createElement('input');" + Environment.NewLine)
        JavaScript.Append("IpFile.id = IpFile.name = 'IpFile_' + Id++;" + Environment.NewLine)
        JavaScript.Append("IpFile.type = 'file';" + Environment.NewLine)
        JavaScript.Append("return IpFile;" + Environment.NewLine)
        JavaScript.Append("}" + Environment.NewLine)
        JavaScript.Append("function CreateItem(IpFile)" + Environment.NewLine)
        JavaScript.Append("{" + Environment.NewLine)
        JavaScript.Append("var Item = document.createElement('div');" + Environment.NewLine)
        JavaScript.Append("Item.style.backgroundColor = '#ffffff';" + Environment.NewLine)
        JavaScript.Append("Item.style.fontWeight = 'normal';" + Environment.NewLine)
        JavaScript.Append("Item.style.textAlign = 'left';" + Environment.NewLine)
        JavaScript.Append("Item.style.verticalAlign = 'middle'; " + Environment.NewLine)
        JavaScript.Append("Item.style.cursor = 'default';" + Environment.NewLine)
        JavaScript.Append("Item.style.height = 20 + 'px';" + Environment.NewLine)
        JavaScript.Append("var Splits = IpFile.value.split('\\\\');" + Environment.NewLine)
        JavaScript.Append("Item.innerHTML = Splits[Splits.length - 1] + '&nbsp;';" + Environment.NewLine)
        JavaScript.Append("Item.value = IpFile.id;" + Environment.NewLine)
        JavaScript.Append("Item.title = IpFile.value;" + Environment.NewLine)
        JavaScript.Append("var A = document.createElement('a');" + Environment.NewLine)
        JavaScript.Append("A.innerHTML = 'Delete';" + Environment.NewLine)
        JavaScript.Append("A.id = 'A_' + Id++;" + Environment.NewLine)
        JavaScript.Append("A.href = '#';" + Environment.NewLine)
        JavaScript.Append("A.style.color = 'blue';" + Environment.NewLine)
        JavaScript.Append("A.onclick = function()" + Environment.NewLine)
        JavaScript.Append("{" + Environment.NewLine)
        JavaScript.Append("DivFiles.removeChild(document.getElementById(this.parentNode.value));" + Environment.NewLine)
        JavaScript.Append("DivListBox.removeChild(this.parentNode);" + Environment.NewLine)
        JavaScript.Append("if(MAX != 0 && GetTotalFiles() - 1 < MAX)" + Environment.NewLine)
        JavaScript.Append("{" + Environment.NewLine)
        JavaScript.Append("GetTopFile().disabled = false;" + Environment.NewLine)
        JavaScript.Append("BtnAdd.disabled = false;" + Environment.NewLine)
        JavaScript.Append("}" + Environment.NewLine)
        JavaScript.Append("}" + Environment.NewLine)
        JavaScript.Append("Item.appendChild(A);" + Environment.NewLine)
        JavaScript.Append("Item.onmouseover = function()" + Environment.NewLine)
        JavaScript.Append("{" + Environment.NewLine)
        JavaScript.Append("Item.bgColor = Item.style.backgroundColor;" + Environment.NewLine)
        JavaScript.Append("Item.fColor = Item.style.color;" + Environment.NewLine)
        JavaScript.Append("Item.style.backgroundColor = '#C6790B';" + Environment.NewLine)
        JavaScript.Append("Item.style.color = '#ffffff';" + Environment.NewLine)
        JavaScript.Append("Item.style.fontWeight = 'bold';" + Environment.NewLine)
        JavaScript.Append("}" + Environment.NewLine)
        JavaScript.Append("Item.onmouseout = function()" + Environment.NewLine)
        JavaScript.Append("{" + Environment.NewLine)
        JavaScript.Append("Item.style.backgroundColor = Item.bgColor;" + Environment.NewLine)
        JavaScript.Append("Item.style.color = Item.fColor;" + Environment.NewLine)
        JavaScript.Append("Item.style.fontWeight = 'normal';" + Environment.NewLine)
        JavaScript.Append("}" + Environment.NewLine)
        JavaScript.Append("return Item;" + Environment.NewLine)
        JavaScript.Append("}" + Environment.NewLine)
        JavaScript.Append("function Clear()" + Environment.NewLine)
        JavaScript.Append("{" + Environment.NewLine)
        JavaScript.Append("DivListBox.innerHTML = '';" + Environment.NewLine)
        JavaScript.Append("DivFiles.innerHTML = '';" + Environment.NewLine)
        JavaScript.Append("DivFiles.appendChild(CreateFile());" + Environment.NewLine)
        JavaScript.Append("BtnAdd.disabled = false;" + Environment.NewLine)
        JavaScript.Append("}" + Environment.NewLine)
        JavaScript.Append("function GetTopFile()" + Environment.NewLine)
        JavaScript.Append("{" + Environment.NewLine)
        JavaScript.Append("var Inputs = DivFiles.getElementsByTagName('input');" + Environment.NewLine)
        JavaScript.Append("var IpFile = null;" + Environment.NewLine)
        JavaScript.Append("for(var n = 0; n < Inputs.length && Inputs[n].type == 'file'; ++n)" + Environment.NewLine)
        JavaScript.Append("{" + Environment.NewLine)
        JavaScript.Append("IpFile = Inputs[n];" + Environment.NewLine)
        JavaScript.Append("break;" + Environment.NewLine)
        JavaScript.Append("}" + Environment.NewLine)
        JavaScript.Append("return IpFile;" + Environment.NewLine)
        JavaScript.Append("}" + Environment.NewLine)
        JavaScript.Append("function GetTotalFiles()" + Environment.NewLine)
        JavaScript.Append("{" + Environment.NewLine)
        JavaScript.Append("var Inputs = DivFiles.getElementsByTagName('input');" + Environment.NewLine)
        JavaScript.Append("var Counter = 0;" + Environment.NewLine)
        JavaScript.Append("for(var n = 0; n < Inputs.length && Inputs[n].type == 'file'; ++n)" + Environment.NewLine)
        JavaScript.Append("Counter++;" + Environment.NewLine)
        JavaScript.Append("return Counter;" + Environment.NewLine)
        JavaScript.Append("}" + Environment.NewLine)
        JavaScript.Append("function GetTotalItems()" + Environment.NewLine)
        JavaScript.Append("{" + Environment.NewLine)
        JavaScript.Append("var Items = DivListBox.getElementsByTagName('div');" + Environment.NewLine)
        JavaScript.Append("return Items.length;" + Environment.NewLine)
        JavaScript.Append("}" + Environment.NewLine)
        JavaScript.Append("function DisableTop()" + Environment.NewLine)
        JavaScript.Append("{" + Environment.NewLine)
        JavaScript.Append("if(GetTotalItems() == 0)" + Environment.NewLine)
        JavaScript.Append("{" + Environment.NewLine)
        JavaScript.Append("alert('Please browse at least one file to upload.');" + Environment.NewLine)
        JavaScript.Append("return false;" + Environment.NewLine)
        JavaScript.Append("}" + Environment.NewLine)
        JavaScript.Append("GetTopFile().disabled = true;" + Environment.NewLine)
        JavaScript.Append("return true;" + Environment.NewLine)
        JavaScript.Append("}" + Environment.NewLine)
        JavaScript.Append("</script>")

        Return JavaScript.ToString

    End Function

End Class



''' <summary>
''' EventArgs class that has some readonly properties regarding posted files corresponding to MultipleFileUpload control.
''' </summary>
Public Class FileCollectionEventArgs
    Inherits EventArgs
    Private _HttpRequest As HttpRequest

    Public ReadOnly Property PostedFiles() As HttpFileCollection
        Get
            Return _HttpRequest.Files
        End Get
    End Property

    Public ReadOnly Property Count() As Integer
        Get
            Return _HttpRequest.Files.Count
        End Get
    End Property

    Public ReadOnly Property HasFiles() As Boolean
        Get
            Return If(_HttpRequest.Files.Count > 0, True, False)
        End Get
    End Property

    Public ReadOnly Property TotalSize() As Double
        Get
            Dim Size As Double = 0.0R
            For n As Integer = 0 To _HttpRequest.Files.Count - 1
                If _HttpRequest.Files(n).ContentLength < 0 Then
                    Continue For
                Else
                    Size += _HttpRequest.Files(n).ContentLength
                End If
            Next

            Return Math.Round(Size / 1024.0R, 2)
        End Get
    End Property

    Public Sub New(ByVal oHttpRequest As HttpRequest)
        _HttpRequest = oHttpRequest
    End Sub
End Class

'Delegate that represents the Click event signature for MultipleFileUpload control.
Public Delegate Sub MultipleFileUploadClick(ByVal sender As Object, ByVal e As FileCollectionEventArgs)

