-- Creates the Receivers lookup table for saved receiver/buyer information.
-- Run once against your ETAInvoicing database.

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Receivers' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE dbo.Receivers (
        Id              INT IDENTITY(1,1)   NOT NULL,
        Rin             NVARCHAR(100)       NOT NULL,
        Name            NVARCHAR(200)       NOT NULL,
        Country         NVARCHAR(5)         NOT NULL DEFAULT 'EG',
        Governate       NVARCHAR(200)       NOT NULL DEFAULT '',
        RegionCity      NVARCHAR(200)       NOT NULL DEFAULT '',
        Street          NVARCHAR(200)       NOT NULL DEFAULT '',
        BuildingNumber  NVARCHAR(50)        NOT NULL DEFAULT '',
        CreatedAtUtc    DATETIME2           NOT NULL DEFAULT SYSUTCDATETIME(),
        UpdatedAtUtc    DATETIME2           NOT NULL DEFAULT SYSUTCDATETIME(),
        CONSTRAINT PK_Receivers PRIMARY KEY CLUSTERED (Id),
        CONSTRAINT UQ_Receivers_Rin UNIQUE (Rin)
    );
END;
