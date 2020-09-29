CREATE TABLE [dbo].[Users] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Login]    NVARCHAR (50) NOT NULL,
    [Password] NVARCHAR (50) NOT NULL,
    [Country]  NVARCHAR (50) NULL,
    [City]     NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Login] ASC)
);

