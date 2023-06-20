CREATE PROCEDURE spDeleteMission
    @mission_id INT,
    @output INT OUT
AS
BEGIN
    BEGIN TRY
        -- Check if enddate is greater than today's date in the mission table
        DECLARE @enddate DATE;
        SELECT @enddate = end_date
        FROM mission
        WHERE mission_id = @mission_id;

        IF @enddate > GETDATE()
        BEGIN
            SET @output = 0;  
            RETURN;
        END

        -- Soft delete from the fav_mission table
        UPDATE favorite_mission
        SET deleted_at = GETDATE()
        WHERE mission_id = @mission_id;

        -- Soft delete from the goal_mission table
        UPDATE goal_mission
        SET deleted_at = GETDATE()
        WHERE mission_id = @mission_id;

        -- Soft delete from the mission_application table
        UPDATE mission_application
        SET deleted_at = GETDATE()
        WHERE mission_id = @mission_id;

        -- Soft delete from the mission_invite table
        UPDATE mission_invite
        SET deleted_at = GETDATE()
        WHERE mission_id = @mission_id;

        -- Soft delete from the mission_media table
        UPDATE mission_media
        SET deleted_at = GETDATE()
        WHERE mission_id = @mission_id;

        -- Soft delete from the mission_rating table
        UPDATE mission_rating
        SET deleted_at = GETDATE()
        WHERE mission_id = @mission_id;

        -- Soft delete from the mission_skill table
        UPDATE mission_skill
        SET deleted_at = GETDATE()
        WHERE mission_id = @mission_id;


        -- Soft delete from the mission table
        UPDATE mission
        SET deleted_at = GETDATE()
        WHERE mission_id = @mission_id;

        SET @output = 1;
    END TRY
    BEGIN CATCH
        SET @output = 0;  
    END CATCH;
     --EXEC spDeleteMission @mission_id = 7, @output = 1;
END

--select * from mission