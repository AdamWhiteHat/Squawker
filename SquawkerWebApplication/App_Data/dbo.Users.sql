CREATE TABLE [dbo].[Users] (
    [Id]             INT                IDENTITY (1, 1) NOT NULL,
    [AspNetUserId]   NVARCHAR (128)     NOT NULL,
    [CreationDate]   DATETIME2 (7)      NOT NULL,
    [TimezoneOffset] DATETIMEOFFSET (0) NOT NULL,
    [UserName]       NVARCHAR (15)      NOT NULL,
    [Email]          NVARCHAR (254)     NOT NULL,
    [FirstName]      NVARCHAR (50)      NOT NULL,
    [Surname]        NVARCHAR (50)      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    --CONSTRAINT [FK_Users_AspNetUserIdToAspNetUsersId] FOREIGN KEY ([AspNetUserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

