USE ActivitySystem
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Polo Chen
-- Create date: 2022-02-17
-- Description:	取得指定筆數最新的 Activity 資料
-- =============================================
CREATE PROCEDURE [GetTheNewestActivities]
	@Count INT = 5
AS
BEGIN	
	SET NOCOUNT ON;
	DECLARE @SQLScript NVARCHAR(500)
	SET @SQLScript = 
	'
		SELECT TOP ' + @Count + '[ActivityId]
		  ,[ActivityName]
		  ,[Location]
		  ,[Description]
		  ,[ActivityStartTime]
		  ,[ActivityEndTime]
		  ,[ActivitySignUpStartTime]
		  ,[ActivitySignUpEndTime]
		  ,[EnrollCount]
		  ,[AlreadyEnrollCount]
		  ,[CreateTime]
		  ,[CreateUser]
		  ,[UpdateTime]
		  ,[UpdateUser]
		FROM [dbo].[Activities] AS T_AC
		ORDER BY T_AC.[CreateTime] DESC 
	'

	EXEC (@SQLScript)
END
GO
