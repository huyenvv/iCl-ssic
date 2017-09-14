USE [master]
GO
/****** Object:  Database [iClassic]    Script Date: 9/14/2017 8:45:21 PM ******/
CREATE DATABASE [iClassic]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'iClassic', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\iClassic.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'iClassic_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\iClassic_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [iClassic] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [iClassic].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [iClassic] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [iClassic] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [iClassic] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [iClassic] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [iClassic] SET ARITHABORT OFF 
GO
ALTER DATABASE [iClassic] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [iClassic] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [iClassic] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [iClassic] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [iClassic] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [iClassic] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [iClassic] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [iClassic] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [iClassic] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [iClassic] SET  ENABLE_BROKER 
GO
ALTER DATABASE [iClassic] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [iClassic] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [iClassic] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [iClassic] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [iClassic] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [iClassic] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [iClassic] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [iClassic] SET RECOVERY FULL 
GO
ALTER DATABASE [iClassic] SET  MULTI_USER 
GO
ALTER DATABASE [iClassic] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [iClassic] SET DB_CHAINING OFF 
GO
ALTER DATABASE [iClassic] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [iClassic] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [iClassic] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'iClassic', N'ON'
GO
USE [iClassic]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 9/14/2017 8:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 9/14/2017 8:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 9/14/2017 8:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 9/14/2017 8:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 9/14/2017 8:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 9/14/2017 8:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[BranchId] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_AspNetUsers_Status]  DEFAULT ((1)),
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Branch]    Script Date: 9/14/2017 8:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Branch](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Address] [nvarchar](500) NULL,
	[SDT] [varchar](20) NULL,
	[Created] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 9/14/2017 8:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MaKH]  AS ('KH'+CONVERT([nvarchar](10),[ID])),
	[TenKH] [nvarchar](200) NOT NULL,
	[SDT] [varchar](20) NOT NULL,
	[KenhQC] [int] NULL,
	[DangNguoi] [nvarchar](500) NULL,
	[Address] [nvarchar](500) NULL,
	[Image] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
	[Created] [datetime2](7) NOT NULL CONSTRAINT [DF__Customer__Create__24927208]  DEFAULT (getdate()),
	[BranchId] [int] NOT NULL,
	[CreateBy] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK__Customer__3214EC07B8C74A10] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 9/14/2017 8:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code]  AS ('HD'+CONVERT([nvarchar](10),[ID])),
	[Total] [float] NOT NULL,
	[DatCoc] [float] NULL,
	[ChietKhau] [float] NULL,
	[NgayThu] [datetime] NOT NULL,
	[NgayTra] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[CreateBy] [nvarchar](128) NOT NULL,
	[ModifiedBy] [nvarchar](128) NOT NULL,
	[BranchId] [int] NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LoaiVai]    Script Date: 9/14/2017 8:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiVai](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MaVai] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[SoTienNhapVao] [float] NULL,
	[Note] [nvarchar](500) NULL,
	[Created] [datetime2](7) NOT NULL,
	[BranchId] [int] NOT NULL,
	[CreateBy] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_LoaiVai] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhieuChi]    Script Date: 9/14/2017 8:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuChi](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MucChi] [nvarchar](500) NOT NULL,
	[SoTien] [float] NOT NULL,
	[NguoiNhanPhieu] [nvarchar](500) NOT NULL,
	[BranchId] [int] NOT NULL,
	[CreateBy] [nvarchar](128) NOT NULL,
	[Created] [datetime2](7) NOT NULL CONSTRAINT [DF__PhieuChi__Create__25869641]  DEFAULT (getdate()),
 CONSTRAINT [PK__PhieuChi__3214EC076110107F] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhieuSanXuat]    Script Date: 9/14/2017 8:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuSanXuat](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TenSanPham] [nvarchar](200) NOT NULL,
	[MaVaiId] [int] NULL,
	[SoLuong] [int] NOT NULL CONSTRAINT [DF__PhieuSanX__SoLuo__267ABA7A]  DEFAULT ((1)),
	[Status] [int] NOT NULL CONSTRAINT [DF__PhieuSanX__Statu__276EDEB3]  DEFAULT ((1)),
	[HasVai] [bit] NOT NULL CONSTRAINT [DF_PhieuSanXuat_HasVai]  DEFAULT ((0)),
	[InvoiceId] [int] NOT NULL,
	[ProductTypeId] [int] NOT NULL,
	[DonGia] [float] NOT NULL,
	[ThoCatId] [int] NOT NULL,
	[ThoMayId] [int] NOT NULL,
	[ThoDoId] [int] NOT NULL,
 CONSTRAINT [PK__PhieuSan__3214EC076E805B12] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhieuSua]    Script Date: 9/14/2017 8:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuSua](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NoiDung] [nvarchar](500) NOT NULL,
	[SoTien] [float] NULL CONSTRAINT [DF_PhieuSua_SoTien]  DEFAULT ((0)),
	[Status] [int] NOT NULL CONSTRAINT [DF__PhieuSua__Status__29572725]  DEFAULT ((1)),
	[Type] [int] NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[ProblemBy] [int] NULL,
	[ThoId] [int] NOT NULL,
 CONSTRAINT [PK__PhieuSua__3214EC07B302505D] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductTyeField]    Script Date: 9/14/2017 8:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductTyeField](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Note] [nvarchar](200) NULL,
	[ProductTypeId] [int] NOT NULL,
 CONSTRAINT [PK_ProductTyeField] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductType]    Script Date: 9/14/2017 8:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Price] [float] NOT NULL,
	[Note] [nvarchar](500) NULL,
 CONSTRAINT [PK_Template] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductTypeLoaiVai]    Script Date: 9/14/2017 8:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductTypeLoaiVai](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MavaiId] [int] NOT NULL,
	[Price] [float] NULL,
	[ProductTypeId] [int] NOT NULL,
 CONSTRAINT [PK_ProductTypeLoaiVai] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductTypeValue]    Script Date: 9/14/2017 8:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductTypeValue](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](200) NULL,
	[ProductTypeFieldId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
 CONSTRAINT [PK_ProductTypeValue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tho]    Script Date: 9/14/2017 8:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tho](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[XuongId] [int] NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK_Tho] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201708151105108_InitialCreate', N'iClassic.Models.ApplicationContext', 0x1F8B0800000000000400DD5CDB6EE436127D5F20FF20E869B3705ABEEC0C668DEE049EB69D35767CC1B427D8B7015B62B78991A88E44393616FB65FB904FCA2F6C5177F12251DDEA8B8300C134593C552C5691C552D17FFCEFF7F14F2F816F3DE32826219DD827A363DBC2D40D3D4297133B618B1F3ED83FFDF8DD5FC6575EF062FD52D09D713A1849E389FDC4D8EADC7162F70907281E05C48DC2385CB0911B060EF242E7F4F8F81FCEC9898301C2062CCB1A7F4E2823014E7FC0CF69485DBC6209F26F430FFB71DE0E3DB314D5BA43018E57C8C5139B4C7D14C7C41D65A4B675E1130462CCB0BFB02D4469C8100321CFBFC478C6A2902E672B6840FEE3EB0A03DD02F931CE853FAFC84DE7717CCAE7E154030B2837895918F4043C39CB15E388C3D752AF5D2A0E5477052A66AF7CD6A9FA26F68D87D3A6CFA10F0A10199E4FFD88134FECDB92C545BCBAC36C540C1C6590D711C0FD1646DF4675C423CB78DC516948A7A363FEDF91354D7C9644784271C222E41F590FC9DC27EEBFF0EB63F80DD3C9D9C97C71F6E1DD7BE49DBDFF3B3E7B579F29CC15E81A0DD0F410852B1C816C7851CEDFB69CE638471C580EAB8DC9B402B6043E615BB7E8E513A64BF604DE72FAC1B6AEC90BF68A96DCB8BE50022E04835894C0CFBBC4F7D1DCC765BFD3CA93FFBF85EBE9BBF78370BD43CF64992EBDC01F1C2702BFFA8CFDB4377E22ABCCBD1AEBFD3527BB8EC280FF6EDA57D6FB75162691CB27136A491E51B4C4AC29DDD8A98CD7C8A439D4F0665DA01EBE69734965F35692F209ADE309058B5D7B4321EF76F91A5BDCC56A058B979A16D7489BC10927D548187A64150495D19C981A0D85C9FC99F7C09BF8C265E4B9DC073F86600688F6C6F91821EA3E5533B8A1ECEC74E81D19FE69A4837626570122FE00FBBE011788B716240AB0B7A97A1FC07E61DBF3FE89E2A7AD2B6886DD24026F9C3114ACB6CEEDE129A4F82E09E6DCC977C76BB0A579FC2DBC462E0BA32BCA476D8CF72974BF8509BBA2DE2562F80B730B40FEF39104E600838873E1BA388EAFC198B1370DE13AB19987F30D79DF71179C062450075EC2D1F1B520AD822F3585148069C85441589BA89FC225A166A216A47A51338A4E5173B2BEA272303349734ABDA02941A79C19D560616DBA42C3C7B529ECE107B69BC52BBABDA0A6C619EC90F8674C7104DB98F78018C311AD56C064DFD8477C942E1F67BAF5B329E5F40BF293A159ADE50DE92630BC37A4B087EF0DA998D0FC4C3C1E9518DCF60A628037A2575F24BB7D4E906CD7EED098E6AE99EF660FD0B9CB451C872E49BD4091E7CBB3344DF92186B3BA5336D96CC4B40F4C0C0C9DF0230F5A606EB66854F7F412FB98618BDFE0781E748A621779B21A61425E0FC18A1355215895FE690AF7378927583A8EF820C42F4131782AA14C760B425DB2427EA79684918647189F7BC943ECB9C42B4C39C34E4D983057677BB800251F6151BA3434766A16D76E889AA855B7E65D216CB5EE5212662736D9113B6BEC328FDFB66298ED1ADB8171B6ABC444006DE6721F069ADF554C0D40BCB81C9A810A37268D81E621D54E0CB4A9B13D186853256FCE40B32BAAE9FA0BF7D54333CFE64579F7C77AABBAF6609B0D7D1C986966B1278C61300247B279F22EFCC21457339032BF9DC579A02B1A08879E61D64CD854D1AE320A75DA4144136A03ACCCAC0334FFE2290149EED443B82293D72A5D1E43F4802DB26EADB0F9CE2FC0D62C40C6AE7FF9AD11EABF0F8BA66974F72867565A8364E24657851A8EC220C4ADAB397103A5E8B2B2B2624C22E13EB1706D62F962B428A8236ED528A998CCE05A2A4CB35B4BAA70AC4F40B6919684E049A3A56232836B29B7D16E252942821E41C1462A6A1EE003395B91E728CF9AB26FEC64D56079C3D8D1948D8D6FD16A45E8B2564696B758B3AC866CFAC3AC7F7D559061386EAC28B32AA52D39B130424B2CF4026B90F49A4431BB440CCD11CFF24CBD4022539CAC9ACDBF60583F3CE5252C4E81829AFF3B1B215629348E59390AC901AE6172010F65D2FCB962E9D5C32D5ED1877C142952F6D3D04F02AA8FACF4A3B30F77F5F1598B8C307604F9A5C849529514DF36F56EB42AB23F0CB14265DCB2FE2AE92174BA2E62CEBAB67571A81EA5484BD55174A9AABDAD9A2E80315F293130ECBF509D08DBF1A8AAFCA68151B69A2355053875A4AA751B5EAE43C82B6CEA1079534F8C5A91860456EB33476DD6D1D4319B3DE68842B14C1D52E8EA2165BD24A62164BD632D3C8D46D514E61CE422983ABADC6B8EAC2887A9432BBAD7C056C82CF699A32A2A66EAC08A6E73ECAA7C463C170EF824D65EC2D63B8AB34BFA6667B106633B9BFC304779AD12A10E546BEE8995D71A486079FB419A92F6A6BA9E29658999CD4C4983A1DF731A1FF09B5B4E6BD5811EB3F155BEB1ADB75525E8F1FA19EC56CD42BAA58A2425F7F2B62ADC4AC7F90DB1FBC5937465CC486CAB50231CE9AF31C3C188138C66BFFA539F60BE811704B78892058E595689629F1E9F9C0AEFA60EE70D9313C79EAFB861EB1E3235D76C074565F41945EE138AE4128F0DAACA2B50297F7E433DFC32B1FF938E3A4F5331FC5F69F39175137FA1E4D7043A1EA3045BFF954B568779F7D07E633CD0572AE65ABDF9F7D76CE891751F81C79C5BC7822ED759E1E6DB955ED26443379066ED172D6FD7A1C4472373C2367E3042687F0C9563FF35402FDFD7917A3F0951AA49F0F0F55F80ACA32BD5EB8F8DE6AB7CE1B111A2E215C7507883A850F74A631D2CED0B0D0F7EB2F48546BFC9AA5F6CAC239AF6B5C63AEE25BED530DF578B917B3C3B1537BC5DECB1A99E3B6BDD372A7CDDF7612B95C46FE4E872D97B0FB80D4ADBD7B08C3756153ED871AF28FA1E0C7B9FA6BDF54AEF4329EEAE0A6FF65BD3BDCB32EE968F767FAAEAED03A837545450EDBF467BD7B6A6CB4A1F78A16BBF4AEC0333B6BCAE6EFFF5D6BB36365DDEFAC08DAD5755F581D9DABECECF3D5B9AF111BAF71A69B9E04BF3754995DCEEAA82CEBE04C00D7F1E8211641165F674555D76D75632DCC1B022D133D5D7FB898C25C791F84A14ED6CFBCD353FF05B279BD3B4B3D554C9B6F1CEF7FF56DE394D3B6F4DEDE93EEAB795D59FAA9AFA8E7DACAD44ED2DD56B3766D2F13CA02B666D2D15784BE5D98328A5E13D9A8FDE6FA71A7B10950CE93A3DAAAFE5EFD77076D6FEAE279CDF31595610FCAF7C52EC364ECD92E6862EC2E2F016242A48840CCD2D66C88323F5226264815C06DD3CC79CBEBD4FF376FC4BC71C7B37F43E61AB84C1947130F71B092F1E04B4F14F4BCC9B328FEF57E99F9119620A2026E1B9F97BFA3121BE57CA7DADC80969207874916774F95A329ED95DBE964877213504CAD55706458F3858F90016DFD319E29FD4FACB06E6F7092F91FB5A65007520DD0BD154FBF892A065848238C7A8C6C34FB0612F78F9F1FFB8F9A853DE560000, N'6.1.3-40302')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'21C07E38-61F9-4B5C-9B3B-DD0C258DAB86', N'Admin')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'A180355D-CD80-4ACF-944E-B362B2CC7C0A', N'Employee')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'EAC8B9F1-F285-47A8-81C5-A029245BFC74', N'SupperAdmin')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'47060352-d63f-4e33-83e3-85a8b16c30fc', N'21C07E38-61F9-4B5C-9B3B-DD0C258DAB86')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f691cf30-3b9e-4e04-9f60-2eaecf32d75d', N'EAC8B9F1-F285-47A8-81C5-A029245BFC74')
