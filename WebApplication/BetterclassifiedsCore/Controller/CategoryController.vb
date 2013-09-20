Imports System.Web
Imports BetterclassifiedsCore.DataModel
Imports BetterclassifiedsCore.Controller
Imports Paramount.ApplicationBlock.Logging.EventLogging

''' <summary>
''' Provides the general data layer methods required to communicate 
''' with the backend Database.
''' </summary>
''' <remarks>
''' 
''' </remarks>
Public Class CategoryController

#Region "Create"

    ''' <summary>
    ''' Creates a new main category into the system. Passing in Nothing for ParentId would be a parent category.
    ''' </summary>
    ''' <param name="title">Title for new category.</param>
    ''' <param name="parentId">Id for the parent category. Pass Nothing to be Parent.</param>
    Public Shared Function CreateNewCategory(ByVal title As String, ByVal parentId As Nullable(Of Integer)) As Boolean
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Dim newCategory As New DataModel.MainCategory With {.Title = title, .ParentId = parentId}
                db.MainCategories.InsertOnSubmit(newCategory)
                db.SubmitChanges()
                Return True
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Retrieve"

    ''' <summary>
    ''' Returns a list of main root categories from the database.
    ''' </summary>
    ''' <returns><see cref="List(Of MainCategory)">List of Main Root Categories</see></returns>
    Public Shared Function GetMainParentCategories() As List(Of MainCategory)
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Return db.spGetMainParentCategories.ToList
            End Using
        Catch ex As Exception
            EventLogManager.Log(ex)
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Returns the Main Category that is the Parent of the SubCategory ID required in the parameter.
    ''' </summary>
    ''' <param name="subCategoryId">ID of the sub category.</param>
    Public Shared Function GetMainParentCategory(ByVal subCategoryId As Integer) As DataModel.MainCategory
        Using db = BetterclassifiedsDataContext.NewContext
            Dim subCategory = db.MainCategories.Where(Function(i) i.MainCategoryId = subCategoryId)

            If subCategory.Count > 0 Then
                Dim main = db.MainCategories.Where(Function(c) c.MainCategoryId = subCategory.FirstOrDefault.ParentId)

                If main.Count > 0 Then
                    Return main.FirstOrDefault

                Else
                    Return Nothing
                End If

            Else
                Return Nothing
            End If

        End Using
    End Function

    ''' <summary>
    ''' Returns a list of main categories by the ParentId from the database.
    ''' </summary>
    ''' <param name="parentId"></param>
    ''' <returns><see cref="List(Of MainCategoery)">List of Main Categories</see></returns>
    Public Shared Function GetMainCategoriesByParent(ByVal parentId As Integer) As List(Of MainCategory)
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Return db.MainCategories.Where(Function(i) i.ParentId = parentId).OrderBy(Function(i) i.Title).ToList
            End Using
        Catch ex As Exception
            EventLogManager.Log(ex)
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Returns stored procedure call that gets all Parent Category Names with amount of ads booked inside brackets (*)
    ''' </summary>
    ''' <param name="bookStatus"></param>
    Public Shared Function GetMainParentCategoriesWithAdCount(ByVal bookStatus As BookingStatus) As List(Of spMainCategoryAdCountResult)
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Return db.spMainCategoryAdCount(bookStatus).ToList
            End Using
        Catch ex As Exception
            EventLogManager.Log(ex)
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Returns all the Sub categories regardless of what the parent is.
    ''' </summary>
    Public Shared Function GetAllSubCategories() As List(Of MainCategory)
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Return db.MainCategories.Where(Function(i) i.ParentId > 0).OrderBy(Function(i) i.ParentId).OrderBy(Function(i) i.Title).ToList
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Returns the PublicationCategory object from DatabaseModel by MainCategoryId and PublicationId
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Function GetPublicationCategory(ByVal mainCategoryId As Integer, ByVal publicationId As Integer) As PublicationCategory
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Dim query = From pc In db.PublicationCategories Where pc.PublicationId = publicationId And _
                            pc.MainCategoryId = mainCategoryId Select pc

                Return query.Single
            End Using
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Returns the PublicationCategory object from DatabaseModel by the Primary Key of PublicationCategoryID
    ''' </summary>
    ''' <param name="publicationCategoryId">Primary Key of the table</param>
    ''' <remarks></remarks>
    Public Shared Function GetPublicationCategory(ByVal publicationCategoryId As Integer) As PublicationCategory
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Dim query = From pc In db.PublicationCategories Where pc.PublicationCategoryId = publicationCategoryId Select pc
                Return query.Single
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Returns MainCategory Object from DatabaseModel by the mainCategoryId table key
    ''' </summary>
    Public Shared Function GetMainCategoryById(ByVal catId As Integer) As DataModel.MainCategory
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Return (db.MainCategories.Where(Function(i) i.MainCategoryId = catId).Single)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function GetMainCategories(ByVal categoryId As Integer) As IList
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Return db.MainCategories.Where(Function(i) i.MainCategoryId = categoryId).ToList
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function GetMainCategoriesByPubCategory(ByVal publicationCategoryId As Integer) As IList
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Dim query = (From mc In db.MainCategories _
                            Join pc In db.PublicationCategories On pc.MainCategoryId Equals mc.ParentId _
                            Where pc.PublicationCategoryId = publicationCategoryId _
                            Order By mc.Title _
                            Select mc)
                Return query.ToList
            End Using
        Catch ex As Exception
            Throw ex ' todo log error
        End Try
    End Function

    Public Shared Function GetUnassignedMainCategories(ByVal isParent As Boolean, ByVal publicationCategoryId As Nullable(Of Integer), ByVal publicationId As Integer) As IList
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Return db.spMainCategoriesUnassigned(isParent, publicationCategoryId, publicationId).ToList
            End Using
        Catch ex As Exception
            Throw ex 'todo log error
        End Try
    End Function

    Public Shared Function GetAllMainCategories() As List(Of MainCategory)
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.MainCategories.ToList
        End Using
    End Function
