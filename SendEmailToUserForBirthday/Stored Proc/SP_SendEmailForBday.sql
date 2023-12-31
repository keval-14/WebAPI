-- =======================================================================================================================================
-- Author:		<Keval Mangukiya>
-- Create date: <21-06-2023>
-- Description:	<Stored Procedure that retrive the list of employees whoes birthday is exactly after 30 days>
-- =======================================================================================================================================


USE [SQL-Practice];
GO
/****** Object:  StoredProcedure [dbo].[SP_SendMailToUserForBday]    Script Date: 20-06-2023 18:00:49 ******/
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
ALTER PROC [dbo].[SP_SendMailToUserForBday]
AS
BEGIN

    SELECT *
    FROM dbo.Employee E
    WHERE E.DateofBirth IS NOT NULL
          AND DAY(CONVERT(DATE, E.DateofBirth)) = DAY(DATEADD(DAY, 30, CONVERT(DATE, GETDATE())))
          AND MONTH(CONVERT(DATE, E.DateofBirth)) = MONTH(DATEADD(DAY, 30, CONVERT(DATE, GETDATE())))
          AND
          (
              E.IsMailSent <> 1
              OR YEAR(E.MailSentDate) < YEAR(GETDATE())
              OR E.MailSentDate IS NULL
          );
END;

--adding day
--SELECT DATEADD(DAY,30, CONVERT(date, GETDATE()))
--SELECT DATEADD(DAY,30, '2023-01-31 11:12:47.643')
--SELECT DATEADD(DAY, 30, '2024-01-30 11:12:47.643')

--SELECT DATEADD(DAY,30, '2023-02-27 11:12:47.643')

--30/03 birthday date
--SELECT DATEADD(DAY, 30, '2023-01-30 11:12:47.643')



--adding month
--SELECT DATEADD(MONTH,1, CONVERT(date, GETDATE()))

--SELECT DATEADD(MONTH,1, '2023-01-28 11:12:47.643')
--SELECT DATEADD(MONTH,1, '2023-01-29 11:12:47.643')
--SELECT DATEADD(MONTH,1, '2023-01-30 11:12:47.643')

--SELECT DATEADD(MONTH,1, '2024-01-28 11:12:47.643')
--SELECT DATEADD(MONTH,1, '2024-01-29 11:12:47.643')
--SELECT DATEADD(MONTH, 1, '2024-01-30 11:12:47.643')
--SELECT DATEADD(MONTH, 1, '2024-01-30 11:12:47.643')

--can't send email to 30/03 date
--common year
--SELECT DATEADD(MONTH,1, '2023-02-28 11:12:47.643')
--SELECT DATEADD(MONTH,1, '2023-02-29 11:12:47.643')

--leap year
--SELECT DATEADD(MONTH, 1, '2024-02-28 11:12:47.643')
--SELECT DATEADD(MONTH, 1, '2024-02-29 11:12:47.643')

--SELECT DATEADD(MONTH, 1, '2024-03-01 11:12:47.643')


--SELECT * FROM dbo.Employee E WHERE DAY(CONVERT(date,e.DateofBirth)) = DAY(DATEADD(DAY,30, CONVERT(date, GETDATE()))) AND MONTH(CONVERT(date,e.DateofBirth)) = MONTH(DATEADD(DAY,30, CONVERT(date, GETDATE()))) AND (E.IsMailSent <> 1 OR YEAR(E.MailSentDate)<YEAR(GETDATE()))
