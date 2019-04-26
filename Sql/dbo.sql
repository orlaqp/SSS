/*
 Navicat Premium Data Transfer

 Source Server         : localdb
 Source Server Type    : SQL Server
 Source Server Version : 13004001
 Source Host           : (localdb)\MSSQLLocalDB:1433
 Source Catalog        : SSS
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 13004001
 File Encoding         : 65001

 Date: 26/04/2019 17:04:19
*/


-- ----------------------------
-- Table structure for Ema
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Ema]') AND type IN ('U'))
	DROP TABLE [dbo].[Ema]
GO

CREATE TABLE [dbo].[Ema] (
  [Id] uniqueidentifier  NOT NULL,
  [instrument] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [createtime] datetime  NULL,
  [ktime] datetime  NULL,
  [yesday_ema] float(53)  NULL,
  [timetype] int  NULL,
  [now_ema] float(53)  NULL,
  [parameter] int  NULL
)
GO

ALTER TABLE [dbo].[Ema] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'主键',
'SCHEMA', N'dbo',
'TABLE', N'Ema',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'交易对',
'SCHEMA', N'dbo',
'TABLE', N'Ema',
'COLUMN', N'instrument'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'dbo',
'TABLE', N'Ema',
'COLUMN', N'createtime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'k线时间',
'SCHEMA', N'dbo',
'TABLE', N'Ema',
'COLUMN', N'ktime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'前ema值',
'SCHEMA', N'dbo',
'TABLE', N'Ema',
'COLUMN', N'yesday_ema'
GO

EXEC sp_addextendedproperty
'MS_Description', N'时间线类型，秒为单位',
'SCHEMA', N'dbo',
'TABLE', N'Ema',
'COLUMN', N'timetype'
GO

EXEC sp_addextendedproperty
'MS_Description', N'当前ema值',
'SCHEMA', N'dbo',
'TABLE', N'Ema',
'COLUMN', N'now_ema'
GO

EXEC sp_addextendedproperty
'MS_Description', N'默认参数为12、26',
'SCHEMA', N'dbo',
'TABLE', N'Ema',
'COLUMN', N'parameter'
GO


-- ----------------------------
-- Table structure for Kdj
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Kdj]') AND type IN ('U'))
	DROP TABLE [dbo].[Kdj]
GO

CREATE TABLE [dbo].[Kdj] (
  [Id] uniqueidentifier  NOT NULL,
  [instrument] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [createtime] datetime  NULL,
  [ktime] datetime  NULL,
  [yesday_d] float(53)  NULL,
  [timetype] int  NULL,
  [yesday_k] float(53)  NULL,
  [k] float(53)  NULL,
  [d] float(53)  NULL,
  [j] float(53)  NULL
)
GO

ALTER TABLE [dbo].[Kdj] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'主键',
'SCHEMA', N'dbo',
'TABLE', N'Kdj',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'交易对',
'SCHEMA', N'dbo',
'TABLE', N'Kdj',
'COLUMN', N'instrument'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'dbo',
'TABLE', N'Kdj',
'COLUMN', N'createtime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'k线时间',
'SCHEMA', N'dbo',
'TABLE', N'Kdj',
'COLUMN', N'ktime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'前d值',
'SCHEMA', N'dbo',
'TABLE', N'Kdj',
'COLUMN', N'yesday_d'
GO

EXEC sp_addextendedproperty
'MS_Description', N'时间线类型，秒为单位',
'SCHEMA', N'dbo',
'TABLE', N'Kdj',
'COLUMN', N'timetype'
GO

EXEC sp_addextendedproperty
'MS_Description', N'前k值',
'SCHEMA', N'dbo',
'TABLE', N'Kdj',
'COLUMN', N'yesday_k'
GO

EXEC sp_addextendedproperty
'MS_Description', N'k值',
'SCHEMA', N'dbo',
'TABLE', N'Kdj',
'COLUMN', N'k'
GO

