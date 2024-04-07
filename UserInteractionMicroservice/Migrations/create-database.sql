IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Users] (
    [Id] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Notifications] (
    [Id] int NOT NULL IDENTITY,
    [CreatorId] nvarchar(450) NOT NULL,
    [Type] nvarchar(max) NOT NULL,
    [Message] nvarchar(max) NOT NULL,
    [HasSeen] bit NOT NULL,
    [DateOfDelivery] datetime2 NOT NULL,
    CONSTRAINT [PK_Notifications] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Notifications_Users_CreatorId] FOREIGN KEY ([CreatorId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Subscriptions] (
    [Id] int NOT NULL IDENTITY,
    [FollowerId] nvarchar(450) NOT NULL,
    [CreatorId] nvarchar(max) NOT NULL,
    [Type] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Subscriptions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Subscriptions_Users_FollowerId] FOREIGN KEY ([FollowerId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Notifications_CreatorId] ON [Notifications] ([CreatorId]);
GO

CREATE INDEX [IX_Subscriptions_FollowerId] ON [Subscriptions] ([FollowerId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240407133232_YourMigrationName', N'7.0.7');
GO

COMMIT;
GO

