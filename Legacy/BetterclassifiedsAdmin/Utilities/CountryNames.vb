Imports System
Imports System.Data
Imports System.Collections
Imports System.Collections.Specialized
Imports System.Configuration
Imports System.IO
Imports System.Linq
Imports System.Web
Imports System.Web.Caching
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq


Namespace CountryNames
    Public NotInheritable Class CountryNames
        Private Sub New()
        End Sub
        Private Shared _countries As String() = New String() {"United States", "Canada", "Mexico", "Afghanistan", "Albania", "Algeria", _
         "American Samoa", "Andorra", "Angola", "Anguilla", "Antarctica", "Antigua And Barbuda", _
         "Argentina", "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan", _
         "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", _
         "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia", "Bosnia Hercegovina", _
         "Botswana", "Bouvet Island", "Brazil", "Brunei Darussalam", "Bulgaria", "Burkina Faso", _
         "Burundi", "Byelorussian SSR", "Cambodia", "Cameroon", "Canada", "Cape Verde", _
         "Cayman Islands", "Central African Republic", "Chad", "Chile", "China", "Christmas Island", _
         "Cocos (Keeling) Islands", "Colombia", "Comoros", "Congo", "Cook Islands", "Costa Rica", _
         "Cote D'Ivoire", "Croatia", "Cuba", "Cyprus", "Czech Republic", "Czechoslovakia", _
         "Denmark", "Djibouti", "Dominica", "Dominican Republic", "East Timor", "Ecuador", _
         "Egypt", "El Salvador", "England", "Equatorial Guinea", "Eritrea", "Estonia", _
         "Ethiopia", "Falkland Islands", "Faroe Islands", "Fiji", "Finland", "France", _
         "Gabon", "Gambia", "Georgia", "Germany", "Ghana", "Gibraltar", _
         "Great Britain", "Greece", "Greenland", "Grenada", "Guadeloupe", "Guam", _
         "Guatemela", "Guernsey", "Guiana", "Guinea", "Guinea-Bissau", "Guyana", _
         "Haiti", "Heard Islands", "Honduras", "Hong Kong", "Hungary", "Iceland", _
         "India", "Indonesia", "Iran", "Iraq", "Ireland", "Isle Of Man", _
         "Israel", "Italy", "Jamaica", "Japan", "Jersey", "Jordan", _
         "Kazakhstan", "Kenya", "Kiribati", "Korea, South", "Korea, North", "Kuwait", _
         "Kyrgyzstan", "Lao People's Dem. Rep.", "Latvia", "Lebanon", "Lesotho", "Liberia", _
         "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Macau", "Macedonia", _
         "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", _
         "Mariana Islands", "Marshall Islands", "Martinique", "Mauritania", "Mauritius", "Mayotte", _
         "Mexico", "Micronesia", "Moldova", "Monaco", "Mongolia", "Montserrat", _
         "Morocco", "Mozambique", "Myanmar", "Namibia", "Nauru", "Nepal", _
         "Netherlands", "Netherlands Antilles", "Neutral Zone", "New Caledonia", "New Zealand", "Nicaragua", _
         "Niger", "Nigeria", "Niue", "Norfolk Island", "Northern Ireland", "Norway", _
         "Oman", "Pakistan", "Palau", "Panama", "Papua New Guinea", "Paraguay", _
         "Peru", "Philippines", "Pitcairn", "Poland", "Polynesia", "Portugal", _
         "Puerto Rico", "Qatar", "Reunion", "Romania", "Russian Federation", "Rwanda", _
         "Saint Helena", "Saint Kitts", "Saint Lucia", "Saint Pierre", "Saint Vincent", "Samoa", _
         "San Marino", "Sao Tome and Principe", "Saudi Arabia", "Scotland", "Senegal", "Seychelles", _
         "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "Solomon Islands", "Somalia", _
         "South Africa", "South Georgia", "Spain", "Sri Lanka", "Sudan", "Suriname", _
         "Svalbard", "Swaziland", "Sweden", "Switzerland", "Syrian Arab Republic", "Taiwan", _
         "Tajikista", "Tanzania", "Thailand", "Togo", "Tokelau", "Tonga", _
         "Trinidad and Tobago", "Tunisia", "Turkey", "Turkmenistan", "Turks and Caicos Islands", "Tuvalu", _
         "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom", "United States", "Uruguay", _
         "Uzbekistan", "Vanuatu", "Vatican City State", "Venezuela", "Vietnam", "Virgin Islands", _
         "Wales", "Western Sahara", "Yemen", "Yugoslavia", "Zaire", "Zambia", _
         "Zimbabwe"}

        ''' <summary>
        ''' Returns an array with all countries
        ''' </summary>     
        Public Shared Function GetCountries() As StringCollection
            Dim countries As New StringCollection()
            countries.AddRange(_countries)
            Return countries
        End Function
        Public Shared Function GetCountries(ByVal insertEmpty As Boolean) As SortedList
            Dim countries As New SortedList()
            If insertEmpty Then
                countries.Add("", "Please select one...")
            End If
            For Each country As String In _countries
                countries.Add(country, country)
            Next
            Return countries
        End Function
    End Class
End Namespace
