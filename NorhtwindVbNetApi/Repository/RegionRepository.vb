Imports System.Data.SqlClient
Imports NorhtwindVbNetApi.Context
Imports NorhtwindVbNetApi.Model

Namespace Repository
    Public Class RegionRepository
        Implements IRegionRepository

        'dependency injection
        Private ReadOnly _context As IRepositoryContext

        Public Sub New(context As IRepositoryContext)
            _context = context
        End Sub

        Public Function CreateRegion(region As Region) As Region Implements IRegionRepository.CreateRegion
            Dim newRegion As New Region()

            'declare stmt
            Dim stmt As String = "Insert into dbo.region(regionId,regionDescription) values (@id,@desc)"

            'primary key using identity integer
            'Dim stmtIdentity As String = "Insert into dbo.region(regionId,regionDescription) values (@id,@desc); " &
            '"select cast(scope_identity() as int)"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}
                    cmd.Parameters.AddWithValue("@id", region.RegionId)
                    cmd.Parameters.AddWithValue("@desc", region.RegionDescription)

                    Try
                        conn.Open()
                        'ExecuteScalar return 1 row & get first column
                        Dim regionId As Int32 = Convert.ToInt32(cmd.ExecuteScalar())
                        newRegion = FindRegionById(region.RegionId)

                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try

                End Using
            End Using

            Return newRegion


        End Function

        Public Function DeleteRegion(id As Integer) As Integer Implements IRegionRepository.DeleteRegion

            Dim rowEffect As Int32 = 0

            'declare stmt
            Dim stmt As String = "delete from dbo.region where regionId=@id"


            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}
                    cmd.Parameters.AddWithValue("@id", id)


                    Try
                        conn.Open()
                        rowEffect = cmd.ExecuteNonQuery()

                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try

                End Using
            End Using

            Return rowEffect
        End Function

        Public Function FindAllRegion() As List(Of Region) Implements IRegionRepository.FindAllRegion

            Dim regionList As New List(Of Region)

            'declare statement
            Dim sql As String = "SELECT RegionId,RegionDescription from dbo.region " &
                                "Order by RegionId desc;"

            'try to connect
            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = sql}

                    Try
                        conn.Open()
                        Dim reader = cmd.ExecuteReader()

                        While reader.Read()
                            regionList.Add(New Region() With {
                                .RegionId = reader.GetInt32(0),
                                .RegionDescription = reader.GetString(1)
                            })
                        End While

                        reader.Close()
                        conn.Close()

                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try

                End Using
            End Using
            Return regionList
        End Function

        Public Async Function FindAllRegionAsync() As Task(Of List(Of Region)) Implements IRegionRepository.FindAllRegionAsync
            Dim regionList As New List(Of Region)

            'declare statement
            Dim sql As String = "SELECT RegionId,RegionDescription from dbo.region " &
                                "Order by RegionId desc;"

            'try to connect
            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = sql}

                    Try
                        conn.Open()
                        Dim reader = Await cmd.ExecuteReaderAsync()

                        While reader.Read()
                            regionList.Add(New Region() With {
                                .RegionId = reader.GetInt32(0),
                                .RegionDescription = reader.GetString(1)
                            })
                        End While

                        reader.Close()
                        conn.Close()

                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try

                End Using
            End Using
            Return regionList
        End Function

        Public Function FindRegionById(id As Integer) As Region Implements IRegionRepository.FindRegionById
            Dim region As New Region With {.RegionId = id}

            'sql statement
            Dim stmt As String = "Select regionID,regionDescription from dbo.region " &
                                 "where regionId = @regionId;"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}

                    cmd.Parameters.AddWithValue("@regionId", id)

                    Try
                        conn.Open()
                        Dim reader = cmd.ExecuteReader()
                        If reader.HasRows Then
                            reader.Read()
                            region.RegionDescription = reader.GetString(1)
                        End If

                        reader.Close()
                        conn.Close()
                    Catch ex As Exception

                    End Try
                End Using
            End Using
            Return region
        End Function

        Public Function UpdateRegionById(id As Integer, value As String, Optional showCommand As Boolean = False) As Boolean Implements IRegionRepository.UpdateRegionById

            Dim updateRegion As New Region()

            'declare stmt
            Dim stmt As String = "Update dbo.region " &
                                 "set " &
                                 "regionDescription=@desc " &
                                 "where regionId = @id;"


            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}
                    cmd.Parameters.AddWithValue("@id", id)
                    cmd.Parameters.AddWithValue("@desc", value)

                    'show command
                    If showCommand Then
                        Console.WriteLine(cmd.CommandText)
                    End If

                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()

                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try

                End Using
            End Using

            Return True
        End Function

        Public Function UpdateRegionBySp(id As Integer, value As String, Optional showCommand As Boolean = False) As Boolean Implements IRegionRepository.UpdateRegionBySp
            Dim updateRegion As New Region()

            'declare stmt
            Dim stmt As String = "spUpdateRegion"


            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}
                    cmd.Parameters.AddWithValue("@id", id)
                    cmd.Parameters.AddWithValue("@desc", value)

                    'show command
                    If showCommand Then
                        Console.WriteLine(cmd.CommandText)
                    End If

                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()

                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try

                End Using
            End Using

            Return True
        End Function
    End Class
End Namespace

