CREATE PROCEDURE CreateMission
    @city_id INT,
    @country_id INT,
    @theme_id INT,
    @title VARCHAR(126),
    @short_description VARCHAR(MAX),
    @description VARCHAR(MAX),
    @start_date DATETIME,
    @end_date DATETIME,
    @mission_type VARCHAR(MAX),
    @status VARCHAR(MAX),
    @deadline DATETIME,
    @organization_name VARCHAR(MAX),
    @organization_detail VARCHAR(MAX),
    @availability VARCHAR(MAX),
	@success INT OUT
	
AS
BEGIN
DECLARE @output BIT;

    SET NOCOUNT ON;
	 BEGIN TRY
    INSERT INTO [dbo].[mission] (
        [city_id],
        [country_id],
        [theme_id],
        [title],
        [short_description],
        [description],
        [start_date],
        [end_date],
        [mission_type],
        [status],
        [deadline],
        [organization_name],
        [organization_detail],
        [availability],
        [created_at],
        [updated_at]
		
    )
    VALUES (
        @city_id,
        @country_id,
        @theme_id,
        @title,
        @short_description,
        @description,
        @start_date,
        @end_date,
        @mission_type,
        @status,
        @deadline,
        @organization_name,
        @organization_detail,
        @availability,
        GETDATE(),
        GETDATE()
		
    );
SET @success = 1;
    END TRY
    BEGIN CATCH
        SET @success = 0; 
    END CATCH;
	SELECT TOP 1 * FROM [dbo].[mission] ORDER BY [mission_id] DESC;

    
    
END
--select * from mission


EXEC CreateMission
    @city_id = 1,
    @country_id = 1,
    @theme_id = 1,
    @title = 'New Mission',
    @short_description = 'Short description of the new mission',
    @description = 'Detailed description of the new mission',
    @start_date = '2023-06-15',
    @end_date = '2023-06-30',
    @mission_type = 'goal',
    @status = '1',
    @deadline = '2023-06-14',
    @organization_name = 'ABC Organization',
    @organization_detail = 'Details of the organization',
    @availability = 'weekly',
    @success = 1;

	
