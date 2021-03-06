CREATE TABLE LOAI_XE
(
	LOAI_XE_ID VARCHAR(20) PRIMARY KEY,
	TEN_LOAI_XE VARCHAR(100) NULL,
	MO_TA VARCHAR(1000) NULL
)

------------------------------------------------------------
GO
/****** OBJECT:  STOREDPROCEDURE [DBO].[LOAI_XE_SEARCH]    SCRIPT DATE: 30/04/2020 5:54:20 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [DBO].[LOAI_XE_SEARCH] @TEN_LOAI_XE VARCHAR(100), @MO_TA VARCHAR(1000)
AS
BEGIN
	SELECT * FROM LOAI_XE
	WHERE (@TEN_LOAI_XE IS NULL OR @TEN_LOAI_XE= '' OR TEN_LOAI_XE LIKE '%' + @TEN_LOAI_XE + '%')
	AND	(@MO_TA IS NULL OR @MO_TA = '' OR MO_TA LIKE '%' + @MO_TA + '%')
END
----------------------------------------------------------------
GO
/****** OBJECT:  STOREDPROCEDURE [DBO].[LOAI_XE_BYID]    SCRIPT DATE: 30/04/2020 5:55:04 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [DBO].[LOAI_XE_BYID] @LOAI_XE_ID VARCHAR(20)
AS
BEGIN
	SELECT * FROM LOAI_XE
	WHERE @LOAI_XE_ID=LOAI_XE_ID
END
--------------------------------------------------------------
GO
/****** OBJECT:  STOREDPROCEDURE [DBO].[LOAI_XE_INSERT]    SCRIPT DATE: 30/04/2020 5:55:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [DBO].[LOAI_XE_INSERT] @TEN_LOAI_XE VARCHAR(100)=NULL, @MO_TA VARCHAR(1000)=NULL
AS
BEGIN

DECLARE @LOAI_XE_ID VARCHAR(20) = NULL
EXEC SYS_ID_GEN 'LOAI_XE', @LOAI_XE_ID OUT
	
INSERT INTO [DBO].[LOAI_XE](LOAI_XE_ID,TEN_LOAI_XE,MO_TA)
VALUES (@LOAI_XE_ID,@TEN_LOAI_XE,@MO_TA)

END
-------------------------------------------------------------------
CREATE PROC [DBO].[LOAI_XE_DELETE] @LOAI_XE_ID VARCHAR(20)
AS
BEGIN
	DELETE FROM LOAI_XE WHERE LOAI_XE_ID = @LOAI_XE_ID 
END
-------------------------------------------------------------------
CREATE PROC [DBO].[LOAI_XE_UPDATE] @LOAI_XE_ID VARCHAR(20) = NULL,
@TEN_LOAI_XE NVARCHAR(100)=NULL,
	@MO_TA VARCHAR(1000)=NULL
AS
BEGIN TRY
IF(NOT EXISTS(SELECT * FROM LOAI_XE WHERE LOAI_XE_ID <> @LOAI_XE_ID ))
BEGIN
	SELECT '-1' RESULT, N'MÃ LOẠI XE KHÔNG HỢP LỆ' ERROR_DESC
	RETURN
END

---- VALIDATION
--IF(EXISTS(SELECT * FROM CM_XE WHERE XE_CODE = @XE_CODE AND XE_ID <> @XE_ID))
--BEGIN
--	RETURN SELECT '-1' RESULT, N'XE ĐÃ TỒN TẠI XE TRONG HỆ THỐNG' ERROR_DESC
--END

BEGIN TRANSACTION
UPDATE [DBO].[LOAI_XE]
   SET [TEN_LOAI_XE] = @TEN_LOAI_XE
      ,[MO_TA] = @MO_TA      
	WHERE [LOAI_XE_ID] = @LOAI_XE_ID

COMMIT TRANSACTION
SELECT '0' AS RESULT, @LOAI_XE_ID AS ID
END TRY
BEGIN CATCH

ROLLBACK TRANSACTION

END CATCH

--- GET ALL
CREATE PROC [dbo].[LOAI_XE_GET_ALL] AS
BEGIN
	SELECT * FROM [dbo].[LOAI_XE]
END

--- SEARCH 2
CREATE PROC [dbo].[LOAI_XE_SEARCH_2] @TO_SEARCH VARCHAR(255) AS
BEGIN
	SELECT * FROM LOAI_XE WHERE TEN_LOAI_XE LIKE '%' + @TO_SEARCH + '%' OR MO_TA LIKE '%' + @TO_SEARCH + '%'
END