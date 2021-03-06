USE [DB_A4AE11_emartapp]
GO
/****** Object:  UserDefinedFunction [dbo].[Split]    Script Date: 7/31/2019 7:08:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[Split]
(
@InputString NVARCHAR(MAX), 
@Separator NVARCHAR(10)
)
RETURNS @tbl TABLE (
ItemValue NVARCHAR(max)
)
AS
BEGIN
    DECLARE @xml XML;
    SELECT    @xml = CAST('<input>' + REPLACE(@InputString, @Separator, '</input><input>') + '</input>' AS XML);
    INSERT INTO @tbl(ItemValue)
    SELECT  Temp.split.value('.', 'NVARCHAR(max)') AS ItemValue
    FROM    @xml.nodes('/input') Temp(split)
    RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[uf_AdminNameByAdminId]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_AdminNameByAdminId] 
(
	@Id bigint
	
)
RETURNS nvarchar(max)
AS
BEGIN  
   DECLARE @Result AS nvarchar(max)
   
   SELECT @Result = [FirstName] + ' ' + [LastName]  from [dbo].[Users] where [UserID] = @Id;
      
   IF @Result IS NULL SET @Result = 'Not Found'
      
   RETURN @Result
END

GO
/****** Object:  UserDefinedFunction [dbo].[uf_AdminRoleNameByRoleID]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_AdminRoleNameByRoleID] 
(
	@Id int
	
)
RETURNS nvarchar(max)
AS
BEGIN  
   DECLARE @Result AS nvarchar(max)
   
   SELECT @Result = [Role]  from [dbo].UserRoles where [Id] = @Id;
      
   IF @Result IS NULL SET @Result = 'Not Found'
      
   RETURN @Result
END

GO
/****** Object:  UserDefinedFunction [dbo].[uf_Get_Admin_Roles_Status_By_UserId_And_RoleId]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_Get_Admin_Roles_Status_By_UserId_And_RoleId] 
(
	@userId bigint,
	@roleId int
	
)
RETURNS int
AS
BEGIN  
   DECLARE @Result int;
   
   SELECT @Result =COUNT(UserId) FROM UserRoleMaps WHERE UserRoleId = @roleId and UserId=@userId
      
   IF @Result IS NULL SET @Result = 0
      
   RETURN @Result
END

GO
/****** Object:  UserDefinedFunction [dbo].[uf_Get_Order_Status_By_Id]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_Get_Order_Status_By_Id] 
(
	@Id bigint
	
)
RETURNS nvarchar(max)
AS
BEGIN  
   DECLARE @Result AS nvarchar(max)
   
   SELECT @Result = Name  from [dbo].[OrderStatus] where [Id] = @Id;
      
   IF @Result IS NULL SET @Result = 'Not Found'
      
   RETURN @Result
END

GO
/****** Object:  UserDefinedFunction [dbo].[uf_Get_Prodcut_category_Name_By_CatID]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_Get_Prodcut_category_Name_By_CatID] 
(
	@Id int
	
)
RETURNS nvarchar(max)
AS
BEGIN  
   DECLARE @Result AS nvarchar(max)
   
   SELECT @Result = Name  from [dbo].[PRODUCT_CATEGORIES] where Id = @Id;
      
   IF @Result IS NULL SET @Result = 'Not Found'
      
   RETURN @Result
END

GO
/****** Object:  UserDefinedFunction [dbo].[uf_Get_Prodcut_category_Path]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_Get_Prodcut_category_Path] 
(
	@Id int
	
)
RETURNS nvarchar(max)
AS
BEGIN  

   DECLARE @Result AS nvarchar(max);
   DECLARE @ParentId int;
   DECLARE @TEMP_TABLE TABLE (id int, ParentCatId int, depth int, CatPath nvarchar(max));
   WITH ctetable(id, ParentCatId, depth, path) as 
	(
		SELECT S.id, S.ParentCatId, 1 AS depth, convert(varchar(100), S.id) AS path
		FROM PRODUCT_CATEGORIES as S
		UNION ALL
		SELECT S2.id, p.ParentCatId, p.depth + 1 AS depth, convert(varchar(100), ( [dbo].[uf_Get_Prodcut_category_Name_By_CatID](p.ParentCatId) +' -> '+ [dbo].[uf_Get_Prodcut_category_Name_By_CatID](RTRIM(p.path)) +' -> '+ [dbo].[uf_Get_Prodcut_category_Name_By_CatID](S2.id)))
		FROM ctetable AS p JOIN PRODUCT_CATEGORIES as S2 on S2.ParentCatId = p.id
		WHERE p.ParentCatId is not null
	)
	
	INSERT INTO @TEMP_TABLE (id, ParentCatId, depth, CatPAth)
	SELECT id, ParentCatId, depth, Path FROM ctetable where id=@Id ORDER BY ParentCatId ;
	
	SELECT @Result = CatPath from @TEMP_TABLE rt where convert(nvarchar(max), rt.ID) <> rt.CatPath order by rt.ParentCatId asc;
	IF @Result IS NULL
		BEGIN
			SELECT @ParentId = ParentCatId from @TEMP_TABLE rt where (convert(nvarchar(max), rt.ID) = rt.CatPath) and rt.ParentCatId is not null order by rt.ParentCatId asc;
			SELECT @Result = [dbo].[uf_Get_Prodcut_category_Name_By_CatID](@ParentId) +' -> '+ [dbo].[uf_Get_Prodcut_category_Name_By_CatID](rt.Id) from @TEMP_TABLE rt where (convert(nvarchar(max), rt.ID) = rt.CatPath) and rt.ParentCatId is not null order by rt.ParentCatId asc;
		
			IF @Result IS NULL and @ParentId is null
				SELECT @Result = [dbo].[uf_Get_Prodcut_category_Name_By_CatID](@id);
		END
	
	
   RETURN @Result
END

GO
/****** Object:  UserDefinedFunction [dbo].[uf_Get_Prodcut_Total_Price_By_QTY_and_Price]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_Get_Prodcut_Total_Price_By_QTY_and_Price] 
(
	@price decimal(18,2),
	@qty int
	
)
RETURNS decimal(18,2)
AS
BEGIN  

	DECLARE @Result  decimal(18,2);

	select @Result = (@price*@qty) 
   if @Result is null set @Result=0

   RETURN @Result
END

GO
/****** Object:  UserDefinedFunction [dbo].[uf_Get_Product_Categories_By_Product_Id]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[uf_Get_Product_Categories_By_Product_Id]
(
	@rowId as int
)
RETURNS  nvarchar(MAX)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @returnValue  nvarchar(MAX);
	SET @returnValue = '';
	Declare @Id int;
	DECLARE @nameTableTemp TABLE(ID INT,Name nvarchar(50))
	Declare @totalRow  int;
    Declare @incrementCounter  int;
	SET @incrementCounter = 1
	
	INSERT INTO @nameTableTemp
	SELECT t.Id, t.Name FROM PRODUCT_CATEGPRY_MAP mp
                INNER JOIN PRODUCT_CATEGORIES t
                 ON
                    mp.CategoryId = t.Id
                WHERE 
                    mp.ProductId=@rowId;



  SELECT @totalRow = Count(*) From @nameTableTemp;

	 While (Select Count(*) From @nameTableTemp) > 0
		Begin

			Select Top 1 @Id = ID From @nameTableTemp

			Select Top 1 @returnValue = @returnValue + Name From @nameTableTemp

			SET @incrementCounter = @incrementCounter + 1;

			IF(@incrementCounter <= @totalRow) 
			BEGIN
			  SET @returnValue = @returnValue +', '
			END

			--Do some processing here

			Delete @nameTableTemp Where @Id = ID

		End

	-- Return the result of the function
	RETURN @returnValue;

END


GO
/****** Object:  UserDefinedFunction [dbo].[uf_Get_Product_Rating_By_ProductId]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_Get_Product_Rating_By_ProductId] 
(
	@Id int
	
)
RETURNS DECIMAL(18,2)
AS
BEGIN  

	DECLARE @TotalRating int;
	DECLARE @TotalReview int;
	DECLARE @AVG DECIMAL(18,2);

	SELECT @TotalRating = SUM(Price+Value+Quality)
	FROM PRODCUT_REVIEWS
	WHERE ProductId=@Id;

	SELECT @TotalReview = COUNT(ProductId)
	FROM PRODCUT_REVIEWS
	WHERE ProductId=@Id;

	SELECT @AVG = (@TotalRating/(@TotalReview*3))
    
	if @AVG is null set @AVG=0
    RETURN @AVG;

END

GO
/****** Object:  UserDefinedFunction [dbo].[uf_Get_User_Roles_With_Comma]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[uf_Get_User_Roles_With_Comma]
(
	@rowId as bigint
)
RETURNS  nvarchar(MAX)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @returnValue  nvarchar(MAX);
	SET @returnValue = '';
	Declare @Id int;
	DECLARE @nameTableTemp TABLE(ID INT,Name nvarchar(50))
	Declare @totalRow  int;
    Declare @incrementCounter  int;
	SET @incrementCounter = 1
	
	INSERT INTO @nameTableTemp
	SELECT t.Id, t.Role FROM UserRoleMaps mp
                INNER JOIN UserRoles t
                 ON
                    mp.UserRoleId = t.Id
                WHERE 
                    mp.UserId=@rowId;



  SELECT @totalRow = Count(*) From @nameTableTemp;

	 While (Select Count(*) From @nameTableTemp) > 0
		Begin

			Select Top 1 @Id = ID From @nameTableTemp

			Select Top 1 @returnValue = @returnValue + Name From @nameTableTemp

			SET @incrementCounter = @incrementCounter + 1;

			IF(@incrementCounter <= @totalRow) 
			BEGIN
			  SET @returnValue = @returnValue +', '
			END

			--Do some processing here

			Delete @nameTableTemp Where @Id = ID

		End

	-- Return the result of the function
	RETURN @returnValue;

END


GO
/****** Object:  UserDefinedFunction [dbo].[uf_Product_Image_By_ProductId]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_Product_Image_By_ProductId] 
(
	@Id int
	
)
RETURNS nvarchar(max)
AS
BEGIN  
   DECLARE @Result AS nvarchar(max)
   
   SELECT @Result = ImageURL  from [dbo].[PRODUCTS] where [Id] = @Id;
      
   RETURN @Result
END

GO
/****** Object:  UserDefinedFunction [dbo].[uf_ProductCategoryNameByCategoryId]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_ProductCategoryNameByCategoryId] 
(
	@Id int
	
)
RETURNS nvarchar(max)
AS
BEGIN  
   DECLARE @Result AS nvarchar(max)
   
   SELECT @Result = [Name] from [dbo].[Categories] where [Id] = @Id;
      
   IF @Result IS NULL SET @Result = 'Not Found'
      
   RETURN @Result
END

GO
/****** Object:  UserDefinedFunction [dbo].[uf_TotalCommentsByPostId]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_TotalCommentsByPostId] 
(
	@Id int
	
)
RETURNS int
AS
BEGIN  
   DECLARE @Result AS int
   
   SELECT @Result = COUNT( * )  from [dbo].[BlogComments] where [PostId] = @Id;
      
   IF @Result IS NULL SET @Result = 0
      
   RETURN @Result
END

GO
/****** Object:  Table [dbo].[BlogComments]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogComments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PostId] [int] NOT NULL,
	[UserId] [bigint] NULL,
	[Name] [nvarchar](50) NULL,
	[Emial] [nvarchar](50) NULL,
	[Coments] [nvarchar](max) NULL,
	[DateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_BlogComments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BlogPosts]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogPosts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[SortDetails] [nvarchar](500) NOT NULL,
	[Descriptions] [nvarchar](max) NOT NULL,
	[FeaturedImagePath] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedId] [bigint] NOT NULL,
	[EditedDate] [datetime] NULL,
	[EditedId] [bigint] NULL,
 CONSTRAINT [PK_BlogPosts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categories]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CountryList]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CountryList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ISOCode] [nvarchar](10) NULL,
	[NameCapitalaize] [nvarchar](500) NULL,
	[Name] [nvarchar](500) NULL,
	[ISO] [nvarchar](10) NULL,
	[NumCode] [int] NULL,
	[PhoneCode] [int] NULL,
 CONSTRAINT [PK_CountryList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FONT_AWESOME_ICONS]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FONT_AWESOME_ICONS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IconClass] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_FONT_AWESOME_ICONS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrderStatus]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_OrderStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PRODCUT_REVIEWS]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODCUT_REVIEWS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Price] [int] NOT NULL,
	[Value] [int] NOT NULL,
	[Quality] [int] NOT NULL,
	[CustomerId] [bigint] NULL,
	[CustomerEmail] [nvarchar](200) NOT NULL,
	[CustomerName] [nvarchar](200) NOT NULL,
	[Summery] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ReviewDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PRODCUT_REVIEWS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PRODUCT_CATEGORIES]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCT_CATEGORIES](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[SortName] [nvarchar](max) NOT NULL,
	[IconClass] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[ImageURL] [nvarchar](max) NULL,
	[ParentCatId] [int] NULL,
	[ShowOnHomePage] [bit] NOT NULL,
	[ShowOnTopMenu] [bit] NOT NULL,
	[Published] [bit] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedId] [bigint] NOT NULL,
	[EditedDate] [datetime] NULL,
	[EditedId] [bigint] NULL,
 CONSTRAINT [PK_PRODUCT_CATEGORIES] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PRODUCT_CATEGPRY_MAP]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCT_CATEGPRY_MAP](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_PRODUCT_CATEGPRY_MAP] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PRODUCT_DISCOUNTS]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCT_DISCOUNTS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PRODUCT_DISCOUNTS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PRODUCT_IMAGE_MAP]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCT_IMAGE_MAP](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[ImagePath] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PRODUCT_IMAGE_MAP] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PRODUCT_INVENTORY_TYPE]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCT_INVENTORY_TYPE](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PRODUCT_INVENTORY_TYPE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PRODUCT_ORDER_MAP]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCT_ORDER_MAP](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductName] [nvarchar](500) NOT NULL,
	[QTY] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[TotalPrice] [decimal](18, 2) NOT NULL,
	[OrderDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PRODUCT_ORDER_MAP] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PRODUCT_ORDERS]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCT_ORDERS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductPrice] [decimal](18, 2) NULL,
	[ShippingCost] [decimal](18, 2) NULL,
	[TotalAmount] [decimal](18, 2) NULL,
	[OrderStatusId] [int] NULL,
	[CustomerId] [bigint] NULL,
	[CustomerName] [nvarchar](200) NULL,
	[CustomerEmail] [nvarchar](200) NULL,
	[CustomerMobile] [nvarchar](15) NULL,
	[Country] [nvarchar](100) NULL,
	[State] [nvarchar](500) NULL,
	[ZipCode] [nvarchar](50) NULL,
	[AddressDetails] [nvarchar](max) NULL,
	[CouponCode] [nvarchar](50) NULL,
	[TempCookie] [nvarchar](50) NULL,
	[OrderDateTime] [datetime] NOT NULL,
	[PaymentMethod] [nvarchar](500) NULL,
	[PaymentStatus] [bit] NOT NULL,
	[bKashTransectionNumber] [nvarchar](50) NULL,
	[RoketTransectionNumber] [nvarchar](50) NULL,
	[ShipDifferentAddress] [bit] NOT NULL,
	[ShipName] [nvarchar](200) NULL,
	[ShipEmail] [nvarchar](200) NULL,
	[ShipPhone] [nvarchar](200) NULL,
	[ShipCity] [nvarchar](200) NULL,
	[ShipPostCode] [nvarchar](50) NULL,
	[ShipAddressDetails] [nvarchar](max) NULL,
	[ShipCountry] [nvarchar](50) NULL,
 CONSTRAINT [PK_PRODUCT_ORDERS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PRODUCTS]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCTS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ImageURL] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[ProductKeyWords] [nvarchar](max) NOT NULL,
	[SortDescription] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[SKU] [nvarchar](50) NULL,
	[Published] [bit] NOT NULL,
	[IsShowIndividualy] [bit] NOT NULL,
	[ShowOnHomePage] [bit] NOT NULL,
	[AvailableSatartDate] [datetime] NULL,
	[AvailableEndDate] [datetime] NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[OldPrice] [decimal](18, 2) NULL,
	[ProductCost] [decimal](18, 2) NULL,
	[AffeliateCommision] [decimal](18, 2) NULL,
	[DiscountId] [int] NULL,
	[TaxExempt] [bit] NOT NULL,
	[TaxCategoryId] [int] NULL,
	[InventoryMethodId] [int] NOT NULL,
	[StockQuantity] [int] NULL,
	[ShippingEnabled] [bit] NOT NULL,
	[ShipWeight] [decimal](18, 2) NULL,
	[ShipLength] [decimal](18, 2) NULL,
	[ShipWidth] [decimal](18, 2) NULL,
	[ShipHeight] [decimal](18, 2) NULL,
	[IsFreeShipping] [bit] NOT NULL,
	[ShipSeperetly] [bit] NOT NULL,
	[VendorId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedId] [bigint] NOT NULL,
	[EditedDate] [datetime] NULL,
	[EditedId] [bigint] NULL,
 CONSTRAINT [PK_PRODUCTS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SiteSettings]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiteSettings](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LogoURL] [nvarchar](max) NOT NULL,
	[FaviconURL] [nvarchar](max) NOT NULL,
	[HeaderContactInfo] [nvarchar](max) NOT NULL,
	[WelcomeMessage] [nvarchar](50) NOT NULL,
	[HomePageSliderItem] [int] NOT NULL,
	[CategoryIdOne] [int] NOT NULL,
	[CategoryIdTwo] [int] NOT NULL,
	[CategoryIdThree] [int] NOT NULL,
	[CategoryIdFour] [int] NOT NULL,
	[CategoryIdFive] [int] NOT NULL,
	[H_SectionOneTitle] [nvarchar](100) NOT NULL,
	[H_SectionTwoTitle] [nvarchar](100) NOT NULL,
	[H_SectionThreeTitle] [nvarchar](100) NOT NULL,
	[H_SectionFourTitle] [nvarchar](100) NOT NULL,
	[H_BannerURL_Left] [nvarchar](max) NOT NULL,
	[H_BannerLink_Left] [nvarchar](200) NOT NULL,
	[H_BannerURL_CenterOne] [nvarchar](max) NOT NULL,
	[H_BannerLink_CenterOne] [nvarchar](200) NOT NULL,
	[H_BannerURL_CenterTwo] [nvarchar](max) NOT NULL,
	[H_BannerLink_CenterTwo] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_SiteSettings] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sliders]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sliders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Caption] [nvarchar](50) NOT NULL,
	[SortDescription] [nvarchar](500) NOT NULL,
	[SliderIsActive] [bit] NOT NULL,
	[SliderTypeId] [int] NOT NULL,
	[CategoryId] [int] NULL,
	[ImagePath] [nvarchar](max) NOT NULL,
	[ThumbnailPath] [nvarchar](max) NOT NULL,
	[AuthorName] [nvarchar](50) NULL,
	[CreatedId] [bigint] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Sliders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SliderType]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SliderType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SliderType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TempCartItems]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TempCartItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NULL,
	[CookieId] [nvarchar](100) NULL,
	[ProductId] [int] NOT NULL,
	[QTY] [int] NOT NULL,
	[CartDateTime] [datetime] NULL,
 CONSTRAINT [PK_TempCartItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserRoleMaps]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoleMaps](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[UserRoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserRoleMaps] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 7/31/2019 7:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [bigint] IDENTITY(1,1) NOT NULL,
	[ManagerOfVendor] [bigint] NULL,
	[FirstName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[UserImagePath] [nvarchar](max) NULL,
	[AffeliateId] [nvarchar](50) NULL,
	[IsTaxExempt] [bit] NOT NULL,
	[AdminComments] [nvarchar](max) NULL,
	[IsNewsLetter] [bit] NOT NULL,
	[AddressOne] [nvarchar](200) NULL,
	[AddressTwo] [nvarchar](200) NULL,
	[City] [nvarchar](100) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[PostCode] [nvarchar](50) NULL,
	[Country] [nvarchar](100) NULL,
	[EmailAddress] [nvarchar](200) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[ConfirmPassword] [nvarchar](100) NULL,
	[CurrentPassword] [nvarchar](max) NULL,
	[RegistrationConfirmed] [bit] NOT NULL,
	[IsActivated] [bit] NOT NULL,
	[UserActivateKey] [nvarchar](500) NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[LastActiveTime] [datetime] NOT NULL,
	[EditedBy] [bigint] NULL,
	[EditedDate] [datetime] NULL,
	[CreatedIP] [nvarchar](500) NULL,
	[EditedIP] [nvarchar](500) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name]) VALUES (1, N'Food')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (2, N'Electronics')
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[CountryList] ON 

INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (1, N'AF', N'AFGHANISTAN', N'Afghanistan', N'AFG', 4, 93)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (2, N'AL', N'ALBANIA', N'Albania', N'ALB', 8, 355)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (3, N'DZ', N'ALGERIA', N'Algeria', N'DZA', 12, 213)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (4, N'AS', N'AMERICAN SAMOA', N'American Samoa', N'ASM', 16, 1684)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (5, N'AD', N'ANDORRA', N'Andorra', N'AND', 20, 376)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (6, N'AO', N'ANGOLA', N'Angola', N'AGO', 24, 244)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (7, N'AI', N'ANGUILLA', N'Anguilla', N'AIA', 660, 1264)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (8, N'AQ', N'ANTARCTICA', N'Antarctica', NULL, NULL, 0)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (9, N'AG', N'ANTIGUA AND BARBUDA', N'Antigua and Barbuda', N'ATG', 28, 1268)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (10, N'AR', N'ARGENTINA', N'Argentina', N'ARG', 32, 54)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (11, N'AM', N'ARMENIA', N'Armenia', N'ARM', 51, 374)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (12, N'AW', N'ARUBA', N'Aruba', N'ABW', 533, 297)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (13, N'AU', N'AUSTRALIA', N'Australia', N'AUS', 36, 61)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (14, N'AT', N'AUSTRIA', N'Austria', N'AUT', 40, 43)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (15, N'AZ', N'AZERBAIJAN', N'Azerbaijan', N'AZE', 31, 994)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (16, N'BS', N'BAHAMAS', N'Bahamas', N'BHS', 44, 1242)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (17, N'BH', N'BAHRAIN', N'Bahrain', N'BHR', 48, 973)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (18, N'BD', N'BANGLADESH', N'Bangladesh', N'BGD', 50, 880)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (19, N'BB', N'BARBADOS', N'Barbados', N'BRB', 52, 1246)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (20, N'BY', N'BELARUS', N'Belarus', N'BLR', 112, 375)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (21, N'BE', N'BELGIUM', N'Belgium', N'BEL', 56, 32)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (22, N'BZ', N'BELIZE', N'Belize', N'BLZ', 84, 501)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (23, N'BJ', N'BENIN', N'Benin', N'BEN', 204, 229)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (24, N'BM', N'BERMUDA', N'Bermuda', N'BMU', 60, 1441)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (25, N'BT', N'BHUTAN', N'Bhutan', N'BTN', 64, 975)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (26, N'BO', N'BOLIVIA', N'Bolivia', N'BOL', 68, 591)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (27, N'BA', N'BOSNIA AND HERZEGOVINA', N'Bosnia and Herzegovina', N'BIH', 70, 387)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (28, N'BW', N'BOTSWANA', N'Botswana', N'BWA', 72, 267)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (29, N'BV', N'BOUVET ISLAND', N'Bouvet Island', NULL, NULL, 0)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (30, N'BR', N'BRAZIL', N'Brazil', N'BRA', 76, 55)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (31, N'IO', N'BRITISH INDIAN OCEAN TERRITORY', N'British Indian Ocean Territory', NULL, NULL, 246)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (32, N'BN', N'BRUNEI DARUSSALAM', N'Brunei Darussalam', N'BRN', 96, 673)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (33, N'BG', N'BULGARIA', N'Bulgaria', N'BGR', 100, 359)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (34, N'BF', N'BURKINA FASO', N'Burkina Faso', N'BFA', 854, 226)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (35, N'BI', N'BURUNDI', N'Burundi', N'BDI', 108, 257)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (36, N'KH', N'CAMBODIA', N'Cambodia', N'KHM', 116, 855)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (37, N'CM', N'CAMEROON', N'Cameroon', N'CMR', 120, 237)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (38, N'CA', N'CANADA', N'Canada', N'CAN', 124, 1)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (39, N'CV', N'CAPE VERDE', N'Cape Verde', N'CPV', 132, 238)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (40, N'KY', N'CAYMAN ISLANDS', N'Cayman Islands', N'CYM', 136, 1345)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (41, N'CF', N'CENTRAL AFRICAN REPUBLIC', N'Central African Republic', N'CAF', 140, 236)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (42, N'TD', N'CHAD', N'Chad', N'TCD', 148, 235)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (43, N'CL', N'CHILE', N'Chile', N'CHL', 152, 56)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (44, N'CN', N'CHINA', N'China', N'CHN', 156, 86)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (45, N'CX', N'CHRISTMAS ISLAND', N'Christmas Island', NULL, NULL, 61)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (46, N'CC', N'COCOS (KEELING) ISLANDS', N'Cocos (Keeling) Islands', NULL, NULL, 672)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (47, N'CO', N'COLOMBIA', N'Colombia', N'COL', 170, 57)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (48, N'KM', N'COMOROS', N'Comoros', N'COM', 174, 269)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (49, N'CG', N'CONGO', N'Congo', N'COG', 178, 242)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (50, N'CD', N'CONGO, THE DEMOCRATIC REPUBLIC OF THE', N'Congo, the Democratic Republic of the', N'COD', 180, 242)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (51, N'CK', N'COOK ISLANDS', N'Cook Islands', N'COK', 184, 682)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (52, N'CR', N'COSTA RICA', N'Costa Rica', N'CRI', 188, 506)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (53, N'CI', N'COTE D''IVOIRE', N'Cote D''Ivoire', N'CIV', 384, 225)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (54, N'HR', N'CROATIA', N'Croatia', N'HRV', 191, 385)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (55, N'CU', N'CUBA', N'Cuba', N'CUB', 192, 53)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (56, N'CY', N'CYPRUS', N'Cyprus', N'CYP', 196, 357)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (57, N'CZ', N'CZECH REPUBLIC', N'Czech Republic', N'CZE', 203, 420)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (58, N'DK', N'DENMARK', N'Denmark', N'DNK', 208, 45)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (59, N'DJ', N'DJIBOUTI', N'Djibouti', N'DJI', 262, 253)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (60, N'DM', N'DOMINICA', N'Dominica', N'DMA', 212, 1767)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (61, N'DO', N'DOMINICAN REPUBLIC', N'Dominican Republic', N'DOM', 214, 1809)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (62, N'EC', N'ECUADOR', N'Ecuador', N'ECU', 218, 593)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (63, N'EG', N'EGYPT', N'Egypt', N'EGY', 818, 20)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (64, N'SV', N'EL SALVADOR', N'El Salvador', N'SLV', 222, 503)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (65, N'GQ', N'EQUATORIAL GUINEA', N'Equatorial Guinea', N'GNQ', 226, 240)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (66, N'ER', N'ERITREA', N'Eritrea', N'ERI', 232, 291)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (67, N'EE', N'ESTONIA', N'Estonia', N'EST', 233, 372)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (68, N'ET', N'ETHIOPIA', N'Ethiopia', N'ETH', 231, 251)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (69, N'FK', N'FALKLAND ISLANDS (MALVINAS)', N'Falkland Islands (Malvinas)', N'FLK', 238, 500)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (70, N'FO', N'FAROE ISLANDS', N'Faroe Islands', N'FRO', 234, 298)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (71, N'FJ', N'FIJI', N'Fiji', N'FJI', 242, 679)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (72, N'FI', N'FINLAND', N'Finland', N'FIN', 246, 358)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (73, N'FR', N'FRANCE', N'France', N'FRA', 250, 33)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (74, N'GF', N'FRENCH GUIANA', N'French Guiana', N'GUF', 254, 594)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (75, N'PF', N'FRENCH POLYNESIA', N'French Polynesia', N'PYF', 258, 689)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (76, N'TF', N'FRENCH SOUTHERN TERRITORIES', N'French Southern Territories', NULL, NULL, 0)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (77, N'GA', N'GABON', N'Gabon', N'GAB', 266, 241)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (78, N'GM', N'GAMBIA', N'Gambia', N'GMB', 270, 220)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (79, N'GE', N'GEORGIA', N'Georgia', N'GEO', 268, 995)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (80, N'DE', N'GERMANY', N'Germany', N'DEU', 276, 49)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (81, N'GH', N'GHANA', N'Ghana', N'GHA', 288, 233)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (82, N'GI', N'GIBRALTAR', N'Gibraltar', N'GIB', 292, 350)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (83, N'GR', N'GREECE', N'Greece', N'GRC', 300, 30)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (84, N'GL', N'GREENLAND', N'Greenland', N'GRL', 304, 299)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (85, N'GD', N'GRENADA', N'Grenada', N'GRD', 308, 1473)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (86, N'GP', N'GUADELOUPE', N'Guadeloupe', N'GLP', 312, 590)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (87, N'GU', N'GUAM', N'Guam', N'GUM', 316, 1671)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (88, N'GT', N'GUATEMALA', N'Guatemala', N'GTM', 320, 502)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (89, N'GN', N'GUINEA', N'Guinea', N'GIN', 324, 224)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (90, N'GW', N'GUINEA-BISSAU', N'Guinea-Bissau', N'GNB', 624, 245)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (91, N'GY', N'GUYANA', N'Guyana', N'GUY', 328, 592)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (92, N'HT', N'HAITI', N'Haiti', N'HTI', 332, 509)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (93, N'HM', N'HEARD ISLAND AND MCDONALD ISLANDS', N'Heard Island and Mcdonald Islands', NULL, NULL, 0)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (94, N'VA', N'HOLY SEE (VATICAN CITY STATE)', N'Holy See (Vatican City State)', N'VAT', 336, 39)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (95, N'HN', N'HONDURAS', N'Honduras', N'HND', 340, 504)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (96, N'HK', N'HONG KONG', N'Hong Kong', N'HKG', 344, 852)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (97, N'HU', N'HUNGARY', N'Hungary', N'HUN', 348, 36)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (98, N'IS', N'ICELAND', N'Iceland', N'ISL', 352, 354)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (99, N'IN', N'INDIA', N'India', N'IND', 356, 91)
GO
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (100, N'ID', N'INDONESIA', N'Indonesia', N'IDN', 360, 62)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (101, N'IR', N'IRAN, ISLAMIC REPUBLIC OF', N'Iran, Islamic Republic of', N'IRN', 364, 98)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (102, N'IQ', N'IRAQ', N'Iraq', N'IRQ', 368, 964)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (103, N'IE', N'IRELAND', N'Ireland', N'IRL', 372, 353)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (104, N'IL', N'ISRAEL', N'Israel', N'ISR', 376, 972)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (105, N'IT', N'ITALY', N'Italy', N'ITA', 380, 39)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (106, N'JM', N'JAMAICA', N'Jamaica', N'JAM', 388, 1876)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (107, N'JP', N'JAPAN', N'Japan', N'JPN', 392, 81)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (108, N'JO', N'JORDAN', N'Jordan', N'JOR', 400, 962)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (109, N'KZ', N'KAZAKHSTAN', N'Kazakhstan', N'KAZ', 398, 7)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (110, N'KE', N'KENYA', N'Kenya', N'KEN', 404, 254)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (111, N'KI', N'KIRIBATI', N'Kiribati', N'KIR', 296, 686)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (112, N'KP', N'KOREA, DEMOCRATIC PEOPLE''S REPUBLIC OF', N'Korea, Democratic People''s Republic of', N'PRK', 408, 850)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (113, N'KR', N'KOREA, REPUBLIC OF', N'Korea, Republic of', N'KOR', 410, 82)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (114, N'KW', N'KUWAIT', N'Kuwait', N'KWT', 414, 965)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (115, N'KG', N'KYRGYZSTAN', N'Kyrgyzstan', N'KGZ', 417, 996)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (116, N'LA', N'LAO PEOPLE''S DEMOCRATIC REPUBLIC', N'Lao People''s Democratic Republic', N'LAO', 418, 856)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (117, N'LV', N'LATVIA', N'Latvia', N'LVA', 428, 371)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (118, N'LB', N'LEBANON', N'Lebanon', N'LBN', 422, 961)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (119, N'LS', N'LESOTHO', N'Lesotho', N'LSO', 426, 266)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (120, N'LR', N'LIBERIA', N'Liberia', N'LBR', 430, 231)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (121, N'LY', N'LIBYAN ARAB JAMAHIRIYA', N'Libyan Arab Jamahiriya', N'LBY', 434, 218)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (122, N'LI', N'LIECHTENSTEIN', N'Liechtenstein', N'LIE', 438, 423)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (123, N'LT', N'LITHUANIA', N'Lithuania', N'LTU', 440, 370)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (124, N'LU', N'LUXEMBOURG', N'Luxembourg', N'LUX', 442, 352)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (125, N'MO', N'MACAO', N'Macao', N'MAC', 446, 853)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (126, N'MK', N'MACEDONIA, THE FORMER YUGOSLAV REPUBLIC OF', N'Macedonia, the Former Yugoslav Republic of', N'MKD', 807, 389)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (127, N'MG', N'MADAGASCAR', N'Madagascar', N'MDG', 450, 261)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (128, N'MW', N'MALAWI', N'Malawi', N'MWI', 454, 265)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (129, N'MY', N'MALAYSIA', N'Malaysia', N'MYS', 458, 60)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (130, N'MV', N'MALDIVES', N'Maldives', N'MDV', 462, 960)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (131, N'ML', N'MALI', N'Mali', N'MLI', 466, 223)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (132, N'MT', N'MALTA', N'Malta', N'MLT', 470, 356)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (133, N'MH', N'MARSHALL ISLANDS', N'Marshall Islands', N'MHL', 584, 692)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (134, N'MQ', N'MARTINIQUE', N'Martinique', N'MTQ', 474, 596)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (135, N'MR', N'MAURITANIA', N'Mauritania', N'MRT', 478, 222)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (136, N'MU', N'MAURITIUS', N'Mauritius', N'MUS', 480, 230)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (137, N'YT', N'MAYOTTE', N'Mayotte', NULL, NULL, 269)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (138, N'MX', N'MEXICO', N'Mexico', N'MEX', 484, 52)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (139, N'FM', N'MICRONESIA, FEDERATED STATES OF', N'Micronesia, Federated States of', N'FSM', 583, 691)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (140, N'MD', N'MOLDOVA, REPUBLIC OF', N'Moldova, Republic of', N'MDA', 498, 373)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (141, N'MC', N'MONACO', N'Monaco', N'MCO', 492, 377)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (142, N'MN', N'MONGOLIA', N'Mongolia', N'MNG', 496, 976)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (143, N'MS', N'MONTSERRAT', N'Montserrat', N'MSR', 500, 1664)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (144, N'MA', N'MOROCCO', N'Morocco', N'MAR', 504, 212)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (145, N'MZ', N'MOZAMBIQUE', N'Mozambique', N'MOZ', 508, 258)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (146, N'MM', N'MYANMAR', N'Myanmar', N'MMR', 104, 95)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (147, N'NA', N'NAMIBIA', N'Namibia', N'NAM', 516, 264)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (148, N'NR', N'NAURU', N'Nauru', N'NRU', 520, 674)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (149, N'NP', N'NEPAL', N'Nepal', N'NPL', 524, 977)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (150, N'NL', N'NETHERLANDS', N'Netherlands', N'NLD', 528, 31)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (151, N'AN', N'NETHERLANDS ANTILLES', N'Netherlands Antilles', N'ANT', 530, 599)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (152, N'NC', N'NEW CALEDONIA', N'New Caledonia', N'NCL', 540, 687)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (153, N'NZ', N'NEW ZEALAND', N'New Zealand', N'NZL', 554, 64)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (154, N'NI', N'NICARAGUA', N'Nicaragua', N'NIC', 558, 505)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (155, N'NE', N'NIGER', N'Niger', N'NER', 562, 227)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (156, N'NG', N'NIGERIA', N'Nigeria', N'NGA', 566, 234)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (157, N'NU', N'NIUE', N'Niue', N'NIU', 570, 683)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (158, N'NF', N'NORFOLK ISLAND', N'Norfolk Island', N'NFK', 574, 672)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (159, N'MP', N'NORTHERN MARIANA ISLANDS', N'Northern Mariana Islands', N'MNP', 580, 1670)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (160, N'NO', N'NORWAY', N'Norway', N'NOR', 578, 47)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (161, N'OM', N'OMAN', N'Oman', N'OMN', 512, 968)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (162, N'PK', N'PAKISTAN', N'Pakistan', N'PAK', 586, 92)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (163, N'PW', N'PALAU', N'Palau', N'PLW', 585, 680)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (164, N'PS', N'PALESTINIAN TERRITORY, OCCUPIED', N'Palestinian Territory, Occupied', NULL, NULL, 970)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (165, N'PA', N'PANAMA', N'Panama', N'PAN', 591, 507)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (166, N'PG', N'PAPUA NEW GUINEA', N'Papua New Guinea', N'PNG', 598, 675)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (167, N'PY', N'PARAGUAY', N'Paraguay', N'PRY', 600, 595)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (168, N'PE', N'PERU', N'Peru', N'PER', 604, 51)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (169, N'PH', N'PHILIPPINES', N'Philippines', N'PHL', 608, 63)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (170, N'PN', N'PITCAIRN', N'Pitcairn', N'PCN', 612, 0)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (171, N'PL', N'POLAND', N'Poland', N'POL', 616, 48)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (172, N'PT', N'PORTUGAL', N'Portugal', N'PRT', 620, 351)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (173, N'PR', N'PUERTO RICO', N'Puerto Rico', N'PRI', 630, 1787)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (174, N'QA', N'QATAR', N'Qatar', N'QAT', 634, 974)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (175, N'RE', N'REUNION', N'Reunion', N'REU', 638, 262)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (176, N'RO', N'ROMANIA', N'Romania', N'ROM', 642, 40)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (177, N'RU', N'RUSSIAN FEDERATION', N'Russian Federation', N'RUS', 643, 70)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (178, N'RW', N'RWANDA', N'Rwanda', N'RWA', 646, 250)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (179, N'SH', N'SAINT HELENA', N'Saint Helena', N'SHN', 654, 290)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (180, N'KN', N'SAINT KITTS AND NEVIS', N'Saint Kitts and Nevis', N'KNA', 659, 1869)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (181, N'LC', N'SAINT LUCIA', N'Saint Lucia', N'LCA', 662, 1758)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (182, N'PM', N'SAINT PIERRE AND MIQUELON', N'Saint Pierre and Miquelon', N'SPM', 666, 508)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (183, N'VC', N'SAINT VINCENT AND THE GRENADINES', N'Saint Vincent and the Grenadines', N'VCT', 670, 1784)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (184, N'WS', N'SAMOA', N'Samoa', N'WSM', 882, 684)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (185, N'SM', N'SAN MARINO', N'San Marino', N'SMR', 674, 378)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (186, N'ST', N'SAO TOME AND PRINCIPE', N'Sao Tome and Principe', N'STP', 678, 239)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (187, N'SA', N'SAUDI ARABIA', N'Saudi Arabia', N'SAU', 682, 966)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (188, N'SN', N'SENEGAL', N'Senegal', N'SEN', 686, 221)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (189, N'CS', N'SERBIA AND MONTENEGRO', N'Serbia and Montenegro', NULL, NULL, 381)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (190, N'SC', N'SEYCHELLES', N'Seychelles', N'SYC', 690, 248)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (191, N'SL', N'SIERRA LEONE', N'Sierra Leone', N'SLE', 694, 232)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (192, N'SG', N'SINGAPORE', N'Singapore', N'SGP', 702, 65)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (193, N'SK', N'SLOVAKIA', N'Slovakia', N'SVK', 703, 421)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (194, N'SI', N'SLOVENIA', N'Slovenia', N'SVN', 705, 386)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (195, N'SB', N'SOLOMON ISLANDS', N'Solomon Islands', N'SLB', 90, 677)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (196, N'SO', N'SOMALIA', N'Somalia', N'SOM', 706, 252)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (197, N'ZA', N'SOUTH AFRICA', N'South Africa', N'ZAF', 710, 27)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (198, N'GS', N'SOUTH GEORGIA AND THE SOUTH SANDWICH ISLANDS', N'South Georgia and the South Sandwich Islands', NULL, NULL, 0)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (199, N'ES', N'SPAIN', N'Spain', N'ESP', 724, 34)
GO
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (200, N'LK', N'SRI LANKA', N'Sri Lanka', N'LKA', 144, 94)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (201, N'SD', N'SUDAN', N'Sudan', N'SDN', 736, 249)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (202, N'SR', N'SURINAME', N'Suriname', N'SUR', 740, 597)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (203, N'SJ', N'SVALBARD AND JAN MAYEN', N'Svalbard and Jan Mayen', N'SJM', 744, 47)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (204, N'SZ', N'SWAZILAND', N'Swaziland', N'SWZ', 748, 268)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (205, N'SE', N'SWEDEN', N'Sweden', N'SWE', 752, 46)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (206, N'CH', N'SWITZERLAND', N'Switzerland', N'CHE', 756, 41)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (207, N'SY', N'SYRIAN ARAB REPUBLIC', N'Syrian Arab Republic', N'SYR', 760, 963)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (208, N'TW', N'TAIWAN, PROVINCE OF CHINA', N'Taiwan, Province of China', N'TWN', 158, 886)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (209, N'TJ', N'TAJIKISTAN', N'Tajikistan', N'TJK', 762, 992)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (210, N'TZ', N'TANZANIA, UNITED REPUBLIC OF', N'Tanzania, United Republic of', N'TZA', 834, 255)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (211, N'TH', N'THAILAND', N'Thailand', N'THA', 764, 66)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (212, N'TL', N'TIMOR-LESTE', N'Timor-Leste', NULL, NULL, 670)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (213, N'TG', N'TOGO', N'Togo', N'TGO', 768, 228)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (214, N'TK', N'TOKELAU', N'Tokelau', N'TKL', 772, 690)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (215, N'TO', N'TONGA', N'Tonga', N'TON', 776, 676)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (216, N'TT', N'TRINIDAD AND TOBAGO', N'Trinidad and Tobago', N'TTO', 780, 1868)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (217, N'TN', N'TUNISIA', N'Tunisia', N'TUN', 788, 216)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (218, N'TR', N'TURKEY', N'Turkey', N'TUR', 792, 90)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (219, N'TM', N'TURKMENISTAN', N'Turkmenistan', N'TKM', 795, 7370)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (220, N'TC', N'TURKS AND CAICOS ISLANDS', N'Turks and Caicos Islands', N'TCA', 796, 1649)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (221, N'TV', N'TUVALU', N'Tuvalu', N'TUV', 798, 688)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (222, N'UG', N'UGANDA', N'Uganda', N'UGA', 800, 256)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (223, N'UA', N'UKRAINE', N'Ukraine', N'UKR', 804, 380)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (224, N'AE', N'UNITED ARAB EMIRATES', N'United Arab Emirates', N'ARE', 784, 971)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (225, N'GB', N'UNITED KINGDOM', N'United Kingdom', N'GBR', 826, 44)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (226, N'US', N'UNITED STATES', N'United States', N'USA', 840, 1)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (227, N'UM', N'UNITED STATES MINOR OUTLYING ISLANDS', N'United States Minor Outlying Islands', NULL, NULL, 1)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (228, N'UY', N'URUGUAY', N'Uruguay', N'URY', 858, 598)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (229, N'UZ', N'UZBEKISTAN', N'Uzbekistan', N'UZB', 860, 998)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (230, N'VU', N'VANUATU', N'Vanuatu', N'VUT', 548, 678)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (231, N'VE', N'VENEZUELA', N'Venezuela', N'VEN', 862, 58)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (232, N'VN', N'VIET NAM', N'Viet Nam', N'VNM', 704, 84)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (233, N'VG', N'VIRGIN ISLANDS, BRITISH', N'Virgin Islands, British', N'VGB', 92, 1284)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (234, N'VI', N'VIRGIN ISLANDS, U.S.', N'Virgin Islands, U.s.', N'VIR', 850, 1340)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (235, N'WF', N'WALLIS AND FUTUNA', N'Wallis and Futuna', N'WLF', 876, 681)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (236, N'EH', N'WESTERN SAHARA', N'Western Sahara', N'ESH', 732, 212)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (237, N'YE', N'YEMEN', N'Yemen', N'YEM', 887, 967)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (238, N'ZM', N'ZAMBIA', N'Zambia', N'ZMB', 894, 260)
INSERT [dbo].[CountryList] ([ID], [ISOCode], [NameCapitalaize], [Name], [ISO], [NumCode], [PhoneCode]) VALUES (239, N'ZW', N'ZIMBABWE', N'Zimbabwe', N'ZWE', 716, 263)
SET IDENTITY_INSERT [dbo].[CountryList] OFF
SET IDENTITY_INSERT [dbo].[FONT_AWESOME_ICONS] ON 

INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (1, N'fa-glass')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (2, N'fa-music')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (3, N'fa-search')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (4, N'fa-envelope-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (5, N'fa-heart')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (6, N'fa-star')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (7, N'fa-star-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (8, N'fa-user')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (9, N'fa-film')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (10, N'fa-th-large')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (11, N'fa-th')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (12, N'fa-th-list')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (13, N'fa-check')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (14, N'fa-remove')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (15, N'fa-close')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (16, N'fa-times')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (17, N'fa-search-plus')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (18, N'fa-search-minus')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (19, N'fa-power-off')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (20, N'fa-signal')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (21, N'fa-gear')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (22, N'fa-cog')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (23, N'fa-trash-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (24, N'fa-home')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (25, N'fa-file-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (26, N'fa-clock-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (27, N'fa-road')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (28, N'fa-download')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (29, N'fa-arrow-circle-o-do')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (30, N'fa-arrow-circle-o-up')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (31, N'fa-inbox')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (32, N'fa-play-circle-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (33, N'fa-rotate-right')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (34, N'fa-repeat')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (35, N'fa-refresh')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (36, N'fa-list-alt')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (37, N'fa-lock')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (38, N'fa-flag')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (39, N'fa-headphones')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (40, N'fa-volume-off')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (41, N'fa-volume-down')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (42, N'fa-volume-up')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (43, N'fa-qrcode')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (44, N'fa-barcode')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (45, N'fa-tag')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (46, N'fa-tags')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (47, N'fa-book')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (48, N'fa-bookmark')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (49, N'fa-print f')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (50, N'fa-camera')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (51, N'fa-font')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (52, N'fa-bold')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (53, N'fa-italic')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (54, N'fa-text-height')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (55, N'fa-text-width')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (56, N'fa-align-left')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (57, N'fa-align-center')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (58, N'fa-align-right')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (59, N'fa-align-justify')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (60, N'fa-list')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (61, N'fa-dedent')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (62, N'fa-outdent')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (63, N'fa-indent')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (64, N'fa-video-camera')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (65, N'fa-photo')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (66, N'fa-image')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (67, N'fa-picture-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (68, N'fa-pencil')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (69, N'fa-map-marker')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (70, N'fa-adjust')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (71, N'fa-tint')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (72, N'fa-edit')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (73, N'fa-pencil-square-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (74, N'fa-share-square-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (75, N'fa-check-square-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (76, N'fa-arrows')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (77, N'fa-step-backward')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (78, N'fa-fast-backward')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (79, N'fa-backward')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (80, N'fa-play')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (81, N'fa-pause')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (82, N'fa-stop')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (83, N'fa-forward')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (84, N'fa-fast-forward')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (85, N'fa-step-forward')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (86, N'fa-eject')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (87, N'fa-chevron-left')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (88, N'fa-chevron-right')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (89, N'fa-plus-circle')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (90, N'fa-minus-circle')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (91, N'fa-times-circle')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (92, N'fa-check-circle')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (93, N'fa-question-circle')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (94, N'fa-info-circle')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (95, N'fa-crosshairs')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (96, N'fa-times-circle-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (97, N'fa-check-circle-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (98, N'fa-ban')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (99, N'fa-arrow-left')
GO
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (100, N'fa-arrow-right')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (101, N'fa-arrow-up')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (102, N'fa-arrow-down')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (103, N'fa-mail-forward')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (104, N'fa-share')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (105, N'fa-expand')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (106, N'fa-compress')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (107, N'fa-plus')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (108, N'fa-minus')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (109, N'fa-asterisk')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (110, N'fa-exclamation-circl')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (111, N'fa-gift')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (112, N'fa-leaf')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (113, N'fa-fire')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (114, N'fa-eye')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (115, N'fa-eye-slash')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (116, N'fa-warning')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (117, N'fa-exclamation-trian')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (118, N'fa-plane')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (119, N'fa-calendar')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (120, N'fa-random')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (121, N'fa-comment')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (122, N'fa-magnet')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (123, N'fa-chevron-up')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (124, N'fa-chevron-down')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (125, N'fa-retweet')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (126, N'fa-shopping-cart')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (127, N'fa-folder')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (128, N'fa-folder-open')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (129, N'fa-arrows-v')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (130, N'fa-arrows-h')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (131, N'fa-bar-chart-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (132, N'fa-bar-chart')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (133, N'fa-twitter-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (134, N'fa-facebook-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (135, N'fa-camera-retro')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (136, N'fa-key')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (137, N'fa-gears')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (138, N'fa-cogs')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (139, N'fa-comments')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (140, N'fa-thumbs-o-up')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (141, N'fa-thumbs-o-down')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (142, N'fa-star-half')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (143, N'fa-heart-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (144, N'fa-sign-out')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (145, N'fa-linkedin-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (146, N'fa-thumb-tack')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (147, N'fa-external-link')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (148, N'fa-sign-in')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (149, N'fa-trophy')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (150, N'fa-github-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (151, N'fa-upload')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (152, N'fa-lemon-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (153, N'fa-phone')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (154, N'fa-square-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (155, N'fa-bookmark-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (156, N'fa-phone-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (157, N'fa-twitter')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (158, N'fa-facebook-f')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (159, N'fa-facebook')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (160, N'fa-github')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (161, N'fa-unlock')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (162, N'fa-credit-card')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (163, N'fa-rss')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (164, N'fa-hdd-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (165, N'fa-bullhorn')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (166, N'fa-bell')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (167, N'fa-certificate')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (168, N'fa-hand-o-right')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (169, N'fa-hand-o-left')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (170, N'fa-hand-o-up')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (171, N'fa-hand-o-down')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (172, N'fa-arrow-circle-left')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (173, N'fa-arrow-circle-righ')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (174, N'fa-arrow-circle-up')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (175, N'fa-arrow-circle-down')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (176, N'fa-globe')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (177, N'fa-wrench')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (178, N'fa-tasks')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (179, N'fa-filter')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (180, N'fa-briefcase')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (181, N'fa-arrows-alt')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (182, N'fa-group')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (183, N'fa-users')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (184, N'fa-chain')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (185, N'fa-link')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (186, N'fa-cloud')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (187, N'fa-flask')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (188, N'fa-cut')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (189, N'fa-scissors')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (190, N'fa-copy')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (191, N'fa-files-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (192, N'fa-paperclip')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (193, N'fa-save')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (194, N'fa-floppy-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (195, N'fa-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (196, N'fa-navicon')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (197, N'fa-reorder')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (198, N'fa-bars')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (199, N'fa-list-ul')
GO
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (200, N'fa-list-ol')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (201, N'fa-strikethrough')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (202, N'fa-underline')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (203, N'fa-table')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (204, N'fa-magic')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (205, N'fa-truck')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (206, N'fa-pinterest')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (207, N'fa-pinterest-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (208, N'fa-google-plus-squar')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (209, N'fa-google-plus')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (210, N'fa-money')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (211, N'fa-caret-down')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (212, N'fa-caret-up')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (213, N'fa-caret-left')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (214, N'fa-caret-right')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (215, N'fa-columns')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (216, N'fa-unsorted')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (217, N'fa-sort')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (218, N'fa-sort-down')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (219, N'fa-sort-desc')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (220, N'fa-sort-up')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (221, N'fa-sort-asc')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (222, N'fa-envelope')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (223, N'fa-linkedin')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (224, N'fa-rotate-left')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (225, N'fa-undo')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (226, N'fa-legal')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (227, N'fa-gavel')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (228, N'fa-dashboard')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (229, N'fa-tachometer')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (230, N'fa-comment-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (231, N'fa-comments-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (232, N'fa-flash')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (233, N'fa-bolt')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (234, N'fa-sitemap')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (235, N'fa-umbrella')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (236, N'fa-paste')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (237, N'fa-clipboard')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (238, N'fa-lightbulb-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (239, N'fa-exchange')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (240, N'fa-cloud-download')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (241, N'fa-cloud-upload')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (242, N'fa-user-md')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (243, N'fa-stethoscope')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (244, N'fa-suitcase')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (245, N'fa-bell-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (246, N'fa-coffee')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (247, N'fa-cutlery')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (248, N'fa-file-text-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (249, N'fa-building-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (250, N'fa-hospital-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (251, N'fa-ambulance')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (252, N'fa-medkit')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (253, N'fa-fighter-jet')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (254, N'fa-beer')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (255, N'fa-h-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (256, N'fa-plus-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (257, N'fa-angle-double-left')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (258, N'fa-angle-double-righ')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (259, N'fa-angle-double-up')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (260, N'fa-angle-double-down')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (261, N'fa-angle-left')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (262, N'fa-angle-right')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (263, N'fa-angle-up')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (264, N'fa-angle-down')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (265, N'fa-desktop')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (266, N'fa-laptop')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (267, N'fa-tablet')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (268, N'fa-mobile-phone')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (269, N'fa-mobile')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (270, N'fa-circle-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (271, N'fa-quote-left')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (272, N'fa-quote-right')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (273, N'fa-spinner')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (274, N'fa-circle')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (275, N'fa-mail-reply')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (276, N'fa-reply')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (277, N'fa-github-alt')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (278, N'fa-folder-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (279, N'fa-folder-open-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (280, N'fa-smile-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (281, N'fa-frown-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (282, N'fa-meh-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (283, N'fa-gamepad')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (284, N'fa-keyboard-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (285, N'fa-flag-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (286, N'fa-flag-checkered')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (287, N'fa-terminal')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (288, N'fa-code')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (289, N'fa-mail-reply-all')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (290, N'fa-reply-all')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (291, N'fa-star-half-empty')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (292, N'fa-star-half-full')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (293, N'fa-star-half-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (294, N'fa-location-arrow')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (295, N'fa-crop')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (296, N'fa-code-fork')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (297, N'fa-unlink')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (298, N'fa-chain-broken')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (299, N'fa-question')
GO
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (300, N'fa-info')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (301, N'fa-exclamation')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (302, N'fa-superscript')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (303, N'fa-subscript')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (304, N'fa-eraser')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (305, N'fa-puzzle-piece')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (306, N'fa-microphone')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (307, N'fa-microphone-slash')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (308, N'fa-shield')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (309, N'fa-calendar-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (310, N'fa-fire-extinguisher')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (311, N'fa-rocket')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (312, N'fa-maxcdn')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (313, N'fa-chevron-circle-le')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (314, N'fa-chevron-circle-ri')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (315, N'fa-chevron-circle-up')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (316, N'fa-chevron-circle-do')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (317, N'fa-html5')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (318, N'fa-css3')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (319, N'fa-anchor')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (320, N'fa-unlock-alt')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (321, N'fa-bullseye')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (322, N'fa-ellipsis-h')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (323, N'fa-ellipsis-v')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (324, N'fa-rss-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (325, N'fa-play-circle')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (326, N'fa-ticket')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (327, N'fa-minus-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (328, N'fa-minus-square-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (329, N'fa-level-up')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (330, N'fa-level-down')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (331, N'fa-check-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (332, N'fa-pencil-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (333, N'fa-external-link-squ')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (334, N'fa-share-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (335, N'fa-compass')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (336, N'fa-toggle-down')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (337, N'fa-caret-square-o-do')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (338, N'fa-toggle-up')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (339, N'fa-caret-square-o-up')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (340, N'fa-toggle-right')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (341, N'fa-caret-square-o-ri')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (342, N'fa-euro')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (343, N'fa-eur')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (344, N'fa-gbp')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (345, N'fa-dollar')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (346, N'fa-usd')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (347, N'fa-rupee')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (348, N'fa-inr')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (349, N'fa-cny')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (350, N'fa-rmb')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (351, N'fa-yen')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (352, N'fa-jpy')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (353, N'fa-ruble')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (354, N'fa-rouble')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (355, N'fa-rub')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (356, N'fa-won')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (357, N'fa-krw')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (358, N'fa-bitcoin')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (359, N'fa-btc')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (360, N'fa-file')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (361, N'fa-file-text')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (362, N'fa-sort-alpha-asc')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (363, N'fa-sort-alpha-desc')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (364, N'fa-sort-amount-asc')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (365, N'fa-sort-amount-desc')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (366, N'fa-sort-numeric-asc')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (367, N'fa-sort-numeric-desc')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (368, N'fa-thumbs-up')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (369, N'fa-thumbs-down')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (370, N'fa-youtube-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (371, N'fa-youtube')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (372, N'fa-xing')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (373, N'fa-xing-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (374, N'fa-youtube-play')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (375, N'fa-dropbox')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (376, N'fa-stack-overflow')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (377, N'fa-instagram')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (378, N'fa-flickr')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (379, N'fa-adn')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (380, N'fa-bitbucket')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (381, N'fa-bitbucket-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (382, N'fa-tumblr')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (383, N'fa-tumblr-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (384, N'fa-long-arrow-down')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (385, N'fa-long-arrow-up')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (386, N'fa-long-arrow-left')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (387, N'fa-long-arrow-right')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (388, N'fa-apple')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (389, N'fa-windows')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (390, N'fa-android')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (391, N'fa-linux')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (392, N'fa-dribbble')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (393, N'fa-skype')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (394, N'fa-foursquare')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (395, N'fa-trello')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (396, N'fa-female')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (397, N'fa-male')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (398, N'fa-gittip')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (399, N'fa-gratipay')
GO
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (400, N'fa-sun-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (401, N'fa-moon-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (402, N'fa-archive')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (403, N'fa-bug')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (404, N'fa-vk')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (405, N'fa-weibo')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (406, N'fa-renren')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (407, N'fa-pagelines')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (408, N'fa-stack-exchange')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (409, N'fa-arrow-circle-o-ri')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (410, N'fa-arrow-circle-o-le')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (411, N'fa-toggle-left')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (412, N'fa-caret-square-o-le')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (413, N'fa-dot-circle-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (414, N'fa-wheelchair')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (415, N'fa-vimeo-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (416, N'fa-turkish-lira')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (417, N'fa-try')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (418, N'fa-plus-square-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (419, N'fa-space-shuttle')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (420, N'fa-slack')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (421, N'fa-envelope-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (422, N'fa-wordpress')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (423, N'fa-openid')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (424, N'fa-institution')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (425, N'fa-bank')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (426, N'fa-university')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (427, N'fa-mortar-board')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (428, N'fa-graduation-cap')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (429, N'fa-yahoo')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (430, N'fa-google')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (431, N'fa-reddit')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (432, N'fa-reddit-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (433, N'fa-stumbleupon-circl')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (434, N'fa-stumbleupon')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (435, N'fa-delicious')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (436, N'fa-digg')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (437, N'fa-pied-piper')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (438, N'fa-pied-piper-alt')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (439, N'fa-drupal')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (440, N'fa-joomla')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (441, N'fa-language')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (442, N'fa-fax')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (443, N'fa-building')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (444, N'fa-child')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (445, N'fa-paw')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (446, N'fa-spoon')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (447, N'fa-cube')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (448, N'fa-cubes')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (449, N'fa-behance')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (450, N'fa-behance-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (451, N'fa-steam')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (452, N'fa-steam-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (453, N'fa-recycle')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (454, N'fa-automobile')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (455, N'fa-car')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (456, N'fa-cab')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (457, N'fa-taxi')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (458, N'fa-tree')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (459, N'fa-spotify')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (460, N'fa-deviantart')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (461, N'fa-soundcloud')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (462, N'fa-database')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (463, N'fa-file-pdf-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (464, N'fa-file-word-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (465, N'fa-file-excel-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (466, N'fa-file-powerpoint-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (467, N'fa-file-photo-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (468, N'fa-file-picture-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (469, N'fa-file-image-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (470, N'fa-file-zip-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (471, N'fa-file-archive-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (472, N'fa-file-sound-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (473, N'fa-file-audio-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (474, N'fa-file-movie-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (475, N'fa-file-video-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (476, N'fa-file-code-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (477, N'fa-vine')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (478, N'fa-codepen')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (479, N'fa-jsfiddle')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (480, N'fa-life-bouy')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (481, N'fa-life-buoy')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (482, N'fa-life-saver')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (483, N'fa-support')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (484, N'fa-life-ring')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (485, N'fa-circle-o-notch')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (486, N'fa-ra')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (487, N'fa-rebel')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (488, N'fa-ge')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (489, N'fa-empire')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (490, N'fa-git-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (491, N'fa-git')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (492, N'fa-hacker-news')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (493, N'fa-tencent-weibo')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (494, N'fa-qq')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (495, N'fa-wechat')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (496, N'fa-weixin')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (497, N'fa-send')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (498, N'fa-paper-plane')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (499, N'fa-send-o')
GO
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (500, N'fa-paper-plane-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (501, N'fa-history')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (502, N'fa-genderless')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (503, N'fa-circle-thin')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (504, N'fa-header')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (505, N'fa-paragraph')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (506, N'fa-sliders')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (507, N'fa-share-alt')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (508, N'fa-share-alt-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (509, N'fa-bomb')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (510, N'fa-soccer-ball-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (511, N'fa-futbol-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (512, N'fa-tty')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (513, N'fa-binoculars')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (514, N'fa-plug')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (515, N'fa-slideshare')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (516, N'fa-twitch')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (517, N'fa-yelp')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (518, N'fa-newspaper-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (519, N'fa-wifi')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (520, N'fa-calculator')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (521, N'fa-paypal')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (522, N'fa-google-wallet')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (523, N'fa-cc-visa')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (524, N'fa-cc-mastercard')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (525, N'fa-cc-discover')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (526, N'fa-cc-amex')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (527, N'fa-cc-paypal')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (528, N'fa-cc-stripe')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (529, N'fa-bell-slash')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (530, N'fa-bell-slash-o')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (531, N'fa-trash')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (532, N'fa-copyright')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (533, N'fa-at')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (534, N'fa-eyedropper')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (535, N'fa-paint-brush')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (536, N'fa-birthday-cake')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (537, N'fa-area-chart')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (538, N'fa-pie-chart')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (539, N'fa-line-chart')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (540, N'fa-lastfm')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (541, N'fa-lastfm-square')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (542, N'fa-toggle-off')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (543, N'fa-toggle-on')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (544, N'fa-bicycle')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (545, N'fa-bus')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (546, N'fa-ioxhost')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (547, N'fa-angellist')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (548, N'fa-cc za')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (549, N'fa-shekel')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (550, N'fa-sheqel')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (551, N'fa-ils')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (552, N'fa-meanpath')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (553, N'fa-buysellads')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (554, N'fa-connectdevelop')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (555, N'fa-dashcube')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (556, N'fa-forumbee')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (557, N'fa-leanpub')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (558, N'fa-sellsy')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (559, N'fa-shirtsinbulk')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (560, N'fa-simplybuilt')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (561, N'fa-skyatlas')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (562, N'fa-cart-plus')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (563, N'fa-cart-arrow-down')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (564, N'fa-diamond')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (565, N'fa-ship')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (566, N'fa-user-secret')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (567, N'fa-motorcycle')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (568, N'fa-street-view')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (569, N'fa-heartbeat')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (570, N'fa-venus')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (571, N'fa-mars')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (572, N'fa-mercury')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (573, N'fa-transgender')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (574, N'fa-transgender-alt')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (575, N'fa-venus-double')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (576, N'fa-mars-double')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (577, N'fa-venus-mars')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (578, N'fa-mars-stroke')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (579, N'fa-mars-stroke-v')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (580, N'fa-mars-stroke-h')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (581, N'fa-neuter')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (582, N'fa-facebook-official')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (583, N'fa-pinterest-p')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (584, N'fa-whatsapp')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (585, N'fa-server')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (586, N'fa-user-plus')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (587, N'fa-user-times')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (588, N'fa-hotel')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (589, N'fa-bed')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (590, N'fa-viacoin')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (591, N'fa-train')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (592, N'fa-subway')
INSERT [dbo].[FONT_AWESOME_ICONS] ([Id], [IconClass]) VALUES (593, N'fa-medium')
SET IDENTITY_INSERT [dbo].[FONT_AWESOME_ICONS] OFF
SET IDENTITY_INSERT [dbo].[OrderStatus] ON 

INSERT [dbo].[OrderStatus] ([Id], [Name]) VALUES (1, N'Pending')
INSERT [dbo].[OrderStatus] ([Id], [Name]) VALUES (2, N'Processing')
INSERT [dbo].[OrderStatus] ([Id], [Name]) VALUES (3, N'Complete')
INSERT [dbo].[OrderStatus] ([Id], [Name]) VALUES (4, N'Cancelled')
SET IDENTITY_INSERT [dbo].[OrderStatus] OFF
SET IDENTITY_INSERT [dbo].[PRODCUT_REVIEWS] ON 

INSERT [dbo].[PRODCUT_REVIEWS] ([Id], [ProductId], [Price], [Value], [Quality], [CustomerId], [CustomerEmail], [CustomerName], [Summery], [Description], [ReviewDate]) VALUES (1, 2, 5, 5, 5, NULL, N'test@gmail.com', N'Test', N'Awesome', N'Awesome', CAST(N'2019-07-17 03:27:04.360' AS DateTime))
INSERT [dbo].[PRODCUT_REVIEWS] ([Id], [ProductId], [Price], [Value], [Quality], [CustomerId], [CustomerEmail], [CustomerName], [Summery], [Description], [ReviewDate]) VALUES (2, 2, 3, 1, 2, NULL, N'test2@adsf.com', N'asdf', N'adsf', N'adsf', CAST(N'2019-07-17 03:27:04.360' AS DateTime))
INSERT [dbo].[PRODCUT_REVIEWS] ([Id], [ProductId], [Price], [Value], [Quality], [CustomerId], [CustomerEmail], [CustomerName], [Summery], [Description], [ReviewDate]) VALUES (3, 2, 1, 1, 1, NULL, N'asdf', N'adf', N'adfs', N'adf', CAST(N'2019-07-17 03:27:04.360' AS DateTime))
INSERT [dbo].[PRODCUT_REVIEWS] ([Id], [ProductId], [Price], [Value], [Quality], [CustomerId], [CustomerEmail], [CustomerName], [Summery], [Description], [ReviewDate]) VALUES (4, 3, 4, 5, 5, NULL, N'ret', N'wretw', N'rewrt', N'sfg', CAST(N'2019-07-17 03:27:04.360' AS DateTime))
INSERT [dbo].[PRODCUT_REVIEWS] ([Id], [ProductId], [Price], [Value], [Quality], [CustomerId], [CustomerEmail], [CustomerName], [Summery], [Description], [ReviewDate]) VALUES (5, 1, 5, 5, 5, NULL, N'test@test.com', N'Md.Shohanur Rahman', N'asdf', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', CAST(N'2019-07-17 17:45:45.077' AS DateTime))
INSERT [dbo].[PRODCUT_REVIEWS] ([Id], [ProductId], [Price], [Value], [Quality], [CustomerId], [CustomerEmail], [CustomerName], [Summery], [Description], [ReviewDate]) VALUES (6, 1, 5, 5, 3, 1, N'admin@gmail.com', N'Admin User', N'Awesome', N'sdfg sfdg sdfg sdfg sfg', CAST(N'2019-07-17 18:16:33.240' AS DateTime))
SET IDENTITY_INSERT [dbo].[PRODCUT_REVIEWS] OFF
SET IDENTITY_INSERT [dbo].[PRODUCT_CATEGORIES] ON 

INSERT [dbo].[PRODUCT_CATEGORIES] ([Id], [Name], [SortName], [IconClass], [Description], [ImageURL], [ParentCatId], [ShowOnHomePage], [ShowOnTopMenu], [Published], [DisplayOrder], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (1, N'Food', N'Food', N'fa-asterisk', N'&lt;strong style="margin:0px;padding:0px;font-family:''Open Sans'', Arial, sans-serif;font-size:14px;text-align:justify;background-color:#ffffff;"&gt;Lorem Ipsum&lt;/strong&gt;&lt;span style="font-family:''Open Sans'', Arial, sans-serif;font-size:14px;text-align:justify;background-color:#ffffff;"&gt;&amp;nbsp;is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.&lt;/span&gt;', NULL, NULL, 1, 1, 1, 4, CAST(N'2019-07-12 21:20:47.427' AS DateTime), -1, CAST(N'2019-07-19 19:20:03.807' AS DateTime), 1)
INSERT [dbo].[PRODUCT_CATEGORIES] ([Id], [Name], [SortName], [IconClass], [Description], [ImageURL], [ParentCatId], [ShowOnHomePage], [ShowOnTopMenu], [Published], [DisplayOrder], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (2, N'Beauty & Health', N'Beauty', N'fa-lightbulb-o', N'&lt;h2 style="margin:0px 0px 10px;padding:0px;font-weight:400;line-height:24px;font-family:DauphinPlain;font-size:24px;background-color:#ffffff;"&gt;Where can I get some?&lt;/h2&gt;&lt;h2 style="margin:0px 0px 10px;padding:0px;font-weight:400;line-height:24px;font-family:DauphinPlain;font-size:24px;background-color:#ffffff;"&gt;Where can I get some?&lt;/h2&gt;&lt;h2 style="margin:0px 0px 10px;padding:0px;font-weight:400;line-height:24px;font-family:DauphinPlain;font-size:24px;background-color:#ffffff;"&gt;Where can I get some?Where can I get some?&lt;/h2&gt;&lt;h2 style="margin:0px 0px 10px;padding:0px;font-weight:400;line-height:24px;font-family:DauphinPlain;font-size:24px;background-color:#ffffff;"&gt;Where can I get some?Where can I get some?&lt;/h2&gt;&lt;h2 style="margin:0px 0px 10px;padding:0px;font-weight:400;line-height:24px;font-family:DauphinPlain;font-size:24px;background-color:#ffffff;"&gt;Where can I get some?Where can I get some?&lt;/h2&gt;&lt;h2 style="margin:0px 0px 10px;padding:0px;font-weight:400;line-height:24px;font-family:DauphinPlain;font-size:24px;background-color:#ffffff;"&gt;Where can I get some?Where can I get some?&lt;/h2&gt;&lt;h2 style="margin:0px 0px 10px;padding:0px;font-weight:400;line-height:24px;font-family:DauphinPlain;font-size:24px;background-color:#ffffff;"&gt;Where can I get some?Where can I get some?&lt;/h2&gt;&lt;h2 style="margin:0px 0px 10px;padding:0px;font-weight:400;line-height:24px;font-family:DauphinPlain;font-size:24px;background-color:#ffffff;"&gt;Where can I get some?&lt;/h2&gt;', NULL, NULL, 0, 1, 1, 2, CAST(N'2019-07-12 21:24:13.740' AS DateTime), -1, CAST(N'2019-07-19 19:25:11.140' AS DateTime), 1)
INSERT [dbo].[PRODUCT_CATEGORIES] ([Id], [Name], [SortName], [IconClass], [Description], [ImageURL], [ParentCatId], [ShowOnHomePage], [ShowOnTopMenu], [Published], [DisplayOrder], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (3, N'Fruits & Vegetables', N'Name', NULL, N'Fruits &amp;amp; Vegetables&amp;nbsp;Fruits &amp;amp; Vegetables&amp;nbsp;Fruits &amp;amp; Vegetables&amp;nbsp;Fruits &amp;amp; Vegetables&amp;nbsp;Fruits &amp;amp; Vegetables&amp;nbsp;Fruits &amp;amp; Vegetables Fruits &amp;amp; Vegetables&amp;nbsp;Fruits &amp;amp; Vegetables&amp;nbsp;Fruits &amp;amp; Vegetables&amp;nbsp;Fruits &amp;amp; Vegetables Fruits &amp;amp; Vegetables&amp;nbsp;Fruits &amp;amp; Vegetables&amp;nbsp;Fruits &amp;amp; Vegetables&amp;nbsp;Fruits &amp;amp; Vegetables Fruits &amp;amp; Vegetables&amp;nbsp;Fruits &amp;amp; Vegetables&amp;nbsp;Fruits &amp;amp; Vegetables&amp;nbsp;Fruits &amp;amp; Vegetables Fruits &amp;amp; Vegetables&amp;nbsp;Fruits &amp;amp; Vegetables&amp;nbsp;Fruits &amp;amp; Vegetables&amp;nbsp;Fruits &amp;amp; Vegetables Fruits &amp;amp; Vegetables&amp;nbsp;Fruits &amp;amp; Vegetables&amp;nbsp;', NULL, 1, 1, 1, 1, 0, CAST(N'2019-07-13 22:43:02.087' AS DateTime), -1, CAST(N'2019-07-14 17:09:40.333' AS DateTime), 3)
INSERT [dbo].[PRODUCT_CATEGORIES] ([Id], [Name], [SortName], [IconClass], [Description], [ImageURL], [ParentCatId], [ShowOnHomePage], [ShowOnTopMenu], [Published], [DisplayOrder], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (4, N'Fresh Fruits', N'Name', NULL, N'&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Fruits &lt;/span&gt;&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Fruits &lt;/span&gt;&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Fruits &lt;/span&gt;&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Fruits &lt;/span&gt;&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Fruits &lt;/span&gt;&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Fruits &lt;/span&gt;&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Fruits &lt;/span&gt;&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Fruits &lt;/span&gt;&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Fruits &lt;/span&gt;&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Fruits &lt;/span&gt;&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Fruits &lt;/span&gt;&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Fruits &lt;/span&gt;&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Fruits &lt;/span&gt;&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Fruits &lt;/span&gt;', NULL, 3, 1, 1, 1, 0, CAST(N'2019-07-13 22:44:20.393' AS DateTime), -1, CAST(N'2019-07-14 17:09:49.310' AS DateTime), 3)
INSERT [dbo].[PRODUCT_CATEGORIES] ([Id], [Name], [SortName], [IconClass], [Description], [ImageURL], [ParentCatId], [ShowOnHomePage], [ShowOnTopMenu], [Published], [DisplayOrder], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (5, N'Fresh Vegetables', N'Name', NULL, N'&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Vegetables &lt;/span&gt;&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Vegetables &lt;/span&gt;&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Vegetables &lt;/span&gt;&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Vegetables &lt;/span&gt;&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Vegetables &lt;/span&gt;&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Vegetables &lt;/span&gt;&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Vegetables &lt;/span&gt;&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Vegetables &lt;/span&gt;&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Vegetables &lt;/span&gt;&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Vegetables &lt;/span&gt;&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Vegetables &lt;/span&gt;&lt;span style="color:#222222;font-family:Consolas, ''Lucida Console'', ''Courier New'', monospace;white-space:pre-wrap;background-color:#ffffff;"&gt;Fresh Vegetables &lt;/span&gt;', NULL, 3, 1, 1, 1, 0, CAST(N'2019-07-13 22:45:20.957' AS DateTime), -1, NULL, NULL)
INSERT [dbo].[PRODUCT_CATEGORIES] ([Id], [Name], [SortName], [IconClass], [Description], [ImageURL], [ParentCatId], [ShowOnHomePage], [ShowOnTopMenu], [Published], [DisplayOrder], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (7, N'Bath & Body', N'Name', NULL, N'sdfsdf', NULL, 2, 0, 1, 1, 0, CAST(N'2019-07-14 17:54:39.893' AS DateTime), 3, NULL, NULL)
INSERT [dbo].[PRODUCT_CATEGORIES] ([Id], [Name], [SortName], [IconClass], [Description], [ImageURL], [ParentCatId], [ShowOnHomePage], [ShowOnTopMenu], [Published], [DisplayOrder], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (8, N'Electronics', N'Electronics', N'fa-puzzle-piece', NULL, NULL, NULL, 0, 1, 1, 0, CAST(N'2019-07-14 18:04:53.710' AS DateTime), 3, CAST(N'2019-07-19 19:27:09.093' AS DateTime), 1)
INSERT [dbo].[PRODUCT_CATEGORIES] ([Id], [Name], [SortName], [IconClass], [Description], [ImageURL], [ParentCatId], [ShowOnHomePage], [ShowOnTopMenu], [Published], [DisplayOrder], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (9, N'Men''s Fashion', N'Men', N'fa-user', NULL, NULL, NULL, 1, 1, 1, 0, CAST(N'2019-07-17 18:40:30.887' AS DateTime), 1, CAST(N'2019-07-19 19:27:34.947' AS DateTime), 1)
INSERT [dbo].[PRODUCT_CATEGORIES] ([Id], [Name], [SortName], [IconClass], [Description], [ImageURL], [ParentCatId], [ShowOnHomePage], [ShowOnTopMenu], [Published], [DisplayOrder], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (10, N'Women''s Fashion', N'Women', N'fa-child', NULL, NULL, NULL, 0, 1, 1, 0, CAST(N'2019-07-17 18:41:10.333' AS DateTime), 1, CAST(N'2019-07-19 19:28:21.087' AS DateTime), 1)
INSERT [dbo].[PRODUCT_CATEGORIES] ([Id], [Name], [SortName], [IconClass], [Description], [ImageURL], [ParentCatId], [ShowOnHomePage], [ShowOnTopMenu], [Published], [DisplayOrder], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (12, N'Clothing', N'Name', NULL, NULL, NULL, 9, 0, 1, 1, 0, CAST(N'2019-07-17 18:42:29.123' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[PRODUCT_CATEGORIES] ([Id], [Name], [SortName], [IconClass], [Description], [ImageURL], [ParentCatId], [ShowOnHomePage], [ShowOnTopMenu], [Published], [DisplayOrder], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (13, N'Clothing', N'Name', NULL, NULL, NULL, 10, 0, 1, 1, 0, CAST(N'2019-07-17 18:42:54.077' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[PRODUCT_CATEGORIES] ([Id], [Name], [SortName], [IconClass], [Description], [ImageURL], [ParentCatId], [ShowOnHomePage], [ShowOnTopMenu], [Published], [DisplayOrder], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (14, N'Shirts', N'Name', NULL, NULL, NULL, 12, 0, 1, 1, 0, CAST(N'2019-07-17 18:45:19.720' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[PRODUCT_CATEGORIES] ([Id], [Name], [SortName], [IconClass], [Description], [ImageURL], [ParentCatId], [ShowOnHomePage], [ShowOnTopMenu], [Published], [DisplayOrder], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (15, N'T-Shirt', N'Name', NULL, NULL, NULL, 12, 0, 1, 1, 0, CAST(N'2019-07-17 18:45:50.393' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[PRODUCT_CATEGORIES] ([Id], [Name], [SortName], [IconClass], [Description], [ImageURL], [ParentCatId], [ShowOnHomePage], [ShowOnTopMenu], [Published], [DisplayOrder], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (16, N'Men''s Footwear', N'Name', NULL, NULL, NULL, 9, 0, 1, 1, 0, CAST(N'2019-07-17 18:46:34.077' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[PRODUCT_CATEGORIES] ([Id], [Name], [SortName], [IconClass], [Description], [ImageURL], [ParentCatId], [ShowOnHomePage], [ShowOnTopMenu], [Published], [DisplayOrder], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (17, N'Sandals', N'Name', NULL, NULL, NULL, 16, 0, 1, 1, 0, CAST(N'2019-07-17 18:47:17.513' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[PRODUCT_CATEGORIES] ([Id], [Name], [SortName], [IconClass], [Description], [ImageURL], [ParentCatId], [ShowOnHomePage], [ShowOnTopMenu], [Published], [DisplayOrder], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (18, N'Watches', N'Name', NULL, NULL, NULL, 9, 0, 1, 1, 0, CAST(N'2019-07-17 18:48:05.440' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[PRODUCT_CATEGORIES] ([Id], [Name], [SortName], [IconClass], [Description], [ImageURL], [ParentCatId], [ShowOnHomePage], [ShowOnTopMenu], [Published], [DisplayOrder], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (19, N'Original Watch', N'Name', NULL, NULL, NULL, 18, 0, 1, 1, 0, CAST(N'2019-07-17 18:48:55.760' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[PRODUCT_CATEGORIES] ([Id], [Name], [SortName], [IconClass], [Description], [ImageURL], [ParentCatId], [ShowOnHomePage], [ShowOnTopMenu], [Published], [DisplayOrder], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (20, N'sdfgsdf', N'sdfgsdfg', N'fa-envelope-o', N'sdfgsdfg', NULL, NULL, 0, 1, 1, 0, CAST(N'2019-07-26 14:44:00.323' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[PRODUCT_CATEGORIES] ([Id], [Name], [SortName], [IconClass], [Description], [ImageURL], [ParentCatId], [ShowOnHomePage], [ShowOnTopMenu], [Published], [DisplayOrder], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (21, N'sdfgs', N'sdfgsdfg', N'fa-star', NULL, NULL, NULL, 0, 1, 1, 0, CAST(N'2019-07-26 14:45:04.947' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[PRODUCT_CATEGORIES] ([Id], [Name], [SortName], [IconClass], [Description], [ImageURL], [ParentCatId], [ShowOnHomePage], [ShowOnTopMenu], [Published], [DisplayOrder], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (22, N'dgh dgh dgh', N'dfghdfghdgh', N'fa-envelope-o', NULL, NULL, NULL, 0, 1, 1, 0, CAST(N'2019-07-26 14:47:18.947' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[PRODUCT_CATEGORIES] ([Id], [Name], [SortName], [IconClass], [Description], [ImageURL], [ParentCatId], [ShowOnHomePage], [ShowOnTopMenu], [Published], [DisplayOrder], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (23, N'sfgsdfg', N'sfdg', N'fa-envelope-o', NULL, NULL, NULL, 0, 1, 1, 0, CAST(N'2019-07-26 15:09:25.480' AS DateTime), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[PRODUCT_CATEGORIES] OFF
SET IDENTITY_INSERT [dbo].[PRODUCT_CATEGPRY_MAP] ON 

INSERT [dbo].[PRODUCT_CATEGPRY_MAP] ([Id], [ProductId], [CategoryId]) VALUES (12, 3, 5)
INSERT [dbo].[PRODUCT_CATEGPRY_MAP] ([Id], [ProductId], [CategoryId]) VALUES (13, 3, 4)
INSERT [dbo].[PRODUCT_CATEGPRY_MAP] ([Id], [ProductId], [CategoryId]) VALUES (14, 3, 1)
INSERT [dbo].[PRODUCT_CATEGPRY_MAP] ([Id], [ProductId], [CategoryId]) VALUES (15, 4, 15)
INSERT [dbo].[PRODUCT_CATEGPRY_MAP] ([Id], [ProductId], [CategoryId]) VALUES (16, 5, 15)
INSERT [dbo].[PRODUCT_CATEGPRY_MAP] ([Id], [ProductId], [CategoryId]) VALUES (17, 2, 1)
INSERT [dbo].[PRODUCT_CATEGPRY_MAP] ([Id], [ProductId], [CategoryId]) VALUES (18, 6, 1)
INSERT [dbo].[PRODUCT_CATEGPRY_MAP] ([Id], [ProductId], [CategoryId]) VALUES (19, 7, 1)
INSERT [dbo].[PRODUCT_CATEGPRY_MAP] ([Id], [ProductId], [CategoryId]) VALUES (30, 1, 1)
INSERT [dbo].[PRODUCT_CATEGPRY_MAP] ([Id], [ProductId], [CategoryId]) VALUES (31, 9, 1)
SET IDENTITY_INSERT [dbo].[PRODUCT_CATEGPRY_MAP] OFF
SET IDENTITY_INSERT [dbo].[PRODUCT_DISCOUNTS] ON 

INSERT [dbo].[PRODUCT_DISCOUNTS] ([Id], [Value], [Name]) VALUES (1, 5, N'5% Discount')
INSERT [dbo].[PRODUCT_DISCOUNTS] ([Id], [Value], [Name]) VALUES (2, 7, N'7% Discount')
INSERT [dbo].[PRODUCT_DISCOUNTS] ([Id], [Value], [Name]) VALUES (3, 10, N'10% Discount')
INSERT [dbo].[PRODUCT_DISCOUNTS] ([Id], [Value], [Name]) VALUES (4, 15, N'15% Discount')
INSERT [dbo].[PRODUCT_DISCOUNTS] ([Id], [Value], [Name]) VALUES (5, 20, N'20% Discount')
SET IDENTITY_INSERT [dbo].[PRODUCT_DISCOUNTS] OFF
SET IDENTITY_INSERT [dbo].[PRODUCT_IMAGE_MAP] ON 

INSERT [dbo].[PRODUCT_IMAGE_MAP] ([Id], [ProductId], [ImagePath]) VALUES (2, 6, N'uploads/products/gallery/72ea1baa-52fc-46f0-9902-ad5660a4f706-boy-882334_1920-768x415.jpg')
INSERT [dbo].[PRODUCT_IMAGE_MAP] ([Id], [ProductId], [ImagePath]) VALUES (3, 6, N'uploads/products/gallery/61e7ed15-b3bf-47c1-9745-2444e6e7dff3-image1.jpg')
INSERT [dbo].[PRODUCT_IMAGE_MAP] ([Id], [ProductId], [ImagePath]) VALUES (4, 6, N'uploads/products/gallery/69e7d645-67f2-460c-acb6-a147a3ed86bb-1.jpg')
INSERT [dbo].[PRODUCT_IMAGE_MAP] ([Id], [ProductId], [ImagePath]) VALUES (5, 6, N'uploads/products/gallery/01c1ba9b-0ea6-475e-b545-bfd5dca14055-image2.png')
INSERT [dbo].[PRODUCT_IMAGE_MAP] ([Id], [ProductId], [ImagePath]) VALUES (6, 7, N'uploads/products/gallery/72ea1baa-52fc-46f0-9902-ad5660a4f706-boy-882334_1920-768x415.jpg')
INSERT [dbo].[PRODUCT_IMAGE_MAP] ([Id], [ProductId], [ImagePath]) VALUES (13, 1, N'uploads/products/gallery/72ea1baa-52fc-46f0-9902-ad5660a4f706-boy-882334_1920-768x415.jpg')
INSERT [dbo].[PRODUCT_IMAGE_MAP] ([Id], [ProductId], [ImagePath]) VALUES (14, 1, N'uploads/products/gallery/f8aa6a89-3ef2-4257-be6e-9e2d0988a5d6-2.jpg')
SET IDENTITY_INSERT [dbo].[PRODUCT_IMAGE_MAP] OFF
SET IDENTITY_INSERT [dbo].[PRODUCT_INVENTORY_TYPE] ON 

INSERT [dbo].[PRODUCT_INVENTORY_TYPE] ([Id], [Name]) VALUES (1, N'Don''t track inventory')
INSERT [dbo].[PRODUCT_INVENTORY_TYPE] ([Id], [Name]) VALUES (2, N'Track inventory')
INSERT [dbo].[PRODUCT_INVENTORY_TYPE] ([Id], [Name]) VALUES (3, N'Track inventory by product attributes')
SET IDENTITY_INSERT [dbo].[PRODUCT_INVENTORY_TYPE] OFF
SET IDENTITY_INSERT [dbo].[PRODUCT_ORDER_MAP] ON 

INSERT [dbo].[PRODUCT_ORDER_MAP] ([Id], [ProductId], [OrderId], [ProductName], [QTY], [Price], [TotalPrice], [OrderDate]) VALUES (1, 2, 1, N'Black Top', 2, CAST(850.00 AS Decimal(18, 2)), CAST(1700.00 AS Decimal(18, 2)), CAST(N'2019-07-19 20:38:56.797' AS DateTime))
INSERT [dbo].[PRODUCT_ORDER_MAP] ([Id], [ProductId], [OrderId], [ProductName], [QTY], [Price], [TotalPrice], [OrderDate]) VALUES (2, 5, 1, N'Ame Amdavadi – Unisex T-Shirt', 1, CAST(250.00 AS Decimal(18, 2)), CAST(250.00 AS Decimal(18, 2)), CAST(N'2019-07-19 20:38:56.797' AS DateTime))
INSERT [dbo].[PRODUCT_ORDER_MAP] ([Id], [ProductId], [OrderId], [ProductName], [QTY], [Price], [TotalPrice], [OrderDate]) VALUES (3, 4, 2, N'Black T-Shirt', 1, CAST(580.00 AS Decimal(18, 2)), CAST(580.00 AS Decimal(18, 2)), CAST(N'2019-07-19 20:41:29.090' AS DateTime))
INSERT [dbo].[PRODUCT_ORDER_MAP] ([Id], [ProductId], [OrderId], [ProductName], [QTY], [Price], [TotalPrice], [OrderDate]) VALUES (4, 5, 2, N'Ame Amdavadi – Unisex T-Shirt', 1, CAST(250.00 AS Decimal(18, 2)), CAST(250.00 AS Decimal(18, 2)), CAST(N'2019-07-19 20:41:29.090' AS DateTime))
INSERT [dbo].[PRODUCT_ORDER_MAP] ([Id], [ProductId], [OrderId], [ProductName], [QTY], [Price], [TotalPrice], [OrderDate]) VALUES (5, 4, 3, N'Black T-Shirt', 2, CAST(580.00 AS Decimal(18, 2)), CAST(1160.00 AS Decimal(18, 2)), CAST(N'2019-07-19 20:50:07.210' AS DateTime))
INSERT [dbo].[PRODUCT_ORDER_MAP] ([Id], [ProductId], [OrderId], [ProductName], [QTY], [Price], [TotalPrice], [OrderDate]) VALUES (6, 5, 3, N'Ame Amdavadi – Unisex T-Shirt', 2, CAST(250.00 AS Decimal(18, 2)), CAST(500.00 AS Decimal(18, 2)), CAST(N'2019-07-19 20:50:07.210' AS DateTime))
INSERT [dbo].[PRODUCT_ORDER_MAP] ([Id], [ProductId], [OrderId], [ProductName], [QTY], [Price], [TotalPrice], [OrderDate]) VALUES (7, 4, 4, N'Black T-Shirt', 1, CAST(580.00 AS Decimal(18, 2)), CAST(580.00 AS Decimal(18, 2)), CAST(N'2019-07-19 11:33:01.857' AS DateTime))
INSERT [dbo].[PRODUCT_ORDER_MAP] ([Id], [ProductId], [OrderId], [ProductName], [QTY], [Price], [TotalPrice], [OrderDate]) VALUES (8, 5, 4, N'Ame Amdavadi – Unisex T-Shirt', 1, CAST(250.00 AS Decimal(18, 2)), CAST(250.00 AS Decimal(18, 2)), CAST(N'2019-07-19 11:33:01.857' AS DateTime))
INSERT [dbo].[PRODUCT_ORDER_MAP] ([Id], [ProductId], [OrderId], [ProductName], [QTY], [Price], [TotalPrice], [OrderDate]) VALUES (9, 4, 5, N'Black T-Shirt', 1, CAST(580.00 AS Decimal(18, 2)), CAST(580.00 AS Decimal(18, 2)), CAST(N'2019-07-20 00:42:17.390' AS DateTime))
INSERT [dbo].[PRODUCT_ORDER_MAP] ([Id], [ProductId], [OrderId], [ProductName], [QTY], [Price], [TotalPrice], [OrderDate]) VALUES (10, 4, 6, N'Black T-Shirt', 1, CAST(580.00 AS Decimal(18, 2)), CAST(580.00 AS Decimal(18, 2)), CAST(N'2019-07-20 00:44:04.157' AS DateTime))
INSERT [dbo].[PRODUCT_ORDER_MAP] ([Id], [ProductId], [OrderId], [ProductName], [QTY], [Price], [TotalPrice], [OrderDate]) VALUES (11, 5, 6, N'Ame Amdavadi – Unisex T-Shirt', 1, CAST(250.00 AS Decimal(18, 2)), CAST(250.00 AS Decimal(18, 2)), CAST(N'2019-07-20 00:44:04.157' AS DateTime))
INSERT [dbo].[PRODUCT_ORDER_MAP] ([Id], [ProductId], [OrderId], [ProductName], [QTY], [Price], [TotalPrice], [OrderDate]) VALUES (12, 5, 7, N'Ame Amdavadi – Unisex T-Shirt', 1, CAST(250.00 AS Decimal(18, 2)), CAST(250.00 AS Decimal(18, 2)), CAST(N'2019-07-20 00:45:09.030' AS DateTime))
INSERT [dbo].[PRODUCT_ORDER_MAP] ([Id], [ProductId], [OrderId], [ProductName], [QTY], [Price], [TotalPrice], [OrderDate]) VALUES (13, 5, 8, N'Ame Amdavadi – Unisex T-Shirt', 1, CAST(250.00 AS Decimal(18, 2)), CAST(250.00 AS Decimal(18, 2)), CAST(N'2019-07-20 00:49:23.737' AS DateTime))
INSERT [dbo].[PRODUCT_ORDER_MAP] ([Id], [ProductId], [OrderId], [ProductName], [QTY], [Price], [TotalPrice], [OrderDate]) VALUES (14, 5, 9, N'Ame Amdavadi – Unisex T-Shirt', 1, CAST(250.00 AS Decimal(18, 2)), CAST(250.00 AS Decimal(18, 2)), CAST(N'2019-07-19 11:54:14.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[PRODUCT_ORDER_MAP] OFF
SET IDENTITY_INSERT [dbo].[PRODUCT_ORDERS] ON 

INSERT [dbo].[PRODUCT_ORDERS] ([Id], [ProductPrice], [ShippingCost], [TotalAmount], [OrderStatusId], [CustomerId], [CustomerName], [CustomerEmail], [CustomerMobile], [Country], [State], [ZipCode], [AddressDetails], [CouponCode], [TempCookie], [OrderDateTime], [PaymentMethod], [PaymentStatus], [bKashTransectionNumber], [RoketTransectionNumber], [ShipDifferentAddress], [ShipName], [ShipEmail], [ShipPhone], [ShipCity], [ShipPostCode], [ShipAddressDetails], [ShipCountry]) VALUES (1, CAST(1950.00 AS Decimal(18, 2)), CAST(50.00 AS Decimal(18, 2)), CAST(2000.00 AS Decimal(18, 2)), 3, 1, N'adsfasdf', N'admin@gmail.com', N'01750519157', N'Bangladesh', N'5400', N'5400', N'Rangpur', NULL, NULL, CAST(N'2019-07-19 20:38:56.797' AS DateTime), N'Cash on delivery', 0, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[PRODUCT_ORDERS] ([Id], [ProductPrice], [ShippingCost], [TotalAmount], [OrderStatusId], [CustomerId], [CustomerName], [CustomerEmail], [CustomerMobile], [Country], [State], [ZipCode], [AddressDetails], [CouponCode], [TempCookie], [OrderDateTime], [PaymentMethod], [PaymentStatus], [bKashTransectionNumber], [RoketTransectionNumber], [ShipDifferentAddress], [ShipName], [ShipEmail], [ShipPhone], [ShipCity], [ShipPostCode], [ShipAddressDetails], [ShipCountry]) VALUES (2, CAST(830.00 AS Decimal(18, 2)), CAST(50.00 AS Decimal(18, 2)), CAST(880.00 AS Decimal(18, 2)), 1, 1, N'Sanjoy Roy Tap', N'sanjoyroytapu@gmail.com', N'01700000000', N'Bangladesh', N'Gulshan', N'1212', N'Gulshan Dhaka, 1212', NULL, NULL, CAST(N'2019-07-19 20:41:29.090' AS DateTime), N'Cash on delivery', 0, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[PRODUCT_ORDERS] ([Id], [ProductPrice], [ShippingCost], [TotalAmount], [OrderStatusId], [CustomerId], [CustomerName], [CustomerEmail], [CustomerMobile], [Country], [State], [ZipCode], [AddressDetails], [CouponCode], [TempCookie], [OrderDateTime], [PaymentMethod], [PaymentStatus], [bKashTransectionNumber], [RoketTransectionNumber], [ShipDifferentAddress], [ShipName], [ShipEmail], [ShipPhone], [ShipCity], [ShipPostCode], [ShipAddressDetails], [ShipCountry]) VALUES (3, CAST(1660.00 AS Decimal(18, 2)), CAST(50.00 AS Decimal(18, 2)), CAST(1710.00 AS Decimal(18, 2)), 1, 1, N'Yahooooo', N'sanjoyroytapu@gmail.com', N'0170000000', N'Bangladesh', N'dhaka', N'121212', N'adadsfadf', NULL, NULL, CAST(N'2019-07-19 20:50:07.210' AS DateTime), N'Cash on delivery', 0, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[PRODUCT_ORDERS] ([Id], [ProductPrice], [ShippingCost], [TotalAmount], [OrderStatusId], [CustomerId], [CustomerName], [CustomerEmail], [CustomerMobile], [Country], [State], [ZipCode], [AddressDetails], [CouponCode], [TempCookie], [OrderDateTime], [PaymentMethod], [PaymentStatus], [bKashTransectionNumber], [RoketTransectionNumber], [ShipDifferentAddress], [ShipName], [ShipEmail], [ShipPhone], [ShipCity], [ShipPostCode], [ShipAddressDetails], [ShipCountry]) VALUES (4, NULL, CAST(50.00 AS Decimal(18, 2)), NULL, 1, NULL, N'ALien', N'mosharafhosen90@gmail.com', N'0170000000', N'Bangladesh', N'Dhaka', N'1212', N'Coming from Uganda', NULL, N'1563561007426', CAST(N'2019-07-19 11:33:01.857' AS DateTime), N'bKash', 0, N'7X5FUE85', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, N'Bangladesh')
INSERT [dbo].[PRODUCT_ORDERS] ([Id], [ProductPrice], [ShippingCost], [TotalAmount], [OrderStatusId], [CustomerId], [CustomerName], [CustomerEmail], [CustomerMobile], [Country], [State], [ZipCode], [AddressDetails], [CouponCode], [TempCookie], [OrderDateTime], [PaymentMethod], [PaymentStatus], [bKashTransectionNumber], [RoketTransectionNumber], [ShipDifferentAddress], [ShipName], [ShipEmail], [ShipPhone], [ShipCity], [ShipPostCode], [ShipAddressDetails], [ShipCountry]) VALUES (5, CAST(580.00 AS Decimal(18, 2)), CAST(50.00 AS Decimal(18, 2)), CAST(630.00 AS Decimal(18, 2)), 1, 1, N'Admin User', N'admin@gmail.com', N'dhdfgh', N'Bangladesh', N'dfgh', N'dfgh', N'dgh', NULL, NULL, CAST(N'2019-07-20 00:42:17.390' AS DateTime), N'Cash on delivery', 0, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, N'Bangladesh')
INSERT [dbo].[PRODUCT_ORDERS] ([Id], [ProductPrice], [ShippingCost], [TotalAmount], [OrderStatusId], [CustomerId], [CustomerName], [CustomerEmail], [CustomerMobile], [Country], [State], [ZipCode], [AddressDetails], [CouponCode], [TempCookie], [OrderDateTime], [PaymentMethod], [PaymentStatus], [bKashTransectionNumber], [RoketTransectionNumber], [ShipDifferentAddress], [ShipName], [ShipEmail], [ShipPhone], [ShipCity], [ShipPostCode], [ShipAddressDetails], [ShipCountry]) VALUES (6, CAST(830.00 AS Decimal(18, 2)), CAST(50.00 AS Decimal(18, 2)), CAST(880.00 AS Decimal(18, 2)), 1, NULL, N'rtyety', N'dghdfh@adsfasdf.oasdf', N'456456', N'Bangladesh', N'456456', N'456', N'456', NULL, N'1563561792348', CAST(N'2019-07-20 00:44:04.157' AS DateTime), N'Cash on delivery', 0, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, N'Bangladesh')
INSERT [dbo].[PRODUCT_ORDERS] ([Id], [ProductPrice], [ShippingCost], [TotalAmount], [OrderStatusId], [CustomerId], [CustomerName], [CustomerEmail], [CustomerMobile], [Country], [State], [ZipCode], [AddressDetails], [CouponCode], [TempCookie], [OrderDateTime], [PaymentMethod], [PaymentStatus], [bKashTransectionNumber], [RoketTransectionNumber], [ShipDifferentAddress], [ShipName], [ShipEmail], [ShipPhone], [ShipCity], [ShipPostCode], [ShipAddressDetails], [ShipCountry]) VALUES (7, CAST(250.00 AS Decimal(18, 2)), CAST(50.00 AS Decimal(18, 2)), CAST(300.00 AS Decimal(18, 2)), 1, NULL, N'sdfgsdfg', N'sfgsfg@ssdfg.sfg', N'sfdgsdf', N'Bangladesh', N'sdfg', N'sdfg', N'sfg', NULL, N'1563561792348', CAST(N'2019-07-20 00:45:09.030' AS DateTime), N'Cash on delivery', 0, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, N'Bangladesh')
INSERT [dbo].[PRODUCT_ORDERS] ([Id], [ProductPrice], [ShippingCost], [TotalAmount], [OrderStatusId], [CustomerId], [CustomerName], [CustomerEmail], [CustomerMobile], [Country], [State], [ZipCode], [AddressDetails], [CouponCode], [TempCookie], [OrderDateTime], [PaymentMethod], [PaymentStatus], [bKashTransectionNumber], [RoketTransectionNumber], [ShipDifferentAddress], [ShipName], [ShipEmail], [ShipPhone], [ShipCity], [ShipPostCode], [ShipAddressDetails], [ShipCountry]) VALUES (9, NULL, CAST(50.00 AS Decimal(18, 2)), NULL, 1, NULL, N'Alien', N'mosharafhosen90@gmail.com', N'95959595959', N'Bangladesh', N'Dhainna', N'X007', N'Mongol Groho, #B74, Plnet Id 547, Range 51-98HZ', NULL, N'1563561007426', CAST(N'2019-07-19 11:54:14.000' AS DateTime), N'Cash on delivery', 0, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, N'Bangladesh')
SET IDENTITY_INSERT [dbo].[PRODUCT_ORDERS] OFF
SET IDENTITY_INSERT [dbo].[PRODUCTS] ON 

INSERT [dbo].[PRODUCTS] ([Id], [ImageURL], [Name], [ProductKeyWords], [SortDescription], [Description], [SKU], [Published], [IsShowIndividualy], [ShowOnHomePage], [AvailableSatartDate], [AvailableEndDate], [Price], [OldPrice], [ProductCost], [AffeliateCommision], [DiscountId], [TaxExempt], [TaxCategoryId], [InventoryMethodId], [StockQuantity], [ShippingEnabled], [ShipWeight], [ShipLength], [ShipWidth], [ShipHeight], [IsFreeShipping], [ShipSeperetly], [VendorId], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (1, N'uploads/products/effad2b4-406b-4d3a-bc42-1b45c28b73fa-cute-955782_1920-100x100.jpg', N'Manago Shirt', N'asdfasdf', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', N'&lt;p&gt;&lt;span style="color:#717171;font-family:Karla, sans-serif;font-size:15px;background-color:#ffffff;"&gt;Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.&lt;/span&gt;&lt;/p&gt;&lt;p style="box-sizing:inherit;margin-bottom:35px;color:#717171;font-family:Karla, sans-serif;font-size:15px;line-height:23px;background-color:#ffffff;"&gt;Pri quas audiam virtute ut, case utamur fuisset eam ut, iisque accommodare an eam. Reque blandit qui eu, cu vix nonumy volumus. Legendos intellegam id usu, vide oporteat vix eu, id illud principes has. Nam tempor utamur gubergren no.&lt;/p&gt;&lt;p style="box-sizing:inherit;margin-bottom:35px;color:#717171;font-family:Karla, sans-serif;font-size:15px;line-height:23px;background-color:#ffffff;"&gt;Ex soleat habemus usu, te nec eligendi deserunt vituperata. Natum consulatu vel ea, duo cetero repudiare efficiendi cu. Has at quas nonumy facilisis, enim percipitur mei ad. Mazim possim adipisci sea ei, omnium aeterno platonem mei no.&lt;/p&gt;', NULL, 1, 1, 0, NULL, NULL, CAST(550.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 0, NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, 0, 0, 0, CAST(N'2019-07-13 17:51:54.257' AS DateTime), -1, NULL, NULL)
INSERT [dbo].[PRODUCTS] ([Id], [ImageURL], [Name], [ProductKeyWords], [SortDescription], [Description], [SKU], [Published], [IsShowIndividualy], [ShowOnHomePage], [AvailableSatartDate], [AvailableEndDate], [Price], [OldPrice], [ProductCost], [AffeliateCommision], [DiscountId], [TaxExempt], [TaxCategoryId], [InventoryMethodId], [StockQuantity], [ShippingEnabled], [ShipWeight], [ShipLength], [ShipWidth], [ShipHeight], [IsFreeShipping], [ShipSeperetly], [VendorId], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (2, N'uploads/products/a401012a-f464-4d5d-933c-6157ed95503d-woman-1477091_1920.jpg', N'Black Top', N'', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', N'&lt;span style="color:#717171;font-family:Karla, sans-serif;font-size:15px;background-color:#ffffff;"&gt;Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.&lt;/span&gt;', NULL, 1, 1, 1, NULL, NULL, CAST(850.00 AS Decimal(18, 2)), CAST(900.00 AS Decimal(18, 2)), NULL, NULL, NULL, 0, NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, 0, 0, 0, CAST(N'2019-07-13 17:54:59.283' AS DateTime), -1, NULL, NULL)
INSERT [dbo].[PRODUCTS] ([Id], [ImageURL], [Name], [ProductKeyWords], [SortDescription], [Description], [SKU], [Published], [IsShowIndividualy], [ShowOnHomePage], [AvailableSatartDate], [AvailableEndDate], [Price], [OldPrice], [ProductCost], [AffeliateCommision], [DiscountId], [TaxExempt], [TaxCategoryId], [InventoryMethodId], [StockQuantity], [ShippingEnabled], [ShipWeight], [ShipLength], [ShipWidth], [ShipHeight], [IsFreeShipping], [ShipSeperetly], [VendorId], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (3, N'uploads/products/8dca0a77-3b2d-406a-8e7c-2aa19c3b3409-cute-955782_1920-150x150.jpg', N'Retis lapen casen', N'', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam fringilla augue nec est tristique auctor. Donec non est at libero vulputate rutrum. Morbi ornare lectus quis justo gravida semper. Nulla tellus mi, vulputate adipiscing cursus eu, suscipit id nulla. Donec a neque libero. Pellentesque aliquet, sem eget laoreet ultrices, ipsum metus feugiat sem, quis fermentum turpis eros eget velit. Donec ac tempus ante.', N'&lt;p style="box-sizing:border-box;margin-bottom:1.5em;font-size:13px;color:#333333;font-family:Poppins, sans-serif;background-color:#ffffff;"&gt;Fusce ac pharetra urna. Duis non lacus sit amet lacus interdum facilisis sed non est. Ut mi metus, semper eu dictum nec, condimentum sed sapien. Nullam lobortis nunc semper ipsum luctus ut viverra ante eleifend. Nunc pretium velit sed augue luctus accumsan.&lt;/p&gt;&lt;p style="box-sizing:border-box;margin-bottom:1.5em;font-size:13px;color:#333333;font-family:Poppins, sans-serif;background-color:#ffffff;"&gt;Aliquam laoreet consequat malesuada. Integer vitae diam sed dolor euismod laoreet eget ac felis. Donec non erat sed elit bibendum sodales. Donec eu cursus velit. Proin nunc lacus, gravida mollis dictum ut, vulputate eu turpis. Sed felis sapien, commodo in iaculis in, feugiat sed enim. Sed nunc ipsum, fermentum varius dignissim vitae, blandit et ante.Maecenas sagittis, lorem sed congue egestas, lectus purus congue nisl, ac molestie enim ligula nec eros. Sed leo tortor, tincidunt sit amet elementum vel, eleifend at orci. Maecenas ut turpis felis. Donec sit amet quam sem, et aliquet est.&lt;/p&gt;&lt;p style="box-sizing:border-box;margin-bottom:1.5em;font-size:13px;color:#333333;font-family:Poppins, sans-serif;background-color:#ffffff;"&gt;Quisque nisl lectus, accumsan et euismod eu, sollicitudin ac augue. In sit amet urna magna. Curabitur imperdiet urna nec purus egestas eget aliquet purus iaculis. Nunc porttitor blandit imperdiet. Nulla facilisi. Cras odio ipsum, vehicula nec vehicula sed, convallis scelerisque quam. Phasellus ut odio dui, ut fermentum neque.&lt;/p&gt;&lt;blockquote style="box-sizing:border-box;padding:20px 20px 30px 67px;margin:0px 0px 20px;font-size:14px;border-left:3px solid rgb(234, 234, 234);line-height:24px;color:#5c5c5c;font-style:italic;font-family:Georgia, ''Times New Roman'', Times, serif;position:relative;background:rgb(248, 248, 248);"&gt;Lorem ipsum dolor sit amet, consecte adipiscing elit. Integer aliquam mi nec dolor placerat a condimentum diam mollis. Ut pulvinar neque eget massa dapibus dolor.&lt;/blockquote&gt;&lt;p style="box-sizing:border-box;margin-bottom:1.5em;font-size:13px;color:#333333;font-family:Poppins, sans-serif;background-color:#ffffff;"&gt;Curabitur at vestibulum sem. Aliquam vehicula neque ac nibh suscipit ultrices. Morbi interdum accumsan arcu nec scelerisque. Phasellus eget purus nulla. Suspendisse quam est, tempor quis consectetur non, interdum vitae diam. Pellentesque volutpat mollis ligula in laoreet. Aenean est dui, sagittis in consequat at, adipiscing at risus. Sed suscipit, est vitae aliquam molestie, sem dolor dignissim leo, eget imperdiet enim urna in justo. Mauris pulvinar tortor lorem. Aliquam sed nisl in ipsum tincidunt ultrices.&lt;/p&gt;&lt;p style="box-sizing:border-box;margin-bottom:1.5em;font-size:13px;color:#333333;font-family:Poppins, sans-serif;background-color:#ffffff;"&gt;Nullam commodo lobortis nibh, vitae accumsan velit dapibus sed. Nunc ac sem eu libero pretium faucib. Quisque et semper odio. Praesent tortor ligula, imperdiet sed aliquet ut, pharetra at nisi. Etiam sit amet molestie est. Donec id turpis vitae leo viverra adipiscing at sed nisi. Donec ut justo nunc. Vivamu bibendum erat ac nunc sollicitudin lacinia. Phasellus sed lacus magna.&lt;/p&gt;', NULL, 1, 1, 0, NULL, NULL, CAST(850.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 0, NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, 0, 0, 0, CAST(N'2019-07-13 20:37:01.333' AS DateTime), -1, NULL, NULL)
INSERT [dbo].[PRODUCTS] ([Id], [ImageURL], [Name], [ProductKeyWords], [SortDescription], [Description], [SKU], [Published], [IsShowIndividualy], [ShowOnHomePage], [AvailableSatartDate], [AvailableEndDate], [Price], [OldPrice], [ProductCost], [AffeliateCommision], [DiscountId], [TaxExempt], [TaxCategoryId], [InventoryMethodId], [StockQuantity], [ShippingEnabled], [ShipWeight], [ShipLength], [ShipWidth], [ShipHeight], [IsFreeShipping], [ShipSeperetly], [VendorId], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (4, N'uploads/products/43920c42-6667-49a5-8cec-f677f338a76f-image2.png', N'Black T-Shirt', N'', N'Gildan is a leading provider of everyday quality family apparel, including t-shirts, fleece, underwear, and socks. Our Classic Short Sleeve T-Shirt provides a roomier fit ', N'&lt;span style="box-sizing:border-box;color:#333333;font-family:Montserrat, sans-serif;background-color:#ffffff;font-size:small;"&gt;Gildan is a leading provider of everyday quality family apparel, including t-shirts, fleece, underwear, and socks. Our Classic Short Sleeve T-Shirt provides a roomier fit and is offered in a wide variety of colors.&amp;nbsp;&lt;/span&gt;&lt;ul style="box-sizing:border-box;margin-top:0px;margin-bottom:10px;color:#333333;font-family:Montserrat, sans-serif;font-size:14px;background-color:#ffffff;"&gt;&lt;li style="box-sizing:border-box;"&gt;6 oz. 100% preshrunk cotton&lt;/li&gt;&lt;li style="box-sizing:border-box;"&gt;Industry leading assortment of colors&lt;/li&gt;&lt;li style="box-sizing:border-box;"&gt;Soft premium cotton for added comfort&lt;/li&gt;&lt;li style="box-sizing:border-box;"&gt;Classic Cut for roomier fit&lt;/li&gt;&lt;li style="box-sizing:border-box;"&gt;Double needle stitching in sleeves and bottom hem for extra durability&lt;/li&gt;&lt;/ul&gt;', NULL, 1, 1, 1, NULL, NULL, CAST(580.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 0, NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, 0, 0, 0, CAST(N'2019-07-17 19:39:35.387' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[PRODUCTS] ([Id], [ImageURL], [Name], [ProductKeyWords], [SortDescription], [Description], [SKU], [Published], [IsShowIndividualy], [ShowOnHomePage], [AvailableSatartDate], [AvailableEndDate], [Price], [OldPrice], [ProductCost], [AffeliateCommision], [DiscountId], [TaxExempt], [TaxCategoryId], [InventoryMethodId], [StockQuantity], [ShippingEnabled], [ShipWeight], [ShipLength], [ShipWidth], [ShipHeight], [IsFreeShipping], [ShipSeperetly], [VendorId], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (5, N'uploads/products/bca1795a-aec3-4a5f-a1f0-e34ca9ddd4f1-image1.jpg', N'Ame Amdavadi – Unisex T-Shirt', N'', N'just a demo', N'&lt;ul&gt;&lt;li&gt;&lt;span class="a-list-item" style="box-sizing:inherit;"&gt;100% Cotton&lt;/span&gt;&lt;/li&gt;&lt;li&gt;&lt;span class="a-list-item" style="box-sizing:inherit;"&gt;Regular fit, Short sleeve&lt;/span&gt;&lt;/li&gt;&lt;li&gt;&lt;span class="a-list-item" style="box-sizing:inherit;"&gt;Varsity College printed t-shirt&lt;/span&gt;&lt;/li&gt;&lt;li&gt;&lt;span class="a-list-item" style="box-sizing:inherit;"&gt;Bio wash for soft handfeel&lt;/span&gt;&lt;/li&gt;&lt;/ul&gt;', NULL, 1, 1, 1, NULL, NULL, CAST(250.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 0, NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, 0, 0, 0, CAST(N'2019-07-17 19:41:43.610' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[PRODUCTS] ([Id], [ImageURL], [Name], [ProductKeyWords], [SortDescription], [Description], [SKU], [Published], [IsShowIndividualy], [ShowOnHomePage], [AvailableSatartDate], [AvailableEndDate], [Price], [OldPrice], [ProductCost], [AffeliateCommision], [DiscountId], [TaxExempt], [TaxCategoryId], [InventoryMethodId], [StockQuantity], [ShippingEnabled], [ShipWeight], [ShipLength], [ShipWidth], [ShipHeight], [IsFreeShipping], [ShipSeperetly], [VendorId], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (6, N'uploads/products/effad2b4-406b-4d3a-bc42-1b45c28b73fa-cute-955782_1920-100x100.jpg', N'Manago Shirt', N'', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', N'&lt;p&gt;&lt;span style="color:#717171;font-family:Karla, sans-serif;font-size:15px;background-color:#ffffff;"&gt;Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.&lt;/span&gt;&lt;/p&gt;&lt;p style="box-sizing:inherit;margin-bottom:35px;color:#717171;font-family:Karla, sans-serif;font-size:15px;line-height:23px;background-color:#ffffff;"&gt;Pri quas audiam virtute ut, case utamur fuisset eam ut, iisque accommodare an eam. Reque blandit qui eu, cu vix nonumy volumus. Legendos intellegam id usu, vide oporteat vix eu, id illud principes has. Nam tempor utamur gubergren no.&lt;/p&gt;&lt;p style="box-sizing:inherit;margin-bottom:35px;color:#717171;font-family:Karla, sans-serif;font-size:15px;line-height:23px;background-color:#ffffff;"&gt;Ex soleat habemus usu, te nec eligendi deserunt vituperata. Natum consulatu vel ea, duo cetero repudiare efficiendi cu. Has at quas nonumy facilisis, enim percipitur mei ad. Mazim possim adipisci sea ei, omnium aeterno platonem mei no.&lt;/p&gt;', NULL, 1, 1, 0, NULL, NULL, CAST(550.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 0, NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, 0, 0, 0, CAST(N'2019-07-20 16:19:56.910' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[PRODUCTS] ([Id], [ImageURL], [Name], [ProductKeyWords], [SortDescription], [Description], [SKU], [Published], [IsShowIndividualy], [ShowOnHomePage], [AvailableSatartDate], [AvailableEndDate], [Price], [OldPrice], [ProductCost], [AffeliateCommision], [DiscountId], [TaxExempt], [TaxCategoryId], [InventoryMethodId], [StockQuantity], [ShippingEnabled], [ShipWeight], [ShipLength], [ShipWidth], [ShipHeight], [IsFreeShipping], [ShipSeperetly], [VendorId], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (7, N'uploads/products/effad2b4-406b-4d3a-bc42-1b45c28b73fa-cute-955782_1920-100x100.jpg', N'Manago Shirt', N'', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', N'&lt;p&gt;&lt;span style="color:#717171;font-family:Karla, sans-serif;font-size:15px;background-color:#ffffff;"&gt;Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.&lt;/span&gt;&lt;/p&gt;&lt;p style="box-sizing:inherit;margin-bottom:35px;color:#717171;font-family:Karla, sans-serif;font-size:15px;line-height:23px;background-color:#ffffff;"&gt;Pri quas audiam virtute ut, case utamur fuisset eam ut, iisque accommodare an eam. Reque blandit qui eu, cu vix nonumy volumus. Legendos intellegam id usu, vide oporteat vix eu, id illud principes has. Nam tempor utamur gubergren no.&lt;/p&gt;&lt;p style="box-sizing:inherit;margin-bottom:35px;color:#717171;font-family:Karla, sans-serif;font-size:15px;line-height:23px;background-color:#ffffff;"&gt;Ex soleat habemus usu, te nec eligendi deserunt vituperata. Natum consulatu vel ea, duo cetero repudiare efficiendi cu. Has at quas nonumy facilisis, enim percipitur mei ad. Mazim possim adipisci sea ei, omnium aeterno platonem mei no.&lt;/p&gt;', NULL, 1, 1, 0, NULL, NULL, CAST(550.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 0, NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, 0, 0, 0, CAST(N'2019-07-20 16:32:45.863' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[PRODUCTS] ([Id], [ImageURL], [Name], [ProductKeyWords], [SortDescription], [Description], [SKU], [Published], [IsShowIndividualy], [ShowOnHomePage], [AvailableSatartDate], [AvailableEndDate], [Price], [OldPrice], [ProductCost], [AffeliateCommision], [DiscountId], [TaxExempt], [TaxCategoryId], [InventoryMethodId], [StockQuantity], [ShippingEnabled], [ShipWeight], [ShipLength], [ShipWidth], [ShipHeight], [IsFreeShipping], [ShipSeperetly], [VendorId], [CreatedDate], [CreatedId], [EditedDate], [EditedId]) VALUES (9, N'uploads/products/a9a377a8-8a1c-444c-af40-4a366cace72f-dfdfg.jpg', N'asdf asdf ', N'test', N'as asdf asdf asdf asdf asdfasdf', N'as dfasdf asdf as fasdf', NULL, 1, 1, 0, NULL, NULL, CAST(350.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 0, NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, 0, 0, 0, CAST(N'2019-07-28 12:18:21.297' AS DateTime), 26, NULL, NULL)
SET IDENTITY_INSERT [dbo].[PRODUCTS] OFF
SET IDENTITY_INSERT [dbo].[SiteSettings] ON 

INSERT [dbo].[SiteSettings] ([ID], [LogoURL], [FaviconURL], [HeaderContactInfo], [WelcomeMessage], [HomePageSliderItem], [CategoryIdOne], [CategoryIdTwo], [CategoryIdThree], [CategoryIdFour], [CategoryIdFive], [H_SectionOneTitle], [H_SectionTwoTitle], [H_SectionThreeTitle], [H_SectionFourTitle], [H_BannerURL_Left], [H_BannerLink_Left], [H_BannerURL_CenterOne], [H_BannerLink_CenterOne], [H_BannerURL_CenterTwo], [H_BannerLink_CenterTwo]) VALUES (1, N'images/logo.png', N'images/favicon.ico', N'<div class="feature-box first"><div class="content"><h3>Free Shipping Worldwide </h3></div></div><span class="separator">/</span><div class="feature-box"><div class="content"><h3>Money Back Guarantee</h3></div></div><span class="separator">/</span><div class="feature-box last"><div class="content"><h3>Hotline  +(888) 123-4567</h3></div>', N'Welcome ', 5, 1, 8, 1, 1, 1, N'New Products adfaf', N'Best Sellers adfadsf ', N'Featured Product afasd afd ', N'Latest Blog adf', N'/uploads/banners/2019-07-19-81e13a5c-24c7-409a-b274-194123077fbd-1.jpg', N'https://via.placeholder.com/262x445', N'/uploads/banners/2019-07-19-5020befa-a850-4d93-a89f-32d91f808f0e-3.png', N'https://via.placeholder.com/262x445', N'/uploads/banners/2019-07-19-3b339b23-a00e-48c5-b643-25579aede537-5.jpg', N'https://via.placeholder.com/262x445')
SET IDENTITY_INSERT [dbo].[SiteSettings] OFF
SET IDENTITY_INSERT [dbo].[Sliders] ON 

INSERT [dbo].[Sliders] ([Id], [Name], [Caption], [SortDescription], [SliderIsActive], [SliderTypeId], [CategoryId], [ImagePath], [ThumbnailPath], [AuthorName], [CreatedId], [CreatedDateTime]) VALUES (4, N'Summer Collection', N' Women Style', N'Fill up this form carefully', 1, 1, 2, N'/uploads/sliders/2019-07-19-2196a98a-4c7b-4bbf-b4e4-e3d90bc751f2-2019-06-27-ab4b867c-55ab-4c9c-b9ba-aba1164fa785-bg2.jpg', N'/uploads/sliders/2019-06-27-ab4b867c-55ab-4c9c-b9ba-aba1164fa785-bg2.jpg', NULL, 1, CAST(N'2019-06-27 12:07:54.780' AS DateTime))
INSERT [dbo].[Sliders] ([Id], [Name], [Caption], [SortDescription], [SliderIsActive], [SliderTypeId], [CategoryId], [ImagePath], [ThumbnailPath], [AuthorName], [CreatedId], [CreatedDateTime]) VALUES (6, N'Hot Deal', N'sidebar', N'Save up to 70% OFF Fahion collection', 1, 3, 1, N'/uploads/sliders/2019-06-27-a27ec8ae-ed74-4600-a9c2-a034047f37e7-banner-side.png', N'/uploads/sliders/2019-06-27-f3bb524a-6027-4795-98e7-ca00d46ab7fe-banner-side.png', NULL, 1, CAST(N'2019-06-27 12:13:35.867' AS DateTime))
INSERT [dbo].[Sliders] ([Id], [Name], [Caption], [SortDescription], [SliderIsActive], [SliderTypeId], [CategoryId], [ImagePath], [ThumbnailPath], [AuthorName], [CreatedId], [CreatedDateTime]) VALUES (7, N'HUGE SALE', N'sidebar', N'Save up to 70% OFF Fahion collection', 1, 3, 1, N'/uploads/sliders/2019-06-27-34980b7d-8186-4794-98a5-d21d1dbed626-banner-side.png', N'/uploads/sliders/2019-06-27-9bfe66c0-91f1-4f39-a239-f755e135e8a1-banner-side.png', N'Alien', 1, CAST(N'2019-06-27 12:25:59.490' AS DateTime))
INSERT [dbo].[Sliders] ([Id], [Name], [Caption], [SortDescription], [SliderIsActive], [SliderTypeId], [CategoryId], [ImagePath], [ThumbnailPath], [AuthorName], [CreatedId], [CreatedDateTime]) VALUES (8, N'Feedback 1', N'feedback', N'How do I add a class an element on a container with a certain index?', 1, 2, 1, N'/uploads/sliders/2019-06-27-13b8e896-88e9-44a9-8545-c11c1a2df114-member2.png', N'/uploads/sliders/2019-06-27-7736c851-6cfd-4c12-b7a9-14fc8ecee955-member2.png', N'Alien', 1, CAST(N'2019-06-27 12:40:29.997' AS DateTime))
SET IDENTITY_INSERT [dbo].[Sliders] OFF
SET IDENTITY_INSERT [dbo].[SliderType] ON 

INSERT [dbo].[SliderType] ([Id], [Name]) VALUES (1, N'Main')
INSERT [dbo].[SliderType] ([Id], [Name]) VALUES (2, N'Feedback')
INSERT [dbo].[SliderType] ([Id], [Name]) VALUES (3, N'Advertisment')
SET IDENTITY_INSERT [dbo].[SliderType] OFF
SET IDENTITY_INSERT [dbo].[TempCartItems] ON 

INSERT [dbo].[TempCartItems] ([Id], [UserId], [CookieId], [ProductId], [QTY], [CartDateTime]) VALUES (27, NULL, N'1563220378777', 1, 1, CAST(N'2019-07-16 10:01:08.613' AS DateTime))
INSERT [dbo].[TempCartItems] ([Id], [UserId], [CookieId], [ProductId], [QTY], [CartDateTime]) VALUES (31, NULL, N'', 5, 1, CAST(N'2019-07-17 11:34:46.567' AS DateTime))
INSERT [dbo].[TempCartItems] ([Id], [UserId], [CookieId], [ProductId], [QTY], [CartDateTime]) VALUES (45, NULL, N'', 2, 1, CAST(N'2019-07-19 13:02:15.107' AS DateTime))
INSERT [dbo].[TempCartItems] ([Id], [UserId], [CookieId], [ProductId], [QTY], [CartDateTime]) VALUES (46, NULL, N'', 2, 1, CAST(N'2019-07-19 13:02:24.810' AS DateTime))
INSERT [dbo].[TempCartItems] ([Id], [UserId], [CookieId], [ProductId], [QTY], [CartDateTime]) VALUES (47, 1, NULL, 5, 1, CAST(N'2019-07-19 20:08:26.463' AS DateTime))
SET IDENTITY_INSERT [dbo].[TempCartItems] OFF
SET IDENTITY_INSERT [dbo].[UserRoleMaps] ON 

INSERT [dbo].[UserRoleMaps] ([Id], [UserId], [UserRoleId]) VALUES (22, 24, 5)
INSERT [dbo].[UserRoleMaps] ([Id], [UserId], [UserRoleId]) VALUES (49, 1, 7)
INSERT [dbo].[UserRoleMaps] ([Id], [UserId], [UserRoleId]) VALUES (50, 1, 1)
INSERT [dbo].[UserRoleMaps] ([Id], [UserId], [UserRoleId]) VALUES (51, 1, 5)
INSERT [dbo].[UserRoleMaps] ([Id], [UserId], [UserRoleId]) VALUES (52, 1, 6)
INSERT [dbo].[UserRoleMaps] ([Id], [UserId], [UserRoleId]) VALUES (53, 1, 3)
INSERT [dbo].[UserRoleMaps] ([Id], [UserId], [UserRoleId]) VALUES (54, 25, 5)
INSERT [dbo].[UserRoleMaps] ([Id], [UserId], [UserRoleId]) VALUES (57, 26, 7)
INSERT [dbo].[UserRoleMaps] ([Id], [UserId], [UserRoleId]) VALUES (58, 23, 5)
INSERT [dbo].[UserRoleMaps] ([Id], [UserId], [UserRoleId]) VALUES (59, 23, 7)
SET IDENTITY_INSERT [dbo].[UserRoleMaps] OFF
SET IDENTITY_INSERT [dbo].[UserRoles] ON 

INSERT [dbo].[UserRoles] ([Id], [Role]) VALUES (1, N'Administrators')
INSERT [dbo].[UserRoles] ([Id], [Role]) VALUES (2, N'Assistant')
INSERT [dbo].[UserRoles] ([Id], [Role]) VALUES (3, N'Forum Moderators')
INSERT [dbo].[UserRoles] ([Id], [Role]) VALUES (4, N'Guests')
INSERT [dbo].[UserRoles] ([Id], [Role]) VALUES (5, N'Registered')
INSERT [dbo].[UserRoles] ([Id], [Role]) VALUES (6, N'Supervisor')
INSERT [dbo].[UserRoles] ([Id], [Role]) VALUES (7, N'Vendors')
SET IDENTITY_INSERT [dbo].[UserRoles] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [ManagerOfVendor], [FirstName], [LastName], [UserImagePath], [AffeliateId], [IsTaxExempt], [AdminComments], [IsNewsLetter], [AddressOne], [AddressTwo], [City], [PhoneNumber], [PostCode], [Country], [EmailAddress], [Password], [ConfirmPassword], [CurrentPassword], [RegistrationConfirmed], [IsActivated], [UserActivateKey], [CreatedBy], [CreatedDate], [LastActiveTime], [EditedBy], [EditedDate], [CreatedIP], [EditedIP]) VALUES (1, 0, N'Admin', N'User', N'uploads/user/c91bbe2f-4d02-44e2-b730-dc5c247e0622-teachers3.jpg', NULL, 0, N'Reference site about Lorem Ipsum, giving information on its origins, as well as a random Lipsum generator.', 0, N'Dhaka, Bangladesh', N'Gulshan. Dhaka, Bangladesh 1212', N'Dhaka', N'01782496285', N'1212', N'Bangladesh', N'admin@gmail.com', N'+hdCdZpGjSbghDPURXUsPoGHmHJr2YbTHINkEkuILQ8=', N'+hdCdZpGjSbghDPURXUsPoGHmHJr2YbTHINkEkuILQ8=', N'+hdCdZpGjSbghDPURXUsPoGHmHJr2YbTHINkEkuILQ8=', 1, 1, N'', 1, CAST(N'2019-07-07 12:28:58.303' AS DateTime), CAST(N'2019-07-08 20:16:34.550' AS DateTime), 1, CAST(N'2019-07-21 13:04:43.220' AS DateTime), NULL, N'169.254.80.80')
INSERT [dbo].[Users] ([UserID], [ManagerOfVendor], [FirstName], [LastName], [UserImagePath], [AffeliateId], [IsTaxExempt], [AdminComments], [IsNewsLetter], [AddressOne], [AddressTwo], [City], [PhoneNumber], [PostCode], [Country], [EmailAddress], [Password], [ConfirmPassword], [CurrentPassword], [RegistrationConfirmed], [IsActivated], [UserActivateKey], [CreatedBy], [CreatedDate], [LastActiveTime], [EditedBy], [EditedDate], [CreatedIP], [EditedIP]) VALUES (23, 0, N'Fahim', N'Numan', N'uploads/user/6ebc73b7-b6de-4848-b6fd-49f85180f7d1-image2.png', NULL, 0, N'asd', 0, N'asdf', N'asdf', N'asfd', N'1782496285', N'af', N'Bangladesh', N'fahiomnuman87@gmail.com', N'ICS6zZNwmwJMFjK+Hj0UBLOX/x5LfE1kSmUlPmjZL9U=', N'ICS6zZNwmwJMFjK+Hj0UBLOX/x5LfE1kSmUlPmjZL9U=', N'ICS6zZNwmwJMFjK+Hj0UBLOX/x5LfE1kSmUlPmjZL9U=', 0, 1, N'6476A31A-E43D-4906-8586-4D1EF6C120DF', NULL, CAST(N'2019-07-19 09:36:17.183' AS DateTime), CAST(N'2019-07-19 09:36:17.183' AS DateTime), 1, CAST(N'2019-07-28 02:28:39.200' AS DateTime), N'10.10.28.164', N'10.10.28.164')
INSERT [dbo].[Users] ([UserID], [ManagerOfVendor], [FirstName], [LastName], [UserImagePath], [AffeliateId], [IsTaxExempt], [AdminComments], [IsNewsLetter], [AddressOne], [AddressTwo], [City], [PhoneNumber], [PostCode], [Country], [EmailAddress], [Password], [ConfirmPassword], [CurrentPassword], [RegistrationConfirmed], [IsActivated], [UserActivateKey], [CreatedBy], [CreatedDate], [LastActiveTime], [EditedBy], [EditedDate], [CreatedIP], [EditedIP]) VALUES (24, NULL, N'Fahim', N'Numan', NULL, NULL, 0, NULL, 0, NULL, NULL, NULL, N'1782496285', NULL, N'Bangladesh', N'2mamun2.bd@gmail.com', N'ICS6zZNwmwJMFjK+Hj0UBLOX/x5LfE1kSmUlPmjZL9U=', N'ICS6zZNwmwJMFjK+Hj0UBLOX/x5LfE1kSmUlPmjZL9U=', N'ICS6zZNwmwJMFjK+Hj0UBLOX/x5LfE1kSmUlPmjZL9U=', 0, 1, N'6C8BABAB-FA38-40C1-A7DA-FA6AD3F85303', NULL, CAST(N'2019-07-19 09:37:13.653' AS DateTime), CAST(N'2019-07-19 09:37:13.653' AS DateTime), 1, CAST(N'2019-07-19 09:38:55.467' AS DateTime), N'10.10.28.164', N'10.10.28.164')
INSERT [dbo].[Users] ([UserID], [ManagerOfVendor], [FirstName], [LastName], [UserImagePath], [AffeliateId], [IsTaxExempt], [AdminComments], [IsNewsLetter], [AddressOne], [AddressTwo], [City], [PhoneNumber], [PostCode], [Country], [EmailAddress], [Password], [ConfirmPassword], [CurrentPassword], [RegistrationConfirmed], [IsActivated], [UserActivateKey], [CreatedBy], [CreatedDate], [LastActiveTime], [EditedBy], [EditedDate], [CreatedIP], [EditedIP]) VALUES (25, 0, N'Test', N'User', NULL, NULL, 0, NULL, 0, NULL, NULL, NULL, N'01782496285', NULL, N'Bangladesh', N'test@gmail.com', N'+hdCdZpGjSbghDPURXUsPoGHmHJr2YbTHINkEkuILQ8=', N'+hdCdZpGjSbghDPURXUsPoGHmHJr2YbTHINkEkuILQ8=', N'+hdCdZpGjSbghDPURXUsPoGHmHJr2YbTHINkEkuILQ8=', 1, 1, N'', 1, CAST(N'2019-07-07 12:28:58.303' AS DateTime), CAST(N'2019-07-08 20:16:34.550' AS DateTime), 0, CAST(N'2019-07-21 21:58:27.727' AS DateTime), NULL, N'10.10.28.164')
INSERT [dbo].[Users] ([UserID], [ManagerOfVendor], [FirstName], [LastName], [UserImagePath], [AffeliateId], [IsTaxExempt], [AdminComments], [IsNewsLetter], [AddressOne], [AddressTwo], [City], [PhoneNumber], [PostCode], [Country], [EmailAddress], [Password], [ConfirmPassword], [CurrentPassword], [RegistrationConfirmed], [IsActivated], [UserActivateKey], [CreatedBy], [CreatedDate], [LastActiveTime], [EditedBy], [EditedDate], [CreatedIP], [EditedIP]) VALUES (26, 0, N'test', N'user', N'uploads/user/e8962a48-eb52-49c2-b060-02546d2ad3aa-image2.png', NULL, 0, N'asdfasdfasdf', 0, N'adsf adsf ', N'asdf asdf', N' asdf', N'234234', N'afasd', N'Bangladesh', N'shohanur.rahman57@gmail.com', N'+hdCdZpGjSbghDPURXUsPoGHmHJr2YbTHINkEkuILQ8=', N'+hdCdZpGjSbghDPURXUsPoGHmHJr2YbTHINkEkuILQ8=', N'+hdCdZpGjSbghDPURXUsPoGHmHJr2YbTHINkEkuILQ8=', 1, 1, N'D2B7D047-FE9B-44F2-918C-2A9D70885091', 1, CAST(N'2019-07-28 11:16:11.013' AS DateTime), CAST(N'2019-07-28 11:16:11.013' AS DateTime), 1, CAST(N'2019-07-28 11:48:18.353' AS DateTime), N'169.254.80.80', N'169.254.80.80')
SET IDENTITY_INSERT [dbo].[Users] OFF
/****** Object:  StoredProcedure [dbo].[Admin_Get_AllProductList]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Admin_Get_AllProductList]
AS 
BEGIN 

	SELECT
	p.Id,
	p.Name,
	p.ImageURL,
	p.[Published],
	p.[Price],
	p.[CreatedDate],
	p.[SKU],
	[dbo].[uf_Get_Product_Categories_By_Product_Id](p.Id) As Categories
	
	from [dbo].[PRODUCTS] p
END



GO
/****** Object:  StoredProcedure [dbo].[Admin_Get_CategoryList]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Admin_Get_CategoryList]
AS 
BEGIN 

	SELECT
	c.Id,
	c.Name,
	[dbo].[uf_Get_Prodcut_category_Path](c.Id) as FullName,
	c.Published,
	c.[CreatedDate],
	c.[DisplayOrder]

	from [dbo].[PRODUCT_CATEGORIES] c
END



GO
/****** Object:  StoredProcedure [dbo].[Admin_Get_OrderList]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Admin_Get_OrderList]
AS 
BEGIN 

	SELECT 
	[Id],
	[ProductPrice],
	[TotalAmount],
	[CustomerName],
	[CustomerEmail],
	[OrderStatusId],
	[OrderDateTime],
	[PaymentMethod],
	[dbo].[uf_Get_Order_Status_By_Id] ([OrderStatusId]) StatusText

	from [PRODUCT_ORDERS]
END



GO
/****** Object:  StoredProcedure [dbo].[Admin_Get_Parent_CategoryList]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Admin_Get_Parent_CategoryList]
AS 
BEGIN 

	SELECT
	c.Id,
	c.Name,
	[dbo].[uf_Get_Prodcut_category_Path](c.Id) as FullName

	from [dbo].[PRODUCT_CATEGORIES] c where c.Published=1
END



GO
/****** Object:  StoredProcedure [dbo].[Admin_Get_PostDetailsByPostId]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Admin_Get_PostDetailsByPostId] (
	@id int
)
AS 
BEGIN 

	SELECT 
	s.Id, 
	s.Title,
	s.SortDetails,
	s.Descriptions,
	s.FeaturedImagePath,
	s.CreatedDate,
	[dbo].[uf_AdminNameByAdminId] (s.CreatedId) AS AdminName,
	[dbo].[uf_TotalCommentsByPostId] (s.Id) AS TotalsComments

	from [dbo].[BlogPosts] s where s.Id= @id order by s.Id desc

END



GO
/****** Object:  StoredProcedure [dbo].[Admin_Get_PostList]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Admin_Get_PostList] 
AS 
BEGIN 

	SELECT 
	s.Id, 
	s.Title,
	s.SortDetails,
	s.Descriptions,
	s.FeaturedImagePath,
	s.CreatedDate,
	[dbo].[uf_AdminNameByAdminId] (s.CreatedId) AS AdminName,
	[dbo].[uf_TotalCommentsByPostId] (s.Id) AS TotalsComments

	from [dbo].[BlogPosts] s order by s.Id desc

END



GO
/****** Object:  StoredProcedure [dbo].[Admin_Get_SliderList]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Admin_Get_SliderList] 
AS 
BEGIN 

	SELECT 
	s.Id, 
	s.Name,
	s.Caption,
	s.SortDescription,
	s.ImagePath,
	s.ThumbnailPath,
	[dbo].[uf_ProductCategoryNameByCategoryId] (s.CategoryId) AS CategoryName,
	convert(varchar(10),CreatedDateTime,103) CreatedDate

	from [dbo].[Sliders] s

END



GO
/****** Object:  StoredProcedure [dbo].[Admin_Get_User_List]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Admin_Get_User_List] 
AS 
BEGIN 

	SELECT 
	u.[UserID],
	[dbo].[uf_AdminNameByAdminId](u.[UserID]) as UserFullName,
	u.[UserImagePath],
	u.[EmailAddress],
	u.[RegistrationConfirmed],
	u.[PhoneNumber],
	u.[CreatedDate],
	u.[LastActiveTime],
	[dbo].[uf_Get_User_Roles_With_Comma] (u.[UserID]) UserRole,
	u.[IsActivated]
	FROM [dbo].[Users] u

	order by u.[UserID] desc
END



GO
/****** Object:  StoredProcedure [dbo].[Admin_Get_Vendor_List]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Admin_Get_Vendor_List] 
AS 
BEGIN 

	SELECT 
	u.[UserID],
	[dbo].[uf_AdminNameByAdminId](u.[UserID]) as UserFullName,
	u.[UserImagePath],
	u.[EmailAddress],
	u.[RegistrationConfirmed],
	u.[PhoneNumber],
	u.[CreatedDate],
	u.[LastActiveTime],
	[dbo].[uf_Get_User_Roles_With_Comma] (u.[UserID]) UserRole,
	u.[IsActivated]
	FROM [dbo].[Users] u

	INNER JOIN [dbo].[UserRoleMaps] mp on mp.[UserId]=u.[UserID]

	where mp.[UserRoleId]=7

	order by u.[UserID] desc
END



GO
/****** Object:  StoredProcedure [dbo].[Admin_Get_VendorList]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Admin_Get_VendorList]
AS 
BEGIN 

	DECLARE @VendorList TABLE
	(
		Id bigint,
		Name nvarchar(200)
	)
	insert into @VendorList (Id, Name) values (0,'Not a vendor');
	INSERT INTO @VendorList (Id, Name)
	select u.[UserId], u.FirstName + ' ' + u.Lastname from [dbo].[Users] u
	INNER JOIN [dbo].[UserRoleMaps] m on m.[UserId]=u.[UserId]
	where m.[UserRoleId]=7
	select * from @VendorList;

END



GO
/****** Object:  StoredProcedure [dbo].[Blog_Get_PostCommentsByPostId]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Blog_Get_PostCommentsByPostId] (
	@id int
)
AS 
BEGIN 

	SELECT 
	s.Id, 
	s.UserId,
	s.Name,
	s.Emial,
	s.Coments,
	s.DateTime,
	[dbo].[uf_AdminNameByAdminId] (s.UserId) AS AdminName

	from [dbo].[BlogComments] s where s.PostId= @id order by s.Id desc

END



GO
/****** Object:  StoredProcedure [dbo].[Customer_Get_OrderList_By_CustomerId]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Customer_Get_OrderList_By_CustomerId](
	@customerId bigint
)
AS 
BEGIN 

	SELECT 
	[Id],
	[ProductPrice],
	[TotalAmount],
	[CustomerName],
	[CustomerEmail],
	[OrderStatusId],
	[OrderDateTime],
	[PaymentMethod],
	[dbo].[uf_Get_Order_Status_By_Id] ([OrderStatusId]) StatusText

	from [PRODUCT_ORDERS] where CustomerId=@customerId
END



GO
/****** Object:  StoredProcedure [dbo].[Get_Autocomplete_Product_List]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_Autocomplete_Product_List]
AS 
BEGIN 

	SELECT
	p.Id,
	p.Name,
	p.ImageURL

	from [dbo].[PRODUCTS] p 

	where p.[Published]=1
	
END



GO
/****** Object:  StoredProcedure [dbo].[Get_Category_Parent_Items]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_Category_Parent_Items](
	@catID int
)
AS 
BEGIN 

	Declare @TABLEVAR table (id int ,parentid int)
	INSERT INTO @TABLEVAR(id,parentid) values(0,@catID)

	;with name_tree as 
	(
	   select id, ParentCatId
	   from PRODUCT_CATEGORIES
	   where id = @catID -- this is the starting point you want in your recursion
	   union all
	   select C.id, C.ParentCatId
	   from PRODUCT_CATEGORIES c
	   join name_tree p on C.id = P.ParentCatId
		AND C.id<>C.ParentCatId
	) 
	INSERT INTO @TABLEVAR
	select *
	from name_tree
	OPTION (MAXRECURSION 0)

	SELECT [dbo].[uf_Get_Prodcut_category_Name_By_CatID] (parentid) Name, parentid FROM @TABLEVAR order by parentid asc

END



GO
/****** Object:  StoredProcedure [dbo].[Get_Category_Product_For_HomePage]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_Category_Product_For_HomePage](
	@catId int
)
AS 
BEGIN 

	SET ROWCOUNT 4;

	SELECT
	p.Id,
	p.Name,
	p.ImageURL,
	p.[Published],
	p.[Price],
	p.[OldPrice],
	[dbo].[uf_Get_Product_Rating_By_ProductId](p.Id) as Rating
	
	from [dbo].[PRODUCTS] p 
	INNER JOIN [dbo].[PRODUCT_CATEGPRY_MAP] cm on cm.ProductId = p.Id

	where cm.CategoryId=@catId and p.[Published]=1 and p.ShowOnHomePage=1
	group by p.Id,p.Name,p.ImageURL,p.[Published],p.[Price],p.[OldPrice]
	order by p.Id desc
END



GO
/****** Object:  StoredProcedure [dbo].[Get_Featured_Product_For_HomePage]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_Featured_Product_For_HomePage]
AS 
BEGIN 

	SET ROWCOUNT 10;

	SELECT
	p.Id,
	p.Name,
	p.ImageURL,
	p.[Published],
	p.[Price],
	p.[OldPrice],
	[dbo].[uf_Get_Product_Rating_By_ProductId](p.Id) as Rating
	
	from [dbo].[PRODUCTS] p 
	INNER JOIN [dbo].[PRODUCT_CATEGPRY_MAP] cm on cm.ProductId = p.Id

	where p.[Published]=1 and p.ShowOnHomePage=1
	group by p.Id,p.Name,p.ImageURL,p.[Published],p.[Price],p.[OldPrice]
	order by p.Id desc
END



GO
/****** Object:  StoredProcedure [dbo].[Get_Popular_PostList]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_Popular_PostList] 
AS 
BEGIN 

	select top 10
	mp.PostId, sum(mp.Id) Total,
	p.Title,
	p.FeaturedImagePath,
	p.CreatedDate
	from BlogComments mp
	inner join BlogPosts p on p.Id=mp.PostId
	group by mp.PostId,p.Title,p.FeaturedImagePath,p.CreatedDate
	Order by sum(mp.Id) desc

END



GO
/****** Object:  StoredProcedure [dbo].[Get_Product_CAtegoryList_By_ProductId]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_Product_CAtegoryList_By_ProductId](
	@id int
)
AS 
BEGIN 

	select 
	[dbo].[uf_Get_Prodcut_category_Name_By_CatID](CategoryId) Name, 
	CategoryId from [dbo].[PRODUCT_CATEGPRY_MAP] 
	where ProductId=@id order by CategoryId desc
END



GO
/****** Object:  StoredProcedure [dbo].[Get_Product_Details_By_ProductId]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_Product_Details_By_ProductId](
	@id int
)
AS 
BEGIN 
	SELECT
	p.Id,
	p.Name,
	p.[SortDescription],
	p.[Description],
	p.ImageURL,
	p.[Published],
	p.[Price],
	p.[OldPrice],
	[dbo].[uf_Get_Product_Rating_By_ProductId](p.Id) as Rating
	
	from [dbo].[PRODUCTS] p 
	where p.Id=@id

END



GO
/****** Object:  StoredProcedure [dbo].[Get_Product_ImageUrl_List_By_ProductId]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_Product_ImageUrl_List_By_ProductId](
	@id int
)
AS 
BEGIN 
	SELECT
	
	i.ImagePath
	
	from [dbo].[PRODUCT_IMAGE_MAP] i 
	where i.ProductId=@id

END



GO
/****** Object:  StoredProcedure [dbo].[Get_Product_List_By_Order_Id]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_Product_List_By_Order_Id](
	@orderBy int
)
AS 
BEGIN 
	
	SELECT 
	p.*,
	[dbo].[uf_Product_Image_By_ProductId] (p.ProductId) ImageURL
	 from
	[dbo].[PRODUCT_ORDER_MAP] p

	where ORDERID=@orderBy
	
END



GO
/****** Object:  StoredProcedure [dbo].[Get_Product_List_By_Param]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_Product_List_By_Param](
	@catID int,
	@orderBy int,
	@min int,
	@max int
)
AS 
BEGIN 

	--DECLARE @catID int;
	--SET @catID =1

	DECLARE @Temp_Product TABLE(
		id int, Name nvarchar(max), 
		ImageURL nvarchar(max), Published bit, Price decimal(18,2), 
		OldPrice  decimal(18,2), SortDescription nvarchar(max), Rating decimal(18,2)
		)
	
	if @catID > 0 and @min > 0 and @max > 0
		BEGIN
			INSERT into @Temp_Product(id,Name,ImageURL,Published,Price,OldPrice,SortDescription,Rating)
			SELECT
			p.Id,
			p.Name,
			p.ImageURL,
			p.[Published],
			p.[Price],
			p.[OldPrice],
			p.[SortDescription],
			[dbo].[uf_Get_Product_Rating_By_ProductId](p.Id) as Rating
	
			from [dbo].[PRODUCTS] p 
			INNER JOIN [dbo].[PRODUCT_CATEGPRY_MAP] cm on cm.ProductId = p.Id

			where cm.CategoryId= @catID and p.[Published]=1 and p.[Price] >= @min and p.[Price]<=@max
			group by p.Id,p.Name,p.ImageURL,p.[Published],p.[Price],p.[OldPrice],p.[SortDescription]
		END

	if @catID > 0 and @min = 0 and @max > 0
		BEGIN
			INSERT into @Temp_Product(id,Name,ImageURL,Published,Price,OldPrice,SortDescription,Rating)
			SELECT
			p.Id,
			p.Name,
			p.ImageURL,
			p.[Published],
			p.[Price],
			p.[OldPrice],
			p.[SortDescription],
			[dbo].[uf_Get_Product_Rating_By_ProductId](p.Id) as Rating
	
			from [dbo].[PRODUCTS] p 
			INNER JOIN [dbo].[PRODUCT_CATEGPRY_MAP] cm on cm.ProductId = p.Id

			where cm.CategoryId= @catID and p.[Published]=1 and p.[Price]<=@max
			group by p.Id,p.Name,p.ImageURL,p.[Published],p.[Price],p.[OldPrice],p.[SortDescription]
		END

	if @catID = 0 and @min = 0 and @max = 0
		BEGIN
			INSERT into @Temp_Product(id,Name,ImageURL,Published,Price,OldPrice,SortDescription,Rating)
			SELECT
			p.Id,
			p.Name,
			p.ImageURL,
			p.[Published],
			p.[Price],
			p.[OldPrice],
			p.[SortDescription],
			[dbo].[uf_Get_Product_Rating_By_ProductId](p.Id) as Rating
	
			from [dbo].[PRODUCTS] p 
			INNER JOIN [dbo].[PRODUCT_CATEGPRY_MAP] cm on cm.ProductId = p.Id

			where p.[Published]=1
			group by p.Id,p.Name,p.ImageURL,p.[Published],p.[Price],p.[OldPrice],p.[SortDescription]
		END


	if @catID > 0 and @min > 0 and @max= 0
		BEGIN
			INSERT into @Temp_Product(id,Name,ImageURL,Published,Price,OldPrice,SortDescription,Rating)
			SELECT
			p.Id,
			p.Name,
			p.ImageURL,
			p.[Published],
			p.[Price],
			p.[OldPrice],
			p.[SortDescription],
			[dbo].[uf_Get_Product_Rating_By_ProductId](p.Id) as Rating
	
			from [dbo].[PRODUCTS] p 
			INNER JOIN [dbo].[PRODUCT_CATEGPRY_MAP] cm on cm.ProductId = p.Id

			where cm.CategoryId= @catID and p.[Published]=1 and p.[Price] >= @min
			group by p.Id,p.Name,p.ImageURL,p.[Published],p.[Price],p.[OldPrice],p.[SortDescription]
		END

	if @catID = 0 and @min > 0 and @max= 0
		BEGIN
			INSERT into @Temp_Product(id,Name,ImageURL,Published,Price,OldPrice,SortDescription,Rating)
			SELECT
			p.Id,
			p.Name,
			p.ImageURL,
			p.[Published],
			p.[Price],
			p.[OldPrice],
			p.[SortDescription],
			[dbo].[uf_Get_Product_Rating_By_ProductId](p.Id) as Rating
	
			from [dbo].[PRODUCTS] p 
			INNER JOIN [dbo].[PRODUCT_CATEGPRY_MAP] cm on cm.ProductId = p.Id

			where p.[Published]=1 and p.[Price] >= @min
			group by p.Id,p.Name,p.ImageURL,p.[Published],p.[Price],p.[OldPrice],p.[SortDescription]
		END

	if @catID = 0 and @min > 0 and @max > 0
		BEGIN
			INSERT into @Temp_Product(id,Name,ImageURL,Published,Price,OldPrice,SortDescription,Rating)
			SELECT
			p.Id,
			p.Name,
			p.ImageURL,
			p.[Published],
			p.[Price],
			p.[OldPrice],
			p.[SortDescription],
			[dbo].[uf_Get_Product_Rating_By_ProductId](p.Id) as Rating
	
			from [dbo].[PRODUCTS] p 
			INNER JOIN [dbo].[PRODUCT_CATEGPRY_MAP] cm on cm.ProductId = p.Id

			where p.[Published]=1 and p.[Price] >= @min and p.[Price]<=@max
			group by p.Id,p.Name,p.ImageURL,p.[Published],p.[Price],p.[OldPrice],p.[SortDescription]
		END

	if @catID = 0 and @min = 0 and @max > 0
		BEGIN
			INSERT into @Temp_Product(id,Name,ImageURL,Published,Price,OldPrice,SortDescription,Rating)
			SELECT
			p.Id,
			p.Name,
			p.ImageURL,
			p.[Published],
			p.[Price],
			p.[OldPrice],
			p.[SortDescription],
			[dbo].[uf_Get_Product_Rating_By_ProductId](p.Id) as Rating
	
			from [dbo].[PRODUCTS] p 
			INNER JOIN [dbo].[PRODUCT_CATEGPRY_MAP] cm on cm.ProductId = p.Id

			where p.[Published]=1 and p.[Price]<=@max
			group by p.Id,p.Name,p.ImageURL,p.[Published],p.[Price],p.[OldPrice],p.[SortDescription]
		END


		if @orderBy = 0
			select * from @Temp_Product  order by Id desc

		if @orderBy = 1
			select * from @Temp_Product  order by Name asc

		if @orderBy = 2
			select * from @Temp_Product  order by Name desc

		if @orderBy = 3
			select * from @Temp_Product  order by Price desc

		if @orderBy = 4
			select * from @Temp_Product  order by Price asc
	
END



GO
/****** Object:  StoredProcedure [dbo].[Get_Product_List_By_Search_Value]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_Product_List_By_Search_Value](
	@searchText nvarchar(max),
	@orderBy int,
	@min int,
	@max int
)
AS 
BEGIN 

	--DECLARE @catID int;
	--SET @catID =1

	DECLARE @Temp_Product TABLE(
		id int, Name nvarchar(max), 
		ImageURL nvarchar(max), Published bit, Price decimal(18,2), 
		OldPrice  decimal(18,2), SortDescription nvarchar(max), Rating decimal(18,2)
		)
	
	if @searchText !='' and  @min = 0 and @max = 0
		BEGIN
			INSERT into @Temp_Product(id,Name,ImageURL,Published,Price,OldPrice,SortDescription,Rating)
			SELECT
			p.Id,
			p.Name,
			p.ImageURL,
			p.[Published],
			p.[Price],
			p.[OldPrice],
			p.[SortDescription],
			[dbo].[uf_Get_Product_Rating_By_ProductId](p.Id) as Rating
	
			from [dbo].[PRODUCTS] p 
			INNER JOIN [dbo].[PRODUCT_CATEGPRY_MAP] cm on cm.ProductId = p.Id

			where (p.[Name] LIKE +'%'+ @searchText +'%' ) and p.[Published]=1
			group by p.Id,p.Name,p.ImageURL,p.[Published],p.[Price],p.[OldPrice],p.[SortDescription]
		END

	if @searchText !='' and  @min > 0 and @max > 0
		BEGIN
			INSERT into @Temp_Product(id,Name,ImageURL,Published,Price,OldPrice,SortDescription,Rating)
			SELECT
			p.Id,
			p.Name,
			p.ImageURL,
			p.[Published],
			p.[Price],
			p.[OldPrice],
			p.[SortDescription],
			[dbo].[uf_Get_Product_Rating_By_ProductId](p.Id) as Rating
	
			from [dbo].[PRODUCTS] p 
			INNER JOIN [dbo].[PRODUCT_CATEGPRY_MAP] cm on cm.ProductId = p.Id

			where (p.[Name] LIKE +'%'+ @searchText +'%' ) and p.[Published]=1 and p.[Price] >= @min and p.[Price]<=@max
			group by p.Id,p.Name,p.ImageURL,p.[Published],p.[Price],p.[OldPrice],p.[SortDescription]
		END

	if @searchText !='' and @min = 0 and @max > 0
		BEGIN
			INSERT into @Temp_Product(id,Name,ImageURL,Published,Price,OldPrice,SortDescription,Rating)
			SELECT
			p.Id,
			p.Name,
			p.ImageURL,
			p.[Published],
			p.[Price],
			p.[OldPrice],
			p.[SortDescription],
			[dbo].[uf_Get_Product_Rating_By_ProductId](p.Id) as Rating
	
			from [dbo].[PRODUCTS] p 
			INNER JOIN [dbo].[PRODUCT_CATEGPRY_MAP] cm on cm.ProductId = p.Id

			where (p.[Name] LIKE +'%'+ @searchText +'%' ) and p.[Published]=1 and p.[Price]<=@max
			group by p.Id,p.Name,p.ImageURL,p.[Published],p.[Price],p.[OldPrice],p.[SortDescription]
		END



	if @searchText !='' and @min > 0 and @max= 0
		BEGIN
			INSERT into @Temp_Product(id,Name,ImageURL,Published,Price,OldPrice,SortDescription,Rating)
			SELECT
			p.Id,
			p.Name,
			p.ImageURL,
			p.[Published],
			p.[Price],
			p.[OldPrice],
			p.[SortDescription],
			[dbo].[uf_Get_Product_Rating_By_ProductId](p.Id) as Rating
	
			from [dbo].[PRODUCTS] p 
			INNER JOIN [dbo].[PRODUCT_CATEGPRY_MAP] cm on cm.ProductId = p.Id

			where (p.[Name] LIKE +'%'+ @searchText +'%' ) and p.[Published]=1 and p.[Price] >= @min
			group by p.Id,p.Name,p.ImageURL,p.[Published],p.[Price],p.[OldPrice],p.[SortDescription]
		END



		if @orderBy = 0
			select * from @Temp_Product  order by Id desc

		if @orderBy = 1
			select * from @Temp_Product  order by Name asc

		if @orderBy = 2
			select * from @Temp_Product  order by Name desc

		if @orderBy = 3
			select * from @Temp_Product  order by Price desc

		if @orderBy = 4
			select * from @Temp_Product  order by Price asc
	
END



GO
/****** Object:  StoredProcedure [dbo].[Get_Related_Product_By_CategoryId]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_Related_Product_By_CategoryId](
	@catID int
)
AS 
BEGIN 

	SET ROWCOUNT 10
	SELECT
	p.Id,
	p.Name,
	p.ImageURL,
	p.[Published],
	p.[Price],
	p.[OldPrice],
	[dbo].[uf_Get_Product_Rating_By_ProductId](p.Id) as Rating
	
	from [dbo].[PRODUCTS] p 
	INNER JOIN [dbo].[PRODUCT_CATEGPRY_MAP] cm on cm.ProductId = p.Id

	where cm.CategoryId= @catID and p.[Published]=1
	group by p.Id,p.Name,p.ImageURL,p.[Published],p.[Price],p.[OldPrice]
END



GO
/****** Object:  StoredProcedure [dbo].[Get_SiteSetting_HomePage_Categories]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_SiteSetting_HomePage_Categories]
AS 
BEGIN 

	select 
	[dbo].[uf_Get_Prodcut_category_Name_By_CatID] (CategoryIdOne) CategoryName1,
	[dbo].[uf_Get_Prodcut_category_Name_By_CatID] (CategoryIdTwo) CategoryName2,
	[dbo].[uf_Get_Prodcut_category_Name_By_CatID] (CategoryIdThree) CategoryName3,
	[dbo].[uf_Get_Prodcut_category_Name_By_CatID] (CategoryIdFour) CategoryName4,
	[dbo].[uf_Get_Prodcut_category_Name_By_CatID] (CategoryIdFive) CategoryName5
	from [dbo].[SiteSettings]
END



GO
/****** Object:  StoredProcedure [dbo].[Get_Temp_CartList_By_Params]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_Temp_CartList_By_Params](
	@cookieId nvarchar(max),
	@uId bigint
	
)
AS 
BEGIN 

	if @uId > 0
		BEGIN
			select 
			p.[ImageURL],
			--sum(p.[Price]) TotalPrice,
			p.[Price],
			p.[Name],
			p.[Id],
			sum(c.[QTY]) TotalQTY,
			[dbo].[uf_Get_Prodcut_Total_Price_By_QTY_and_Price] (p.[Price], sum(c.[QTY])) TotalPrice
			from [dbo].[PRODUCTS] p
			INNER JOIN [dbo].[TempCartItems] c on c.[ProductId]=p.[Id]
			where c.[UserId]=@uId
			group by p.[Id],p.[ImageURL],p.[Name],p.[Price]
		END

	if @uId =0 and @cookieId !='' and @cookieId !='0'
		BEGIN
			select 
			p.[ImageURL],
			--sum(p.[Price]) TotalPrice,
			p.[Price],
			p.[Name],
			p.[Id],
			sum(c.[QTY]) TotalQTY,
			[dbo].[uf_Get_Prodcut_Total_Price_By_QTY_and_Price] (p.[Price], sum(c.[QTY])) TotalPrice
			from [dbo].[PRODUCTS] p

			INNER JOIN [dbo].[TempCartItems] c on c.[ProductId]=p.[Id]
			where c.CookieId=@cookieId
			group by p.[Id],p.[ImageURL],p.[Name],p.[Price]
		END
END



GO
/****** Object:  StoredProcedure [dbo].[Get_Temp_CartList_By_Params_For_Cart_Page]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_Temp_CartList_By_Params_For_Cart_Page](
	@cookieId nvarchar(max),
	@uId bigint
)
AS 
BEGIN 

	if @uId > 0
		BEGIN
			select 
			p.[ImageURL],
			sum(p.[Price]) TotalPrice,
			p.[Price],
			p.[Name],
			p.[Id],
			sum(c.[QTY]) TotalQTY

			from [dbo].[PRODUCTS] p
			INNER JOIN [dbo].[TempCartItems] c on c.[ProductId]=p.[Id]
			where c.[UserId]=@uId
			group by p.[Id],p.[ImageURL],p.[Name],p.[Price]
		END

	if @uId =0 and @cookieId !=''
		BEGIN
			select 
			p.[ImageURL],
			sum(p.[Price]) TotalPrice,
			p.[Price],
			p.[Name],
			p.[Id],
			sum(c.[QTY]) TotalQTY

			from [dbo].[PRODUCTS] p
			INNER JOIN [dbo].[TempCartItems] c on c.[ProductId]=p.[Id]
			where c.CookieId=@cookieId
			group by p.[Id],p.[ImageURL],p.[Name],p.[Price]
		END
END



GO
/****** Object:  StoredProcedure [dbo].[Get_Top_Ten_Best_Selling_Product]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_Top_Ten_Best_Selling_Product]
AS 
BEGIN 

	select top 10 
	mp.ProductId, sum(mp.QTY) NumberOfSell,
	p.Name,
	p.ImageURL,
	p.[Price],
	p.[OldPrice],
	[dbo].[uf_Get_Product_Rating_By_ProductId](mp.ProductId) as Rating

	from PRODUCT_ORDER_MAP mp
	inner join [dbo].[PRODUCTS] p on p.Id=mp.ProductId
	group by mp.ProductId,p.Name,p.ImageURL,p.[Price],p.[OldPrice]
	Order by sum(mp.QTY) desc
END



GO
/****** Object:  StoredProcedure [dbo].[Get_User_Permission_Status]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_User_Permission_Status](
	
	@userId bigint
)
AS 
BEGIN 
	
	DECLARE @TempRoleTable TABLE(
		HasAdministrators int, 
		HasAssistant int, 
		HasForumModerators int,
		HasGuests int,
		HasRegistered int, 
		HasSupervisor int, 
		HasVendors int
	)
	DECLARE @HasAdministrators int;
	DECLARE @HasAssistant int;
	DECLARE @HasForumModerators int;
	DECLARE @HasGuests int;
	DECLARE @HasRegistered int;
	DECLARE @HasSupervisor int;
	DECLARE @HasVendors int;

	select @HasAdministrators = [dbo].[uf_Get_Admin_Roles_Status_By_UserId_And_RoleId] (@userId,1);
	select @HasAssistant = [dbo].[uf_Get_Admin_Roles_Status_By_UserId_And_RoleId] (@userId,2);
	select @HasForumModerators = [dbo].[uf_Get_Admin_Roles_Status_By_UserId_And_RoleId] (@userId,3);
	select @HasGuests = [dbo].[uf_Get_Admin_Roles_Status_By_UserId_And_RoleId] (@userId,4);
	select @HasRegistered = [dbo].[uf_Get_Admin_Roles_Status_By_UserId_And_RoleId] (@userId,5);
	select @HasSupervisor = [dbo].[uf_Get_Admin_Roles_Status_By_UserId_And_RoleId] (@userId,6);
	select @HasVendors = [dbo].[uf_Get_Admin_Roles_Status_By_UserId_And_RoleId] (@userId,7);
	
	INSERT INTO 
	@TempRoleTable(HasAdministrators,HasAssistant,HasForumModerators,HasGuests,HasRegistered,HasSupervisor,HasVendors)
	VALUES(@HasAdministrators,@HasAssistant,@HasForumModerators,@HasGuests,@HasRegistered,@HasSupervisor,@HasVendors);

	select * from @TempRoleTable;
END



GO
/****** Object:  StoredProcedure [dbo].[Vendor_Get_Product_List]    Script Date: 7/31/2019 7:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Vendor_Get_Product_List](
	@userId bigint
)
AS 
BEGIN 

	SELECT
	p.Id,
	p.Name,
	p.ImageURL,
	p.[Published],
	p.[Price],
	p.[CreatedDate],
	p.[SKU],
	[dbo].[uf_Get_Product_Categories_By_Product_Id](p.Id) As Categories
	
	from [dbo].[PRODUCTS] p where p.[CreatedId]=@userId
END



GO
