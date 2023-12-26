--USE [Retails];
--GO

IF OBJECT_ID (N'dbo.[FK_Accounts_Customers]', N'F') IS NOT NULL  
    ALTER TABLE [dbo].[Accounts] DROP CONSTRAINT [FK_Accounts_Customers];
IF OBJECT_ID (N'dbo.[FK_Accounts_AccountStatus]', N'F') IS NOT NULL  
    ALTER TABLE [dbo].[Accounts] DROP CONSTRAINT [FK_Accounts_AccountStatus];
GO

IF OBJECT_ID (N'dbo.[FK_Emails_Contacts]', N'F') IS NOT NULL  
    ALTER TABLE [dbo].[Emails] DROP CONSTRAINT [FK_Emails_Contacts];
GO

IF OBJECT_ID (N'dbo.[FK_AccountRoles_Accounts]', N'F') IS NOT NULL  
    ALTER TABLE [dbo].[AccountRoles] DROP CONSTRAINT [FK_AccountRoles_Accounts];
IF OBJECT_ID (N'dbo.[FK_AccountRoles_Roles]', N'F') IS NOT NULL  
    ALTER TABLE [dbo].[AccountRoles] DROP CONSTRAINT [FK_AccountRoles_Roles];
GO

IF OBJECT_ID (N'dbo.[FK_Stocks_Products]', N'F') IS NOT NULL  
    ALTER TABLE [dbo].[Stocks] DROP CONSTRAINT [FK_Stocks_Products];
GO

IF OBJECT_ID (N'dbo.[FK_Orders_Accounts]', N'F') IS NOT NULL  
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_Accounts];
GO

IF OBJECT_ID (N'dbo.[FK_OrderItems_Orders]', N'F') IS NOT NULL  
    ALTER TABLE [dbo].[OrderItems] DROP CONSTRAINT [FK_OrderItems_Orders];
IF OBJECT_ID (N'dbo.[FK_OrderItems_Products]', N'F') IS NOT NULL  
    ALTER TABLE [dbo].[OrderItems] DROP CONSTRAINT [FK_OrderItems_Products];
GO

DROP TABLE IF EXISTS [dbo].[Roles];
GO
CREATE TABLE [dbo].[Roles]
(
	[RoleId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [RoleName] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(50) NULL
);
GO

DROP TABLE IF EXISTS [dbo].[Permissions];
GO
CREATE TABLE [dbo].[Permissions]
(
	[PermissionId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [RoleID] NVARCHAR(50) NOT NULL, 
    [PermissionName] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(50) NULL
);
GO

DROP TABLE IF EXISTS [dbo].[Contacts];
GO
CREATE TABLE [dbo].[Contacts]
(
	[ContactId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [Country] NVARCHAR(50) NULL, 
    [State] NVARCHAR(50) NULL, 
    [Street] NVARCHAR(50) NULL, 
    [ZipCode] VARCHAR(15) NULL
);
GO

DROP TABLE IF EXISTS [dbo].[Customers];
GO
CREATE TABLE [dbo].[Customers]
(
	[CustomerId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ContactId] UNIQUEIDENTIFIER NULL, 
);
GO

DROP TABLE IF EXISTS [dbo].[Emails];
GO
CREATE TABLE [dbo].[Emails]
(
	[EmailId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ContactId] UNIQUEIDENTIFIER NOT NULL, 
    [EmailAddress] VARCHAR(255) NOT NULL, 
    [IsPrimary] BIT NULL, 
);

ALTER TABLE [dbo].[Emails]
    ADD CONSTRAINT [FK_Emails_Contacts] FOREIGN KEY ([ContactId])
    REFERENCES [dbo].[Contacts]([ContactId]);
GO

DROP TABLE IF EXISTS [dbo].[AccountStatus];
GO
CREATE TABLE [dbo].[AccountStatus]
(
	[AccountStatusId] TINYINT NOT NULL PRIMARY KEY, 
    [Status] VARCHAR(8) NOT NULL 
)
GO

DROP TABLE IF EXISTS [dbo].[Accounts];
GO
CREATE TABLE [dbo].[Accounts]
(
	[AccountId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [OId] UNIQUEIDENTIFIER NULL,
    [Username] NVARCHAR(50), 
    [Password] VARBINARY(64) NOT NULL, 
    [Salt] VARBINARY(64) NULL, 
    [AccountStatusId] TINYINT NOT NULL DEFAULT 0,
    [CustomerId] UNIQUEIDENTIFIER NULL, 
);

ALTER TABLE [dbo].[Accounts]
    ADD CONSTRAINT [FK_Accounts_Customers] FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customers]([CustomerId]);

ALTER TABLE [dbo].[Accounts]
    ADD CONSTRAINT [FK_Accounts_AccountStatus] FOREIGN KEY ([AccountStatusId])
    REFERENCES [dbo].AccountStatus([AccountStatusId]);
GO

DROP TABLE IF EXISTS [dbo].[AccountRoles];
GO
CREATE TABLE [dbo].[AccountRoles]
(
    [AccountId] UNIQUEIDENTIFIER NOT NULL,
    [RoleId] UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY ([AccountId], [RoleId]), 
);

ALTER TABLE [dbo].[AccountRoles]
    ADD CONSTRAINT [FK_AccountRoles_Accounts] FOREIGN KEY ([AccountId])
    REFERENCES [dbo].[Accounts]([AccountId]);

ALTER TABLE [dbo].[AccountRoles]
    ADD CONSTRAINT [FK_AccountRoles_Roles] FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Roles]([RoleId]);
GO

DROP TABLE IF EXISTS [dbo].[Products];
GO
CREATE TABLE [dbo].[Products]
(
    [ProductId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [ProductName] NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(255) NULL,
    [Price] DECIMAL(10,2) NOT NULL
);

DROP TABLE IF EXISTS [dbo].[Stocks];
GO
CREATE TABLE [dbo].[Stocks]
(
    [StockId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [ProductId] UNIQUEIDENTIFIER NOT NULL,
    [Quantity] INT NOT NULL
);

ALTER TABLE [dbo].[Stocks]
    ADD CONSTRAINT [FK_Stocks_Products] FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]([ProductId]);
GO

DROP TABLE IF EXISTS [dbo].[Orders];
GO
CREATE TABLE [dbo].[Orders]
(
    [OrderId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [AccountId] UNIQUEIDENTIFIER NOT NULL,
    [OrderDate] DATETIME NOT NULL
);

ALTER TABLE [dbo].[Orders]
    ADD CONSTRAINT [FK_Orders_Accounts] FOREIGN KEY ([AccountId])
    REFERENCES [dbo].[Accounts]([AccountId]);
GO

DROP TABLE IF EXISTS [dbo].[OrderItems];
GO
CREATE TABLE [dbo].[OrderItems]
(
    [OrderItemId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [OrderId] UNIQUEIDENTIFIER NOT NULL,
    [ProductId] UNIQUEIDENTIFIER NOT NULL,
    [Quantity] INT NOT NULL,
    [Price] DECIMAL(10,2) NOT NULL
);

ALTER TABLE [dbo].[OrderItems]
    ADD CONSTRAINT [FK_OrderItems_Orders] FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[Orders]([OrderId]);

ALTER TABLE [dbo].[OrderItems]
    ADD CONSTRAINT [FK_OrderItems_Products] FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]([ProductId]);
GO