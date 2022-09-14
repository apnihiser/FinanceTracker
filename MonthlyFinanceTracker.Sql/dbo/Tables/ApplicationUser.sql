CREATE TABLE [dbo].[ApplicationUser]
(
    [ApplicationUserId] INT NOT NULL PRIMARY KEY IDENTITY,
    [Fullname] NVARCHAR(30) NOT NULL,
    [UserName] NVARCHAR(20) NOT NULL,
    [NormalizedUserName] NVARCHAR(20) NOT NULL,
    [Email] NVARCHAR(30) NULL,
    [NormalizedEmail] NVARCHAR(30) NULL,
    [EmailConfirmed] BIT NOT NULL,
    [PasswordHash] NVARCHAR(MAX) NULL,
    [PhoneNumber] NVARCHAR(20) NULL,
    [PhoneNumberConfirmed] BIT NOT NULL,
    [TwoFactorEnabled] BIT NOT NULL
)
 
GO
 
CREATE INDEX [IX_ApplicationUser_NormalizedUserName] ON [dbo].[ApplicationUser] ([NormalizedUserName])
 
GO
 
CREATE INDEX [IX_ApplicationUser_NormalizedEmail] ON [dbo].[ApplicationUser] ([NormalizedEmail])
