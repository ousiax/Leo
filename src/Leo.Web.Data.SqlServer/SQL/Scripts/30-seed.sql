--USE [Retails];
--GO

INSERT INTO [dbo].[AccountStatus]([AccountStatusId], [Status]) VALUES(0, 'active');
INSERT INTO [dbo].[AccountStatus]([AccountStatusId], [Status]) VALUES(1, 'deactive');
INSERT INTO [dbo].[AccountStatus]([AccountStatusId], [Status]) VALUES(2, 'suspend');
GO

INSERT INTO [dbo].[Roles]([RoleId], [RoleName], [Description])
VALUES
    (NEWID(), 'Admin', 'Has full access to all system functions, can manage and assign roles'),
    (NEWID(), 'Manager', 'Can access managerial functions, manage certain aspects of the system and has oversight over Employee role'),
    (NEWID(), 'Employee', 'Can perform day-to-day tasks relevant to their role but with limited system access'),
    (NEWID(), 'Consumer', 'Has access to consumer-related functions such as making a purchase, browsing products'),
    (NEWID(), 'Guest', 'Has limited access to basic functionalities for browsing and perhaps making purchases, usually without creating a full account');
GO