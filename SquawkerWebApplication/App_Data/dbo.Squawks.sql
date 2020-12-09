CREATE TABLE [dbo].[Squawks] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [UserId]       INT            NOT NULL,
    [CreationDate] DATETIME2 (7)  NOT NULL,
    [Content]      NVARCHAR (280) NOT NULL,
    [Latitude]     DECIMAL (8, 6) NOT NULL,
    [Longitude]    DECIMAL (9, 6) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Squawks_UserIdToUsersId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

