Imports NorhtwindVbNetApi.Context
Imports NorhtwindVbNetApi.Repository

Namespace Base
    Public Class RepositoryManager
        Implements IRepositoryManager

        Private _regionRepository As IRegionRepository

        Private ReadOnly _repositoryContext As IRepositoryContext

        Public Sub New(repositoryContext As IRepositoryContext)
            _repositoryContext = repositoryContext
        End Sub

        Public ReadOnly Property Region As IRegionRepository Implements IRepositoryManager.Region
            Get
                'up iregionrepository & implementation
                If _regionRepository Is Nothing Then
                    _regionRepository = New RegionRepository(_repositoryContext)
                End If
                Return _regionRepository
            End Get
        End Property
    End Class
End Namespace

