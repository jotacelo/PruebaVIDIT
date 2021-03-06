USE [ProductProvider]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 05-04-2018 2:25:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[idProduct] [int] IDENTITY(1,1) NOT NULL,
	[nameProduct] [varchar](100) NOT NULL,
	[listPrice] [money] NOT NULL,
	[color] [varchar](20) NOT NULL,
	[sku] [varchar](50) NULL,
	[upc] [varchar](12) NULL,
	[quantity] [int] NOT NULL,
	[isAvailable] [bit] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[idProduct] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductProvider]    Script Date: 05-04-2018 2:25:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductProvider](
	[idProvider] [int] NOT NULL,
	[idProduct] [int] NOT NULL,
 CONSTRAINT [PK_ProductProvider] PRIMARY KEY CLUSTERED 
(
	[idProvider] ASC,
	[idProduct] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Provider]    Script Date: 05-04-2018 2:25:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Provider](
	[idProvider] [int] IDENTITY(1,1) NOT NULL,
	[nameProvider] [varchar](100) NOT NULL,
	[addressProvider] [varchar](100) NULL,
	[isAviable] [bit] NOT NULL,
	[website] [varchar](100) NULL,
 CONSTRAINT [PK_Provider] PRIMARY KEY CLUSTERED 
(
	[idProvider] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