EXEC sp_addextendedproperty
'MS_Description', N'd值',
'SCHEMA', N'dbo',
'TABLE', N'Kdj',
'COLUMN', N'd'
GO

EXEC sp_addextendedproperty
'MS_Description', N'j值',
'SCHEMA', N'dbo',
'TABLE', N'Kdj',
'COLUMN', N'j'
GO


-- ----------------------------
-- Table structure for Ma
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Ma]') AND type IN ('U'))
	DROP TABLE [dbo].[Ma]
GO

CREATE TABLE [dbo].[Ma] (
  [Id] uniqueidentifier  NOT NULL,
  [instrument] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [createtime] datetime  NULL,
  [ktime] datetime  NULL,
  [now_ma] float(53)  NULL,
  [type] int  NULL,
  [parameter] int  NULL,
  [timetype] int  NULL
)
GO

ALTER TABLE [dbo].[Ma] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'主键',
'SCHEMA', N'dbo',
'TABLE', N'Ma',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'交易对',
'SCHEMA', N'dbo',
'TABLE', N'Ma',
'COLUMN', N'instrument'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'dbo',
'TABLE', N'Ma',
'COLUMN', N'createtime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'k线时间',
'SCHEMA', N'dbo',
'TABLE', N'Ma',
'COLUMN', N'ktime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'当前值',
'SCHEMA', N'dbo',
'TABLE', N'Ma',
'COLUMN', N'now_ma'
GO

EXEC sp_addextendedproperty
'MS_Description', N'1代表均量线 2代表均价线',
'SCHEMA', N'dbo',
'TABLE', N'Ma',
'COLUMN', N'type'
GO

EXEC sp_addextendedproperty
'MS_Description', N'默认参数为5、10',
'SCHEMA', N'dbo',
'TABLE', N'Ma',
'COLUMN', N'parameter'
GO

EXEC sp_addextendedproperty
'MS_Description', N'时间类型，秒为单位',
'SCHEMA', N'dbo',
'TABLE', N'Ma',
'COLUMN', N'timetype'
GO


-- ----------------------------
-- Table structure for Macd
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Macd]') AND type IN ('U'))
	DROP TABLE [dbo].[Macd]
GO

CREATE TABLE [dbo].[Macd] (
  [Id] uniqueidentifier  NOT NULL,
  [instrument] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [timetype] int  NULL,
  [createtime] datetime  NULL,
  [ktime] datetime  NULL,
  [ema12] float(53)  NULL,
  [ema26] float(53)  NULL,
  [dif] float(53)  NULL,
  [dea] float(53)  NULL,
  [macd] float(53)  NULL,
  [yesday_dea] float(53)  NULL,
  [yesday_ema12] float(53)  NULL,
  [yesday_ema26] float(53)  NULL
)
GO

ALTER TABLE [dbo].[Macd] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'主键',
'SCHEMA', N'dbo',
'TABLE', N'Macd',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'交易对',
'SCHEMA', N'dbo',
'TABLE', N'Macd',
'COLUMN', N'instrument'
GO

EXEC sp_addextendedproperty
'MS_Description', N'时间线类型，秒为单位',
'SCHEMA', N'dbo',
'TABLE', N'Macd',
'COLUMN', N'timetype'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'dbo',
'TABLE', N'Macd',
'COLUMN', N'createtime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'k线时间',
'SCHEMA', N'dbo',
'TABLE', N'Macd',
'COLUMN', N'ktime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'ema12值',
'SCHEMA', N'dbo',
'TABLE', N'Macd',
'COLUMN', N'ema12'
GO

EXEC sp_addextendedproperty
'MS_Description', N'ema26值',
'SCHEMA', N'dbo',
'TABLE', N'Macd',
'COLUMN', N'ema26'
GO

