USE [master]
GO
/****** Object:  Database [Remotes]    Script Date: 2021/9/11 下午 06:31:14 ******/
CREATE DATABASE [Remotes]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Remotes', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS01\MSSQL\DATA\Remotes.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Remotes_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS01\MSSQL\DATA\Remotes_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Remotes] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Remotes].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Remotes] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Remotes] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Remotes] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Remotes] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Remotes] SET ARITHABORT OFF 
GO
ALTER DATABASE [Remotes] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Remotes] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Remotes] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Remotes] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Remotes] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Remotes] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Remotes] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Remotes] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Remotes] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Remotes] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Remotes] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Remotes] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Remotes] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Remotes] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Remotes] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Remotes] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Remotes] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Remotes] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Remotes] SET  MULTI_USER 
GO
ALTER DATABASE [Remotes] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Remotes] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Remotes] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Remotes] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Remotes] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Remotes] SET QUERY_STORE = OFF
GO
USE [Remotes]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [Remotes]
GO
/****** Object:  Table [dbo].[APILog]    Script Date: 2021/9/11 下午 06:31:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[APILog](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[TraceID] [varchar](36) NOT NULL,
	[TransName] [varchar](50) NOT NULL,
	[Method] [varchar](10) NOT NULL,
	[RequestData] [nvarchar](max) NOT NULL,
	[ResponseData] [nvarchar](max) NULL,
	[RequestTime] [datetime] NOT NULL,
	[ResponseTime] [datetime] NULL,
 CONSTRAINT [PK_APILog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameProvider]    Script Date: 2021/9/11 下午 06:31:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameProvider](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_GameProvider] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 2021/9/11 下午 06:31:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderID] [varchar](32) NOT NULL,
	[GameProviderID] [bigint] NOT NULL,
	[UserID] [bigint] NOT NULL,
	[Amount] [float] NOT NULL,
	[RefNo] [bigint] NULL,
	[State] [smallint] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifiedTime] [datetime] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2021/9/11 下午 06:31:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Currency] [varchar](3) NOT NULL,
	[Balance] [decimal](19, 6) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[CreateAPILog]    Script Date: 2021/9/11 下午 06:31:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateAPILog]
    @ID bigint,
	@TraceID varchar(36),
	@TransName varchar(50),
	@Method varchar(10),
	@RequestData	nvarchar(MAX),
	@ResponseData nvarchar(MAX),
	@RequestTime datetime,
	@ResponseTime datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[APILog](TraceID,TransName,Method,RequestData,ResponseData,RequestTime,ResponseTime) 
	VALUES(@TraceID,@TransName,@Method,@RequestData,@ResponseData,@RequestTime,@ResponseTime) 
	SELECT CAST(SCOPE_IDENTITY() as bigint)
END
GO
/****** Object:  StoredProcedure [dbo].[CreateOrder]    Script Date: 2021/9/11 下午 06:31:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateOrder]
    @ID bigint,
	@OrderID varchar(32),
	@GameProviderID	bigint,
	@UserID bigint,
	@Amount float,
	@RefNo bigint,
	@State smallint,
	@CreateTime	datetime,
	@ModifiedTime datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[Order](OrderID,GameProviderID,UserID,Amount,RefNo,State,CreateTime,ModifiedTime) 
	VALUES(@OrderID,@GameProviderID,@UserID,@Amount,@RefNo,@State,@CreateTime,@ModifiedTime)
	SELECT CAST(SCOPE_IDENTITY() as bigint)
END
GO
/****** Object:  StoredProcedure [dbo].[CreateUser]    Script Date: 2021/9/11 下午 06:31:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateUser]
	@ID bigint,
	@UserName nvarchar(100),
	@Currency varchar(3),
	@Balance decimal(19, 6)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[User]([UserName],[Currency],[Balance]) 
	VALUES(@UserName,@Currency,@Balance)
	SELECT CAST(SCOPE_IDENTITY() as bigint)
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllAPILog]    Script Date: 2021/9/11 下午 06:31:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllAPILog]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [dbo].[APILog]
END
GO
/****** Object:  StoredProcedure [dbo].[GetTop1OrderByOrderID]    Script Date: 2021/9/11 下午 06:31:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetTop1OrderByOrderID]
	@ID bigint,
	@OrderID varchar(32),
	@GameProviderID	bigint,
	@UserID bigint,
	@Amount float,
	@RefNo bigint,
	@State smallint,
	@CreateTime	datetime,
	@ModifiedTime datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP(1)* FROM [dbo].[Order] WHERE [OrderID] = @OrderID
END
GO
/****** Object:  StoredProcedure [dbo].[GetTop1UserByUserName]    Script Date: 2021/9/11 下午 06:31:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetTop1UserByUserName]
	@ID bigint,
	@UserName nvarchar(100),
	@Currency varchar(3),
	@Balance decimal(19, 6)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP(1)* FROM [dbo].[User] WHERE [UserName] =@UserName
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateAPILogResponseData]    Script Date: 2021/9/11 下午 06:31:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateAPILogResponseData]
	@ID bigint,
	@TraceID varchar(36),
	@TransName varchar(50),
	@Method varchar(10),
	@RequestData	nvarchar(MAX),
	@ResponseData nvarchar(MAX),
	@RequestTime datetime,
	@ResponseTime datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[APILog] SET [ResponseData] = @ResponseData, [ResponseTime] = @ResponseTime WHERE [TraceID] = @TraceID
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateOrderStateByOrderID]    Script Date: 2021/9/11 下午 06:31:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateOrderStateByOrderID]
	@ID bigint,
	@OrderID varchar(32),
	@GameProviderID	bigint,
	@UserID bigint,
	@Amount float,
	@RefNo bigint,
	@State smallint,
	@CreateTime	datetime,
	@ModifiedTime datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[Order] SET [State] = @State, [ModifiedTime] = @ModifiedTime WHERE [OrderID] = @OrderID
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUserBalanceByID]    Script Date: 2021/9/11 下午 06:31:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateUserBalanceByID]
	@ID bigint,
	@UserName nvarchar(100),
	@Currency varchar(3),
	@Balance decimal(19, 6)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[User] SET [Balance] = @Balance WHERE [ID] = @ID
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'遊戲商名稱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameProvider', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'遊戲商ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'遊戲商ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'GameProviderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'會員ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'下注金額' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'Amount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'子單號ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'RefNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'結果 0:Bet, 1:Won, 2:Lose, 3:Draw' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'State'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'下單時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'ModifiedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'會員名稱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'結餘' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Balance'
GO
USE [master]
GO
ALTER DATABASE [Remotes] SET  READ_WRITE 
GO
