CREATE TABLE [dbo].[ApplicationUser]
(
    [ApplicationUserId] INT NOT NULL PRIMARY KEY IDENTITY,
    [Fullname] NVARCHAR(30) NOT NULL,
    [Username] NVARCHAR(20) NOT NULL,
    [NormalizedUsername] NVARCHAR(20) NOT NULL,
    [Email] NVARCHAR(30) NOT NULL,
    [NormalizedEmail] NVARCHAR(30) NOT NULL,
    [EmailConfirmed] BIT NOT NULL DEFAULT 0,
    [PasswordHash] NVARCHAR(MAX) NULL,
    [PhoneNumber] NVARCHAR(20) NULL,
    [PhoneNumberConfirmed] BIT NULL,
    [TwoFactorEnabled] BIT NULL
)
 
GO
 
CREATE INDEX [IX_ApplicationUser_NormalizedUserName] ON [dbo].[ApplicationUser] ([NormalizedUsername])
 
GO
 
CREATE INDEX [IX_ApplicationUser_NormalizedEmail] ON [dbo].[ApplicationUser] ([NormalizedEmail])
