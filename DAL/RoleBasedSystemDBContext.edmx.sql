
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/07/2015 10:45:38
-- Generated from EDMX file: C:\Art\RoleBasedRateSystemSolution\DAL\RoleBasedSystemDBContext.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [RoleBasedSystem];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dbo_EmployeeProjects_dbo_Employees_EmployeeId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeProjects] DROP CONSTRAINT [FK_dbo_EmployeeProjects_dbo_Employees_EmployeeId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_EmployeeProjects_dbo_ProjectRoles_ProjectRoleId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeProjects] DROP CONSTRAINT [FK_dbo_EmployeeProjects_dbo_ProjectRoles_ProjectRoleId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_EmployeeProjects_dbo_Projects_ProjectId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeProjects] DROP CONSTRAINT [FK_dbo_EmployeeProjects_dbo_Projects_ProjectId];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Departments];
GO
IF OBJECT_ID(N'[dbo].[EmployeeProjects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeProjects];
GO
IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[ProjectRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectRoles];
GO
IF OBJECT_ID(N'[dbo].[Projects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Projects];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'DepartmentRoles'
CREATE TABLE [dbo].[DepartmentRoles] (
    [DepartmentRoleId] int  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Description] nvarchar(500)  NOT NULL,
    [DateTimeCreated] datetime  NOT NULL,
    [RatePerHour] decimal(19,4)  NOT NULL
);
GO

-- Creating table 'Departments'
CREATE TABLE [dbo].[Departments] (
    [DepartmentId] int  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [MailStop] nvarchar(max)  NOT NULL,
    [DateTimeCreated] datetime  NOT NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [EmployeeId] int  NOT NULL,
    [FirstName] nvarchar(50)  NOT NULL,
    [LastName] nvarchar(50)  NOT NULL,
    [Address] nvarchar(100)  NULL,
    [Phone] nchar(24)  NULL,
    [DateTimeCreated] datetime  NULL,
    [DepartmentRoleDepartmentRoleId] int  NOT NULL,
    [DepartmentDepartmentId] int  NOT NULL
);
GO

-- Creating table 'ProjectRoles'
CREATE TABLE [dbo].[ProjectRoles] (
    [ProjectRoleId] int  NOT NULL,
    [Name] nvarchar(500)  NOT NULL,
    [Description] nvarchar(40)  NOT NULL,
    [DateTimeCreated] datetime  NOT NULL
);
GO

-- Creating table 'Projects'
CREATE TABLE [dbo].[Projects] (
    [ProjectId] int  NOT NULL,
    [Name] nvarchar(40)  NOT NULL,
    [Description] nvarchar(500)  NOT NULL,
    [DateTimeCreated] binary(8)  NOT NULL
);
GO

-- Creating table 'EmployeeProjects1'
CREATE TABLE [dbo].[EmployeeProjects1] (
    [EmployeeId] int  NOT NULL,
    [ProjectId] int  NOT NULL,
    [ProjectRoleId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [DepartmentRoleId] in table 'DepartmentRoles'
ALTER TABLE [dbo].[DepartmentRoles]
ADD CONSTRAINT [PK_DepartmentRoles]
    PRIMARY KEY CLUSTERED ([DepartmentRoleId] ASC);
GO

-- Creating primary key on [DepartmentId] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [PK_Departments]
    PRIMARY KEY CLUSTERED ([DepartmentId] ASC);
GO

-- Creating primary key on [EmployeeId] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([EmployeeId] ASC);
GO

-- Creating primary key on [ProjectRoleId] in table 'ProjectRoles'
ALTER TABLE [dbo].[ProjectRoles]
ADD CONSTRAINT [PK_ProjectRoles]
    PRIMARY KEY CLUSTERED ([ProjectRoleId] ASC);
GO

-- Creating primary key on [ProjectId] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [PK_Projects]
    PRIMARY KEY CLUSTERED ([ProjectId] ASC);
GO

-- Creating primary key on [EmployeeId], [ProjectId], [ProjectRoleId] in table 'EmployeeProjects1'
ALTER TABLE [dbo].[EmployeeProjects1]
ADD CONSTRAINT [PK_EmployeeProjects1]
    PRIMARY KEY CLUSTERED ([EmployeeId], [ProjectId], [ProjectRoleId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DepartmentRoleDepartmentRoleId] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_DepartmentRoleEmployee]
    FOREIGN KEY ([DepartmentRoleDepartmentRoleId])
    REFERENCES [dbo].[DepartmentRoles]
        ([DepartmentRoleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DepartmentRoleEmployee'
CREATE INDEX [IX_FK_DepartmentRoleEmployee]
ON [dbo].[Employees]
    ([DepartmentRoleDepartmentRoleId]);
GO

-- Creating foreign key on [DepartmentDepartmentId] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_DepartmentEmployee]
    FOREIGN KEY ([DepartmentDepartmentId])
    REFERENCES [dbo].[Departments]
        ([DepartmentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DepartmentEmployee'
CREATE INDEX [IX_FK_DepartmentEmployee]
ON [dbo].[Employees]
    ([DepartmentDepartmentId]);
GO

-- Creating foreign key on [EmployeeId] in table 'EmployeeProjects1'
ALTER TABLE [dbo].[EmployeeProjects1]
ADD CONSTRAINT [FK_dbo_EmployeeProjects_dbo_Employees_EmployeeId]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProjectRoleId] in table 'EmployeeProjects1'
ALTER TABLE [dbo].[EmployeeProjects1]
ADD CONSTRAINT [FK_dbo_EmployeeProjects_dbo_ProjectRoles_ProjectRoleId]
    FOREIGN KEY ([ProjectRoleId])
    REFERENCES [dbo].[ProjectRoles]
        ([ProjectRoleId])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_EmployeeProjects_dbo_ProjectRoles_ProjectRoleId'
CREATE INDEX [IX_FK_dbo_EmployeeProjects_dbo_ProjectRoles_ProjectRoleId]
ON [dbo].[EmployeeProjects1]
    ([ProjectRoleId]);
GO

-- Creating foreign key on [ProjectId] in table 'EmployeeProjects1'
ALTER TABLE [dbo].[EmployeeProjects1]
ADD CONSTRAINT [FK_dbo_EmployeeProjects_dbo_Projects_ProjectId]
    FOREIGN KEY ([ProjectId])
    REFERENCES [dbo].[Projects]
        ([ProjectId])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_EmployeeProjects_dbo_Projects_ProjectId'
CREATE INDEX [IX_FK_dbo_EmployeeProjects_dbo_Projects_ProjectId]
ON [dbo].[EmployeeProjects1]
    ([ProjectId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------