USE [master]
GO
/****** Object:  Database [Concept]    Script Date: 12/6/2013 2:15:38 PM ******/
CREATE DATABASE [Concept]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Concept', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Concept.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Concept_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Concept_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Concept] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Concept].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Concept] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Concept] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Concept] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Concept] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Concept] SET ARITHABORT OFF 
GO
ALTER DATABASE [Concept] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Concept] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Concept] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Concept] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Concept] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Concept] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Concept] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Concept] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Concept] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Concept] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Concept] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Concept] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Concept] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Concept] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Concept] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Concept] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Concept] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Concept] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Concept] SET RECOVERY FULL 
GO
ALTER DATABASE [Concept] SET  MULTI_USER 
GO
ALTER DATABASE [Concept] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Concept] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Concept] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Concept] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Concept', N'ON'
GO
USE [Concept]
GO
/****** Object:  Table [dbo].[ConceptAll]    Script Date: 12/6/2013 2:15:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ConceptAll](
	[ConceptID] [varchar](10) NOT NULL,
	[ConceptName] [nvarchar](50) NULL,
 CONSTRAINT [PK_ConceptAll] PRIMARY KEY CLUSTERED 
(
	[ConceptID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ConceptsForTopic]    Script Date: 12/6/2013 2:15:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ConceptsForTopic](
	[ConceptID] [varchar](10) NOT NULL,
	[TopicID] [varchar](10) NOT NULL,
	[Question] [nvarchar](50) NULL,
	[Levels] [varchar](50) NULL,
 CONSTRAINT [PK_ConceptsForTopic] PRIMARY KEY CLUSTERED 
(
	[ConceptID] ASC,
	[TopicID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Level]    Script Date: 12/6/2013 2:15:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Level](
	[LevelID] [varchar](10) NOT NULL,
	[LevelName] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Level] PRIMARY KEY CLUSTERED 
(
	[LevelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Link]    Script Date: 12/6/2013 2:15:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Link](
	[LinkID] [varchar](10) NOT NULL,
	[ConceptID1] [varchar](10) NOT NULL,
	[ConceptID2] [varchar](10) NOT NULL,
	[Text] [nvarchar](50) NULL,
	[Result] [bit] NULL,
 CONSTRAINT [PK_Link] PRIMARY KEY CLUSTERED 
(
	[LinkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LinkOfMap]    Script Date: 12/6/2013 2:15:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LinkOfMap](
	[MapID] [varchar](10) NOT NULL,
	[LinkID] [varchar](10) NOT NULL,
	[Stt] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_LinkOfMap] PRIMARY KEY CLUSTERED 
(
	[MapID] ASC,
	[LinkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MapOfUser]    Script Date: 12/6/2013 2:15:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MapOfUser](
	[MapID] [varchar](10) NOT NULL,
	[MapName] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MapOfUser3] PRIMARY KEY CLUSTERED 
(
	[MapID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Topic]    Script Date: 12/6/2013 2:15:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Topic](
	[TopicID] [varchar](10) NOT NULL,
	[ToppicName] [nvarchar](50) NULL,
	[TopicImage] [varchar](50) NULL,
 CONSTRAINT [PK_Topic] PRIMARY KEY CLUSTERED 
(
	[TopicID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TopicOfLevel]    Script Date: 12/6/2013 2:15:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TopicOfLevel](
	[TopicID] [varchar](10) NOT NULL,
	[LevelID] [varchar](10) NOT NULL,
	[Stt] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_TopicOfLevel] PRIMARY KEY CLUSTERED 
(
	[TopicID] ASC,
	[LevelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 12/6/2013 2:15:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserName] [nvarchar](50) NOT NULL,
	[Pass] [nvarchar](200) NULL,
	[PassSalt] [nvarchar](200) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[ConceptAll] ([ConceptID], [ConceptName]) VALUES (N'1', N'Toán')
INSERT [dbo].[ConceptAll] ([ConceptID], [ConceptName]) VALUES (N'10', N'Quần thể')
INSERT [dbo].[ConceptAll] ([ConceptID], [ConceptName]) VALUES (N'11', N'Tổ chức')
INSERT [dbo].[ConceptAll] ([ConceptID], [ConceptName]) VALUES (N'2', N'Đại số')
INSERT [dbo].[ConceptAll] ([ConceptID], [ConceptName]) VALUES (N'3', N'Hình học')
INSERT [dbo].[ConceptAll] ([ConceptID], [ConceptName]) VALUES (N'4', N'Tích Phân')
INSERT [dbo].[ConceptAll] ([ConceptID], [ConceptName]) VALUES (N'5', N'Đạo Hàm')
INSERT [dbo].[ConceptAll] ([ConceptID], [ConceptName]) VALUES (N'6', N'Hình 3D')
INSERT [dbo].[ConceptAll] ([ConceptID], [ConceptName]) VALUES (N'7', N'Hình 2D')
INSERT [dbo].[ConceptAll] ([ConceptID], [ConceptName]) VALUES (N'8', N'Xã hội')
INSERT [dbo].[ConceptAll] ([ConceptID], [ConceptName]) VALUES (N'9', N'Con người')
INSERT [dbo].[ConceptsForTopic] ([ConceptID], [TopicID], [Question], [Levels]) VALUES (N'1', N'TP3', N'Toán học là gì', N'LV2,LV3,LV4,LV5')
INSERT [dbo].[ConceptsForTopic] ([ConceptID], [TopicID], [Question], [Levels]) VALUES (N'2', N'TP3', N'Đại số là gì', N'LV2,LV3,LV4,LV5')
INSERT [dbo].[ConceptsForTopic] ([ConceptID], [TopicID], [Question], [Levels]) VALUES (N'3', N'TP3', N'Hình học là gì', N'LV3,LV4,LV5')
INSERT [dbo].[ConceptsForTopic] ([ConceptID], [TopicID], [Question], [Levels]) VALUES (N'4', N'TP3', N'Thế nào là tích phân', N'LV4,LV5')
INSERT [dbo].[ConceptsForTopic] ([ConceptID], [TopicID], [Question], [Levels]) VALUES (N'5', N'TP3', N'Thế nào là đạo hàm', N'LV5')
INSERT [dbo].[ConceptsForTopic] ([ConceptID], [TopicID], [Question], [Levels]) VALUES (N'6', N'TP3', N'Hình học 3D', N'LV4,LV5')
INSERT [dbo].[ConceptsForTopic] ([ConceptID], [TopicID], [Question], [Levels]) VALUES (N'7', N'TP3', N'Hình học 2D', N'3,4,5')
INSERT [dbo].[Level] ([LevelID], [LevelName], [Description]) VALUES (N'LV1', N'Mầm Non', N'Mầm non')
INSERT [dbo].[Level] ([LevelID], [LevelName], [Description]) VALUES (N'LV2', N'Cấp 1', N'Cấp 1')
INSERT [dbo].[Level] ([LevelID], [LevelName], [Description]) VALUES (N'LV3', N'Cấp 2', N'Cấp 2')
INSERT [dbo].[Level] ([LevelID], [LevelName], [Description]) VALUES (N'LV4', N'Cấp 3', N'Cấp 3')
INSERT [dbo].[Level] ([LevelID], [LevelName], [Description]) VALUES (N'LV5', N'Đại học', N'Đại học')
INSERT [dbo].[Level] ([LevelID], [LevelName], [Description]) VALUES (N'LV6', N'Khác', N'#')
INSERT [dbo].[Link] ([LinkID], [ConceptID1], [ConceptID2], [Text], [Result]) VALUES (N'1', N'1', N'2', N'Gồm', NULL)
INSERT [dbo].[Link] ([LinkID], [ConceptID1], [ConceptID2], [Text], [Result]) VALUES (N'2', N'1', N'3', N'Gồm', NULL)
INSERT [dbo].[Link] ([LinkID], [ConceptID1], [ConceptID2], [Text], [Result]) VALUES (N'3', N'2', N'4', N'có', NULL)
INSERT [dbo].[Link] ([LinkID], [ConceptID1], [ConceptID2], [Text], [Result]) VALUES (N'4', N'2', N'5', N'có', NULL)
INSERT [dbo].[Link] ([LinkID], [ConceptID1], [ConceptID2], [Text], [Result]) VALUES (N'5', N'3', N'6', N'có', NULL)
INSERT [dbo].[Link] ([LinkID], [ConceptID1], [ConceptID2], [Text], [Result]) VALUES (N'6', N'3', N'7', N'có', NULL)
INSERT [dbo].[Link] ([LinkID], [ConceptID1], [ConceptID2], [Text], [Result]) VALUES (N'7', N'8', N'10', N'bao gồm', NULL)
INSERT [dbo].[Link] ([LinkID], [ConceptID1], [ConceptID2], [Text], [Result]) VALUES (N'8', N'8', N'11', N'bao gồm', NULL)
INSERT [dbo].[Link] ([LinkID], [ConceptID1], [ConceptID2], [Text], [Result]) VALUES (N'9', N'10', N'9', N'có', NULL)
INSERT [dbo].[Topic] ([TopicID], [ToppicName], [TopicImage]) VALUES (N'TP1', N'Gia Đình', N'giadinh.jpg')
INSERT [dbo].[Topic] ([TopicID], [ToppicName], [TopicImage]) VALUES (N'TP2', N'Văn Hóa', N'vanhoa.jpg')
INSERT [dbo].[Topic] ([TopicID], [ToppicName], [TopicImage]) VALUES (N'TP3', N'Toán Học', NULL)
INSERT [dbo].[Topic] ([TopicID], [ToppicName], [TopicImage]) VALUES (N'TP4', N'Xã Hội', NULL)
INSERT [dbo].[Topic] ([TopicID], [ToppicName], [TopicImage]) VALUES (N'TP5', N'Sinh Học', N'sinhhoc.jpg')
INSERT [dbo].[Topic] ([TopicID], [ToppicName], [TopicImage]) VALUES (N'TP6', N'Vật Lý', N'vatli.jpg')
SET IDENTITY_INSERT [dbo].[TopicOfLevel] ON 

INSERT [dbo].[TopicOfLevel] ([TopicID], [LevelID], [Stt]) VALUES (N'TP1', N'LV1', 1)
INSERT [dbo].[TopicOfLevel] ([TopicID], [LevelID], [Stt]) VALUES (N'TP1', N'LV2', 2)
INSERT [dbo].[TopicOfLevel] ([TopicID], [LevelID], [Stt]) VALUES (N'TP1', N'LV3', 3)
INSERT [dbo].[TopicOfLevel] ([TopicID], [LevelID], [Stt]) VALUES (N'TP1', N'LV4', 4)
INSERT [dbo].[TopicOfLevel] ([TopicID], [LevelID], [Stt]) VALUES (N'TP1', N'LV5', 5)
INSERT [dbo].[TopicOfLevel] ([TopicID], [LevelID], [Stt]) VALUES (N'TP2', N'LV1', 6)
INSERT [dbo].[TopicOfLevel] ([TopicID], [LevelID], [Stt]) VALUES (N'TP2', N'LV2', 7)
INSERT [dbo].[TopicOfLevel] ([TopicID], [LevelID], [Stt]) VALUES (N'TP2', N'LV3', 8)
INSERT [dbo].[TopicOfLevel] ([TopicID], [LevelID], [Stt]) VALUES (N'TP2', N'LV4', 9)
INSERT [dbo].[TopicOfLevel] ([TopicID], [LevelID], [Stt]) VALUES (N'TP2', N'LV5', 10)
INSERT [dbo].[TopicOfLevel] ([TopicID], [LevelID], [Stt]) VALUES (N'TP3', N'LV2', 11)
INSERT [dbo].[TopicOfLevel] ([TopicID], [LevelID], [Stt]) VALUES (N'TP3', N'LV3', 12)
INSERT [dbo].[TopicOfLevel] ([TopicID], [LevelID], [Stt]) VALUES (N'TP3', N'LV4', 13)
INSERT [dbo].[TopicOfLevel] ([TopicID], [LevelID], [Stt]) VALUES (N'TP3', N'LV5', 14)
INSERT [dbo].[TopicOfLevel] ([TopicID], [LevelID], [Stt]) VALUES (N'TP4', N'LV3', 15)
INSERT [dbo].[TopicOfLevel] ([TopicID], [LevelID], [Stt]) VALUES (N'TP4', N'LV4', 16)
INSERT [dbo].[TopicOfLevel] ([TopicID], [LevelID], [Stt]) VALUES (N'TP4', N'LV5', 17)
INSERT [dbo].[TopicOfLevel] ([TopicID], [LevelID], [Stt]) VALUES (N'TP5', N'LV4', 18)
INSERT [dbo].[TopicOfLevel] ([TopicID], [LevelID], [Stt]) VALUES (N'TP5', N'LV5', 19)
INSERT [dbo].[TopicOfLevel] ([TopicID], [LevelID], [Stt]) VALUES (N'TP6', N'LV5', 20)
SET IDENTITY_INSERT [dbo].[TopicOfLevel] OFF
INSERT [dbo].[User] ([UserName], [Pass], [PassSalt]) VALUES (N'ngale', N'mU8L6/6Kx0HmAjMUHKyguniIT3QKMYkDVIqpOYgih2lameZq/k9W/j0d4fA4+flyH6MC067kXsVmnS3z24G8nw==', N'100000.YKO/I/ws1VPhaKkNxncMoLJgIOSXU/jbbxSCkhfTmX22Yg==')
ALTER TABLE [dbo].[ConceptsForTopic]  WITH CHECK ADD  CONSTRAINT [FK_ConceptsForTopic_ConceptAll] FOREIGN KEY([ConceptID])
REFERENCES [dbo].[ConceptAll] ([ConceptID])
GO
ALTER TABLE [dbo].[ConceptsForTopic] CHECK CONSTRAINT [FK_ConceptsForTopic_ConceptAll]
GO
ALTER TABLE [dbo].[ConceptsForTopic]  WITH CHECK ADD  CONSTRAINT [FK_ConceptsForTopic_Topic] FOREIGN KEY([TopicID])
REFERENCES [dbo].[Topic] ([TopicID])
GO
ALTER TABLE [dbo].[ConceptsForTopic] CHECK CONSTRAINT [FK_ConceptsForTopic_Topic]
GO
ALTER TABLE [dbo].[Link]  WITH CHECK ADD  CONSTRAINT [FK_Link_ConceptAll] FOREIGN KEY([ConceptID1])
REFERENCES [dbo].[ConceptAll] ([ConceptID])
GO
ALTER TABLE [dbo].[Link] CHECK CONSTRAINT [FK_Link_ConceptAll]
GO
ALTER TABLE [dbo].[Link]  WITH CHECK ADD  CONSTRAINT [FK_Link_ConceptAll1] FOREIGN KEY([ConceptID2])
REFERENCES [dbo].[ConceptAll] ([ConceptID])
GO
ALTER TABLE [dbo].[Link] CHECK CONSTRAINT [FK_Link_ConceptAll1]
GO
ALTER TABLE [dbo].[LinkOfMap]  WITH CHECK ADD  CONSTRAINT [FK_LinkOfMap_Link] FOREIGN KEY([LinkID])
REFERENCES [dbo].[Link] ([LinkID])
GO
ALTER TABLE [dbo].[LinkOfMap] CHECK CONSTRAINT [FK_LinkOfMap_Link]
GO
ALTER TABLE [dbo].[LinkOfMap]  WITH CHECK ADD  CONSTRAINT [FK_LinkOfMap_MapOfUser] FOREIGN KEY([MapID])
REFERENCES [dbo].[MapOfUser] ([MapID])
GO
ALTER TABLE [dbo].[LinkOfMap] CHECK CONSTRAINT [FK_LinkOfMap_MapOfUser]
GO
ALTER TABLE [dbo].[MapOfUser]  WITH CHECK ADD  CONSTRAINT [FK_MapOfUser_User1] FOREIGN KEY([UserName])
REFERENCES [dbo].[User] ([UserName])
GO
ALTER TABLE [dbo].[MapOfUser] CHECK CONSTRAINT [FK_MapOfUser_User1]
GO
ALTER TABLE [dbo].[TopicOfLevel]  WITH CHECK ADD  CONSTRAINT [FK_TopicOfLevel_Level] FOREIGN KEY([LevelID])
REFERENCES [dbo].[Level] ([LevelID])
GO
ALTER TABLE [dbo].[TopicOfLevel] CHECK CONSTRAINT [FK_TopicOfLevel_Level]
GO
ALTER TABLE [dbo].[TopicOfLevel]  WITH CHECK ADD  CONSTRAINT [FK_TopicOfLevel_Topic] FOREIGN KEY([TopicID])
REFERENCES [dbo].[Topic] ([TopicID])
GO
ALTER TABLE [dbo].[TopicOfLevel] CHECK CONSTRAINT [FK_TopicOfLevel_Topic]
GO
USE [master]
GO
ALTER DATABASE [Concept] SET  READ_WRITE 
GO
