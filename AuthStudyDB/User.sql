CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [UserName] NCHAR(10) NOT NULL, 
    [Password] NCHAR(20) NOT NULL
)

GO

CREATE INDEX [IX_User_Id] ON [dbo].[User] ([Id])
