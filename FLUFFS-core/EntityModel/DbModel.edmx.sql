
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/10/2015 18:59:03
-- Generated from EDMX file: C:\Users\dupoi\Documents\GitHub\FLUFFS\FLUFFS-core\EntityModel\DbModel.edmx
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
IF OBJECT_ID(N'[dbo].[FK_SearchJobTrackedFile_SearchJob]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SearchJobTrackedFile] DROP CONSTRAINT [FK_SearchJobTrackedFile_SearchJob];
GO
IF OBJECT_ID(N'[dbo].[FK_SearchJobTrackedFile_TrackedFile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SearchJobTrackedFile] DROP CONSTRAINT [FK_SearchJobTrackedFile_TrackedFile];
GO
IF OBJECT_ID(N'[dbo].[FK_CategoryTrackedFile_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CategoryTrackedFile] DROP CONSTRAINT [FK_CategoryTrackedFile_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_CategoryTrackedFile_TrackedFile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CategoryTrackedFile] DROP CONSTRAINT [FK_CategoryTrackedFile_TrackedFile];
GO
IF OBJECT_ID(N'[dbo].[FK_TrackedFileShrinkJob_TrackedFile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TrackedFileShrinkJob] DROP CONSTRAINT [FK_TrackedFileShrinkJob_TrackedFile];
GO
IF OBJECT_ID(N'[dbo].[FK_TrackedFileShrinkJob_ShrinkJob]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TrackedFileShrinkJob] DROP CONSTRAINT [FK_TrackedFileShrinkJob_ShrinkJob];
GO
IF OBJECT_ID(N'[dbo].[FK_ShrinkJobReductionLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReductionLogs] DROP CONSTRAINT [FK_ShrinkJobReductionLog];
GO
IF OBJECT_ID(N'[dbo].[FK_TrackedFileReductionLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReductionLogs] DROP CONSTRAINT [FK_TrackedFileReductionLog];
GO
IF OBJECT_ID(N'[dbo].[FK_UserIndex_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserIndex] DROP CONSTRAINT [FK_UserIndex_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserIndex_Index]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserIndex] DROP CONSTRAINT [FK_UserIndex_Index];
GO
IF OBJECT_ID(N'[dbo].[FK_TrackedFileWorkingSet_TrackedFile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TrackedFileWorkingSet] DROP CONSTRAINT [FK_TrackedFileWorkingSet_TrackedFile];
GO
IF OBJECT_ID(N'[dbo].[FK_TrackedFileWorkingSet_WorkingSet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TrackedFileWorkingSet] DROP CONSTRAINT [FK_TrackedFileWorkingSet_WorkingSet];
GO
IF OBJECT_ID(N'[dbo].[FK_UserWorkingSet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WorkingSets] DROP CONSTRAINT [FK_UserWorkingSet];
GO
IF OBJECT_ID(N'[dbo].[FK_UserShrinkJob]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShrinkJobs] DROP CONSTRAINT [FK_UserShrinkJob];
GO
IF OBJECT_ID(N'[dbo].[FK_UserSearchJob]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SearchJobs] DROP CONSTRAINT [FK_UserSearchJob];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRegExTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RegExTemplates] DROP CONSTRAINT [FK_UserRegExTemplate];
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
IF OBJECT_ID(N'[dbo].[ShrinkJobs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShrinkJobs];
GO
IF OBJECT_ID(N'[dbo].[ReductionLogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReductionLogs];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[WorkingSets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WorkingSets];
GO
IF OBJECT_ID(N'[dbo].[RegExTemplates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RegExTemplates];
GO
IF OBJECT_ID(N'[dbo].[SearchJobTrackedFile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SearchJobTrackedFile];
GO
IF OBJECT_ID(N'[dbo].[CategoryTrackedFile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CategoryTrackedFile];
GO
IF OBJECT_ID(N'[dbo].[TrackedFileShrinkJob]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TrackedFileShrinkJob];
GO
IF OBJECT_ID(N'[dbo].[UserIndex]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserIndex];
GO
IF OBJECT_ID(N'[dbo].[TrackedFileWorkingSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TrackedFileWorkingSet];
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
    [Category_Id] int  NOT NULL,
    [User_Id] int  NOT NULL
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

-- Creating table 'ShrinkJobs'
CREATE TABLE [dbo].[ShrinkJobs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Alias] nvarchar(max)  NOT NULL,
    [Status] int  NOT NULL,
    [ShrinkJpegs] nvarchar(max)  NOT NULL,
    [JpegTarget] int  NOT NULL,
    [ShrinkPdfs] nvarchar(max)  NOT NULL,
    [PdfTarget] int  NOT NULL,
    [PdfJpegTarget] int  NULL,
    [ShrinkWord] bit  NOT NULL,
    [UpgradeWord] nvarchar(max)  NOT NULL,
    [WordJpegTarget] int  NOT NULL,
    [User_Id] int  NOT NULL
);
GO

-- Creating table 'ReductionLogs'
CREATE TABLE [dbo].[ReductionLogs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OldSize] bigint  NOT NULL,
    [NewSize] bigint  NOT NULL,
    [ShrinkJob_Id] int  NOT NULL,
    [TrackedFile_Id] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Firstname] nvarchar(max)  NULL,
    [Surname] nvarchar(max)  NULL,
    [Salt] nvarchar(max)  NOT NULL,
    [Hash] nvarchar(max)  NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [IsSysAdmin] bit  NOT NULL,
    [NewPasswordDue] bit  NOT NULL
);
GO

-- Creating table 'WorkingSets'
CREATE TABLE [dbo].[WorkingSets] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [User_Id] int  NOT NULL
);
GO

-- Creating table 'RegExTemplates'
CREATE TABLE [dbo].[RegExTemplates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Syntax] nvarchar(max)  NOT NULL,
    [User_Id] int  NOT NULL
);
GO

-- Creating table 'SearchJobTrackedFile'
CREATE TABLE [dbo].[SearchJobTrackedFile] (
    [SearchJobs_Id] int  NOT NULL,
    [TrackedFiles_Id] int  NOT NULL
);
GO

-- Creating table 'CategoryTrackedFile'
CREATE TABLE [dbo].[CategoryTrackedFile] (
    [Categories_Id] int  NOT NULL,
    [TrackedFiles_Id] int  NOT NULL
);
GO

-- Creating table 'TrackedFileShrinkJob'
CREATE TABLE [dbo].[TrackedFileShrinkJob] (
    [TrackedFiles_Id] int  NOT NULL,
    [ShrinkJobs_Id] int  NOT NULL
);
GO

-- Creating table 'UserIndex'
CREATE TABLE [dbo].[UserIndex] (
    [Users_Id] int  NOT NULL,
    [Indices_Id] int  NOT NULL
);
GO

-- Creating table 'TrackedFileWorkingSet'
CREATE TABLE [dbo].[TrackedFileWorkingSet] (
    [TrackedFiles_Id] int  NOT NULL,
    [WorkingSets_Id] int  NOT NULL
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

-- Creating primary key on [Id] in table 'ShrinkJobs'
ALTER TABLE [dbo].[ShrinkJobs]
ADD CONSTRAINT [PK_ShrinkJobs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ReductionLogs'
ALTER TABLE [dbo].[ReductionLogs]
ADD CONSTRAINT [PK_ReductionLogs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WorkingSets'
ALTER TABLE [dbo].[WorkingSets]
ADD CONSTRAINT [PK_WorkingSets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RegExTemplates'
ALTER TABLE [dbo].[RegExTemplates]
ADD CONSTRAINT [PK_RegExTemplates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [SearchJobs_Id], [TrackedFiles_Id] in table 'SearchJobTrackedFile'
ALTER TABLE [dbo].[SearchJobTrackedFile]
ADD CONSTRAINT [PK_SearchJobTrackedFile]
    PRIMARY KEY CLUSTERED ([SearchJobs_Id], [TrackedFiles_Id] ASC);
GO

-- Creating primary key on [Categories_Id], [TrackedFiles_Id] in table 'CategoryTrackedFile'
ALTER TABLE [dbo].[CategoryTrackedFile]
ADD CONSTRAINT [PK_CategoryTrackedFile]
    PRIMARY KEY CLUSTERED ([Categories_Id], [TrackedFiles_Id] ASC);
GO

-- Creating primary key on [TrackedFiles_Id], [ShrinkJobs_Id] in table 'TrackedFileShrinkJob'
ALTER TABLE [dbo].[TrackedFileShrinkJob]
ADD CONSTRAINT [PK_TrackedFileShrinkJob]
    PRIMARY KEY CLUSTERED ([TrackedFiles_Id], [ShrinkJobs_Id] ASC);
GO

-- Creating primary key on [Users_Id], [Indices_Id] in table 'UserIndex'
ALTER TABLE [dbo].[UserIndex]
ADD CONSTRAINT [PK_UserIndex]
    PRIMARY KEY CLUSTERED ([Users_Id], [Indices_Id] ASC);
GO

-- Creating primary key on [TrackedFiles_Id], [WorkingSets_Id] in table 'TrackedFileWorkingSet'
ALTER TABLE [dbo].[TrackedFileWorkingSet]
ADD CONSTRAINT [PK_TrackedFileWorkingSet]
    PRIMARY KEY CLUSTERED ([TrackedFiles_Id], [WorkingSets_Id] ASC);
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
    ON DELETE CASCADE ON UPDATE NO ACTION;
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
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TrackedFolderTrackedFile'
CREATE INDEX [IX_FK_TrackedFolderTrackedFile]
ON [dbo].[TrackedFiles]
    ([TrackedFolderId]);
GO

-- Creating foreign key on [SearchJobs_Id] in table 'SearchJobTrackedFile'
ALTER TABLE [dbo].[SearchJobTrackedFile]
ADD CONSTRAINT [FK_SearchJobTrackedFile_SearchJob]
    FOREIGN KEY ([SearchJobs_Id])
    REFERENCES [dbo].[SearchJobs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [TrackedFiles_Id] in table 'SearchJobTrackedFile'
ALTER TABLE [dbo].[SearchJobTrackedFile]
ADD CONSTRAINT [FK_SearchJobTrackedFile_TrackedFile]
    FOREIGN KEY ([TrackedFiles_Id])
    REFERENCES [dbo].[TrackedFiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SearchJobTrackedFile_TrackedFile'
CREATE INDEX [IX_FK_SearchJobTrackedFile_TrackedFile]
ON [dbo].[SearchJobTrackedFile]
    ([TrackedFiles_Id]);
GO

-- Creating foreign key on [Categories_Id] in table 'CategoryTrackedFile'
ALTER TABLE [dbo].[CategoryTrackedFile]
ADD CONSTRAINT [FK_CategoryTrackedFile_Category]
    FOREIGN KEY ([Categories_Id])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [TrackedFiles_Id] in table 'CategoryTrackedFile'
ALTER TABLE [dbo].[CategoryTrackedFile]
ADD CONSTRAINT [FK_CategoryTrackedFile_TrackedFile]
    FOREIGN KEY ([TrackedFiles_Id])
    REFERENCES [dbo].[TrackedFiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryTrackedFile_TrackedFile'
CREATE INDEX [IX_FK_CategoryTrackedFile_TrackedFile]
ON [dbo].[CategoryTrackedFile]
    ([TrackedFiles_Id]);
GO

-- Creating foreign key on [TrackedFiles_Id] in table 'TrackedFileShrinkJob'
ALTER TABLE [dbo].[TrackedFileShrinkJob]
ADD CONSTRAINT [FK_TrackedFileShrinkJob_TrackedFile]
    FOREIGN KEY ([TrackedFiles_Id])
    REFERENCES [dbo].[TrackedFiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ShrinkJobs_Id] in table 'TrackedFileShrinkJob'
ALTER TABLE [dbo].[TrackedFileShrinkJob]
ADD CONSTRAINT [FK_TrackedFileShrinkJob_ShrinkJob]
    FOREIGN KEY ([ShrinkJobs_Id])
    REFERENCES [dbo].[ShrinkJobs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TrackedFileShrinkJob_ShrinkJob'
CREATE INDEX [IX_FK_TrackedFileShrinkJob_ShrinkJob]
ON [dbo].[TrackedFileShrinkJob]
    ([ShrinkJobs_Id]);
GO

-- Creating foreign key on [ShrinkJob_Id] in table 'ReductionLogs'
ALTER TABLE [dbo].[ReductionLogs]
ADD CONSTRAINT [FK_ShrinkJobReductionLog]
    FOREIGN KEY ([ShrinkJob_Id])
    REFERENCES [dbo].[ShrinkJobs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ShrinkJobReductionLog'
CREATE INDEX [IX_FK_ShrinkJobReductionLog]
ON [dbo].[ReductionLogs]
    ([ShrinkJob_Id]);
GO

-- Creating foreign key on [TrackedFile_Id] in table 'ReductionLogs'
ALTER TABLE [dbo].[ReductionLogs]
ADD CONSTRAINT [FK_TrackedFileReductionLog]
    FOREIGN KEY ([TrackedFile_Id])
    REFERENCES [dbo].[TrackedFiles]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TrackedFileReductionLog'
CREATE INDEX [IX_FK_TrackedFileReductionLog]
ON [dbo].[ReductionLogs]
    ([TrackedFile_Id]);
GO

-- Creating foreign key on [Users_Id] in table 'UserIndex'
ALTER TABLE [dbo].[UserIndex]
ADD CONSTRAINT [FK_UserIndex_User]
    FOREIGN KEY ([Users_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Indices_Id] in table 'UserIndex'
ALTER TABLE [dbo].[UserIndex]
ADD CONSTRAINT [FK_UserIndex_Index]
    FOREIGN KEY ([Indices_Id])
    REFERENCES [dbo].[Indices]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserIndex_Index'
CREATE INDEX [IX_FK_UserIndex_Index]
ON [dbo].[UserIndex]
    ([Indices_Id]);
GO

-- Creating foreign key on [TrackedFiles_Id] in table 'TrackedFileWorkingSet'
ALTER TABLE [dbo].[TrackedFileWorkingSet]
ADD CONSTRAINT [FK_TrackedFileWorkingSet_TrackedFile]
    FOREIGN KEY ([TrackedFiles_Id])
    REFERENCES [dbo].[TrackedFiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [WorkingSets_Id] in table 'TrackedFileWorkingSet'
ALTER TABLE [dbo].[TrackedFileWorkingSet]
ADD CONSTRAINT [FK_TrackedFileWorkingSet_WorkingSet]
    FOREIGN KEY ([WorkingSets_Id])
    REFERENCES [dbo].[WorkingSets]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TrackedFileWorkingSet_WorkingSet'
CREATE INDEX [IX_FK_TrackedFileWorkingSet_WorkingSet]
ON [dbo].[TrackedFileWorkingSet]
    ([WorkingSets_Id]);
GO

-- Creating foreign key on [User_Id] in table 'WorkingSets'
ALTER TABLE [dbo].[WorkingSets]
ADD CONSTRAINT [FK_UserWorkingSet]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserWorkingSet'
CREATE INDEX [IX_FK_UserWorkingSet]
ON [dbo].[WorkingSets]
    ([User_Id]);
GO

-- Creating foreign key on [User_Id] in table 'ShrinkJobs'
ALTER TABLE [dbo].[ShrinkJobs]
ADD CONSTRAINT [FK_UserShrinkJob]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserShrinkJob'
CREATE INDEX [IX_FK_UserShrinkJob]
ON [dbo].[ShrinkJobs]
    ([User_Id]);
GO

-- Creating foreign key on [User_Id] in table 'SearchJobs'
ALTER TABLE [dbo].[SearchJobs]
ADD CONSTRAINT [FK_UserSearchJob]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserSearchJob'
CREATE INDEX [IX_FK_UserSearchJob]
ON [dbo].[SearchJobs]
    ([User_Id]);
GO

-- Creating foreign key on [User_Id] in table 'RegExTemplates'
ALTER TABLE [dbo].[RegExTemplates]
ADD CONSTRAINT [FK_UserRegExTemplate]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRegExTemplate'
CREATE INDEX [IX_FK_UserRegExTemplate]
ON [dbo].[RegExTemplates]
    ([User_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------