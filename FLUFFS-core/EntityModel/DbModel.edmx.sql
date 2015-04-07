
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/07/2015 14:55:56
-- Generated from EDMX file: G:\Git\FLUFFS\FLUFFS-core\EntityModel\DbModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [FLUFFS];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_TrackedFolderTrackedFolder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TrackedFolders] DROP CONSTRAINT [FK_TrackedFolderTrackedFolder];
GO
IF OBJECT_ID(N'[dbo].[FK_SearchJobCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SearchJobs] DROP CONSTRAINT [FK_SearchJobCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_SearchJobSearchString]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SearchStrings] DROP CONSTRAINT [FK_SearchJobSearchString];
GO
IF OBJECT_ID(N'[dbo].[FK_SearchJobRegex]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Regexes] DROP CONSTRAINT [FK_SearchJobRegex];
GO
IF OBJECT_ID(N'[dbo].[FK_IndexTrackedFolder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Indices] DROP CONSTRAINT [FK_IndexTrackedFolder];
GO
IF OBJECT_ID(N'[dbo].[FK_TrackedFolderTrackedFile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TrackedFiles] DROP CONSTRAINT [FK_TrackedFolderTrackedFile];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Indices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Indices];
GO
IF OBJECT_ID(N'[dbo].[TrackedFolders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TrackedFolders];
GO
IF OBJECT_ID(N'[dbo].[TrackedFiles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TrackedFiles];
GO
IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO
IF OBJECT_ID(N'[dbo].[SearchJobs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SearchJobs];
GO
IF OBJECT_ID(N'[dbo].[SearchStrings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SearchStrings];
GO
IF OBJECT_ID(N'[dbo].[Regexes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Regexes];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Indices'
CREATE TABLE [dbo].[Indices] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Alias] nvarchar(max)  NOT NULL,
    [Root_Id] int  NOT NULL
);
GO

-- Creating table 'TrackedFolders'
CREATE TABLE [dbo].[TrackedFolders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [FullPath] nvarchar(max)  NOT NULL,
    [TrackedFolderId] int  NULL
);
GO

-- Creating table 'TrackedFiles'
CREATE TABLE [dbo].[TrackedFiles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [FullPath] nvarchar(max)  NOT NULL,
    [Length] bigint  NULL,
    [Extension] nvarchar(max)  NULL,
    [PreHash] nvarchar(max)  NULL,
    [MD5] nvarchar(max)  NULL,
    [SHA1] nvarchar(max)  NULL,
    [SHA256] nvarchar(max)  NULL,
    [TrackForUpdates] bit  NOT NULL,
    [Created] datetime  NOT NULL,
    [LastSeen] datetime  NOT NULL,
    [TrackedFolderId] int  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SearchJobs'
CREATE TABLE [dbo].[SearchJobs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Alias] nvarchar(max)  NOT NULL,
    [Status] int  NOT NULL,
    [RequestorComments] nvarchar(max)  NOT NULL,
    [AdminComments] nvarchar(max)  NOT NULL,
    [Category_Id] int  NOT NULL
);
GO

-- Creating table 'SearchStrings'
CREATE TABLE [dbo].[SearchStrings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [SearchJobId] int  NOT NULL
);
GO

-- Creating table 'Regexes'
CREATE TABLE [dbo].[Regexes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [SearchJobId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Indices'
ALTER TABLE [dbo].[Indices]
ADD CONSTRAINT [PK_Indices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TrackedFolders'
ALTER TABLE [dbo].[TrackedFolders]
ADD CONSTRAINT [PK_TrackedFolders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TrackedFiles'
ALTER TABLE [dbo].[TrackedFiles]
ADD CONSTRAINT [PK_TrackedFiles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SearchJobs'
ALTER TABLE [dbo].[SearchJobs]
ADD CONSTRAINT [PK_SearchJobs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SearchStrings'
ALTER TABLE [dbo].[SearchStrings]
ADD CONSTRAINT [PK_SearchStrings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Regexes'
ALTER TABLE [dbo].[Regexes]
ADD CONSTRAINT [PK_Regexes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TrackedFolderId] in table 'TrackedFolders'
ALTER TABLE [dbo].[TrackedFolders]
ADD CONSTRAINT [FK_TrackedFolderTrackedFolder]
    FOREIGN KEY ([TrackedFolderId])
    REFERENCES [dbo].[TrackedFolders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TrackedFolderTrackedFolder'
CREATE INDEX [IX_FK_TrackedFolderTrackedFolder]
ON [dbo].[TrackedFolders]
    ([TrackedFolderId]);
GO

-- Creating foreign key on [Category_Id] in table 'SearchJobs'
ALTER TABLE [dbo].[SearchJobs]
ADD CONSTRAINT [FK_SearchJobCategory]
    FOREIGN KEY ([Category_Id])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SearchJobCategory'
CREATE INDEX [IX_FK_SearchJobCategory]
ON [dbo].[SearchJobs]
    ([Category_Id]);
GO

-- Creating foreign key on [SearchJobId] in table 'SearchStrings'
ALTER TABLE [dbo].[SearchStrings]
ADD CONSTRAINT [FK_SearchJobSearchString]
    FOREIGN KEY ([SearchJobId])
    REFERENCES [dbo].[SearchJobs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SearchJobSearchString'
CREATE INDEX [IX_FK_SearchJobSearchString]
ON [dbo].[SearchStrings]
    ([SearchJobId]);
GO

-- Creating foreign key on [SearchJobId] in table 'Regexes'
ALTER TABLE [dbo].[Regexes]
ADD CONSTRAINT [FK_SearchJobRegex]
    FOREIGN KEY ([SearchJobId])
    REFERENCES [dbo].[SearchJobs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SearchJobRegex'
CREATE INDEX [IX_FK_SearchJobRegex]
ON [dbo].[Regexes]
    ([SearchJobId]);
GO

-- Creating foreign key on [Root_Id] in table 'Indices'
ALTER TABLE [dbo].[Indices]
ADD CONSTRAINT [FK_IndexTrackedFolder]
    FOREIGN KEY ([Root_Id])
    REFERENCES [dbo].[TrackedFolders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_IndexTrackedFolder'
CREATE INDEX [IX_FK_IndexTrackedFolder]
ON [dbo].[Indices]
    ([Root_Id]);
GO

-- Creating foreign key on [TrackedFolderId] in table 'TrackedFiles'
ALTER TABLE [dbo].[TrackedFiles]
ADD CONSTRAINT [FK_TrackedFolderTrackedFile]
    FOREIGN KEY ([TrackedFolderId])
    REFERENCES [dbo].[TrackedFolders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TrackedFolderTrackedFile'
CREATE INDEX [IX_FK_TrackedFolderTrackedFile]
ON [dbo].[TrackedFiles]
    ([TrackedFolderId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------