using Dapper;
using DataAccess.DataModels;
using DataAccess.EmpDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace SendEmailToUserForBirthday.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendEmailToUser : ControllerBase
    {
        private readonly SqlPracticeContext _sqlPracticeContext;
        private readonly IConfiguration _configuration;

        public string? ConnectionString { get; private set; }
        private string providerName;
        public SendEmailToUser(SqlPracticeContext sqlPracticeContext, IConfiguration configuration)
        {
            _sqlPracticeContext = sqlPracticeContext;
            _configuration = configuration;
            ConnectionString = configuration.GetConnectionString("practiceDB");
            providerName = "using System.Data.SqlClient";
        }

        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }

        [HttpGet]
        public IActionResult EmpList()
        {
            List<Employee> employees = new List<Employee>();
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                DynamicParameters param = new DynamicParameters();

                employees = dbConnection.Query<Employee>("SP_SendMailToUserForBday", commandType: CommandType.StoredProcedure).ToList();
                dbConnection.Close();


                if (employees.Count() >= 1)
                {

                    foreach (var emp in employees)
                    {

                        //if ((bool)emp.IsMailSent && emp.MailSentDate <= DateTime.Now)
                        //{

                        //}
                        //else
                        //{

                        //}

                        var fromAddress = new MailAddress("evanzandu@gmail.com", $"Hey! {emp.FirstName}...!");
                        var toAddress = new MailAddress(emp.Address);
                        var subject = "Your Birthday Is Coming Soon...!";
                        var body = $"Hi, Your Birthday Is Coming Soon on {emp.DateofBirth?.ToString("dd-MM-yyyy")}";
                        var message = new MailMessage(fromAddress, toAddress)
                        {
                            Subject = subject,
                            Body = body,
                            IsBodyHtml = true
                        };
                        var smtpClient = new SmtpClient("smtp.gmail.com", 587)
                        {
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential("evanzandu@gmail.com", "timrrquqhqzvdpns"),
                            EnableSsl = true
                        };
                        smtpClient.Send(message);

                        emp.IsMailSent = true;
                        emp.MailSentDate = DateTime.Now;

                    }
                    _sqlPracticeContext.SaveChanges();
                }
                return Ok(employees);

            }










        }
    }
}

