CREATE TABLE [dbo].[ApplicationRole]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Name] NVARCHAR(32) NOT NULL,
    [NormalizedName] NVARCHAR(32) NOT NULL
)
 
GO
 
CREATE INDEX [IX_ApplicationRole_NormalizedName] ON [dbo].[ApplicationRole] ([NormalizedName])