USE [master]
GO
/****** Object:  Database [BMSDb]    Script Date: 13-11-2024 11:49:06 ******/
CREATE DATABASE [BMSDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BMSDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\BMSDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BMSDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\BMSDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [BMSDb] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BMSDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BMSDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BMSDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BMSDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BMSDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BMSDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [BMSDb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BMSDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BMSDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BMSDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BMSDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BMSDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BMSDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BMSDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BMSDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BMSDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BMSDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BMSDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BMSDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BMSDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BMSDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BMSDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [BMSDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BMSDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BMSDb] SET  MULTI_USER 
GO
ALTER DATABASE [BMSDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BMSDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BMSDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BMSDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BMSDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BMSDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BMSDb] SET QUERY_STORE = ON
GO
ALTER DATABASE [BMSDb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [BMSDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 13-11-2024 11:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 13-11-2024 11:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[AccountType] [nvarchar](max) NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Admins]    Script Date: 13-11-2024 11:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admins](
	[AdminId] [int] NOT NULL,
	[AdminName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Admins] PRIMARY KEY CLUSTERED 
(
	[AdminId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 13-11-2024 11:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Loans]    Script Date: 13-11-2024 11:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Loans](
	[LoanId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[LoanAmount] [decimal](18, 2) NOT NULL,
	[InterestRate] [decimal](18, 2) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[ApplicationDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Loans] PRIMARY KEY CLUSTERED 
(
	[LoanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 13-11-2024 11:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] NOT NULL,
	[RoleName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 13-11-2024 11:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[TransactionId] [int] NOT NULL,
	[AccountId] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[TransactionType] [nvarchar](max) NOT NULL,
	[TransactionDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 13-11-2024 11:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[UserRole] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241108053001_BMS_Draft1', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241113043923_Draft2', N'8.0.10')
GO
INSERT [dbo].[Accounts] ([AccountId], [CustomerId], [AccountType], [Balance], [CreatedDate], [IsActive]) VALUES (101, 1, N'Savings', CAST(900000.00 AS Decimal(18, 2)), CAST(N'2024-11-13T05:31:08.4930000' AS DateTime2), 1)
INSERT [dbo].[Accounts] ([AccountId], [CustomerId], [AccountType], [Balance], [CreatedDate], [IsActive]) VALUES (102, 2, N'Student', CAST(5000.00 AS Decimal(18, 2)), CAST(N'2024-11-13T05:31:08.4930000' AS DateTime2), 1)
INSERT [dbo].[Accounts] ([AccountId], [CustomerId], [AccountType], [Balance], [CreatedDate], [IsActive]) VALUES (103, 3, N'Student', CAST(10000.00 AS Decimal(18, 2)), CAST(N'2024-11-13T05:31:08.4930000' AS DateTime2), 1)
INSERT [dbo].[Accounts] ([AccountId], [CustomerId], [AccountType], [Balance], [CreatedDate], [IsActive]) VALUES (104, 4, N'Savings', CAST(1100000.00 AS Decimal(18, 2)), CAST(N'2024-11-13T05:31:08.4930000' AS DateTime2), 1)
INSERT [dbo].[Accounts] ([AccountId], [CustomerId], [AccountType], [Balance], [CreatedDate], [IsActive]) VALUES (105, 5, N'Savings', CAST(1000980.00 AS Decimal(18, 2)), CAST(N'2024-11-13T05:31:08.4930000' AS DateTime2), 1)
GO
INSERT [dbo].[Admins] ([AdminId], [AdminName], [Email], [Password]) VALUES (1, N'Sukrutha', N'sukrutha@gmail.com', N'sukrutha')
INSERT [dbo].[Admins] ([AdminId], [AdminName], [Email], [Password]) VALUES (2, N'Deepthi', N'deepthi@gmail.com', N'deepthi')
INSERT [dbo].[Admins] ([AdminId], [AdminName], [Email], [Password]) VALUES (3, N'Archana', N'archana@gmail.com', N'archana')
INSERT [dbo].[Admins] ([AdminId], [AdminName], [Email], [Password]) VALUES (4, N'Surabhi', N'surabhi@gmail.com', N'surabhi')
INSERT [dbo].[Admins] ([AdminId], [AdminName], [Email], [Password]) VALUES (5, N'Aishwarya', N'aishwaryaray@gmail.com', N'aishwarya')
GO
INSERT [dbo].[Customers] ([CustomerId], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [PasswordHash], [RoleId]) VALUES (1, N'Prathiba', N'Reddy', N'prathiba.reddy@gmail.com', N'7893764455', N'Hyderabad', N'prathiba@1', 1001)
INSERT [dbo].[Customers] ([CustomerId], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [PasswordHash], [RoleId]) VALUES (2, N'Aanya', N'Sharma', N'aanya06@gmail.com', N'7456787655', N'Mumbai', N'aanya@2', 1002)
INSERT [dbo].[Customers] ([CustomerId], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [PasswordHash], [RoleId]) VALUES (3, N'Bhavik', N'Reddy', N'bhavik0207@gmail.com', N'6218765456', N'Bangalore', N'bhavik@3', 1002)
INSERT [dbo].[Customers] ([CustomerId], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [PasswordHash], [RoleId]) VALUES (4, N'Ramesh', N'Yadav', N'ramesh708@gmail.com', N'9834567898', N'KGF', N'ramesh@4', 1001)
INSERT [dbo].[Customers] ([CustomerId], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [PasswordHash], [RoleId]) VALUES (5, N'Santhosh', N'Kumar', N'santhosh31@gmail.com', N'8764366786', N'Bangalore', N'santhosh@5', 1001)
GO
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (1001, N'Regular')
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (1002, N'Ordinary')
GO
INSERT [dbo].[Users] ([UserId], [UserName], [UserRole]) VALUES (1, N'Sukrutha', N'Admin')
INSERT [dbo].[Users] ([UserId], [UserName], [UserRole]) VALUES (2, N'Deepthi', N'Admin')
INSERT [dbo].[Users] ([UserId], [UserName], [UserRole]) VALUES (3, N'Archana', N'Admin')
INSERT [dbo].[Users] ([UserId], [UserName], [UserRole]) VALUES (4, N'Surabhi', N'Admin')
INSERT [dbo].[Users] ([UserId], [UserName], [UserRole]) VALUES (5, N'Aishwarya', N'Admin')
INSERT [dbo].[Users] ([UserId], [UserName], [UserRole]) VALUES (6, N'Prathiba', N'Customer')
INSERT [dbo].[Users] ([UserId], [UserName], [UserRole]) VALUES (7, N'Aanya', N'Customer')
INSERT [dbo].[Users] ([UserId], [UserName], [UserRole]) VALUES (8, N'Bhavik', N'Customer')
INSERT [dbo].[Users] ([UserId], [UserName], [UserRole]) VALUES (9, N'Ramesh', N'Customer')
INSERT [dbo].[Users] ([UserId], [UserName], [UserRole]) VALUES (10, N'Santhosh', N'Customer')
GO
/****** Object:  Index [IX_Accounts_CustomerId]    Script Date: 13-11-2024 11:49:07 ******/
CREATE NONCLUSTERED INDEX [IX_Accounts_CustomerId] ON [dbo].[Accounts]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Customers_RoleId]    Script Date: 13-11-2024 11:49:07 ******/
CREATE NONCLUSTERED INDEX [IX_Customers_RoleId] ON [dbo].[Customers]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Loans_CustomerId]    Script Date: 13-11-2024 11:49:07 ******/
CREATE NONCLUSTERED INDEX [IX_Loans_CustomerId] ON [dbo].[Loans]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Transactions_AccountId]    Script Date: 13-11-2024 11:49:07 ******/
CREATE NONCLUSTERED INDEX [IX_Transactions_AccountId] ON [dbo].[Transactions]
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Roles] ADD  DEFAULT (N'') FOR [RoleName]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Customers_CustomerId]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_Customers_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_Roles_RoleId]
GO
ALTER TABLE [dbo].[Loans]  WITH CHECK ADD  CONSTRAINT [FK_Loans_Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Loans] CHECK CONSTRAINT [FK_Loans_Customers_CustomerId]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Accounts_AccountId] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([AccountId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Accounts_AccountId]
GO
USE [master]
GO
ALTER DATABASE [BMSDb] SET  READ_WRITE 
GO
