using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MissionWebApi.Models;
using Project.Entities.Models;
using Project.Entities.Models.Mission.Mission_Request_Model;
using Project.Repository.Interface;

namespace Project.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly CIDBContext _CIDBContext;
        private readonly IConfiguration _configuration;
        public string? ConnectionString { get; private set; }
        private string providerName;
        public UserRepository(CIDBContext CIDBContext, IConfiguration configuration)
        {
            _CIDBContext = CIDBContext;
            _configuration = configuration;
            ConnectionString = configuration.GetConnectionString("practiceDB");
            providerName = "using System.Data.SqlClient";
        }
        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }


        #region Get Mission With Filter

        public List<Login> Login(Login login, out string? statusCode)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@userEmail", login.Email);
                    param.Add("@password", login.Password);

                    var details = dbConnection.Query<Login>("LoginByUsernamePassword", param, commandType: CommandType.StoredProcedure).ToList();
                    dbConnection.Close();
                    if (details.Count() == 0)
                    {
                        statusCode = HttpStatusCode.NotFound.ToString();
                        return null;
                    }
                    else
                    {
                        statusCode = HttpStatusCode.OK.ToString();
                        return details;
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
    }
}
