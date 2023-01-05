Namespace Model
    Public Class Region
        Private _regionId As Integer
        Private _regionDescription As String

        Public Sub New()
        End Sub

        Public Sub New(regionId As Integer, regionDescription As String)
            Me.RegionId = regionId
            Me.RegionDescription = regionDescription
        End Sub

        Public Overrides Function ToString() As String
            Return $"RegionId : {RegionId} | Description : {RegionDescription}"
        End Function

        Public Property RegionId As Integer
            Get
                Return _regionId
            End Get
            Set(value As Integer)
                _regionId = value
            End Set
        End Property

        Public Property RegionDescription As String
            Get
                Return _regionDescription
            End Get
            Set(value As String)
                _regionDescription = value
            End Set
        End Property
    End Class
End Namespace

