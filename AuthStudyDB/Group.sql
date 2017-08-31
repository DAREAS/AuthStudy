CREATE TABLE [dbo].[Group]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(175) NOT NULL
)

GO

CREATE INDEX [IX_Group_ID] ON [dbo].[Group] ([Id])
