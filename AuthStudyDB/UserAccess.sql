CREATE TABLE [dbo].[UserAccess]
(
	[UserId] INT NOT NULL , 
    [AccessId] INT NOT NULL
	
    PRIMARY KEY ([UserId], [AccessId]),
	CONSTRAINT [FK_UserAccess_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]),
	CONSTRAINT [FK_UserAccess_Access] FOREIGN KEY ([AccessId]) REFERENCES [Access]([Id])
)
