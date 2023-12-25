IF OBJECT_ID (N'dbo.[FK_Accounts_Roles]', N'F') IS NOT NULL  
    ALTER TABLE [dbo].[Accounts] DROP CONSTRAINT [FK_Accounts_Roles];
IF OBJECT_ID (N'dbo.[FK_Accounts_Customers]', N'F') IS NOT NULL  
    ALTER TABLE [dbo].[Accounts] DROP CONSTRAINT [FK_Accounts_Customers];
IF OBJECT_ID (N'dbo.[FK_Accounts_AccountStatus]', N'F') IS NOT NULL  
    ALTER TABLE [dbo].[Accounts] DROP CONSTRAINT [FK_Accounts_AccountStatus];
GO

IF OBJECT_ID (N'dbo.[FK_Emails_Contacts]', N'F') IS NOT NULL  
    ALTER TABLE [dbo].[Emails] DROP CONSTRAINT [FK_Emails_Contacts];
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
GO
DROP TABLE IF EXISTS [dbo].[Accounts];
GO
CREATE TABLE [dbo].[Accounts]
(
	[AccountId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Username] NVARCHAR(50), 
    [Password] VARBINARY(64) NOT NULL, 
    [Salt] VARBINARY(64) NULL, 
    [AccountStatusId] TINYINT NOT NULL DEFAULT 0,
    [RoleId] UNIQUEIDENTIFIER NULL, 
    [CustomerId] UNIQUEIDENTIFIER NULL, 
);
GO
DROP TABLE IF EXISTS [dbo].[AccountStatus];
GO
CREATE TABLE [dbo].[AccountStatus]
(
	[AccountStatusId] TINYINT NOT NULL PRIMARY KEY, 
    [Status] VARCHAR(8) NOT NULL 
)
INSERT INTO [dbo].[AccountStatus](AccountStatusId, Status) VALUES(0, 'active');
INSERT INTO [dbo].[AccountStatus](AccountStatusId, Status) VALUES(1, 'deactive');
INSERT INTO [dbo].[AccountStatus](AccountStatusId, Status) VALUES(2, 'suspend');
GO

ALTER TABLE [dbo].[Accounts]
    ADD CONSTRAINT [FK_Accounts_Roles] FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Roles] ([RoleId]);
ALTER TABLE [dbo].[Accounts]
    ADD CONSTRAINT [FK_Accounts_Customers] FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customers]([CustomerId]);
ALTER TABLE [dbo].[Accounts]
    ADD CONSTRAINT [FK_Accounts_AccountStatus] FOREIGN KEY ([AccountStatusId])
    REFERENCES [dbo].AccountStatus([AccountStatusId]);
GO
ALTER TABLE [dbo].[Emails]
    ADD CONSTRAINT [FK_Emails_Contacts] FOREIGN KEY ([ContactId])
    REFERENCES [dbo].[Contacts]([ContactId]);
GO