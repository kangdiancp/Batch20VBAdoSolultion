Imports NorhtwindVbNetApi.Model

Namespace Repository
    Public Interface IRegionRepository
        Function CreateRegion(ByVal region As Region) As Region

        Function DeleteRegion(ByVal id As Int32) As Int32

        Function FindAllRegion() As List(Of Region)

        Function FindAllRegionAsync() As Task(Of List(Of Region))

        Function FindRegionById(ByVal id As Int32) As Region

        Function UpdateRegionById(id As Integer, value As String, Optional showCommand As Boolean = False) As Boolean

        Function UpdateRegionBySp(id As Integer, value As String, Optional showCommand As Boolean = False) As Boolean
    End Interface

End Namespace

