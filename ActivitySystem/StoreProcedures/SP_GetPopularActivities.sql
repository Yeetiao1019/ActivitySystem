USE ActivitySystem
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Polo Chen
-- Create date: 2022-02-17
-- Description:	取得指定筆數報名人數多（尚未額滿）的 Activity 資料
-- =============================================
ALTER PROCEDURE [GetPopularActivities]
	@Count INT = 5
AS
BEGIN	
	SET NOCOUNT ON;
	DECLARE @SQLScript NVARCHAR(MAX)
	SET @SQLScript = 
	'
		SELECT TOP ' + CAST(@Count AS VARCHAR(10)) + '
			 T_AC.[ActivityId]
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
			,ISNULL(T_COUNT.[Qty], 0) AS [Qty]
			FROM [dbo].[Activities] AS T_AC
			LEFT JOIN
			(
				SELECT
					COUNT(*) AS [Qty]
					,T_AC.[ActivityId]
				FROM [dbo].[Activities] AS T_AC
				INNER JOIN [dbo].[Enrolls] AS T_EN
				ON T_AC.[ActivityId] = T_EN.[ActivityId]
				GROUP BY T_AC.[ActivityId]
			) AS T_COUNT
			ON T_AC.[ActivityId] = T_COUNT.[ActivityId]
		WHERE 
		T_AC.[EnrollCount] > T_COUNT.[Qty]	--報名人數尚未額滿的活動
		OR T_COUNT.[Qty] IS NULL --尚未有人報名的活動
		ORDER BY T_COUNT.[Qty] DESC
	'

	EXEC (@SQLScript)
END
GO
