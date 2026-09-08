Option Strict On
Imports System.Data.Common
Imports Microsoft.Data.SqlClient
Imports SphereERP.Models

Namespace Data

    ''' <summary>Data access for the EgsCodes table (local item-code library).</summary>
    Public Class EgsCodeRepository

        Public Function GetAll(Optional activeOnly As Boolean = False) As List(Of EgsCodeModel)
            Dim list As New List(Of EgsCodeModel)()
            Using conn = DbConnectionFactory.CreateOpenConnection()
                Dim sql As String = "SELECT EgsCode, Description, UnitType, DefaultPrice, DefaultTaxType, DefaultTaxRate, Category, IsActive, CreatedAtUtc FROM dbo.EgsCodes"
                If activeOnly Then sql &= " WHERE IsActive = 1"
                sql &= " ORDER BY EgsCode"

                Using cmd As New SqlCommand(sql, conn)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(New EgsCodeModel With {
                                .EgsCode = reader.GetString(0),
                                .Description = reader.GetString(1),
                                .UnitType = reader.GetString(2),
                                .DefaultPrice = reader.GetDecimal(3),
                                .DefaultTaxType = reader.GetString(4),
                                .DefaultTaxRate = reader.GetDecimal(5),
                                .Category = If(reader.IsDBNull(6), "", reader.GetString(6)),
                                .IsActive = reader.GetBoolean(7),
                                .CreatedAtUtc = reader.GetDateTime(8)
                            })
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        Public Sub Upsert(item As EgsCodeModel)
            Using conn = DbConnectionFactory.CreateOpenConnection()
                Dim sql As String = "
                    MERGE dbo.EgsCodes AS target
                    USING (SELECT @EgsCode AS EgsCode) AS src
                    ON target.EgsCode = src.EgsCode
                    WHEN MATCHED THEN UPDATE SET
                        Description = @Description, UnitType = @UnitType, DefaultPrice = @DefaultPrice,
                        DefaultTaxType = @DefaultTaxType, DefaultTaxRate = @DefaultTaxRate,
                        Category = @Category, IsActive = @IsActive
                    WHEN NOT MATCHED THEN
                        INSERT (EgsCode, Description, UnitType, DefaultPrice, DefaultTaxType, DefaultTaxRate, Category, IsActive)
                        VALUES (@EgsCode, @Description, @UnitType, @DefaultPrice, @DefaultTaxType, @DefaultTaxRate, @Category, @IsActive);"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@EgsCode", item.EgsCode)
                    cmd.Parameters.AddWithValue("@Description", item.Description)
                    cmd.Parameters.AddWithValue("@UnitType", item.UnitType)
                    cmd.Parameters.AddWithValue("@DefaultPrice", item.DefaultPrice)
                    cmd.Parameters.AddWithValue("@DefaultTaxType", item.DefaultTaxType)
                    cmd.Parameters.AddWithValue("@DefaultTaxRate", item.DefaultTaxRate)
                    cmd.Parameters.AddWithValue("@Category", If(item.Category, ""))
                    cmd.Parameters.AddWithValue("@IsActive", item.IsActive)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Public Sub Delete(egsCode As String)
            Using conn = DbConnectionFactory.CreateOpenConnection()
                Using cmd As New SqlCommand("DELETE FROM dbo.EgsCodes WHERE EgsCode = @EgsCode", conn)
                    cmd.Parameters.AddWithValue("@EgsCode", egsCode)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Public Function Search(searchText As String) As List(Of EgsCodeModel)
            Dim all = GetAll()
            If String.IsNullOrWhiteSpace(searchText) Then Return all
            Dim t = searchText.Trim().ToLowerInvariant()
            Return all.Where(Function(x) x.EgsCode.ToLowerInvariant().Contains(t) OrElse
                                          x.Description.ToLowerInvariant().Contains(t) OrElse
                                          x.Category.ToLowerInvariant().Contains(t)).ToList()
        End Function

    End Class

    ''' <summary>Data access for batch import headers/items (Excel/CSV submissions).</summary>
    Public Class BatchImportRepository

        Public Function InsertBatch(fileName As String, environmentName As String) As Integer
            Using conn = DbConnectionFactory.CreateOpenConnection()
                Dim sql As String = "
                    INSERT INTO dbo.BatchImports (FileName, Environment)
                    OUTPUT INSERTED.Id
                    VALUES (@FileName, @Environment);"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@FileName", fileName)
                    cmd.Parameters.AddWithValue("@Environment", environmentName)
                    Return CInt(cmd.ExecuteScalar())
                End Using
            End Using
        End Function

        Public Function InsertItem(batchImportId As Integer, internalId As String, lineCount As Integer,
                                    totalAmount As Decimal, status As String, rawLinesJson As String) As Integer
            Using conn = DbConnectionFactory.CreateOpenConnection()
                Dim sql As String = "
                    INSERT INTO dbo.BatchImportItems (BatchImportId, InternalId, LineCount, TotalAmount, Status, RawLinesJson)
                    OUTPUT INSERTED.Id
                    VALUES (@BatchImportId, @InternalId, @LineCount, @TotalAmount, @Status, @RawLinesJson);"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@BatchImportId", batchImportId)
                    cmd.Parameters.AddWithValue("@InternalId", internalId)
                    cmd.Parameters.AddWithValue("@LineCount", lineCount)
                    cmd.Parameters.AddWithValue("@TotalAmount", totalAmount)
                    cmd.Parameters.AddWithValue("@Status", status)
                    cmd.Parameters.AddWithValue("@RawLinesJson", rawLinesJson)
                    Return CInt(cmd.ExecuteScalar())
                End Using
            End Using
        End Function

        Public Sub UpdateItemResult(itemId As Integer, documentId As Integer?, status As String, errorMessage As String)
            Using conn = DbConnectionFactory.CreateOpenConnection()
                Dim sql As String = "
                    UPDATE dbo.BatchImportItems SET
                        DocumentId = @DocumentId, Status = @Status, ErrorMessage = @ErrorMessage
                    WHERE Id = @Id;"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@DocumentId", If(documentId.HasValue, CType(documentId.Value, Object), DBNull.Value))
                    cmd.Parameters.AddWithValue("@Status", status)
                    cmd.Parameters.AddWithValue("@ErrorMessage", If(errorMessage, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@Id", itemId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Public Sub UpdateBatchSummary(batchImportId As Integer, total As Integer, succeeded As Integer, failed As Integer)
            Using conn = DbConnectionFactory.CreateOpenConnection()
                Dim sql As String = "
                    UPDATE dbo.BatchImports SET TotalInvoices = @Total, SucceededCount = @Succeeded, FailedCount = @Failed
                    WHERE Id = @Id;"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@Total", total)
                    cmd.Parameters.AddWithValue("@Succeeded", succeeded)
                    cmd.Parameters.AddWithValue("@Failed", failed)
                    cmd.Parameters.AddWithValue("@Id", batchImportId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

    End Class

    ''' <summary>Data access for notification events (validation, issuance, rejection, cancellation).</summary>
    Public Class NotificationRepository

        Public Sub Insert(documentId As Integer?, internalId As String, uuid As String, eventType As String, message As String)
            Using conn = DbConnectionFactory.CreateOpenConnection()
                Dim sql As String = "
                    INSERT INTO dbo.NotificationLog (DocumentId, InternalId, Uuid, EventType, Message)
                    VALUES (@DocumentId, @InternalId, @Uuid, @EventType, @Message);"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@DocumentId", If(documentId.HasValue, CType(documentId.Value, Object), DBNull.Value))
                    cmd.Parameters.AddWithValue("@InternalId", If(internalId, ""))
                    cmd.Parameters.AddWithValue("@Uuid", If(uuid, ""))
                    cmd.Parameters.AddWithValue("@EventType", eventType)
                    cmd.Parameters.AddWithValue("@Message", If(message, ""))
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Public Function GetRecent(Optional unreadOnly As Boolean = False, Optional top As Integer = 100) As List(Of NotificationRow)
            Dim list As New List(Of NotificationRow)()
            Using conn = DbConnectionFactory.CreateOpenConnection()
                Dim sql As String = $"SELECT TOP ({top}) Id, DocumentId, InternalId, Uuid, EventType, Message, IsRead, CreatedAtUtc FROM dbo.NotificationLog"
                If unreadOnly Then sql &= " WHERE IsRead = 0"
                sql &= " ORDER BY CreatedAtUtc DESC"

                Using cmd As New SqlCommand(sql, conn)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(New NotificationRow With {
                                .Id = reader.GetInt32(0),
                                .DocumentId = If(reader.IsDBNull(1), CType(Nothing, Integer?), reader.GetInt32(1)),
                                .InternalId = If(reader.IsDBNull(2), "", reader.GetString(2)),
                                .Uuid = If(reader.IsDBNull(3), "", reader.GetString(3)),
                                .EventType = reader.GetString(4),
                                .Message = If(reader.IsDBNull(5), "", reader.GetString(5)),
                                .IsRead = reader.GetBoolean(6),
                                .CreatedAtUtc = reader.GetDateTime(7)
                            })
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        Public Sub MarkAllRead()
            Using conn = DbConnectionFactory.CreateOpenConnection()
                Using cmd As New SqlCommand("UPDATE dbo.NotificationLog SET IsRead = 1 WHERE IsRead = 0", conn)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Public Function GetUnreadCount() As Integer
            Using conn = DbConnectionFactory.CreateOpenConnection()
                Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.NotificationLog WHERE IsRead = 0", conn)
                    Return CInt(cmd.ExecuteScalar())
                End Using
            End Using
        End Function

    End Class

    Public Class NotificationRow
        Public Property Id As Integer
        Public Property DocumentId As Integer?
        Public Property InternalId As String
        Public Property Uuid As String
        Public Property EventType As String
        Public Property Message As String
        Public Property IsRead As Boolean
        Public Property CreatedAtUtc As DateTime
    End Class

    ''' <summary>Simple application/audit logger writing to dbo.AppLog.</summary>
    Public Class AppLogRepository

        Public Sub Log(level As String, source As String, message As String)
            Try
                Using conn = DbConnectionFactory.CreateOpenConnection()
                    Using cmd As New SqlCommand("INSERT INTO dbo.AppLog (LogLevel, Source, Message) VALUES (@Level, @Source, @Message)", conn)
                        cmd.Parameters.AddWithValue("@Level", level)
                        cmd.Parameters.AddWithValue("@Source", If(source, ""))
                        cmd.Parameters.AddWithValue("@Message", If(message, ""))
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            Catch
                ' Logging must never throw and break the calling operation.
            End Try
        End Sub

        Public Sub Info(source As String, message As String)
            Log("Info", source, message)
        End Sub

        Public Sub [Error](source As String, message As String)
            Log("Error", source, message)
        End Sub

        Public Sub Warning(source As String, message As String)
            Log("Warning", source, message)
        End Sub

    End Class

    ''' <summary>Data access for the Receivers lookup table.</summary>
    Public Class ReceiverRepository

        Public Function GetAll() As List(Of ReceiverModel)
            Dim list As New List(Of ReceiverModel)()
            Using conn = DbConnectionFactory.CreateOpenConnection()
                Using cmd As New SqlCommand("SELECT Id, Rin, Name, Country, Governate, RegionCity, Street, BuildingNumber, CreatedAtUtc FROM dbo.Receivers ORDER BY Name", conn)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(ReadReceiver(reader))
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        Public Function GetById(id As Integer) As ReceiverModel
            Using conn = DbConnectionFactory.CreateOpenConnection()
                Using cmd As New SqlCommand("SELECT Id, Rin, Name, Country, Governate, RegionCity, Street, BuildingNumber, CreatedAtUtc FROM dbo.Receivers WHERE Id = @Id", conn)
                    cmd.Parameters.AddWithValue("@Id", id)
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then Return ReadReceiver(reader)
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        Public Function GetByRin(rin As String) As ReceiverModel
            Using conn = DbConnectionFactory.CreateOpenConnection()
                Using cmd As New SqlCommand("SELECT Id, Rin, Name, Country, Governate, RegionCity, Street, BuildingNumber, CreatedAtUtc FROM dbo.Receivers WHERE Rin = @Rin", conn)
                    cmd.Parameters.AddWithValue("@Rin", rin)
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then Return ReadReceiver(reader)
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        ''' <summary>Inserts a new receiver. Returns (Id, IsDuplicate).</summary>
        Public Function Insert(item As ReceiverModel) As (id As Integer, isDuplicate As Boolean)
            Using conn = DbConnectionFactory.CreateOpenConnection()
                Dim sql As String = "
                    IF NOT EXISTS (SELECT 1 FROM dbo.Receivers WHERE Rin = @Rin)
                    BEGIN
                        INSERT INTO dbo.Receivers (Rin, Name, Country, Governate, RegionCity, Street, BuildingNumber)
                        OUTPUT INSERTED.Id
                        VALUES (@Rin, @Name, @Country, @Governate, @RegionCity, @Street, @BuildingNumber);
                    END
                    ELSE
                    BEGIN
                        SELECT -1 AS Id;
                    END"

                Using cmd As New SqlCommand(sql, conn)
                    AddParameters(cmd, item)
                    Dim result = cmd.ExecuteScalar()
                    Dim id = CInt(result)
                    If id > 0 Then
                        Return (id, False)
                    Else
                        Return (0, True)
                    End If
                End Using
            End Using
        End Function

        Public Sub Update(item As ReceiverModel)
            Using conn = DbConnectionFactory.CreateOpenConnection()
                Dim sql As String = "
                    UPDATE dbo.Receivers SET
                        Rin = @Rin, Name = @Name, Country = @Country, Governate = @Governate,
                        RegionCity = @RegionCity, Street = @Street, BuildingNumber = @BuildingNumber,
                        UpdatedAtUtc = SYSUTCDATETIME()
                    WHERE Id = @Id;"

                Using cmd As New SqlCommand(sql, conn)
                    AddParameters(cmd, item)
                    cmd.Parameters.AddWithValue("@Id", item.Id)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Public Sub Delete(id As Integer)
            Using conn = DbConnectionFactory.CreateOpenConnection()
                Using cmd As New SqlCommand("DELETE FROM dbo.Receivers WHERE Id = @Id", conn)
                    cmd.Parameters.AddWithValue("@Id", id)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Public Function Search(searchText As String) As List(Of ReceiverModel)
            Dim all = GetAll()
            If String.IsNullOrWhiteSpace(searchText) Then Return all
            Dim t = searchText.Trim().ToLowerInvariant()
            Return all.Where(Function(x) x.Rin.ToLowerInvariant().Contains(t) OrElse
                                       x.Name.ToLowerInvariant().Contains(t)).ToList()
        End Function

        Private Shared Function ReadReceiver(reader As SqlDataReader) As ReceiverModel
            Return New ReceiverModel With {
                .Id = reader.GetInt32(0),
                .Rin = reader.GetString(1),
                .Name = reader.GetString(2),
                .Country = If(reader.IsDBNull(3), "EG", reader.GetString(3)),
                .Governate = If(reader.IsDBNull(4), "", reader.GetString(4)),
                .RegionCity = If(reader.IsDBNull(5), "", reader.GetString(5)),
                .Street = If(reader.IsDBNull(6), "", reader.GetString(6)),
                .BuildingNumber = If(reader.IsDBNull(7), "", reader.GetString(7)),
                .CreatedAtUtc = reader.GetDateTime(8)
            }
        End Function

        Private Shared Sub AddParameters(cmd As SqlCommand, item As ReceiverModel)
            cmd.Parameters.AddWithValue("@Rin", item.Rin)
            cmd.Parameters.AddWithValue("@Name", item.Name)
            cmd.Parameters.AddWithValue("@Country", If(String.IsNullOrWhiteSpace(item.Country), "EG", item.Country))
            cmd.Parameters.AddWithValue("@Governate", If(item.Governate, ""))
            cmd.Parameters.AddWithValue("@RegionCity", If(item.RegionCity, ""))
            cmd.Parameters.AddWithValue("@Street", If(item.Street, ""))
            cmd.Parameters.AddWithValue("@BuildingNumber", If(item.BuildingNumber, ""))
        End Sub

    End Class

End Namespace
