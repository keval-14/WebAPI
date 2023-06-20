using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MissionWebApi.Models;
using Project.Entities.Models;
using Project.Entities.Models.Mission.Mission_Request_Model;
using Project.Entities.Models.Mission.Mission_Response_Model;
using Project.Repository.Interface;
using System.Data;
using System.Net;
using System.Text.Json;

namespace MissionWebApi.Repository.Repository
{
    public class MissionRepository : IMissionRepository
    {
        private readonly CIDBContext _CIDBContext;
        private readonly IConfiguration _configuration;
        public string? ConnectionString { get; private set; }
        private string providerName;
        public MissionRepository(CIDBContext CIDBContext, IConfiguration configuration)
        {
            _CIDBContext = CIDBContext;
            _configuration = configuration;
            ConnectionString = configuration.GetConnectionString("CI");
            providerName = "using System.Data.SqlClient";
        }
        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }
        #region To Get MissionList
        public List<ResponseMission> GetMissions(out string? statusCode)
        {
            try
            {
                List<ResponseMission> details;

                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    DynamicParameters param = new DynamicParameters();
                    details = dbConnection.Query<ResponseMission>("SerachMissionWebAPI", commandType: CommandType.StoredProcedure).ToList();
                    dbConnection.Close();

                }
                statusCode = HttpStatusCode.OK.ToString();
                return details;
            }
            catch (Exception ex)
            {

                ErrorDetails error = new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
                throw new Exception(JsonSerializer.Serialize(error));
            }
        }

        #endregion

        #region Get Mission With Filter

        public List<ResponseMission> GetMissionFilter(RequestMission missionFilter, out string? statusCode)
        {
            try
            {

                List<ResponseMission> details;
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@SearchText", missionFilter.Search);
                    param.Add("@city_id", missionFilter.CityId);
                    param.Add("@country_id", missionFilter.CountryId);
                    param.Add("@theme_id", missionFilter.ThemeId);
                    param.Add("@title", null);
                    param.Add("@short_description", null);
                    param.Add("@start_date", missionFilter.StartDate);
                    param.Add("@end_date", missionFilter.EndDate);
                    param.Add("@mission_type", missionFilter.MissionType);
                    param.Add("@organization_name", missionFilter.OrganizationName);
                    param.Add("@availability", missionFilter.MissionAvailibility);
                    param.Add("@deadline", missionFilter.Deadline);
                    param.Add("@minimum_rating", missionFilter.Rating);
                    param.Add("@maximum_rating", missionFilter.Rating);
                    param.Add("@skillids", missionFilter.MissionSkill);
                    param.Add("@storyviews", null);
                    param.Add("@PageNumber", missionFilter.PageNumber);
                    param.Add("@PageSize", missionFilter.PageSize);
                    param.Add("@Expression", missionFilter.Expression);
                    param.Add("@IsSortByAsc", missionFilter.IsSortByAsc);
                    details = dbConnection.Query<ResponseMission>("SerachMissionWebAPI", param, commandType: CommandType.StoredProcedure).ToList();
                    dbConnection.Close();
                }
                if (details.Count == 0)
                {
                    statusCode = HttpStatusCode.NotFound.ToString();
                    throw new Exception("No missions found for the given search input.");
                }
                statusCode = HttpStatusCode.OK.ToString();
                return details;
            }
            catch (Exception ex)
            {

                ErrorDetails error = new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
                throw new Exception(JsonSerializer.Serialize(error));
            }
        }
        #endregion

        #region Create Mission

        public List<CreateResponseMission> CreateMission(CreateRequestMission createMission, out string? statusCode)
        {
            try
            {
                if (createMission.CityId == null || createMission.CountryId == null || createMission.ThemeId == null || createMission.title == null)
                {
                    statusCode = HttpStatusCode.NotFound.ToString();
                    throw new Exception("One or more required fields are missing.");
                }

                List<CreateResponseMission> details;

                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@city_id", createMission.CityId);
                    param.Add("@country_id", createMission.CountryId);
                    param.Add("@theme_id", createMission.ThemeId);
                    param.Add("@title", createMission.title);
                    param.Add("@short_description", createMission.shortDescription);
                    param.Add("@description", createMission.description);
                    param.Add("@start_date", createMission.startDate);
                    param.Add("@end_date", createMission.endDate);
                    param.Add("@mission_type", createMission.missionType);
                    param.Add("@status", createMission.missionStatus);
                    param.Add("@deadline", createMission.deadline);
                    param.Add("@organization_name", createMission.organizationName);
                    param.Add("@organization_detail", createMission.organizationDetail);
                    param.Add("@availability", createMission.missionAvailibility);
                    param.Add("@success", direction: ParameterDirection.Output, size: 150);
                    details = dbConnection.Query<CreateResponseMission>("CreateMission", param, commandType: CommandType.StoredProcedure).ToList();

                    dbConnection.Close();
                    var response = param.Get<String>("@success");
                    {
                        if (response == "1")
                        {
                            statusCode = HttpStatusCode.OK.ToString();
                            return details;
                        }

                        statusCode = HttpStatusCode.NotFound.ToString();
                        throw new Exception("Insertion Of Mission Failed.");
                    }
                }
            }

            catch (Exception ex)
            {

                ErrorDetails error = new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
                throw new Exception(JsonSerializer.Serialize(error));
            }


        }



        #endregion

        #region Update Mission
        public UpdateResponseMission UpdateMission(UpdateRequestMission updateMission, out HttpStatusCode? statusCode, out string? errorText)
        {
            UpdateResponseMission details = new UpdateResponseMission();
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@MissionId", updateMission.missionId);
                    param.Add("@city_id", updateMission.CityId);
                    param.Add("@country_id", updateMission.CountryId);
                    param.Add("@theme_id", updateMission.ThemeId);
                    param.Add("@Title", updateMission.title);
                    param.Add("@ShortDescription", updateMission.shortDescription);
                    param.Add("@StartDate", updateMission.startDate);
                    param.Add("@EndDate", updateMission.endDate);
                    param.Add("@MissionType", updateMission.missionType);
                    param.Add("@Status", updateMission.missionStatus);
                    param.Add("@deadline", updateMission.deadline);
                    param.Add("@OrganizationName", updateMission.organizationName);
                    param.Add("@availability", updateMission.missionAvailibility);
                    param.Add("@output", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    details = dbConnection.QuerySingleOrDefault<UpdateResponseMission>("UpdateFinalMissionwebAPI", param, commandType: CommandType.StoredProcedure);
                    dbConnection.Close();
                    var success = param.Get<int>("@output");

                    if (success == 1)
                    {
                        statusCode = HttpStatusCode.OK;
                        errorText = null;
                        return details;
                    }
                    statusCode = HttpStatusCode.NotFound;
                    errorText = null;
                    return details;

                }
            }


            catch (Exception ex)
            {
                statusCode = HttpStatusCode.InternalServerError;
                errorText = "Something went wrong in GetMission repository with Error: " + ex.Message;
            }
            return details;
        }


        #endregion

        #region Delete Mission
        public void DeleteMission(long missionId, out HttpStatusCode? statusCode, out string? errorText)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@mission_id", missionId);
                    param.Add("@output", direction: ParameterDirection.Output, size: 150);
                    dbConnection.Execute("spDeleteMission", param, commandType: CommandType.StoredProcedure);
                    var response = param.Get<string>("@output");
                    if (response == "1")
                    {
                        statusCode = HttpStatusCode.OK;
                        errorText = "Mission Deleted Successfully";
                    }
                    else
                    {
                        statusCode = HttpStatusCode.NotFound;
                        errorText = "Cant delete it as this mission is currently ";
                    }
                }
            }
            catch (Exception ex)
            {
                statusCode = HttpStatusCode.InternalServerError;
                errorText = "Something went wrong in GetMission repository with Error: " + ex.Message;
            }
        }


        #endregion
    }

}