#End Region

#Region "Update"

    Public Shared Function UpdateCategoryDetails(ByVal updatedCategory As DataModel.MainCategory) As Boolean
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                With updatedCategory
                    Dim obj = (From c In db.MainCategories Where c.MainCategoryId = .MainCategoryId Select c).Single

                    obj.Title = .Title
                    obj.ParentId = updatedCategory.ParentId
                    obj.ImageUrl = .ImageUrl
                    obj.Description = .Description
                    obj.OnlineAdTag = IIf(String.IsNullOrEmpty(.OnlineAdTag), Nothing, .OnlineAdTag)

                    db.SubmitChanges()

                    Return True
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Delete"

    ''' <summary>
    ''' Deletes parent category and all it's children.
    ''' </summary>
    ''' <param name="parentId">Id for the Main Parent Category to delete.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DeleteParentCategory(ByVal parentId As Integer) As Boolean
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Dim subCategories = From mc In db.MainCategories Where mc.ParentId = parentId Select mc
                Dim mCategory = From mc In db.MainCategories Where mc.MainCategoryId = parentId Select mc
                db.MainCategories.DeleteAllOnSubmit(subCategories.ToList)
                db.MainCategories.DeleteOnSubmit(mCategory.Single)
                db.SubmitChanges()
                Return True
            End Using
        Catch ex As Exception
            Throw ex
            ' log error
        End Try
    End Function

    ''' <summary>
    ''' Deletes the single sub category by the id.
    ''' </summary>
    ''' <param name="categoryId">Id for the main category to delete.</param>
    Public Shared Function DeleteSubCategory(ByVal categoryId As Integer) As Boolean
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Dim category = From mc In db.MainCategories Where mc.MainCategoryId = categoryId Select mc
                db.MainCategories.DeleteOnSubmit(category.Single)
                db.SubmitChanges()
                Return True
            End Using
        Catch ex As Exception
            Throw ex
            ' log error
        End Try
    End Function

#End Region

    ''' <summary>
    ''' Queries the Categories for the category title and returns true if the record exists.
    ''' </summary>
    ''' <param name="title">Title for the category to check.</param>
    Public Shared Function Exists(ByVal title As String) As Boolean
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Dim query = From c In db.MainCategories Where c.Title = title Select c
                Return query.Count > 0
            End Using
        Catch ex As Exception
            Throw ex
            ' log error
        End Try
    End Function

    ''' <summary>
    ''' Queries the Categories for the category title and parent Id and returns true if record exists.
    ''' </summary>
    ''' <param name="title"></param>
    ''' <param name="parentId"></param>
    ''' <returns>True if category exists.</returns>
    ''' <remarks></remarks>
    Public Shared Function Exists(ByVal title As String, ByVal parentId As Integer) As Boolean
        Using db = BetterclassifiedsDataContext.NewContext
            Dim query = From c In db.MainCategories Where c.Title = title And c.ParentId = parentId Select c
            Return query.Count > 0
        End Using
    End Function

End Class
