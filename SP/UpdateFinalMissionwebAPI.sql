ALTER PROCEDURE UpdateFinalMissionwebAPI
    @MissionId BIGINT,
    @city_id INT = NULL,
    @country_id INT = NULL,
    @theme_id INT = NULL,
    @Title VARCHAR(128) = NULL,
    @ShortDescription VARCHAR(MAX) = NULL,
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL,
    @MissionType VARCHAR(20) = NULL,
    @Status INT = NULL,
    @deadline DATETIME = NULL,
    @OrganizationName VARCHAR(255) = NULL,
    @availability VARCHAR(100) = NULL,
    @output INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

        BEGIN
            UPDATE mission
            SET city_id = @city_id,
                country_id = @country_id,
                theme_id = @theme_id,
                title = @Title,
                short_description = @ShortDescription,
                start_date = @StartDate,
                end_date = @EndDate,
                mission_type = @MissionType,
                status = @Status,
                deadline = @deadline,
                organization_name = @OrganizationName,
                availability = @availability,
				updated_at = GETDATE()
            WHERE mission_id = @MissionId;

            SET @output = 1;
        END;
    END TRY
    BEGIN CATCH
        SET @output = 0; -- Update failed
    END CATCH;
    SELECT mission_id,
           city_id,
           country_id,
           theme_id,
           title,
           short_description,
           start_date,
           end_date,
           mission_type,
           status,
           deadline,
           organization_name,
           availability
    FROM mission
    WHERE mission_id = @MissionId;

-- Retrieve the updated mission record
--exec UpdateFinalMissionwebAPI @MissionId = 7,@Title = 'bdvcjszv',@StartDate = '2023-01-01', @output =1

END;
--select * from mission