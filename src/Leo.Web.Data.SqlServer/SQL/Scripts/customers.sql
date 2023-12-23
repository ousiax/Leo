-- USE [Customers];
-- GO

DROP TABLE IF EXISTS [dbo].[customer];
GO
CREATE TABLE [dbo].[customer]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [name] NVARCHAR(50) NOT NULL, 
    [phone] NVARCHAR(20) NOT NULL, 
    [gender] NVARCHAR(10) NULL, 
    [birthday] DATETIME NULL, 
    [cardno] NVARCHAR(50) NULL, 
    [created_at] DATETIME NULL, 
    [created_by] NVARCHAR(50) NULL, 
    [updated_at] DATETIME NULL, 
    [updated_by] NVARCHAR(50) NULL
);
GO

DROP TABLE IF EXISTS [dbo].[customer_detail];
GO
CREATE TABLE [dbo].[customer_detail]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [customer_id] UNIQUEIDENTIFIER NOT NULL, 
    [date] DATE NOT NULL, 
    [item] NVARCHAR(50) NOT NULL, 
    [count] INT NULL, 
    [height] NUMERIC(18, 2) NULL, 
    [weight] NUMERIC(18, 2) NULL, 
    [created_at] DATETIME NULL, 
    [created_by] NVARCHAR(50) NULL, 
    [updated_at] DATETIME NULL, 
    [updated_by] NVARCHAR(50) NULL
);
GO