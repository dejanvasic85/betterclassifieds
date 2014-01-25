Imports System.Xml.Linq
Imports System.IO
Imports System.Management
Imports System.Security.AccessControl
Imports BetterclassifiedsCore.DataModel
Imports BetterclassifiedsCore.Booking
Imports System.Drawing.Imaging
Imports Paramount.ApplicationBlock.Configuration
Imports Paramount.Betterclassified.Utilities.Encryption
Imports BetterclassifiedsCore.BundleBooking
Imports Paramount.DSL.UIController

Public Module GeneralRoutine

    Public Function GetAppSetting(ByVal moduleName As String, ByVal key As String) As Object
        Dim settingValue = AppKeyReader(Of Object).ReadFromStore(moduleName, key)
        Return settingValue
    End Function

    Public Function LineAdRatePrice(ByVal rate As Ratecard, ByVal numOfInserts As Integer, ByVal wordCount As Integer, _
                                       ByVal usePhoto As Boolean, ByVal boldHeader As Boolean) As Decimal

        ' get the initial charge
        Dim price As Decimal = rate.MinCharge

        ' calculate the charge for the words
        Dim chargedWords = wordCount - rate.MeasureUnitLimit
        If (chargedWords > 0) Then
            price += chargedWords * rate.RatePerMeasureUnit
        End If

        ' then we add the other charges
        If (usePhoto) Then
            price += rate.PhotoCharge
        End If

        If (boldHeader) Then
            price += rate.BoldHeading
        End If

        ' return maximum charge if it's used by the admin and lower than the normal price
        If rate.MaxCharge > 0 Then
            If rate.MaxCharge < price Then
                price = rate.MaxCharge
            End If
        End If

        LineAdRatePrice = price

    End Function

    Public Function OnlineAdRatePrice(ByVal rate As Ratecard) As Decimal
        ' this function will need to be modified later.
        If rate IsNot Nothing Then
            If rate.MinCharge IsNot Nothing Then
                Return rate.MinCharge
            End If
        End If
        Throw New Exception("Online Ratecard is not available.")
    End Function

    ''' <summary>
    ''' Returns the amount of words that are in the adText passed in through the parameter.
    ''' </summary>
    ''' <param name="adText">The text used for a line ad.</param>
    ''' <returns>Integer value of the amount of words.</returns>
    ''' <remarks>We use an AppSetting value of WordSeparators and WordMaxLength to calculate the words.</remarks>
    Public Function LineAdWordCount(ByVal adText As String) As Integer

        Dim setting As String = AppKeyReader(Of String).ReadFromStore(AppKey.WordSeparators, defaultIfNotExists:="")
        Dim maxWord As Integer = AppKeyReader(Of Integer).ReadFromStore(AppKey.WordMaxLength, defaultIfNotExists:=15)

        Dim charArray() As Char = CType(setting, Char()) ' convert the DB value to character array
        Dim arrayOfWords As String() = adText.Split(charArray) ' use the char array to split the ad text

        ' loop through the text and ensure there are no white spaces
        ' count each word and handle words longer than the max chars in word
        Dim count As Integer
        For Each item In arrayOfWords
            ' we remove any empty strings from the array and add the right value into the array list object
            If item <> String.Empty Then
                If (item.Trim.Length = maxWord) Then
                    count += 1
                Else
                    count += (item.Trim.Length \ maxWord) + 1
                End If
            End If
        Next

        Return count

    End Function

    Private Function GetCharArray(ByVal text As String()) As Char()
        Dim items As Char() = text.ToString.ToCharArray()
        Return items
    End Function

    ''' <summary>
    ''' Returns a List of Dates starting from the Current Edition Date for the Publication passed as parameter
    ''' </summary>
    ''' <param name="publication">Publication that is being checked.</param>
    ''' <param name="noOfInserts">Number of editions to extract</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEditionDates(ByVal publication As Publication, ByVal noOfInserts As Integer) As List(Of Date)
        Try
            ' create return value
            Dim editionList As New List(Of Date)

            ' non publication dates to compare 
            Dim nonEditionDates As New List(Of DateTime) ' = PublicationController.GetNonPublicationDates(publication.PublicationId)

            Select Case publication.FrequencyType
                ' WEEKLY PAPERS
                Case PublicationFrequency.Weekly.ToString
                    ' get the days of the week that this paper is published
                    Dim freq As String() = publication.FrequencyValue.Split(";")

                    'Dim temp As Date = publication.CurrentEditionDate
                    Dim temp As DateTime = Today.Date

                    ' we first add the current date and time into the list
                    'If (temp >= Today) Then

                    '' check the current date and time and ensure it's not passed the deadline for current edition
                    'If ((publication.Deadline.Value.Date = Today) And (publication.Deadline.Value.TimeOfDay > Now.TimeOfDay)) Or publication.Deadline.Value.Date > Today Then
                    '    editionList.Add(temp)
                    'End If

                    Dim f As Integer = 1 ' looping variable - may or may not increment (reason for while loop)
                    While f < noOfInserts

                        ' loop through the days of the week in the frequency array
                        For c As Integer = 0 To freq.Length - 1

                            ' store the next available date into a variable
                            Dim nextDate = NextAvaliableDate(temp, Str(freq(c)))

                            ' check to ensure that this date is not part of non publications
                            If (nonEditionDates.Contains(nextDate) = False) Then
                                ' add the date into the edition list
                                editionList.Add(nextDate)

                                ' increment the insertion
                                f = f + 1
                            End If

                            ' increment for one week
                            temp = temp.AddDays(7)

                        Next

                    End While

                    'End If

            End Select

            Return editionList
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function GetEditionDates(ByVal publication As Publication, ByVal startDate As Date, ByVal insertions As Integer) As List(Of Date)

        Try
            'create the return value
            Dim editionList As New List(Of Date)

            ' non publication dates to compare 
            Dim nonEditionDates As New List(Of DateTime) '= 'PublicationController.GetNonPublicationDates(publication.PublicationId)

            If PublicationController.GetPublicationType(publication.PublicationId) <> SystemAdType.ONLINE.ToString Then

                Select Case publication.FrequencyType
                    ' check if this paper is a weekly paper
                    Case PublicationFrequency.Weekly.ToString
                        ' get the days of the week that this paper is published
                        Dim freq As String() = publication.FrequencyValue.Split(";")

                        Dim f As Integer = 1 ' looping variable - may or may not increment 

                        ' call method to check that the specified start date is already an edition date.
                        If (CheckStartDateEdition(startDate, freq)) Then
                            ' if so, we add the date into our list
                            editionList.Add(startDate)
                        Else
                            ' otherwise we set the loop variable to start at 0 to capture all insertion dates.
                            f = 0
                        End If

                        While f < insertions

                            ' loop through the days of the week in the frequency array
                            For c As Integer = 0 To freq.Length - 1

                                ' store the next available date into a variable
                                Dim nextDate = NextAvaliableDate(startDate, Str(freq(c)))

                                ' check to ensure that this date is not part of non publications
                                If (nonEditionDates.Contains(nextDate) = False) Then
                                    ' add the date into the edition list
                                    editionList.Add(nextDate)

                                    ' increment the insertion
                                    f = f + 1
                                End If

                                ' increment for one week
                                startDate = startDate.AddDays(7)

                            Next

                        End While
                End Select
            Else
                ' otherwise, online papers will just contain the start date
                editionList.Add(startDate)
            End If

            Return editionList
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Returns the next available date by passing the date to compare and the day of week as the frequency.
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="day"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function NextAvaliableDate(ByVal dt As Date, ByVal day As DayOfWeek) As Date
        Dim wk = 7
        Dim c = day - dt.DayOfWeek
        If c > 0 Then
            Return dt.AddDays(c)
        Else
            Return (dt.AddDays(c + wk))
        End If
        wk = Nothing
        c = Nothing
    End Function

    ''' <summary>
    ''' Checks the date to all the frequency days in the week and returns true if one of the editions falls on the day.
    ''' </summary>
    ''' <param name="dt">The date to compare.</param>
    ''' <param name="frequency">Array of days integer values converted to string.</param>
    ''' <returns><see cref="Boolean" /></returns>
    ''' <remarks></remarks>
    Public Function CheckStartDateEdition(ByVal dt As Date, ByVal frequency As String()) As Boolean

        Dim contains As Boolean ' return value

        ' loop through each day in the frequency week and check that the date they passed matches a publication date
        For Each freq In frequency
            Dim day As DayOfWeek = Str(freq)
            Dim c = day - dt.DayOfWeek
            If (c = 0) Then
                contains = True
            End If
        Next

        Return contains

    End Function

