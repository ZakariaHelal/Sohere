Option Strict On
Imports Microsoft.Data.SqlClient

Namespace Data

    ''' <summary>
    ''' Provides SqlConnection instances using the connection string from App.config.
    ''' Default points to LocalDB instance "ETAInvoicingDB".
    ''' </summary>
    Public Module DbConnectionFactory

        ' Public Const DefaultConnectionString As String =
        '"Data Source= " 192.168.1.2 "," 1434 " ; Initial Catalog=" ETAInvoicingDB " ; User ID=za; Password=Alhayat2020; Trusted_Connection=False"

        '"Server=(localdb)\MSSQLLocalDB;Database=ETAInvoicingDB;Integrated Security=True;TrustServerCertificate=True;"

        Public Const DefaultConnectionString As String =
            "Data Source=192.168.1.2,1434;Initial Catalog=ETAInvoicingDB;User ID=za;Password=Alhayat2020;Trusted_Connection=False;TrustServerCertificate=True;"
        Private _connectionString As String = ""

        ''' <summary>
        ''' Gets the active connection string. Reads from App.config (key "ETAInvoicingDB")
        ''' if present, otherwise falls back to the LocalDB default.
        ''' </summary>
        Public Function GetConnectionString() As String
            If Not String.IsNullOrEmpty(_connectionString) Then Return _connectionString

            Try
                Dim configValue As String = System.Configuration.ConfigurationManager.ConnectionStrings("ETAInvoicingDB")?.ConnectionString
                If Not String.IsNullOrWhiteSpace(configValue) Then
                    _connectionString = configValue
                    Return _connectionString
                End If
            Catch
                ' fall through to default
            End Try

            _connectionString = DefaultConnectionString
            Return _connectionString
        End Function

        ''' <summary>Allows overriding the connection string at runtime (e.g. from a settings screen).</summary>
        Public Sub SetConnectionString(connectionString As String)
            _connectionString = connectionString
        End Sub

        ''' <summary>Creates (and opens) a new SqlConnection.</summary>
        Public Function CreateOpenConnection() As SqlConnection
            Dim conn As New SqlConnection(GetConnectionString())
            conn.Open()
            Return conn
        End Function

        ''' <summary>
        ''' Creates the configured LocalDB database and required tables when they do not exist.
        ''' This lets a fresh install start without manually running SQL\01_CreateDatabase.sql.
        ''' </summary>
        Public Sub EnsureDatabaseCreated()
            Dim connectionString = GetConnectionString()

            Try
                Using conn As New SqlConnection(connectionString)
                    conn.Open()
                    EnsureSchema(conn)
                    Return
                End Using
            Catch ex As SqlException When IsMissingDatabaseError(ex)
                CreateDatabase(connectionString)
            End Try

            Using conn As New SqlConnection(connectionString)
                conn.Open()
                EnsureSchema(conn)
            End Using
        End Sub

        Private Sub CreateDatabase(connectionString As String)
            Dim builder As New SqlConnectionStringBuilder(connectionString)
            Dim databaseName = builder.InitialCatalog

            If String.IsNullOrWhiteSpace(databaseName) Then
                Throw New InvalidOperationException("The database name is missing from the ETAInvoicingDB connection string.")
            End If

            builder.InitialCatalog = "master"

            Using conn As New SqlConnection(builder.ConnectionString)
                conn.Open()
                Using cmd As New SqlCommand("
                    IF DB_ID(@DatabaseName) IS NULL
                    BEGIN
                        DECLARE @sql nvarchar(max) = N'CREATE DATABASE ' + QUOTENAME(@DatabaseName);
                        EXEC(@sql);
                    END", conn)
                    cmd.Parameters.AddWithValue("@DatabaseName", databaseName)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Private Function IsMissingDatabaseError(ex As SqlException) As Boolean
            Return ex.Errors.Cast(Of SqlError)().Any(Function(err) err.Number = 4060 OrElse err.Number = 911)
        End Function

        Private Sub EnsureSchema(conn As SqlConnection)
            ExecuteNonQuery(conn, "
                IF OBJECT_ID('dbo.Documents', 'U') IS NULL
                BEGIN
                    CREATE TABLE dbo.Documents
                    (
                        Id              INT IDENTITY(1,1) PRIMARY KEY,
                        InternalId      NVARCHAR(50)  NOT NULL,
                        DocumentType    NVARCHAR(5)   NOT NULL,
                        Uuid            NVARCHAR(100) NULL,
                        SubmissionUuid  NVARCHAR(100) NULL,
                        LongId          NVARCHAR(100) NULL,
                        IssuerName      NVARCHAR(250) NULL,
                        IssuerRIN       NVARCHAR(20)  NULL,
                        ReceiverName    NVARCHAR(250) NULL,
                        ReceiverRIN     NVARCHAR(20)  NULL,
                        DateTimeIssued  DATETIME2     NULL,
                        TotalAmount     DECIMAL(18,5) NULL,
                        NetAmount       DECIMAL(18,5) NULL,
                        TaxAmount       DECIMAL(18,5) NULL,
                        Currency        NVARCHAR(5)   NULL DEFAULT 'EGP',
                        Environment     NVARCHAR(20)  NOT NULL,
                        Status          NVARCHAR(30)  NOT NULL DEFAULT 'Draft',
                        StatusReason    NVARCHAR(500) NULL,
                        DocumentJson    NVARCHAR(MAX) NOT NULL,
                        ResponseJson    NVARCHAR(MAX) NULL,
                        CreatedAtUtc    DATETIME2     NOT NULL DEFAULT SYSUTCDATETIME(),
                        UpdatedAtUtc    DATETIME2     NOT NULL DEFAULT SYSUTCDATETIME(),
                        LastStatusCheckUtc DATETIME2  NULL
                    );

                    CREATE INDEX IX_Documents_InternalId ON dbo.Documents(InternalId);
                    CREATE INDEX IX_Documents_Uuid ON dbo.Documents(Uuid);
                    CREATE INDEX IX_Documents_Status ON dbo.Documents(Status);
                    CREATE INDEX IX_Documents_DateTimeIssued ON dbo.Documents(DateTimeIssued);
                END")

            ExecuteNonQuery(conn, "
                IF OBJECT_ID('dbo.BatchImports', 'U') IS NULL
                BEGIN
                    CREATE TABLE dbo.BatchImports
                    (
                        Id              INT IDENTITY(1,1) PRIMARY KEY,
                        FileName        NVARCHAR(260) NOT NULL,
                        ImportedAtUtc   DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
                        TotalInvoices   INT NOT NULL DEFAULT 0,
                        SucceededCount  INT NOT NULL DEFAULT 0,
                        FailedCount     INT NOT NULL DEFAULT 0,
                        Environment     NVARCHAR(20) NOT NULL,
                        Notes           NVARCHAR(500) NULL
                    );
                END")

            ExecuteNonQuery(conn, "
                IF OBJECT_ID('dbo.BatchImportItems', 'U') IS NULL
                BEGIN
                    CREATE TABLE dbo.BatchImportItems
                    (
                        Id              INT IDENTITY(1,1) PRIMARY KEY,
                        BatchImportId   INT NOT NULL,
                        InternalId      NVARCHAR(50) NOT NULL,
                        DocumentId      INT NULL,
                        LineCount       INT NOT NULL DEFAULT 0,
                        TotalAmount     DECIMAL(18,5) NULL,
                        Status          NVARCHAR(30) NOT NULL DEFAULT 'Pending',
                        ErrorMessage    NVARCHAR(1000) NULL,
                        RawLinesJson    NVARCHAR(MAX) NULL,
                        CONSTRAINT FK_BatchImportItems_BatchImports FOREIGN KEY (BatchImportId)
                            REFERENCES dbo.BatchImports(Id) ON DELETE CASCADE
                    );

                    CREATE INDEX IX_BatchImportItems_BatchImportId ON dbo.BatchImportItems(BatchImportId);
                END")

            ExecuteNonQuery(conn, "
                IF OBJECT_ID('dbo.EgsCodes', 'U') IS NULL
                BEGIN
                    CREATE TABLE dbo.EgsCodes
                    (
                        Id              INT IDENTITY(1,1) PRIMARY KEY,
                        EgsCode         NVARCHAR(50) NOT NULL UNIQUE,
                        Description     NVARCHAR(500) NOT NULL,
                        UnitType        NVARCHAR(10) NOT NULL DEFAULT 'EA',
                        DefaultPrice    DECIMAL(18,5) NOT NULL DEFAULT 0,
                        DefaultTaxType  NVARCHAR(5) NOT NULL DEFAULT 'T1',
                        DefaultTaxRate  DECIMAL(9,4) NOT NULL DEFAULT 14,
                        Category        NVARCHAR(100) NULL,
                        IsActive        BIT NOT NULL DEFAULT 1,
                        CreatedAtUtc    DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
                    );

                    CREATE INDEX IX_EgsCodes_Code ON dbo.EgsCodes(EgsCode);
                END")

            ExecuteNonQuery(conn, "
                IF OBJECT_ID('dbo.NotificationLog', 'U') IS NULL
                BEGIN
                    CREATE TABLE dbo.NotificationLog
                    (
                        Id              INT IDENTITY(1,1) PRIMARY KEY,
                        DocumentId      INT NULL,
                        InternalId      NVARCHAR(50) NULL,
                        Uuid            NVARCHAR(100) NULL,
                        EventType       NVARCHAR(30) NOT NULL,
                        Message         NVARCHAR(1000) NULL,
                        IsRead          BIT NOT NULL DEFAULT 0,
                        CreatedAtUtc    DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
                        CONSTRAINT FK_NotificationLog_Documents FOREIGN KEY (DocumentId)
                            REFERENCES dbo.Documents(Id) ON DELETE SET NULL
                    );

                    CREATE INDEX IX_NotificationLog_IsRead ON dbo.NotificationLog(IsRead);
                    CREATE INDEX IX_NotificationLog_CreatedAt ON dbo.NotificationLog(CreatedAtUtc);
                END")

            ExecuteNonQuery(conn, "
                IF OBJECT_ID('dbo.AppLog', 'U') IS NULL
                BEGIN
                    CREATE TABLE dbo.AppLog
                    (
                        Id          INT IDENTITY(1,1) PRIMARY KEY,
                        LogLevel    NVARCHAR(20) NOT NULL,
                        Source      NVARCHAR(100) NULL,
                        Message     NVARCHAR(MAX) NOT NULL,
                        CreatedAtUtc DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
                    );

                    CREATE INDEX IX_AppLog_CreatedAt ON dbo.AppLog(CreatedAtUtc);
                END")
        End Sub

        Private Sub ExecuteNonQuery(conn As SqlConnection, sql As String)
            Using cmd As New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
            End Using
        End Sub

    End Module

End Namespace
