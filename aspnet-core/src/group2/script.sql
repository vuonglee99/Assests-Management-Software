USE [DbPratice]
GO
/****** Object:  StoredProcedure [dbo].[ThietBi_GetAll]    Script Date: 18/06/2020 10:38:01 CH ******/
DROP PROCEDURE IF EXISTS [dbo].[ThietBi_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[ThietBi_ById]    Script Date: 18/06/2020 10:38:01 CH ******/
DROP PROCEDURE IF EXISTS [dbo].[ThietBi_ById]
GO
/****** Object:  StoredProcedure [dbo].[KiemKe_Update]    Script Date: 18/06/2020 10:38:01 CH ******/
DROP PROCEDURE IF EXISTS [dbo].[KiemKe_Update]
GO
/****** Object:  StoredProcedure [dbo].[KiemKe_Insert]    Script Date: 18/06/2020 10:38:01 CH ******/
DROP PROCEDURE IF EXISTS [dbo].[KiemKe_Insert]
GO
/****** Object:  StoredProcedure [dbo].[KiemKe_GetById]    Script Date: 18/06/2020 10:38:01 CH ******/
DROP PROCEDURE IF EXISTS [dbo].[KiemKe_GetById]
GO
/****** Object:  StoredProcedure [dbo].[ChiTietKiemKe_XacNhanKK]    Script Date: 18/06/2020 10:38:01 CH ******/
DROP PROCEDURE IF EXISTS [dbo].[ChiTietKiemKe_XacNhanKK]
GO
/****** Object:  StoredProcedure [dbo].[ChiTietKiemKe_Insert]    Script Date: 18/06/2020 10:38:01 CH ******/
DROP PROCEDURE IF EXISTS [dbo].[ChiTietKiemKe_Insert]
GO
/****** Object:  StoredProcedure [dbo].[ChiTietKiemKe_GetByIdKK_IdDV]    Script Date: 18/06/2020 10:38:01 CH ******/
DROP PROCEDURE IF EXISTS [dbo].[ChiTietKiemKe_GetByIdKK_IdDV]
GO
/****** Object:  StoredProcedure [dbo].[ChiTietKiemKe_Get]    Script Date: 18/06/2020 10:38:01 CH ******/
DROP PROCEDURE IF EXISTS [dbo].[ChiTietKiemKe_Get]
GO
/****** Object:  StoredProcedure [dbo].[BKK_Get]    Script Date: 18/06/2020 10:38:01 CH ******/
DROP PROCEDURE IF EXISTS [dbo].[BKK_Get]
GO
/****** Object:  StoredProcedure [dbo].[BKK_CHECK]    Script Date: 18/06/2020 10:38:01 CH ******/
DROP PROCEDURE IF EXISTS [dbo].[BKK_CHECK]
GO
/****** Object:  Table [dbo].[ThietBi]    Script Date: 18/06/2020 10:38:01 CH ******/
DROP TABLE IF EXISTS [dbo].[ThietBi]
GO
/****** Object:  Table [dbo].[KiemKe]    Script Date: 18/06/2020 10:38:01 CH ******/
DROP TABLE IF EXISTS [dbo].[KiemKe]
GO
/****** Object:  Table [dbo].[ChiTietBanKiemKe]    Script Date: 18/06/2020 10:38:01 CH ******/
DROP TABLE IF EXISTS [dbo].[ChiTietBanKiemKe]
GO
/****** Object:  Table [dbo].[BRANCH]    Script Date: 18/06/2020 10:38:01 CH ******/
DROP TABLE IF EXISTS [dbo].[BRANCH]
GO
/****** Object:  Table [dbo].[BRANCH]    Script Date: 18/06/2020 10:38:01 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BRANCH](
	[BRANCH_ID] [varchar](20) NOT NULL,
	[FATHER_ID] [varchar](20) NULL,
	[IS_POTENTIAL] [varchar](1) NULL,
	[BRANCH_CODE] [varchar](10) NULL,
	[BRANCH_NAME] [nvarchar](200) NULL,
	[DAO_CODE] [varchar](40) NULL,
	[DAO_NAME] [nvarchar](500) NULL,
	[REGION_ID] [varchar](15) NULL,
	[BRANCH_TYPE] [varchar](5) NULL,
	[BRANCH_EMAIL] [varchar](50) NULL,
	[BRANCH_FAX] [varchar](50) NULL,
	[BRANCH_STATUS] [varchar](50) NULL,
	[ADDR] [nvarchar](200) NULL,
	[PROVICE] [nvarchar](500) NULL,
	[TEL] [varchar](20) NULL,
	[TAX_NO] [varchar](50) NULL,
	[NOTES] [nvarchar](1000) NULL,
	[RECORD_STATUS] [varchar](1) NULL,
	[MAKER_ID] [varchar](15) NULL,
	[CREATE_DT] [datetime] NULL,
	[AUTH_STATUS] [varchar](50) NULL,
	[CHECKER_ID] [varchar](15) NULL,
	[APPROVE_DT] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[BRANCH_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietBanKiemKe]    Script Date: 18/06/2020 10:38:01 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietBanKiemKe](
	[CTBKK_ID] [varchar](20) NOT NULL,
	[CTBKK_BKK_ID] [varchar](20) NULL,
	[CTBKK_MA_TB] [varchar](20) NULL,
	[CTBKK_TT] [nvarchar](20) NULL,
	[CTBKK_CHECK] [varchar](20) NULL,
	[CTBKK_TT_SAU] [nvarchar](20) NULL,
	[CTBKK_THOI_GIAN] [datetime] NULL,
	[RECORD_STATUS] [varchar](1) NULL,
	[MAKER_ID] [varchar](12) NULL,
	[CREATE_DT] [datetime] NULL,
	[AUTH_STATUS] [varchar](50) NULL,
	[CHECKER_ID] [varchar](12) NULL,
	[APPROVE_DT] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[CTBKK_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KiemKe]    Script Date: 18/06/2020 10:38:01 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KiemKe](
	[KK_ID] [varchar](20) NOT NULL,
	[KK_CODE] [varchar](15) NULL,
	[KK_NGAYTAO] [datetime] NULL,
	[KK_NGUOITAO] [nvarchar](50) NULL,
	[KK_MADONVI] [varchar](15) NULL,
	[KK_TENDONVI] [nvarchar](50) NULL,
	[KK_NGAYCHOT] [datetime] NULL,
	[KK_TONGTB_DUOCKIEMKE] [int] NULL,
	[KK_TONGTB_DUSOVOISAOKE] [int] NULL,
	[KK_TONGTB_THIEUSOVOISAOKE] [int] NULL,
	[KK_TONGTB_THUASOVOISAOKE] [int] NULL,
	[KK_TRANGTHAI] [nvarchar](10) NULL,
	[RECORD_STATUS] [varchar](1) NULL,
	[MAKER_ID] [varchar](12) NULL,
	[CREATE_DT] [datetime] NULL,
	[AUTH_STATUS] [varchar](50) NULL,
	[CHECKER_ID] [varchar](12) NULL,
	[APPROVE_DT] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[KK_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThietBi]    Script Date: 18/06/2020 10:38:01 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThietBi](
	[TB_ID] [varchar](20) NOT NULL,
	[TB_TEN] [nvarchar](30) NULL,
	[TB_TT_HOAT_DONG] [nvarchar](20) NULL,
	[TB_DV_QL] [varchar](20) NULL,
	[TB_CHU_KY_BAO_DUONG] [nvarchar](15) NULL,
	[TB_NGAY_MUA] [date] NULL,
	[TB_NGAY_BH] [date] NULL,
	[TB_NGAY_HET_BH] [date] NULL,
	[TB_MO_TA] [nvarchar](100) NULL,
	[TB_NSX] [nvarchar](30) NULL,
	[TB_Nam_SX] [int] NULL,
	[TB_SERIAL] [varchar](100) NULL,
	[RECORD_STATUS] [varchar](1) NULL,
	[MAKER_ID] [varchar](12) NULL,
	[CREATE_DT] [datetime] NULL,
	[AUTH_STATUS] [varchar](50) NULL,
	[CHECKER_ID] [varchar](12) NULL,
	[APPROVE_DT] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[TB_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[BRANCH] ([BRANCH_ID], [FATHER_ID], [IS_POTENTIAL], [BRANCH_CODE], [BRANCH_NAME], [DAO_CODE], [DAO_NAME], [REGION_ID], [BRANCH_TYPE], [BRANCH_EMAIL], [BRANCH_FAX], [BRANCH_STATUS], [ADDR], [PROVICE], [TEL], [TAX_NO], [NOTES], [RECORD_STATUS], [MAKER_ID], [CREATE_DT], [AUTH_STATUS], [CHECKER_ID], [APPROVE_DT]) VALUES (N'donvi1', NULL, NULL, NULL, N'phong1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[BRANCH] ([BRANCH_ID], [FATHER_ID], [IS_POTENTIAL], [BRANCH_CODE], [BRANCH_NAME], [DAO_CODE], [DAO_NAME], [REGION_ID], [BRANCH_TYPE], [BRANCH_EMAIL], [BRANCH_FAX], [BRANCH_STATUS], [ADDR], [PROVICE], [TEL], [TAX_NO], [NOTES], [RECORD_STATUS], [MAKER_ID], [CREATE_DT], [AUTH_STATUS], [CHECKER_ID], [APPROVE_DT]) VALUES (N'donvi2', NULL, NULL, NULL, N'phong2', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ChiTietBanKiemKe] ([CTBKK_ID], [CTBKK_BKK_ID], [CTBKK_MA_TB], [CTBKK_TT], [CTBKK_CHECK], [CTBKK_TT_SAU], [CTBKK_THOI_GIAN], [RECORD_STATUS], [MAKER_ID], [CREATE_DT], [AUTH_STATUS], [CHECKER_ID], [APPROVE_DT]) VALUES (N'CTKK0000000000000001', N'kiemke1', N'thietbi1', NULL, NULL, N'hư hỏng', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ChiTietBanKiemKe] ([CTBKK_ID], [CTBKK_BKK_ID], [CTBKK_MA_TB], [CTBKK_TT], [CTBKK_CHECK], [CTBKK_TT_SAU], [CTBKK_THOI_GIAN], [RECORD_STATUS], [MAKER_ID], [CREATE_DT], [AUTH_STATUS], [CHECKER_ID], [APPROVE_DT]) VALUES (N'CTKK0000000000000002', N'kiemke1', N'thietbi2', NULL, NULL, N'hư hỏng', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ChiTietBanKiemKe] ([CTBKK_ID], [CTBKK_BKK_ID], [CTBKK_MA_TB], [CTBKK_TT], [CTBKK_CHECK], [CTBKK_TT_SAU], [CTBKK_THOI_GIAN], [RECORD_STATUS], [MAKER_ID], [CREATE_DT], [AUTH_STATUS], [CHECKER_ID], [APPROVE_DT]) VALUES (N'CTKK0000000000000003', N'kiemke1', N'thietbi3', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ChiTietBanKiemKe] ([CTBKK_ID], [CTBKK_BKK_ID], [CTBKK_MA_TB], [CTBKK_TT], [CTBKK_CHECK], [CTBKK_TT_SAU], [CTBKK_THOI_GIAN], [RECORD_STATUS], [MAKER_ID], [CREATE_DT], [AUTH_STATUS], [CHECKER_ID], [APPROVE_DT]) VALUES (N'CTKK0000000000000004', N'kiemke2', N'thietbi4', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ChiTietBanKiemKe] ([CTBKK_ID], [CTBKK_BKK_ID], [CTBKK_MA_TB], [CTBKK_TT], [CTBKK_CHECK], [CTBKK_TT_SAU], [CTBKK_THOI_GIAN], [RECORD_STATUS], [MAKER_ID], [CREATE_DT], [AUTH_STATUS], [CHECKER_ID], [APPROVE_DT]) VALUES (N'CTKK0000000000000005', N'kiemke2', N'thietbi5', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ChiTietBanKiemKe] ([CTBKK_ID], [CTBKK_BKK_ID], [CTBKK_MA_TB], [CTBKK_TT], [CTBKK_CHECK], [CTBKK_TT_SAU], [CTBKK_THOI_GIAN], [RECORD_STATUS], [MAKER_ID], [CREATE_DT], [AUTH_STATUS], [CHECKER_ID], [APPROVE_DT]) VALUES (N'CTKK0000000000000006', N'kiemke1', N'thietbi5', N'Thừa', NULL, N'hư hỏng', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[KiemKe] ([KK_ID], [KK_CODE], [KK_NGAYTAO], [KK_NGUOITAO], [KK_MADONVI], [KK_TENDONVI], [KK_NGAYCHOT], [KK_TONGTB_DUOCKIEMKE], [KK_TONGTB_DUSOVOISAOKE], [KK_TONGTB_THIEUSOVOISAOKE], [KK_TONGTB_THUASOVOISAOKE], [KK_TRANGTHAI], [RECORD_STATUS], [MAKER_ID], [CREATE_DT], [AUTH_STATUS], [CHECKER_ID], [APPROVE_DT]) VALUES (N'kiemke1', NULL, CAST(N'2020-06-15T00:00:00.000' AS DateTime), NULL, N'donvi1', N'P301', NULL, NULL, NULL, NULL, NULL, N'Mở', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[KiemKe] ([KK_ID], [KK_CODE], [KK_NGAYTAO], [KK_NGUOITAO], [KK_MADONVI], [KK_TENDONVI], [KK_NGAYCHOT], [KK_TONGTB_DUOCKIEMKE], [KK_TONGTB_DUSOVOISAOKE], [KK_TONGTB_THIEUSOVOISAOKE], [KK_TONGTB_THUASOVOISAOKE], [KK_TRANGTHAI], [RECORD_STATUS], [MAKER_ID], [CREATE_DT], [AUTH_STATUS], [CHECKER_ID], [APPROVE_DT]) VALUES (N'kiemke2', NULL, CAST(N'2020-06-18T00:00:00.000' AS DateTime), NULL, N'donvi2', N'P302', NULL, NULL, NULL, NULL, NULL, N'Mở', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ThietBi] ([TB_ID], [TB_TEN], [TB_TT_HOAT_DONG], [TB_DV_QL], [TB_CHU_KY_BAO_DUONG], [TB_NGAY_MUA], [TB_NGAY_BH], [TB_NGAY_HET_BH], [TB_MO_TA], [TB_NSX], [TB_Nam_SX], [TB_SERIAL], [RECORD_STATUS], [MAKER_ID], [CREATE_DT], [AUTH_STATUS], [CHECKER_ID], [APPROVE_DT]) VALUES (N'thietbi1', N'micro', N'hư hỏng', N'donvi1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ThietBi] ([TB_ID], [TB_TEN], [TB_TT_HOAT_DONG], [TB_DV_QL], [TB_CHU_KY_BAO_DUONG], [TB_NGAY_MUA], [TB_NGAY_BH], [TB_NGAY_HET_BH], [TB_MO_TA], [TB_NSX], [TB_Nam_SX], [TB_SERIAL], [RECORD_STATUS], [MAKER_ID], [CREATE_DT], [AUTH_STATUS], [CHECKER_ID], [APPROVE_DT]) VALUES (N'thietbi2', N'tv', N'hư hỏng', N'donvi1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ThietBi] ([TB_ID], [TB_TEN], [TB_TT_HOAT_DONG], [TB_DV_QL], [TB_CHU_KY_BAO_DUONG], [TB_NGAY_MUA], [TB_NGAY_BH], [TB_NGAY_HET_BH], [TB_MO_TA], [TB_NSX], [TB_Nam_SX], [TB_SERIAL], [RECORD_STATUS], [MAKER_ID], [CREATE_DT], [AUTH_STATUS], [CHECKER_ID], [APPROVE_DT]) VALUES (N'thietbi3', N'remote', N'Bình thường', N'donvi1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ThietBi] ([TB_ID], [TB_TEN], [TB_TT_HOAT_DONG], [TB_DV_QL], [TB_CHU_KY_BAO_DUONG], [TB_NGAY_MUA], [TB_NGAY_BH], [TB_NGAY_HET_BH], [TB_MO_TA], [TB_NSX], [TB_Nam_SX], [TB_SERIAL], [RECORD_STATUS], [MAKER_ID], [CREATE_DT], [AUTH_STATUS], [CHECKER_ID], [APPROVE_DT]) VALUES (N'thietbi4', N'monitor', N'Bình thường', N'donvi2', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ThietBi] ([TB_ID], [TB_TEN], [TB_TT_HOAT_DONG], [TB_DV_QL], [TB_CHU_KY_BAO_DUONG], [TB_NGAY_MUA], [TB_NGAY_BH], [TB_NGAY_HET_BH], [TB_MO_TA], [TB_NSX], [TB_Nam_SX], [TB_SERIAL], [RECORD_STATUS], [MAKER_ID], [CREATE_DT], [AUTH_STATUS], [CHECKER_ID], [APPROVE_DT]) VALUES (N'thietbi5', N'table', N'hư hỏng', N'donvi2', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
/****** Object:  StoredProcedure [dbo].[BKK_CHECK]    Script Date: 18/06/2020 10:38:01 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[BKK_CHECK] @DV_QL VARCHAR(20),@BKK_ID VARCHAR(20), @NGAY_TAO datetime
AS
declare @dt date;
set @dt=cast(@NGAY_TAO as Date);
IF(EXISTS(
SELECT * FROM KiemKe WHERE KK_ID=@BKK_ID AND KK_MADONVI=@DV_QL AND (@dt=CAST(KK_NGAYTAO AS DATE))))
select 'Hop Le' as result
ELSE
select 'Khong Hop Le' as result
GO
/****** Object:  StoredProcedure [dbo].[BKK_Get]    Script Date: 18/06/2020 10:38:01 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[BKK_Get] @BKK_ID VARCHAR(20)
AS
BEGIN
SELECT * FROM KiemKe WHERE KK_ID=@BKK_ID
END
GO
/****** Object:  StoredProcedure [dbo].[ChiTietKiemKe_Get]    Script Date: 18/06/2020 10:38:01 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ChiTietKiemKe_Get] @BKK_ID varchar(20)
AS
BEGIN
	SELECT b.TB_ID,b.TB_TEN,b.TB_TT_HOAT_DONG,b.TB_DV_QL,d.BRANCH_NAME,a.CTBKK_TT_SAU,a.CTBKK_THOI_GIAN,a.CTBKK_TT 
	FROM ChiTietBanKiemKe a,ThietBi b,KiemKe c,BRANCH d
	Where a.CTBKK_MA_TB=b.TB_ID and c.KK_ID=a.CTBKK_BKK_ID and c.KK_ID=@BKK_ID and d.BRANCH_ID=b.TB_DV_QL
	
END
GO
/****** Object:  StoredProcedure [dbo].[ChiTietKiemKe_GetByIdKK_IdDV]    Script Date: 18/06/2020 10:38:01 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ChiTietKiemKe_GetByIdKK_IdDV](@IDKK varchar(20),@IDTB varchar(20))
as
select * from ChiTietBanKiemKe c
where c.CTBKK_BKK_ID=@IDKK and c.CTBKK_MA_TB=@IDTB
GO
/****** Object:  StoredProcedure [dbo].[ChiTietKiemKe_Insert]    Script Date: 18/06/2020 10:38:01 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ChiTietKiemKe_Insert] @CTBKK_BKK_ID VARCHAR(20)=NULL,
	@CTBKK_MA_TB VARCHAR(20)=NULL,@CTBKK_TT NVARCHAR(20)=NULL,@CTBKK_CHECK VARCHAR(20)=NULL,
	@CTBKK_TT_SAU NVARCHAR(20)=NULL,@CTBKK_THOI_GIAN datetime=NULL,@RECORD_STATUS varchar(1)=NULL,
	@MAKER_ID varchar(12)=NULL,@CREATE_DT datetime=NULL,@AUTH_STATUS varchar(50)=NULL,
	@CHECKER_ID varchar(12)=NULL,@APPROVE_DT datetime=NULL
AS
BEGIN TRY
BEGIN TRANSACTION

declare @CTBKK_ID varchar(20) = NULL
exec SYS_ID_Gen 'CTKK', @CTBKK_ID out

	
INSERT INTO [dbo].[ChiTietBanKiemKe](CTBKK_ID,CTBKK_BKK_ID,CTBKK_MA_TB,CTBKK_TT,CTBKK_CHECK,CTBKK_TT_SAU,CTBKK_THOI_GIAN,RECORD_STATUS,
	MAKER_ID,CREATE_DT,AUTH_STATUS,CHECKER_ID,APPROVE_DT)
values (@CTBKK_ID,@CTBKK_BKK_ID,@CTBKK_MA_TB,@CTBKK_TT,@CTBKK_CHECK,@CTBKK_TT_SAU,@CTBKK_THOI_GIAN,@RECORD_STATUS,
	@MAKER_ID,@CREATE_DT,@AUTH_STATUS,@CHECKER_ID,@APPROVE_DT)

COMMIT TRANSACTION
SELECT '0' AS RESULT, 'them thanh cong '+@CTBKK_ID AS ID
END TRY
BEGIN CATCH

ROLLBACK TRANSACTION

END CATCH
GO
/****** Object:  StoredProcedure [dbo].[ChiTietKiemKe_XacNhanKK]    Script Date: 18/06/2020 10:38:01 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ChiTietKiemKe_XacNhanKK]
	@DV_QL VARCHAR(20),
	@BKK_ID VARCHAR(20),
	@NGAY_TAO datetime,
	@MA_TB VARCHAR(30),
	@TT_SAU NVARCHAR(20) = NULL
AS 

DECLARE @DT DATE;
SET @DT=CAST(@NGAY_TAO AS DATE);

IF(EXISTS(SELECT * FROM KiemKe WHERE KK_ID=@BKK_ID AND KK_MADONVI=@DV_QL AND CAST(KK_NGAYTAO AS DATE)=@DT  AND KK_TRANGTHAI=(N'Mở'))) /* Tồn tại bảng và trạng thái mở */
BEGIN
IF(EXISTS(SELECT * FROM ChiTietBanKiemKe WHERE CTBKK_MA_TB = @MA_TB and CTBKK_BKK_ID=@BKK_ID)) /* Là TB của ĐVQL */
BEGIN
	Update ChiTietBanKiemKe
	SET CTBKK_TT_SAU=@TT_SAU
	WHERE CTBKK_MA_TB=@MA_TB

	Update ThietBi
	set TB_TT_HOAT_DONG=@TT_SAU
	where TB_ID=@MA_TB

	SELECT 'Update Thanh Cong' as result
	return;
END
ELSE /* Là TB của ĐV khác */
declare @CTBKK_ID VARCHAR(20) = NULL
exec SYS_ID_Gen 'CTKK', @CTBKK_ID out
begin
	Insert INTO ChiTietBanKiemKe(CTBKK_ID,CTBKK_BKK_ID,CTBKK_MA_TB,CTBKK_TT,CTBKK_CHECK,CTBKK_TT_SAU,CTBKK_THOI_GIAN,
							 RECORD_STATUS,MAKER_ID,CREATE_DT,AUTH_STATUS,CHECKER_ID,APPROVE_DT)
VALUES(@CTBKK_ID,@BKK_ID,@MA_TB,N'Thừa',null,@TT_SAU,null,null,null,null,null,null,null)

Update ThietBi
	set TB_TT_HOAT_DONG=@TT_SAU
	where TB_ID=@MA_TB
SELECT 'Add Thanh Cong' as result
end
END
ELSE
BEGIN
SELECT N'Lỗi' as result
END
GO
/****** Object:  StoredProcedure [dbo].[KiemKe_GetById]    Script Date: 18/06/2020 10:38:01 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[KiemKe_GetById] @BKK_ID VARCHAR(30)
AS
BEGIN
SELECT * FROM KiemKe WHERE KK_ID=@BKK_ID
END
GO
/****** Object:  StoredProcedure [dbo].[KiemKe_Insert]    Script Date: 18/06/2020 10:38:01 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[KiemKe_Insert] @KK_CODE VARCHAR(15)=NULL,@KK_NGAYTAO DATETIME=NULL,
	@KK_NGUOITAO NVARCHAR(50)=NULL,@KK_MADONVI VARCHAR(15)=NULL,@KK_TENDONVI NVARCHAR(50)=NULL,
	@KK_NGAYCHOT DATETIME=NULL,@KK_TONGTB_DUOCKIEMKE INT=NULL,@KK_TONGTB_DUSOVOISAOKE INT=NULL,
	@KK_TONGTB_THIEUSOVOISAOKE INT=NULL,@KK_TONGTB_THUASOVOISAOKE INT=NULL,
	@KK_TRANGTHAI NVARCHAR(10)=NULL,@RECORD_STATUS VARCHAR(1)=NULL,@MAKER_ID varchar(12)=NULL,
	@CREATE_DT datetime=NULL,@AUTH_STATUS varchar(50)=NULL,@CHECKER_ID varchar(12)=NULL,
	@APPROVE_DT datetime=NULL
as
BEGIN TRY
BEGIN TRANSACTION

declare @KK_ID varchar(20) = NULL
exec SYS_ID_Gen 'KiemKe', @KK_ID out

	
INSERT INTO [dbo].[KiemKe](KK_ID,KK_CODE,KK_NGAYTAO,KK_NGUOITAO,KK_MADONVI,KK_TENDONVI,KK_NGAYCHOT,
	KK_TONGTB_DUOCKIEMKE,KK_TONGTB_DUSOVOISAOKE,KK_TONGTB_THIEUSOVOISAOKE,KK_TONGTB_THUASOVOISAOKE,
	KK_TRANGTHAI,RECORD_STATUS,MAKER_ID,CREATE_DT,AUTH_STATUS,CHECKER_ID,APPROVE_DT)
values (@KK_ID,@KK_CODE,@KK_NGAYTAO,@KK_NGUOITAO,@KK_MADONVI,@KK_TENDONVI,@KK_NGAYCHOT,
	@KK_TONGTB_DUOCKIEMKE,@KK_TONGTB_DUSOVOISAOKE,@KK_TONGTB_THIEUSOVOISAOKE,@KK_TONGTB_THUASOVOISAOKE,
	@KK_TRANGTHAI,@RECORD_STATUS,@MAKER_ID,@CREATE_DT,@AUTH_STATUS,@CHECKER_ID,@APPROVE_DT)

COMMIT TRANSACTION
SELECT '0' AS RESULT, 'them thanh cong '+@KK_ID AS ID
END TRY
BEGIN CATCH

ROLLBACK TRANSACTION

END CATCH
GO
/****** Object:  StoredProcedure [dbo].[KiemKe_Update]    Script Date: 18/06/2020 10:38:01 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[KiemKe_Update] @KK_ID VARCHAR(30),@KK_CODE VARCHAR(15),@KK_NGAYTAO DATETIME,
	@KK_NGUOITAO NVARCHAR(50),@KK_MADONVI VARCHAR(15),@KK_TENDONVI NVARCHAR(50),
	@KK_NGAYCHOT DATETIME,@KK_TONGTB_DUOCKIEMKE INT,@KK_TONGTB_DUSOVOISAOKE INT,
	@KK_TONGTB_THIEUSOVOISAOKE INT,@KK_TONGTB_THUASOVOISAOKE INT,
	@KK_TRANGTHAI NVARCHAR(10),@RECORD_STATUS VARCHAR(1),@MAKER_ID varchar(12),
	@CREATE_DT datetime,@AUTH_STATUS varchar(50),@CHECKER_ID varchar(12),
	@APPROVE_DT datetime
as
BEGIN TRY
IF(NOT EXISTS(SELECT * FROM KiemKe WHERE KK_ID = @KK_ID ))
BEGIN
	SELECT '-1' RESULT, N'MÃ KIỂM KÊ KHÔNG HỢP LỆ' ERROR_DESC
	RETURN
END

BEGIN TRANSACTION
UPDATE [dbo].[KiemKe]
   SET 
		[KK_CODE]=@KK_CODE ,
		[KK_NGAYTAO]=@KK_NGAYTAO ,
		[KK_NGUOITAO]=@KK_NGUOITAO,
		[KK_MADONVI]=@KK_MADONVI ,
		[KK_TENDONVI]=@KK_TENDONVI ,
		[KK_NGAYCHOT]=@kk_NGAYCHOT ,
		[KK_TONGTB_DUOCKIEMKE]=@KK_TONGTB_DUOCKIEMKE ,
		[KK_TONGTB_DUSOVOISAOKE]=@KK_TONGTB_DUSOVOISAOKE  ,
		[KK_TONGTB_THIEUSOVOISAOKE]=@KK_TONGTB_THIEUSOVOISAOKE ,
		[KK_TONGTB_THUASOVOISAOKE]=@KK_TONGTB_THUASOVOISAOKE ,
		[KK_TRANGTHAI]=@KK_TRANGTHAI  ,
		[RECORD_STATUS]=@RECORD_STATUS ,
		[MAKER_ID]=@MAKER_ID ,
		[CREATE_DT]=@CREATE_DT , 
		[AUTH_STATUS]=@AUTH_STATUS ,
		[CHECKER_ID]=@CHECKER_ID ,
		[APPROVE_DT]=@APPROVE_DT 

	where KK_ID = @KK_ID

COMMIT TRANSACTION
SELECT '0' AS RESULT, @KK_ID AS ID
END TRY
BEGIN CATCH

ROLLBACK TRANSACTION

END CATCH
GO
/****** Object:  StoredProcedure [dbo].[ThietBi_ById]    Script Date: 18/06/2020 10:38:01 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ThietBi_ById] @TB_ID varchar(20)
AS
BEGIN
	SELECT * FROM ThietBi WHERE TB_ID=@TB_ID
END
GO
/****** Object:  StoredProcedure [dbo].[ThietBi_GetAll]    Script Date: 18/06/2020 10:38:01 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[ThietBi_GetAll]
AS
BEGIN
	SELECT * FROM ThietBi
END
GO