#Region "Place Ad Booking and Special Booking"

    ''' <summary>
    ''' Places all the Ad Booking Details in the table by mapping all objects inside the Ad Booking and creating new instances.
    ''' </summary>
    ''' <param name="adBooking">AdBooking Object that contains all required booking details.</param>
    ''' <returns>Returns true if booking successful</returns>
    Public Function PlaceAd(ByVal cart As BookCart, ByVal transactionType As TransactionType) As Boolean
        ' call method to ensure that the booking already exists.
        If BookingController.Exists(cart.BookReference) Then
            Throw New ApplicationException(String.Format("The booking reference already exists. {0}", cart.BookReference))
        End If

        Using db = BetterclassifiedsDataContext.NewContext

            ' AD BOOKING
            Dim b As New AdBooking
            With b
                .StartDate = cart.StartDate
                .EndDate = cart.EndDate
                .TotalPrice = cart.TotalPrice
                .UserId = cart.UserId
                .BookReference = cart.BookReference
                .MainCategoryId = cart.MainCategoryId
                .BookingStatus = cart.BookingStatus
                .Insertions = cart.Insertions
                .BookingType = BookingController.GetBookingTypeString
                .BookingDate = DateTime.Now
            End With

            '' AD
            b.Ad = New Ad
            With b.Ad
                .Title = cart.Ad.Title.Trim
                .Comments = cart.Ad.Comments
                .UseAsTemplate = cart.Ad.UseAsTemplate
            End With

            '' AD DESIGN
            For Each design In cart.Ad.AdDesigns

                Dim d As New AdDesign
                d.AdTypeId = design.AdTypeId

                Select Case AdController.GetAdType(design.AdTypeId).Code

                    Case SystemAdType.LINE.ToString
                        '' LINE AD

                        ' set up the line ad
                        Dim line As New LineAd
                        With line
                            .AdHeader = design.LineAds(0).AdHeader.Trim
                            .AdText = design.LineAds(0).AdText.Trim
                            .NumOfWords = design.LineAds(0).NumOfWords
                            .UseBoldHeader = design.LineAds(0).UseBoldHeader
                        End With

                        ' line ad image
                        If cart.LineAdGraphic IsNot Nothing Then
                            line.UsePhoto = True
                            d.AdGraphics.Add(New AdGraphic With {.DocumentID = cart.LineAdGraphic.DocumentID})
                        End If

                        ' add the line Ad into the design
                        d.Status = AdDesignStatus.Pending
                        d.LineAds.Add(line)

                    Case SystemAdType.ONLINE.ToString
                        '' ONLINE AD
                        Dim onlineAd As New OnlineAd
                        With onlineAd
                            .Heading = design.OnlineAds(0).Heading
                            .Description = design.OnlineAds(0).Description
                            .HtmlText = design.OnlineAds(0).HtmlText
                            .Price = design.OnlineAds(0).Price
                            .LocationId = design.OnlineAds(0).LocationId
                            .LocationAreaId = design.OnlineAds(0).LocationAreaId
                            .ContactName = design.OnlineAds(0).ContactName
                            .ContactType = design.OnlineAds(0).ContactType
                            .ContactValue = design.OnlineAds(0).ContactValue
                            .OnlineAdTag = design.OnlineAds(0).OnlineAdTag
                            .NumOfViews = 1
                        End With
                        d.Status = AdDesignStatus.Approved
                        d.OnlineAds.Add(onlineAd)

                        If cart.TutorAd IsNot Nothing Then
                            onlineAd.TutorAds.Add(cart.TutorAd)
                        End If

                End Select

                '' AD GRAPHICS
                For Each imageGuid As String In cart.ImageList
                    Dim g As New AdGraphic With {.DocumentID = imageGuid, .ImageType = Nothing, .ModifiedDate = DateTime.Now}
                    d.AdGraphics.Add(g)
                Next

                ' add each design into the Ad Object
                b.Ad.AdDesigns.Add(d)
            Next

            '' BOOK ENTRY
            For Each entry In cart.BookEntries
                b.BookEntries.Add(New BookEntry With {.BaseRateId = entry.BaseRateId, _
                                                              .EditionAdPrice = entry.EditionAdPrice, _
                                                              .EditionDate = entry.EditionDate, _
                                                              .PublicationId = entry.PublicationId, _
                                                              .PublicationPrice = entry.PublicationPrice, _
                                                              .RateType = entry.RateType})
            Next

            '' TRANSACTION
            Dim transaction As New Transaction With { _
                .Amount = b.TotalPrice, _
                .Description = BookingController.GetBookingTypeString, _
                .Title = b.BookReference, _
                .TransactionDate = DateTime.Now, _
                .TransactionType = transactionType, _
                .UserId = b.UserId}

            '' SUBMIT TO DATABASE
            db.AdBookings.InsertOnSubmit(b)
            db.Transactions.InsertOnSubmit(transaction)
            db.SubmitChanges()
            b = Nothing

            Return True ' booking successful
        End Using
    End Function

    Public Function PlaceBundledAd(ByVal cart As BundleCart, ByVal transactionType As TransactionType, ByVal bookingStatus As Controller.BookingStatus) As Boolean

        Dim bundleController As New BundleController

        ' ensure first that the booking reference does not already exist
        If BookingController.Exists(cart.BookReference) Then
            Throw New ApplicationException(String.Format("The booking reference already exists. {0}", cart.BookReference))
        End If

        Using db = BetterclassifiedsDataContext.NewContext()
            ' ADBOOKING Object
            ' create a new adbooking object and map the details to the Cart paramater from session
            Dim bk As New AdBooking With {.StartDate = cart.StartDate, .EndDate = cart.EndDate, _
                                          .TotalPrice = cart.TotalPrice, _
                                          .UserId = cart.Username, _
                                          .BookReference = cart.BookReference, _
                                          .BookingStatus = bookingStatus, _
                                          .MainCategoryId = cart.MainSubCategory.MainCategoryId, _
                                          .Insertions = cart.Insertions, _
                                          .BookingType = BookingController.GetBookingTypeString, _
                                          .BookingDate = DateTime.Now}
            '' AD Object
            bk.Ad = New DataModel.Ad With {.Title = Nothing, .Comments = Nothing, .UseAsTemplate = False}

            '' LINEAD Object
            Dim lineDesign As New AdDesign With {.Ad = bk.Ad, _
                                                 .AdTypeId = AdController.GetAdTypeByCode(SystemAdType.LINE).AdTypeId, _
                                                 .Status = AdDesignStatus.Approved}

            Dim lineAd As New LineAd With {.AdDesign = lineDesign, _
                                           .AdHeader = cart.LineAd.AdHeader, _
                                           .AdText = cart.LineAd.AdText, _
                                           .NumOfWords = cart.LineAd.NumOfWords, _
                                           .UseBoldHeader = cart.LineAd.UseBoldHeader, _
                                           .UsePhoto = cart.LineAd.UsePhoto, _
                                           .IsSuperBoldHeading = cart.LineAd.IsSuperBoldHeading, _
                                           .IsSuperHeadingPurchased = cart.LineAd.IsSuperBoldHeading, _
                                           .IsColourBoldHeading = cart.LineAd.IsColourBoldHeading, _
                                           .IsColourBorder = cart.LineAd.IsColourBorder, _
                                           .IsColourBackground = cart.LineAd.IsColourBackground}

            If lineAd.IsColourBoldHeading Then
                lineAd.BoldHeadingColourCode = cart.LineAd.BoldHeadingColourCode
            End If

            If lineAd.IsColourBorder Then
                lineAd.BorderColourCode = cart.LineAd.BorderColourCode
            End If

            If lineAd.IsColourBackground Then
                lineAd.BackgroundColourCode = cart.LineAd.BackgroundColourCode
            End If

            If cart.LineAdGraphic IsNot Nothing Then
                ' LINE AD Photo
                Dim lineGraphic As New AdGraphic With {.AdDesign = lineDesign, _
                                                       .DocumentID = cart.LineAdGraphic.DocumentID}
            End If

            ' ONLINEAD Object
            Dim onlineDesign As New DataModel.AdDesign With {.Ad = bk.Ad, _
                                                             .AdTypeId = AdController.GetAdTypeByCode(SystemAdType.ONLINE).AdTypeId, _
                                                             .Status = AdDesignStatus.Approved}

            Dim onlineAd As New DataModel.OnlineAd With {.AdDesign = onlineDesign, .ContactName = cart.OnlineAd.ContactName, _
                                                         .ContactType = cart.OnlineAd.ContactType, .ContactValue = cart.OnlineAd.ContactValue, _
                                                         .Description = cart.OnlineAd.Description, .Heading = cart.OnlineAd.Heading, _
                                                         .HtmlText = cart.OnlineAd.HtmlText, .LocationId = cart.OnlineAd.LocationId, _
                                                         .LocationAreaId = cart.OnlineAd.LocationAreaId, .NumOfViews = cart.OnlineAd.NumOfViews, _
                                                         .Price = cart.OnlineAd.Price, .OnlineAdTag = cart.OnlineAd.OnlineAdTag}
            ' add the ad graphics if any
            For Each onlineGraphic As DataModel.AdGraphic In cart.OnlineAdGraphics
                onlineDesign.AdGraphics.Add(New DataModel.AdGraphic With {.DocumentID = onlineGraphic.DocumentID, .Filename = onlineGraphic.Filename, _
                                                                          .ImageType = onlineGraphic.ImageType, .ModifiedDate = onlineGraphic.ModifiedDate})
            Next

            '' TUTOR AD
            If cart.TutorAd IsNot Nothing Then
                onlineAd.TutorAds.Add(cart.TutorAd)
            End If

            ' ************
            ' Book Entries
            ' ************
            ' retrieve from the session now
            For Each entry In cart.BookEntries
                bk.BookEntries.Add(New BookEntry With {.BaseRateId = entry.BaseRateId, _
                                                       .EditionAdPrice = entry.EditionAdPrice, _
                                                       .EditionDate = entry.EditionDate, _
                                                       .PublicationId = entry.PublicationId, _
                                                       .PublicationPrice = entry.PublicationPrice, _
                                                       .RateType = entry.RateType})
            Next


            ' TRANSACTION Table
            Dim transaction As New DataModel.Transaction With {.Amount = cart.TotalPrice, .Description = BookingController.GetBookingTypeString, _
                                                               .Title = cart.BookReference, .TransactionDate = DateTime.Now, _
                                                               .TransactionType = transactionType, .UserId = cart.Username}

            db.AdBookings.InsertOnSubmit(bk)
            db.Transactions.InsertOnSubmit(transaction)
            db.SubmitChanges()
            Return True
        End Using

    End Function

#End Region

    Private Function Increment(ByVal value As Integer) As Integer
        Return value + 1
    End Function

    ''' <summary>
    ''' Produces a Booking reference number by concatenating the first three letters of Category name and a new incremented Booking number.
    ''' </summary>
    ''' <param name="categoryId">The Database ID of the Category the Ad is booked for.</param>
    ''' <param name="incrementRef">If newRef is True, database value is incremented otherwise same number is value is used.</param>
    ''' <returns>Returns a string representation of a New Booking Reference Number.</returns>
    Public Function GetBookingReference(ByVal categoryId As Integer, ByVal incrementRef As Boolean) As String

        Dim newValue As Integer
        Dim bookRef As String ' return value

        Using db = BetterclassifiedsDataContext.NewContext

            newValue = AppKeyReader(Of Integer).ReadFromStore(AppKey.BookingReference, 1, AddressOf Increment)

            Dim categoryName = (From c In db.MainCategories Where c.MainCategoryId = categoryId Select c).Single.Title

            If categoryName.Length < 3 Then
                bookRef = categoryName
            Else
                bookRef = categoryName.Substring(0, 3).ToUpper
            End If

        End Using

        Dim length = newValue.ToString.Length
        Dim str As String = "000000"

        ' insert the zero values in front if booking number is short
        If (str.Length > length) Then
            bookRef += "-" + str.Substring(0, str.Length - length) + newValue.ToString
        Else
            bookRef += "-" + newValue.ToString
        End If

        Return bookRef
    End Function

    Public Function GetAdStoreDirectory(ByVal bookingReference As String, ByVal adType As SystemAdType) As String

        ' we will exclude the Category out of the booking reference
        Dim ref() As String = bookingReference.Split("-")
        Dim folderName As String = ref(1)

        ' for line ads, we need to get the storage path from the database
        ' and we create the directory here if it doesn't already exist
        Dim storageFolder = GeneralRoutine.GetAppSetting(Utilities.Constants.CONST_MODULE_SYSTEM, Utilities.Constants.CONST_KEY_Image_Store_Path)

        '' check the type of ad, then store into required folder
        If adType = SystemAdType.LINE Then
            storageFolder += "LineAds" + "\"
        ElseIf adType = SystemAdType.ONLINE Then
            storageFolder += "OnlineAds" + "\"
        Else
            Throw New Exception("System Ad Type is not implemented to contain images in the folder.")
        End If

        Dim dirInfo As New IO.DirectoryInfo(storageFolder + folderName)

        If Not dirInfo.Exists Then
            dirInfo.Create()
        End If

        Return dirInfo.FullName

    End Function

#Region "Export Bookings"

    Public Function ExportBookings(ByVal publicationId As Integer, ByVal editionDate As DateTime, ByVal adStorePath As String) As XDocument
        ' Fetch the required details from the database
        Dim exportItems As List(Of spLineAdExportListResult) = AdController.GetExportItems(publicationId, editionDate)
        Dim publication As DataModel.Publication = PublicationController.GetPublicationById(publicationId)
        Dim imagePathSetting As String = GetAppSetting(Utilities.Constants.CONST_MODULE_SYSTEM, Utilities.Constants.CONST_KEY_Image_Store_Path)
        Dim imagePath As String = Path.Combine(imagePathSetting, publication.Title)
        imagePath = Path.Combine(imagePath, String.Format("{0} {1:yyyy-MM-dd}", publication.Title, editionDate))
        Dim imageDirectory As New DirectoryInfo(imagePath)

        ' Delete the directory if exists then create
        If imageDirectory.Exists Then
            imageDirectory.Delete(True)
        End If
        imageDirectory.Create()

        ' Create and define the XML document to work with
        Dim xmlDoc As New XDocument(New XDeclaration("1.0", "utf-8", "yes"), _
                                    New XElement("Root"))

        Dim categoryIdList As New List(Of Integer)
        Dim subCategoryIdList As New List(Of Integer)

        Dim isFreeAd As Boolean = False
        Dim xmlExportStrategy As XmlExportStrategy = GetExportStrategy()

        ' Loop through each ad
        For Each exportItem In exportItems
            If xmlExportStrategy = xmlExportStrategy.Styled Then
                isFreeAd = exportItem.TotalPrice = 0
            End If

            ' Setup the node names
            Dim headerNodeName As String = "ListHead"
            Dim graphicNodeName As String = "graphic"
            Dim listbodyNodeName As String = "ListBody"
            Dim iFlogIdNodeName As String = "iFlogID"
            If xmlExportStrategy = xmlExportStrategy.Styled Then
                If isFreeAd Then
                    headerNodeName += "_Free"
                    graphicNodeName += "_Free"
                    listbodyNodeName += "_Free"
                    iFlogIdNodeName += "_Free"
                Else
                    headerNodeName += "_Paid"
                    graphicNodeName += "_Paid"
                    listbodyNodeName += "_Paid"
                    iFlogIdNodeName += "_Paid"
                End If
            End If

            ' Add Category if required
            If Not categoryIdList.Contains(exportItem.MainCategoryId) Then
                categoryIdList.Add(exportItem.MainCategoryId)
                Dim elementCategory As New XElement("Category", exportItem.MainCategory)
                xmlDoc.Element("Root").Add(elementCategory)
            End If

            ' Add the sub category if required
            If Not subCategoryIdList.Contains(exportItem.SubCategoryId) Then
                subCategoryIdList.Add(exportItem.SubCategoryId)
                Dim elementSubCategory As New XElement("SubCategory", exportItem.SubCategory)
                xmlDoc.Element("Root").Add(elementSubCategory)
            End If

            ' Ad Header
            If Not String.IsNullOrEmpty(exportItem.AdHeader) Then
                Dim elementHeader As New XElement(headerNodeName, exportItem.AdHeader)
                xmlDoc.Element("Root").Add(elementHeader)
            End If

            ' Ad Graphic
            Dim graphic = AdController.GetAdGraphics(exportItem.AdDesignId).FirstOrDefault

            If graphic IsNot Nothing Then
                ' Call method to save the ad graphic to disk.
                SaveLineAdImageToDisk(graphic, exportItem.AdBookingId, imageDirectory.FullName)
                Dim xmlStorePath = String.Format("{0}{1}.jpg", adStorePath, exportItem.AdBookingId)
                Dim elementGraphic As New XElement(graphicNodeName, New XAttribute("href", xmlStorePath))
                xmlDoc.Element("Root").Add(elementGraphic)
            End If

            Dim elementBodyText As New XElement(listbodyNodeName, exportItem.AdText.Replace(Environment.NewLine, " "))
            xmlDoc.Element("Root").Add(elementBodyText)
            xmlDoc.Element("Root").Add(New XElement("cantDelete"))
            xmlDoc.Element("Root").Add(New XElement(iFlogIdNodeName, "Ad ID: ", String.Format("{0}-{1}", publicationId, exportItem.AdBookingId)))
        Next

        Return xmlDoc
    End Function

    Public Function GetExportStrategy() As XmlExportStrategy
        Dim appSetting As String = GetAppSetting("AdBooking", "XmlExportStrategy")
        If appSetting.Equals("Styled", StringComparison.OrdinalIgnoreCase) Then
            Return XmlExportStrategy.Styled
        ElseIf appSetting.Equals("Simple", StringComparison.OrdinalIgnoreCase) Then
            Return XmlExportStrategy.Simple
        End If
    End Function

#End Region

    Public Sub SaveLineAdImageToDisk(ByVal graphic As AdGraphic, ByVal adId As Integer, ByVal imageDir As String)
        Dim documentId As Guid
        Try
            Dim imageFullName = Path.Combine(imageDir, String.Format("{0}.jpg", adId))
            documentId = New Guid(graphic.DocumentID)
            DslController.DownloadFile(documentId, ConfigSettingReader.ClientCode, imageFullName)
        Catch e As Exception
            Dim errorMsg As String = String.Format("Error in export. Unable to save image from DSL for iFlog ID [{0}], Document ID [{1}]. Details [{2}]", adId, documentId, e.Message)
            Throw New ApplicationException(errorMsg)
        End Try
    End Sub

    Public Function CheckMachineMacAddress(ByVal key As String) As Boolean
        Try
            Dim mc As System.Management.ManagementClass
            Dim mo As ManagementObject
            mc = New ManagementClass("Win32_NetworkAdapterConfiguration")
            Dim moc As ManagementObjectCollection = mc.GetInstances()

            Dim hasRightKey As Boolean = False

            ' loop through all the network adapters
            For Each mo In moc
                ' check if they have IP address enabled
                If mo.Item("IPEnabled") = True Then

                    Dim macAddress As String = mo.Item("MacAddress")

                    ' perform the same encryption that the key should be using
                    Dim decrypted = ParamountEncryption.Decrypt(key, "paramountKey")

                    If macAddress = decrypted Then
                        hasRightKey = True ' set the flag
                        Exit For ' exit the loop - we found a match
                    End If
                End If
            Next

            Return hasRightKey ' return the flag

        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Returns true if the user Id was the one who booked thed required AD Booking ID
    ''' </summary>
    ''' <param name="userId">User ID that we need to check.</param>
    ''' <param name="bookingId">The Booking ID that needs to be checked.</param>
    ''' <returns>True if the Booking belongs to the user.</returns>
    Public Function CheckUserAdBooking(ByVal userId As String, ByVal bookingId As Integer) As Boolean
        Using db = BetterclassifiedsDataContext.NewContext
            Dim isOk = False
            Dim booking = (From b In db.AdBookings Where b.AdBookingId = bookingId Select b).FirstOrDefault
            If booking IsNot Nothing Then
                ' Check whether the User belongs to this booking
                isOk = booking.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase)
            End If
            Return isOk
        End Using
    End Function

    Public Function CheckUserAdBooking(ByVal userId As String, ByVal bookingId As Integer, ByVal adDesignId As Integer) As Boolean
        Using db = BetterclassifiedsDataContext.NewContext
            Dim isOk = False
            ' Fetch the booking by ID to check whether the Ad Design and UserId exist.
            Dim booking = (From b In db.AdBookings Where b.AdBookingId = bookingId Select b).FirstOrDefault
            If booking IsNot Nothing Then
                ' Get the Ad Design Also for this booking
                Dim design = booking.Ad.AdDesigns.Where(Function(i) i.AdDesignId = adDesignId).FirstOrDefault
                ' Check whether the User belongs to this booking
                isOk = booking.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase) And design IsNot Nothing
            End If
            Return isOk
        End Using
    End Function

    Public Sub UpdateApplicationSetting(ByVal moduleName As String, ByVal key As String, ByVal newValue As String)
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                ' get the key from the database
                Dim setting = (From a In db.AppSettings Where a.Module = moduleName And a.AppKey = key Select a).Single
                ' update the date and time to be NOW
                setting.SettingValue = newValue

                db.SubmitChanges()
            End Using
        Catch
            Throw New ApplicationException("Unable to update the application setting - LastBackupTime")
        End Try
    End Sub

    Public Function IsFileTypeAccepted(ByVal fileName As String) As Boolean
        If fileName.ToLower.EndsWith("jpg") _
            Or fileName.ToLower.EndsWith("jpeg") _
            Or fileName.ToLower.EndsWith("gif") _
            Or fileName.ToLower.EndsWith("bmp") _
            Or fileName.ToLower.EndsWith("tiff") _
            Or fileName.ToLower.EndsWith("tif") _
            Or fileName.ToLower.EndsWith("png") Then
            Return True
        Else
            Return False
        End If
    End Function

End Module
