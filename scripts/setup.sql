USE master
GO

IF NOT EXISTS(SELECT name FROM dbo.sysdatabases WHERE name = N'Customer')
    CREATE DATABASE Customer
GO

USE Customer
GO

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
BEGIN
    CREATE TABLE Customer (
    Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Name VARCHAR(150) NOT NULL,
    DateCreated DATETIME2 NOT NULL
)
END

