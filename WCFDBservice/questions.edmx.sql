
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/19/2020 22:32:32
-- Generated from EDMX file: D:\zhubin\code\Exercise_plaf\WCFDBservice\questions.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [db_exercise];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_exerL_Course]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[exerL] DROP CONSTRAINT [FK_exerL_Course];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AQues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AQues];
GO
IF OBJECT_ID(N'[dbo].[class_student]', 'U') IS NOT NULL
    DROP TABLE [dbo].[class_student];
GO
IF OBJECT_ID(N'[dbo].[classExer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[classExer];
GO
IF OBJECT_ID(N'[dbo].[classinfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[classinfo];
GO
IF OBJECT_ID(N'[dbo].[Course]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Course];
GO
IF OBJECT_ID(N'[dbo].[exerDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[exerDetail];
GO
IF OBJECT_ID(N'[dbo].[exerL]', 'U') IS NOT NULL
    DROP TABLE [dbo].[exerL];
GO
IF OBJECT_ID(N'[dbo].[mchoiceQues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[mchoiceQues];
GO
IF OBJECT_ID(N'[dbo].[SQues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SQues];
GO
IF OBJECT_ID(N'[dbo].[studAnsw]', 'U') IS NOT NULL
    DROP TABLE [dbo].[studAnsw];
GO
IF OBJECT_ID(N'[dbo].[StudInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StudInfo];
GO
IF OBJECT_ID(N'[dbo].[teacherinfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[teacherinfo];
GO
IF OBJECT_ID(N'[dbo].[tech_course]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tech_course];
GO
IF OBJECT_ID(N'[dbo].[TFQues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TFQues];
GO
IF OBJECT_ID(N'[db_exerciseModelStoreContainer].[V_tea_course]', 'U') IS NOT NULL
    DROP TABLE [db_exerciseModelStoreContainer].[V_tea_course];
GO
IF OBJECT_ID(N'[db_exerciseModelStoreContainer].[View_detai_exerL]', 'U') IS NOT NULL
    DROP TABLE [db_exerciseModelStoreContainer].[View_detai_exerL];
GO
IF OBJECT_ID(N'[db_exerciseModelStoreContainer].[View_student]', 'U') IS NOT NULL
    DROP TABLE [db_exerciseModelStoreContainer].[View_student];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'mchoiceQues'
CREATE TABLE [dbo].[mchoiceQues] (
    [id] int IDENTITY(1,1) NOT NULL,
    [con] int  NULL,
    [objective] int  NULL,
    [diff] int  NULL,
    [usenum] int  NULL,
    [errornum] int  NULL,
    [question] varbinary(max)  NULL,
    [courseid] int  NULL,
    [answ] int  NULL,
    [teacherid] nchar(4)  NULL
);
GO

-- Creating table 'classinfo'
CREATE TABLE [dbo].[classinfo] (
    [classid] int  NOT NULL,
    [classinfo1] varchar(100)  NULL,
    [courseid] int  NULL,
    [teacher] nchar(4)  NULL,
    [addtime] datetime  NULL,
    [finish] int  NULL
);
GO

-- Creating table 'Course'
CREATE TABLE [dbo].[Course] (
    [id] int IDENTITY(1,1) NOT NULL,
    [CourseName] varchar(50)  NOT NULL,
    [Courseid] varchar(50)  NULL,
    [numobjective] int  NOT NULL,
    [numcontent] int  NOT NULL,
    [diff] int  NOT NULL
);
GO

-- Creating table 'teacherinfo'
CREATE TABLE [dbo].[teacherinfo] (
    [teacherid] nchar(4)  NOT NULL,
    [name] nchar(10)  NULL,
    [pd] nchar(10)  NULL
);
GO

-- Creating table 'TFQues'
CREATE TABLE [dbo].[TFQues] (
    [id] int IDENTITY(1,1) NOT NULL,
    [teacherid] nchar(4)  NULL,
    [courseid] int  NULL,
    [con] int  NULL,
    [objective] int  NULL,
    [diff] int  NULL,
    [usenum] int  NULL,
    [errornum] int  NULL,
    [question] varbinary(max)  NULL,
    [answ] bit  NULL
);
GO

-- Creating table 'SQues'
CREATE TABLE [dbo].[SQues] (
    [id] int IDENTITY(1,1) NOT NULL,
    [teacherid] nchar(4)  NULL,
    [courseid] int  NULL,
    [con] int  NULL,
    [objective] int  NULL,
    [diff] int  NULL,
    [usenum] int  NULL,
    [errornum] int  NULL,
    [question] varbinary(max)  NULL,
    [answ] varbinary(max)  NULL
);
GO

-- Creating table 'AQues'
CREATE TABLE [dbo].[AQues] (
    [id] int IDENTITY(1,1) NOT NULL,
    [teacherid] nchar(4)  NULL,
    [courseid] int  NULL,
    [con] int  NULL,
    [objective] int  NULL,
    [diff] int  NULL,
    [usenum] int  NULL,
    [errornum] int  NULL,
    [question] varbinary(max)  NULL,
    [answ] varbinary(max)  NULL
);
GO

-- Creating table 'classExer'
CREATE TABLE [dbo].[classExer] (
    [cid] int  NOT NULL,
    [eid] int  NOT NULL,
    [starttime] datetime  NULL,
    [endtime] datetime  NULL,
    [dispaly] int  NULL
);
GO

-- Creating table 'exerL'
CREATE TABLE [dbo].[exerL] (
    [id] int IDENTITY(1,1) NOT NULL,
    [teacherid] nchar(4)  NOT NULL,
    [name] varchar(50)  NOT NULL,
    [courseid] int  NOT NULL,
    [pub] bit  NOT NULL
);
GO

-- Creating table 'class_student'
CREATE TABLE [dbo].[class_student] (
    [classid] int  NOT NULL,
    [studentid] varchar(11)  NOT NULL,
    [classno] int  NULL
);
GO

-- Creating table 'StudInfoes'
CREATE TABLE [dbo].[StudInfoes] (
    [studentid] nvarchar(11)  NOT NULL,
    [name] varchar(50)  NULL,
    [pd] varchar(8)  NULL
);
GO

-- Creating table 'exerDetail'
CREATE TABLE [dbo].[exerDetail] (
    [typeq] int  NOT NULL,
    [qid] int  NOT NULL,
    [lid] int  NOT NULL,
    [score] int  NULL,
    [id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'studAnsw'
CREATE TABLE [dbo].[studAnsw] (
    [stid] nchar(11)  NOT NULL,
    [did] int  NOT NULL,
    [answ1] int  NULL,
    [answ2] bit  NULL,
    [answ3] varbinary(max)  NULL,
    [mark] int  NOT NULL,
    [lid] int  NOT NULL
);
GO

-- Creating table 'View_student'
CREATE TABLE [dbo].[View_student] (
    [stid] nvarchar(11)  NOT NULL,
    [stname] varchar(50)  NULL,
    [cno] int  NULL,
    [cfinish] int  NULL,
    [cid] int  NOT NULL
);
GO

-- Creating table 'View_detai_exerL'
CREATE TABLE [dbo].[View_detai_exerL] (
    [id] int  NOT NULL,
    [typeq] int  NOT NULL,
    [qid] int  NOT NULL,
    [Expr1] int  NOT NULL,
    [score] int  NULL,
    [lid] int  NOT NULL
);
GO

-- Creating table 'V_tea_course'
CREATE TABLE [dbo].[V_tea_course] (
    [teacherid] nchar(4)  NOT NULL,
    [name] nchar(10)  NULL,
    [pd] nchar(10)  NULL,
    [couseid] int  NOT NULL,
    [relid] int  NOT NULL,
    [CourseName] varchar(50)  NOT NULL,
    [numobjective] int  NOT NULL,
    [numcontent] int  NOT NULL,
    [diff] int  NOT NULL
);
GO

-- Creating table 'tech_course'
CREATE TABLE [dbo].[tech_course] (
    [id] int IDENTITY(1,1) NOT NULL,
    [tid] nchar(4)  NOT NULL,
    [couseid] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'mchoiceQues'
ALTER TABLE [dbo].[mchoiceQues]
ADD CONSTRAINT [PK_mchoiceQues]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [classid] in table 'classinfo'
ALTER TABLE [dbo].[classinfo]
ADD CONSTRAINT [PK_classinfo]
    PRIMARY KEY CLUSTERED ([classid] ASC);
GO

-- Creating primary key on [id] in table 'Course'
ALTER TABLE [dbo].[Course]
ADD CONSTRAINT [PK_Course]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [teacherid] in table 'teacherinfo'
ALTER TABLE [dbo].[teacherinfo]
ADD CONSTRAINT [PK_teacherinfo]
    PRIMARY KEY CLUSTERED ([teacherid] ASC);
GO

-- Creating primary key on [id] in table 'TFQues'
ALTER TABLE [dbo].[TFQues]
ADD CONSTRAINT [PK_TFQues]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'SQues'
ALTER TABLE [dbo].[SQues]
ADD CONSTRAINT [PK_SQues]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'AQues'
ALTER TABLE [dbo].[AQues]
ADD CONSTRAINT [PK_AQues]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [cid], [eid] in table 'classExer'
ALTER TABLE [dbo].[classExer]
ADD CONSTRAINT [PK_classExer]
    PRIMARY KEY CLUSTERED ([cid], [eid] ASC);
GO

-- Creating primary key on [id] in table 'exerL'
ALTER TABLE [dbo].[exerL]
ADD CONSTRAINT [PK_exerL]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [classid], [studentid] in table 'class_student'
ALTER TABLE [dbo].[class_student]
ADD CONSTRAINT [PK_class_student]
    PRIMARY KEY CLUSTERED ([classid], [studentid] ASC);
GO

-- Creating primary key on [studentid] in table 'StudInfoes'
ALTER TABLE [dbo].[StudInfoes]
ADD CONSTRAINT [PK_StudInfoes]
    PRIMARY KEY CLUSTERED ([studentid] ASC);
GO

-- Creating primary key on [id] in table 'exerDetail'
ALTER TABLE [dbo].[exerDetail]
ADD CONSTRAINT [PK_exerDetail]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [stid], [did], [lid] in table 'studAnsw'
ALTER TABLE [dbo].[studAnsw]
ADD CONSTRAINT [PK_studAnsw]
    PRIMARY KEY CLUSTERED ([stid], [did], [lid] ASC);
GO

-- Creating primary key on [stid], [cid] in table 'View_student'
ALTER TABLE [dbo].[View_student]
ADD CONSTRAINT [PK_View_student]
    PRIMARY KEY CLUSTERED ([stid], [cid] ASC);
GO

-- Creating primary key on [id], [typeq], [qid], [Expr1], [lid] in table 'View_detai_exerL'
ALTER TABLE [dbo].[View_detai_exerL]
ADD CONSTRAINT [PK_View_detai_exerL]
    PRIMARY KEY CLUSTERED ([id], [typeq], [qid], [Expr1], [lid] ASC);
GO

-- Creating primary key on [teacherid], [couseid], [relid], [CourseName], [numobjective], [numcontent], [diff] in table 'V_tea_course'
ALTER TABLE [dbo].[V_tea_course]
ADD CONSTRAINT [PK_V_tea_course]
    PRIMARY KEY CLUSTERED ([teacherid], [couseid], [relid], [CourseName], [numobjective], [numcontent], [diff] ASC);
GO

-- Creating primary key on [id] in table 'tech_course'
ALTER TABLE [dbo].[tech_course]
ADD CONSTRAINT [PK_tech_course]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [courseid] in table 'exerL'
ALTER TABLE [dbo].[exerL]
ADD CONSTRAINT [FK_exerL_Course]
    FOREIGN KEY ([courseid])
    REFERENCES [dbo].[Course]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_exerL_Course'
CREATE INDEX [IX_FK_exerL_Course]
ON [dbo].[exerL]
    ([courseid]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------