EXEC sp_addextendedproperty
'MS_Description', N'dif值',
'SCHEMA', N'dbo',
'TABLE', N'Macd',
'COLUMN', N'dif'
GO

EXEC sp_addextendedproperty
'MS_Description', N'dea值',
'SCHEMA', N'dbo',
'TABLE', N'Macd',
'COLUMN', N'dea'
GO

EXEC sp_addextendedproperty
'MS_Description', N'macd值',
'SCHEMA', N'dbo',
'TABLE', N'Macd',
'COLUMN', N'macd'
GO

EXEC sp_addextendedproperty
'MS_Description', N'前dea值',
'SCHEMA', N'dbo',
'TABLE', N'Macd',
'COLUMN', N'yesday_dea'
GO

EXEC sp_addextendedproperty
'MS_Description', N'前ema12值',
'SCHEMA', N'dbo',
'TABLE', N'Macd',
'COLUMN', N'yesday_ema12'
GO

EXEC sp_addextendedproperty
'MS_Description', N'前ema26值',
'SCHEMA', N'dbo',
'TABLE', N'Macd',
'COLUMN', N'yesday_ema26'
GO


-- ----------------------------
-- Table structure for Student
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Student]') AND type IN ('U'))
	DROP TABLE [dbo].[Student]
GO

CREATE TABLE [dbo].[Student] (
  [Name] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Age] int  NOT NULL,
  [Id] uniqueidentifier  NOT NULL
)
GO

ALTER TABLE [dbo].[Student] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Indexes structure for table Ema
-- ----------------------------
CREATE NONCLUSTERED INDEX [instrument]
ON [dbo].[Ema] (
  [instrument] ASC
)
GO

CREATE NONCLUSTERED INDEX [ktime]
ON [dbo].[Ema] (
  [ktime] ASC
)
GO

CREATE NONCLUSTERED INDEX [timetype]
ON [dbo].[Ema] (
  [timetype] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table Ema
-- ----------------------------
ALTER TABLE [dbo].[Ema] ADD CONSTRAINT [PK__Ema__3214EC077EE1396B] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table Kdj
-- ----------------------------
CREATE NONCLUSTERED INDEX [instrument]
ON [dbo].[Kdj] (
  [instrument] ASC
)
GO

CREATE NONCLUSTERED INDEX [ktime]
ON [dbo].[Kdj] (
  [ktime] ASC
)
GO

CREATE NONCLUSTERED INDEX [timetype]
ON [dbo].[Kdj] (
  [timetype] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table Kdj
-- ----------------------------
ALTER TABLE [dbo].[Kdj] ADD CONSTRAINT [PK__Ema__3214EC077EE1396B_copy1] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table Ma
-- ----------------------------
CREATE NONCLUSTERED INDEX [instrument]
ON [dbo].[Ma] (
  [instrument] ASC
)
GO

CREATE NONCLUSTERED INDEX [ktime]
ON [dbo].[Ma] (
  [ktime] ASC
)
GO

CREATE NONCLUSTERED INDEX [type]
ON [dbo].[Ma] (
  [type] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table Ma
-- ----------------------------
ALTER TABLE [dbo].[Ma] ADD CONSTRAINT [PK__Ma__3214EC07C1BB3787] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table Macd
-- ----------------------------
CREATE NONCLUSTERED INDEX [instrument]
ON [dbo].[Macd] (
  [instrument] ASC
)
GO

CREATE NONCLUSTERED INDEX [ktime]
ON [dbo].[Macd] (
  [ktime] ASC
)
GO

CREATE NONCLUSTERED INDEX [timetype]
ON [dbo].[Macd] (
  [timetype] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table Macd
-- ----------------------------
ALTER TABLE [dbo].[Macd] ADD CONSTRAINT [PK__Macd__3214EC07C7616207] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Student
-- ----------------------------
ALTER TABLE [dbo].[Student] ADD CONSTRAINT [PK__Student__3214EC07651C1A2E] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

