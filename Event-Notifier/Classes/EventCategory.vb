Public Class EventCategory

    Public Property CategoryID As Integer
    Public Property CategoryName As String
    Public Property Description As String

    ' Constructor
    Public Sub New()
        CategoryID = 0
        CategoryName = ""
        Description = ""
    End Sub

    ' Display method
    Public Overrides Function ToString() As String
        Return CategoryName
    End Function

End Class
