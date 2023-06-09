USE [master]
GO
/****** Object:  Database [kasir_lks]    Script Date: 26/04/2023 21:25:27 ******/
CREATE DATABASE [kasir_lks]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'kasir_lks', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\kasir_lks.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'kasir_lks_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\kasir_lks_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [kasir_lks] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [kasir_lks].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [kasir_lks] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [kasir_lks] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [kasir_lks] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [kasir_lks] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [kasir_lks] SET ARITHABORT OFF 
GO
ALTER DATABASE [kasir_lks] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [kasir_lks] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [kasir_lks] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [kasir_lks] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [kasir_lks] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [kasir_lks] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [kasir_lks] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [kasir_lks] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [kasir_lks] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [kasir_lks] SET  DISABLE_BROKER 
GO
ALTER DATABASE [kasir_lks] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [kasir_lks] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [kasir_lks] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [kasir_lks] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [kasir_lks] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [kasir_lks] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [kasir_lks] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [kasir_lks] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [kasir_lks] SET  MULTI_USER 
GO
ALTER DATABASE [kasir_lks] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [kasir_lks] SET DB_CHAINING OFF 
GO
ALTER DATABASE [kasir_lks] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [kasir_lks] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [kasir_lks] SET DELAYED_DURABILITY = DISABLED 
GO
USE [kasir_lks]
GO
/****** Object:  Table [dbo].[keranjang]    Script Date: 26/04/2023 21:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[keranjang](
	[id_keranjang] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[kode_produk] [varchar](50) NULL,
	[transaksi_id] [numeric](18, 0) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[produk]    Script Date: 26/04/2023 21:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[produk](
	[kode_produk] [varchar](50) NOT NULL,
	[produk] [varchar](50) NULL,
	[jumlah] [numeric](18, 0) NULL,
	[harga] [numeric](18, 0) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[transaksi]    Script Date: 26/04/2023 21:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[transaksi](
	[id_transaksi] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[tgl_transaksi] [datetime] NULL,
	[status] [varchar](50) NULL,
	[tgl_bayar] [date] NULL,
	[total_harga] [numeric](18, 0) NULL,
	[bayar] [numeric](18, 0) NULL,
	[kembalian] [numeric](18, 0) NULL,
 CONSTRAINT [PK_transaksi] PRIMARY KEY CLUSTERED 
(
	[id_transaksi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[users]    Script Date: 26/04/2023 21:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[users](
	[id_user] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[nama_lengkap] [varchar](50) NULL,
	[username] [varchar](50) NULL,
	[password] [varchar](50) NULL,
	[lvl] [varchar](50) NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id_user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[keranjang] ON 

INSERT [dbo].[keranjang] ([id_keranjang], [kode_produk], [transaksi_id]) VALUES (CAST(38 AS Numeric(18, 0)), N'BRG001', CAST(46 AS Numeric(18, 0)))
INSERT [dbo].[keranjang] ([id_keranjang], [kode_produk], [transaksi_id]) VALUES (CAST(39 AS Numeric(18, 0)), N'BRG001', CAST(46 AS Numeric(18, 0)))
INSERT [dbo].[keranjang] ([id_keranjang], [kode_produk], [transaksi_id]) VALUES (CAST(40 AS Numeric(18, 0)), N'BRG001', CAST(46 AS Numeric(18, 0)))
INSERT [dbo].[keranjang] ([id_keranjang], [kode_produk], [transaksi_id]) VALUES (CAST(41 AS Numeric(18, 0)), N'BRG001', CAST(46 AS Numeric(18, 0)))
SET IDENTITY_INSERT [dbo].[keranjang] OFF
INSERT [dbo].[produk] ([kode_produk], [produk], [jumlah], [harga]) VALUES (N'BRG001', N'Indomie', CAST(0 AS Numeric(18, 0)), CAST(3000 AS Numeric(18, 0)))
SET IDENTITY_INSERT [dbo].[transaksi] ON 

INSERT [dbo].[transaksi] ([id_transaksi], [tgl_transaksi], [status], [tgl_bayar], [total_harga], [bayar], [kembalian]) VALUES (CAST(46 AS Numeric(18, 0)), CAST(N'2023-05-02 18:34:32.000' AS DateTime), N'1', CAST(N'2023-02-07' AS Date), CAST(12000 AS Numeric(18, 0)), CAST(12000 AS Numeric(18, 0)), NULL)
INSERT [dbo].[transaksi] ([id_transaksi], [tgl_transaksi], [status], [tgl_bayar], [total_harga], [bayar], [kembalian]) VALUES (CAST(47 AS Numeric(18, 0)), CAST(N'2023-07-02 11:56:51.000' AS DateTime), N'0', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[transaksi] OFF
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([id_user], [nama_lengkap], [username], [password], [lvl]) VALUES (CAST(1 AS Numeric(18, 0)), N'Seorang Admin', N'admin', N'12345678', N'admin')
INSERT [dbo].[users] ([id_user], [nama_lengkap], [username], [password], [lvl]) VALUES (CAST(2 AS Numeric(18, 0)), N'Seorang Kasir', N'kasir', N'12345678', N'kasir')
INSERT [dbo].[users] ([id_user], [nama_lengkap], [username], [password], [lvl]) VALUES (CAST(10002 AS Numeric(18, 0)), N'Seorang Manager Tampan', N'manager', N'12345678', N'manager')
SET IDENTITY_INSERT [dbo].[users] OFF
/****** Object:  Index [IX_keranjang]    Script Date: 26/04/2023 21:25:27 ******/
CREATE NONCLUSTERED INDEX [IX_keranjang] ON [dbo].[keranjang]
(
	[id_keranjang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_keranjang_kodeproduk]    Script Date: 26/04/2023 21:25:27 ******/
CREATE NONCLUSTERED INDEX [IX_keranjang_kodeproduk] ON [dbo].[keranjang]
(
	[kode_produk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_produk]    Script Date: 26/04/2023 21:25:27 ******/
CREATE NONCLUSTERED INDEX [IX_produk] ON [dbo].[produk]
(
	[kode_produk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[keranjang]  WITH CHECK ADD  CONSTRAINT [FK_keranjang_transaksi] FOREIGN KEY([transaksi_id])
REFERENCES [dbo].[transaksi] ([id_transaksi])
GO
ALTER TABLE [dbo].[keranjang] CHECK CONSTRAINT [FK_keranjang_transaksi]
GO
USE [master]
GO
ALTER DATABASE [kasir_lks] SET  READ_WRITE 
GO
