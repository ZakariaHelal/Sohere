/* =====================================================================
   ETA Invoicing Desktop - Database schema (SQL Server Express / LocalDB)
   Run this once against your LocalDB instance, e.g.:
   (localdb)\MSSQLLocalDB
   ===================================================================== */

IF DB_ID('ETAInvoicingDB') IS NULL
BEGIN
    CREATE DATABASE ETAInvoicingDB;
END
GO

USE ETAInvoicingDB;
GO

-- ---------------------------------------------------------------------
-- Documents submitted (invoices, credit notes, debit notes)
-- ---------------------------------------------------------------------
IF OBJECT_ID('dbo.Documents', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Documents
    (
        Id              INT IDENTITY(1,1) PRIMARY KEY,
        InternalId      NVARCHAR(50)  NOT NULL,
        DocumentType    NVARCHAR(5)   NOT NULL,        -- I, C, D
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
        Environment     NVARCHAR(20)  NOT NULL,        -- Preproduction / Production
        Status          NVARCHAR(30)  NOT NULL DEFAULT 'Draft', -- Draft, Submitted, Valid, Invalid, Rejected, Cancelled, Failed
        StatusReason    NVARCHAR(500) NULL,
        DocumentJson    NVARCHAR(MAX) NOT NULL,        -- full payload as sent
        ResponseJson    NVARCHAR(MAX) NULL,            -- last API response
        CreatedAtUtc    DATETIME2     NOT NULL DEFAULT SYSUTCDATETIME(),
        UpdatedAtUtc    DATETIME2     NOT NULL DEFAULT SYSUTCDATETIME(),
        LastStatusCheckUtc DATETIME2  NULL
    );

    CREATE INDEX IX_Documents_InternalId ON dbo.Documents(InternalId);
    CREATE INDEX IX_Documents_Uuid ON dbo.Documents(Uuid);
    CREATE INDEX IX_Documents_Status ON dbo.Documents(Status);
    CREATE INDEX IX_Documents_DateTimeIssued ON dbo.Documents(DateTimeIssued);
END
GO

-- ---------------------------------------------------------------------
-- Batch submissions (Excel/CSV imports) - header per file
-- ---------------------------------------------------------------------
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
END
GO

-- ---------------------------------------------------------------------
-- Link batch import rows to created documents
-- ---------------------------------------------------------------------
IF OBJECT_ID('dbo.BatchImportItems', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.BatchImportItems
    (
        Id              INT IDENTITY(1,1) PRIMARY KEY,
        BatchImportId   INT NOT NULL,
        InternalId      NVARCHAR(50) NOT NULL,
        DocumentId      INT NULL,                       -- FK to Documents.Id once created
        LineCount       INT NOT NULL DEFAULT 0,
        TotalAmount     DECIMAL(18,5) NULL,
        Status          NVARCHAR(30) NOT NULL DEFAULT 'Pending', -- Pending, Submitted, Failed
        ErrorMessage    NVARCHAR(1000) NULL,
        RawLinesJson    NVARCHAR(MAX) NULL,             -- raw line items for breakup preview
        CONSTRAINT FK_BatchImportItems_BatchImports FOREIGN KEY (BatchImportId)
            REFERENCES dbo.BatchImports(Id) ON DELETE CASCADE
    );

    CREATE INDEX IX_BatchImportItems_BatchImportId ON dbo.BatchImportItems(BatchImportId);
END
GO

-- ---------------------------------------------------------------------
-- EGS / Item codes library
-- ---------------------------------------------------------------------
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
END
GO

-- ---------------------------------------------------------------------
-- Notification log (validation / issuance / rejection / cancellation events)
-- ---------------------------------------------------------------------
IF OBJECT_ID('dbo.NotificationLog', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.NotificationLog
    (
        Id              INT IDENTITY(1,1) PRIMARY KEY,
        DocumentId      INT NULL,
        InternalId      NVARCHAR(50) NULL,
        Uuid            NVARCHAR(100) NULL,
        EventType       NVARCHAR(30) NOT NULL,  -- Validation, Issuance, Rejection, Cancellation
        Message         NVARCHAR(1000) NULL,
        IsRead          BIT NOT NULL DEFAULT 0,
        CreatedAtUtc    DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
        CONSTRAINT FK_NotificationLog_Documents FOREIGN KEY (DocumentId)
            REFERENCES dbo.Documents(Id) ON DELETE SET NULL
    );

    CREATE INDEX IX_NotificationLog_IsRead ON dbo.NotificationLog(IsRead);
    CREATE INDEX IX_NotificationLog_CreatedAt ON dbo.NotificationLog(CreatedAtUtc);
END
GO

-- ---------------------------------------------------------------------
-- Application log (audit trail of API calls, errors, signing operations)
-- ---------------------------------------------------------------------
IF OBJECT_ID('dbo.AppLog', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.AppLog
    (
        Id          INT IDENTITY(1,1) PRIMARY KEY,
        LogLevel    NVARCHAR(20) NOT NULL,  -- Info, Warning, Error
        Source      NVARCHAR(100) NULL,
        Message     NVARCHAR(MAX) NOT NULL,
        CreatedAtUtc DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
    );

    CREATE INDEX IX_AppLog_CreatedAt ON dbo.AppLog(CreatedAtUtc);
END
GO