INSERT [dbo].[AspNetUsers] ([Id], [BranchId], [Name], [Email], [IsActive], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'47060352-d63f-4e33-83e3-85a8b16c30fc', 2, N'Thền Ngọc Sơn', NULL, 1, 0, N'ACsxujIcdhMZitgR0hrpVRFN7fybVAU9xzGGbVLVwsb2cDkLrZ1xccbZWmFKhmj/wQ==', N'7e532d3e-8d87-43cb-930e-ac9ec1112c92', NULL, 0, 0, NULL, 1, 0, N'ngocson')
INSERT [dbo].[AspNetUsers] ([Id], [BranchId], [Name], [Email], [IsActive], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'f691cf30-3b9e-4e04-9f60-2eaecf32d75d', 2, N'Lê Anh Tuấn', NULL, 1, 0, N'ANvQ8+wg2oFVa/Wi9vNsVhkQ7+XhketQzn+tlKRMQOeuqV2RkJEnYt3JG2wKi+YOXw==', N'f1f08ad4-ace5-4bfe-9de1-bd0c511cec25', NULL, 0, 0, NULL, 0, 0, N'admin')
SET IDENTITY_INSERT [dbo].[Branch] ON 

INSERT [dbo].[Branch] ([Id], [Name], [Address], [SDT], [Created]) VALUES (2, N'Charles & Brenton Laocai', N'Lào Cai', N'0983.343.3322', CAST(N'2017-08-11 14:32:22.9999293' AS DateTime2))
INSERT [dbo].[Branch] ([Id], [Name], [Address], [SDT], [Created]) VALUES (3, N'iClassic', N'Hồ Tây', N'0909.999.999', CAST(N'2017-08-11 17:48:38.9084727' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Branch] OFF
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([Id], [TenKH], [SDT], [KenhQC], [DangNguoi], [Address], [Image], [Note], [Created], [BranchId], [CreateBy]) VALUES (4, N'Trần Quốc Toàn', N'0988.000.111', 2, N'người chuẩn mẫu', N'Hà Nội', NULL, NULL, CAST(N'2017-09-01 00:58:44.6114368' AS DateTime2), 2, N'f691cf30-3b9e-4e04-9f60-2eaecf32d75d')
SET IDENTITY_INSERT [dbo].[Customer] OFF
SET IDENTITY_INSERT [dbo].[Invoice] ON 

INSERT [dbo].[Invoice] ([Id], [Total], [DatCoc], [ChietKhau], [NgayThu], [NgayTra], [Status], [CustomerId], [CreateBy], [ModifiedBy], [BranchId], [Created], [ModifiedDate]) VALUES (1, 14050000, 4000000, 400000, CAST(N'2017-09-13 13:38:39.000' AS DateTime), CAST(N'2017-09-13 13:38:39.000' AS DateTime), 2, 4, N'f691cf30-3b9e-4e04-9f60-2eaecf32d75d', N'f691cf30-3b9e-4e04-9f60-2eaecf32d75d', 2, CAST(N'2017-09-06 13:41:13.8284861' AS DateTime2), CAST(N'2017-09-06 13:41:13.8284861' AS DateTime2))
INSERT [dbo].[Invoice] ([Id], [Total], [DatCoc], [ChietKhau], [NgayThu], [NgayTra], [Status], [CustomerId], [CreateBy], [ModifiedBy], [BranchId], [Created], [ModifiedDate]) VALUES (2, 400000, 100000, NULL, CAST(N'2017-09-13 23:20:58.000' AS DateTime), CAST(N'2017-09-13 23:20:58.000' AS DateTime), 1, 4, N'f691cf30-3b9e-4e04-9f60-2eaecf32d75d', N'f691cf30-3b9e-4e04-9f60-2eaecf32d75d', 2, CAST(N'2017-09-06 23:22:04.9977166' AS DateTime2), CAST(N'2017-09-06 23:22:04.9987171' AS DateTime2))
INSERT [dbo].[Invoice] ([Id], [Total], [DatCoc], [ChietKhau], [NgayThu], [NgayTra], [Status], [CustomerId], [CreateBy], [ModifiedBy], [BranchId], [Created], [ModifiedDate]) VALUES (3, 0, NULL, NULL, CAST(N'2017-09-19 19:35:56.000' AS DateTime), CAST(N'2017-09-19 19:35:56.000' AS DateTime), 1, 4, N'f691cf30-3b9e-4e04-9f60-2eaecf32d75d', N'f691cf30-3b9e-4e04-9f60-2eaecf32d75d', 2, CAST(N'2017-09-12 19:36:14.9409337' AS DateTime2), CAST(N'2017-09-12 19:36:14.9409337' AS DateTime2))
INSERT [dbo].[Invoice] ([Id], [Total], [DatCoc], [ChietKhau], [NgayThu], [NgayTra], [Status], [CustomerId], [CreateBy], [ModifiedBy], [BranchId], [Created], [ModifiedDate]) VALUES (6, 7000000, NULL, NULL, CAST(N'2017-09-20 21:01:41.000' AS DateTime), CAST(N'2017-09-20 21:01:41.000' AS DateTime), 0, 4, N'f691cf30-3b9e-4e04-9f60-2eaecf32d75d', N'f691cf30-3b9e-4e04-9f60-2eaecf32d75d', 2, CAST(N'2017-09-13 21:02:47.4620548' AS DateTime2), CAST(N'2017-09-13 21:02:47.4620548' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Invoice] OFF
SET IDENTITY_INSERT [dbo].[LoaiVai] ON 

INSERT [dbo].[LoaiVai] ([Id], [MaVai], [Name], [SoTienNhapVao], [Note], [Created], [BranchId], [CreateBy]) VALUES (1, N'DJK9000', N'Vải cao cấp 2', 1000000, N'OK', CAST(N'2017-09-01 01:07:52.3874686' AS DateTime2), 2, N'f691cf30-3b9e-4e04-9f60-2eaecf32d75d')
INSERT [dbo].[LoaiVai] ([Id], [MaVai], [Name], [SoTienNhapVao], [Note], [Created], [BranchId], [CreateBy]) VALUES (2, N'KJ2008DFF', N'Vải cao cấp 1', 5000000, N'vvv', CAST(N'2017-09-01 01:24:39.0151819' AS DateTime2), 2, N'f691cf30-3b9e-4e04-9f60-2eaecf32d75d')
SET IDENTITY_INSERT [dbo].[LoaiVai] OFF
SET IDENTITY_INSERT [dbo].[PhieuChi] ON 

INSERT [dbo].[PhieuChi] ([Id], [MucChi], [SoTien], [NguoiNhanPhieu], [BranchId], [CreateBy], [Created]) VALUES (1, N'Chi mua vải', 5000000, N'Mr. Tuấn', 2, N'f691cf30-3b9e-4e04-9f60-2eaecf32d75d', CAST(N'2017-09-01 01:09:00.6203675' AS DateTime2))
SET IDENTITY_INSERT [dbo].[PhieuChi] OFF
SET IDENTITY_INSERT [dbo].[PhieuSanXuat] ON 

INSERT [dbo].[PhieuSanXuat] ([Id], [TenSanPham], [MaVaiId], [SoLuong], [Status], [HasVai], [InvoiceId], [ProductTypeId], [DonGia], [ThoCatId], [ThoMayId], [ThoDoId]) VALUES (1, N'Suit', 1, 2, 0, 0, 1, 3, 7000000, 2, 3, 1)
INSERT [dbo].[PhieuSanXuat] ([Id], [TenSanPham], [MaVaiId], [SoLuong], [Status], [HasVai], [InvoiceId], [ProductTypeId], [DonGia], [ThoCatId], [ThoMayId], [ThoDoId]) VALUES (2, N'Áo choàng', NULL, 2, 0, 0, 2, 5, 200000, 2, 3, 1)
INSERT [dbo].[PhieuSanXuat] ([Id], [TenSanPham], [MaVaiId], [SoLuong], [Status], [HasVai], [InvoiceId], [ProductTypeId], [DonGia], [ThoCatId], [ThoMayId], [ThoDoId]) VALUES (4, N'ok', 1, 1, 0, 0, 6, 3, 7000000, 2, 3, 1)
SET IDENTITY_INSERT [dbo].[PhieuSanXuat] OFF
SET IDENTITY_INSERT [dbo].[PhieuSua] ON 

INSERT [dbo].[PhieuSua] ([Id], [NoiDung], [SoTien], [Status], [Type], [InvoiceId], [ProblemBy], [ThoId]) VALUES (2, N'Sửa đũng', 50000, 0, 1, 1, 2, 1)
INSERT [dbo].[PhieuSua] ([Id], [NoiDung], [SoTien], [Status], [Type], [InvoiceId], [ProblemBy], [ThoId]) VALUES (12, N'Chật nách', 0, 0, 0, 3, 3, 6)
SET IDENTITY_INSERT [dbo].[PhieuSua] OFF
SET IDENTITY_INSERT [dbo].[ProductTyeField] ON 

INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (1, N'Vai', NULL, 3)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (2, N'Xuôi vai', NULL, 3)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (3, N'Dài áo', NULL, 3)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (4, N'Dài tay', NULL, 3)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (5, N'Bắp tay', NULL, 3)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (6, N'Vòng nách', NULL, 3)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (7, N'Cổ', NULL, 3)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (8, N'Ngực', NULL, 3)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (9, N'Eo', NULL, 3)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (10, N'Mông', NULL, 3)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (11, N'Ngực sau', NULL, 3)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (12, N'Ngực trước', NULL, 3)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (25, N'Vai', NULL, 5)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (26, N'Xuôi vai', NULL, 5)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (27, N'Dài áo', NULL, 5)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (28, N'Dài tay', NULL, 5)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (29, N'Bắp tay', NULL, 5)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (30, N'Vòng nách', NULL, 5)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (31, N'Cổ', NULL, 5)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (32, N'Ngực', NULL, 5)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (33, N'Eo', NULL, 5)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (34, N'Mông', NULL, 5)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (35, N'Ngực sau', NULL, 5)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (36, N'Ngực trước', NULL, 5)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (37, N'Dài', NULL, 6)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (38, N'Bụng', NULL, 6)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (39, N'Mông', NULL, 6)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (40, N'Đùi', NULL, 6)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (41, N'Gối', NULL, 6)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (42, N'Đáy', NULL, 6)
INSERT [dbo].[ProductTyeField] ([Id], [Name], [Note], [ProductTypeId]) VALUES (43, N'Ống', NULL, 6)
SET IDENTITY_INSERT [dbo].[ProductTyeField] OFF
SET IDENTITY_INSERT [dbo].[ProductType] ON 

INSERT [dbo].[ProductType] ([Id], [Name], [Price], [Note]) VALUES (3, N'ÁO VEST', 500000, NULL)
INSERT [dbo].[ProductType] ([Id], [Name], [Price], [Note]) VALUES (5, N'SƠ MI', 200000, NULL)
INSERT [dbo].[ProductType] ([Id], [Name], [Price], [Note]) VALUES (6, N'QUẦN ÂU', 300000, NULL)
INSERT [dbo].[ProductType] ([Id], [Name], [Price], [Note]) VALUES (7, N'ÁO GILE', 600000, NULL)
INSERT [dbo].[ProductType] ([Id], [Name], [Price], [Note]) VALUES (8, N'ÁO MĂNG TÔ', 700000, NULL)
SET IDENTITY_INSERT [dbo].[ProductType] OFF
SET IDENTITY_INSERT [dbo].[ProductTypeLoaiVai] ON 

INSERT [dbo].[ProductTypeLoaiVai] ([Id], [MavaiId], [Price], [ProductTypeId]) VALUES (1, 1, 7000000, 3)
INSERT [dbo].[ProductTypeLoaiVai] ([Id], [MavaiId], [Price], [ProductTypeId]) VALUES (2, 1, 500000, 5)
INSERT [dbo].[ProductTypeLoaiVai] ([Id], [MavaiId], [Price], [ProductTypeId]) VALUES (3, 1, 700000, 6)
INSERT [dbo].[ProductTypeLoaiVai] ([Id], [MavaiId], [Price], [ProductTypeId]) VALUES (4, 2, 400000, 3)
INSERT [dbo].[ProductTypeLoaiVai] ([Id], [MavaiId], [Price], [ProductTypeId]) VALUES (5, 2, 300000, 5)
INSERT [dbo].[ProductTypeLoaiVai] ([Id], [MavaiId], [Price], [ProductTypeId]) VALUES (6, 2, 600000, 6)
SET IDENTITY_INSERT [dbo].[ProductTypeLoaiVai] OFF
SET IDENTITY_INSERT [dbo].[ProductTypeValue] ON 

INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (47, N'80', 1, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (48, NULL, 2, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (49, NULL, 3, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (50, NULL, 4, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (51, NULL, 5, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (52, NULL, 6, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (53, NULL, 7, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (54, NULL, 8, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (55, NULL, 9, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (56, NULL, 10, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (57, NULL, 11, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (58, NULL, 12, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (59, N'60', 25, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (60, NULL, 26, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (61, NULL, 27, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (62, NULL, 28, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (63, NULL, 29, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (64, NULL, 30, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (65, NULL, 31, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (66, NULL, 32, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (67, NULL, 33, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (68, NULL, 34, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (69, NULL, 35, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (70, NULL, 36, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (71, N'15', 37, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (72, NULL, 38, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (73, NULL, 39, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (74, NULL, 40, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (75, NULL, 41, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (76, NULL, 42, 4)
INSERT [dbo].[ProductTypeValue] ([Id], [Value], [ProductTypeFieldId], [CustomerId]) VALUES (77, NULL, 43, 4)
SET IDENTITY_INSERT [dbo].[ProductTypeValue] OFF
SET IDENTITY_INSERT [dbo].[Tho] ON 

INSERT [dbo].[Tho] ([Id], [Name], [XuongId], [Type]) VALUES (1, N'Phong', 1, 1)
INSERT [dbo].[Tho] ([Id], [Name], [XuongId], [Type]) VALUES (2, N'Vinh', 2, 2)
INSERT [dbo].[Tho] ([Id], [Name], [XuongId], [Type]) VALUES (3, N'Chuyên', 3, 3)
INSERT [dbo].[Tho] ([Id], [Name], [XuongId], [Type]) VALUES (4, N'Chính', NULL, 2)
INSERT [dbo].[Tho] ([Id], [Name], [XuongId], [Type]) VALUES (5, N'Trọng', NULL, 1)
INSERT [dbo].[Tho] ([Id], [Name], [XuongId], [Type]) VALUES (6, N'Khánh', NULL, 3)
SET IDENTITY_INSERT [dbo].[Tho] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 9/14/2017 8:45:21 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 9/14/2017 8:45:21 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 9/14/2017 8:45:21 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 9/14/2017 8:45:21 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 9/14/2017 8:45:21 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 9/14/2017 8:45:21 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__Customer__2725CF1F2A98D8C6]    Script Date: 9/14/2017 8:45:21 PM ******/
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [UQ__Customer__2725CF1F2A98D8C6] UNIQUE NONCLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_Branch] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_Branch]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_AspNetUsers] FOREIGN KEY([CreateBy])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_AspNetUsers]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Branch] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Branch]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Branch1] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Branch1]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Customer]
GO
ALTER TABLE [dbo].[LoaiVai]  WITH CHECK ADD  CONSTRAINT [FK_LoaiVai_AspNetUsers] FOREIGN KEY([CreateBy])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[LoaiVai] CHECK CONSTRAINT [FK_LoaiVai_AspNetUsers]
GO
ALTER TABLE [dbo].[LoaiVai]  WITH CHECK ADD  CONSTRAINT [FK_LoaiVai_Branch] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[LoaiVai] CHECK CONSTRAINT [FK_LoaiVai_Branch]
GO
ALTER TABLE [dbo].[PhieuChi]  WITH CHECK ADD  CONSTRAINT [FK_PhieuChi_AspNetUsers] FOREIGN KEY([CreateBy])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[PhieuChi] CHECK CONSTRAINT [FK_PhieuChi_AspNetUsers]
GO
ALTER TABLE [dbo].[PhieuChi]  WITH CHECK ADD  CONSTRAINT [FK_PhieuChi_Branch] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[PhieuChi] CHECK CONSTRAINT [FK_PhieuChi_Branch]
GO
ALTER TABLE [dbo].[PhieuSanXuat]  WITH CHECK ADD  CONSTRAINT [FK_PhieuSanXuat_Invoice] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PhieuSanXuat] CHECK CONSTRAINT [FK_PhieuSanXuat_Invoice]
GO
ALTER TABLE [dbo].[PhieuSanXuat]  WITH CHECK ADD  CONSTRAINT [FK_PhieuSanXuat_LoaiVai] FOREIGN KEY([MaVaiId])
REFERENCES [dbo].[LoaiVai] ([Id])
GO
ALTER TABLE [dbo].[PhieuSanXuat] CHECK CONSTRAINT [FK_PhieuSanXuat_LoaiVai]
GO
ALTER TABLE [dbo].[PhieuSanXuat]  WITH CHECK ADD  CONSTRAINT [FK_PhieuSanXuat_ProductType] FOREIGN KEY([ProductTypeId])
REFERENCES [dbo].[ProductType] ([Id])
GO
ALTER TABLE [dbo].[PhieuSanXuat] CHECK CONSTRAINT [FK_PhieuSanXuat_ProductType]
GO
ALTER TABLE [dbo].[PhieuSanXuat]  WITH CHECK ADD  CONSTRAINT [FK_PhieuSanXuat_Tho] FOREIGN KEY([ThoDoId])
REFERENCES [dbo].[Tho] ([Id])
GO
ALTER TABLE [dbo].[PhieuSanXuat] CHECK CONSTRAINT [FK_PhieuSanXuat_Tho]
GO
ALTER TABLE [dbo].[PhieuSanXuat]  WITH CHECK ADD  CONSTRAINT [FK_PhieuSanXuat_Tho1] FOREIGN KEY([ThoMayId])
REFERENCES [dbo].[Tho] ([Id])
GO
ALTER TABLE [dbo].[PhieuSanXuat] CHECK CONSTRAINT [FK_PhieuSanXuat_Tho1]
GO
ALTER TABLE [dbo].[PhieuSanXuat]  WITH CHECK ADD  CONSTRAINT [FK_PhieuSanXuat_Tho2] FOREIGN KEY([ThoCatId])
REFERENCES [dbo].[Tho] ([Id])
GO
ALTER TABLE [dbo].[PhieuSanXuat] CHECK CONSTRAINT [FK_PhieuSanXuat_Tho2]
GO
ALTER TABLE [dbo].[PhieuSua]  WITH CHECK ADD  CONSTRAINT [FK_PhieuSua_Invoice] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PhieuSua] CHECK CONSTRAINT [FK_PhieuSua_Invoice]
GO
ALTER TABLE [dbo].[PhieuSua]  WITH CHECK ADD  CONSTRAINT [FK_PhieuSua_Tho] FOREIGN KEY([ThoId])
REFERENCES [dbo].[Tho] ([Id])
GO
ALTER TABLE [dbo].[PhieuSua] CHECK CONSTRAINT [FK_PhieuSua_Tho]
GO
ALTER TABLE [dbo].[PhieuSua]  WITH CHECK ADD  CONSTRAINT [FK_PhieuSua_Tho1] FOREIGN KEY([ProblemBy])
REFERENCES [dbo].[Tho] ([Id])
GO
ALTER TABLE [dbo].[PhieuSua] CHECK CONSTRAINT [FK_PhieuSua_Tho1]
GO
ALTER TABLE [dbo].[ProductTyeField]  WITH CHECK ADD  CONSTRAINT [FK_ProductTyeField_ProductType] FOREIGN KEY([ProductTypeId])
REFERENCES [dbo].[ProductType] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductTyeField] CHECK CONSTRAINT [FK_ProductTyeField_ProductType]
GO
ALTER TABLE [dbo].[ProductTypeLoaiVai]  WITH CHECK ADD  CONSTRAINT [FK_ProductTypeLoaiVai_LoaiVai] FOREIGN KEY([MavaiId])
REFERENCES [dbo].[LoaiVai] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductTypeLoaiVai] CHECK CONSTRAINT [FK_ProductTypeLoaiVai_LoaiVai]
GO
ALTER TABLE [dbo].[ProductTypeLoaiVai]  WITH CHECK ADD  CONSTRAINT [FK_ProductTypeLoaiVai_ProductType] FOREIGN KEY([ProductTypeId])
REFERENCES [dbo].[ProductType] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductTypeLoaiVai] CHECK CONSTRAINT [FK_ProductTypeLoaiVai_ProductType]
GO
ALTER TABLE [dbo].[ProductTypeValue]  WITH CHECK ADD  CONSTRAINT [FK_ProductTypeValue_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductTypeValue] CHECK CONSTRAINT [FK_ProductTypeValue_Customer]
GO
ALTER TABLE [dbo].[ProductTypeValue]  WITH CHECK ADD  CONSTRAINT [FK_ProductTypeValue_ProductTyeField] FOREIGN KEY([ProductTypeFieldId])
REFERENCES [dbo].[ProductTyeField] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductTypeValue] CHECK CONSTRAINT [FK_ProductTypeValue_ProductTyeField]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Miêu tả lỗi sản phẩm' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PhieuSua', @level2type=N'COLUMN',@level2name=N'NoiDung'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bảo hành hay khách nhờ sửa' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PhieuSua', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Do người cắt, người may, người đo, hay lý do khác' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PhieuSua', @level2type=N'COLUMN',@level2name=N'ProblemBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1: Do, 2: Cat, 3: May' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tho', @level2type=N'COLUMN',@level2name=N'Type'
GO
USE [master]
GO
ALTER DATABASE [iClassic] SET  READ_WRITE 
GO
