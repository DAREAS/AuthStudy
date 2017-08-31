CREATE TABLE [dbo].[GroupAccess]
(
	[GroupId] INT NOT NULL , 
    [AccessId] INT NOT NULL 

    PRIMARY KEY ([GroupId], [AccessId]), 
    CONSTRAINT [FK_GroupAccess_Group] FOREIGN KEY ([GroupId]) REFERENCES [Group]([Id]),
	CONSTRAINT [FK_GroupAccess_Access] FOREIGN KEY ([AccessId]) REFERENCES [Access]([Id])
)
