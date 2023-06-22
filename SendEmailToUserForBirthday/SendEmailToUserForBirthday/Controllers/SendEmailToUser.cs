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

        #region Dependancy Injection

        private readonly SqlPracticeContext _sqlPracticeContext;
        private readonly IConfiguration _configuration;

        public string? ConnectionString { get; private set; }
        
        public SendEmailToUser(SqlPracticeContext sqlPracticeContext, IConfiguration configuration)
        {
            _sqlPracticeContext = sqlPracticeContext;
            _configuration = configuration;
            ConnectionString = configuration.GetConnectionString("practiceDB");
        }

        #endregion

        #region SQL Connection
        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }

        #endregion



        /// <summary>
        /// Returns List of Employee whoes birthday is exactly one month later from today and send them Email for Birthday reminder using email template from EmailTemplates Folder
        /// </summary>
        /// <returns></returns>

        #region Get Employees and send Emails
        [HttpGet]
        public IActionResult EmpList()
        {
            try
            {
                List<Employee> employees = new List<Employee>();
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    employees = dbConnection.Query<Employee>("SP_SendMailToUserForBday", commandType: CommandType.StoredProcedure).ToList();
                    dbConnection.Close();

                    if (employees.Count() > 0)
                    {
                        foreach (var emp in employees)
                        {

                            //get values from appsettings.json file 
                            string emailTemplate = _configuration.GetValue<string>("EmailService:template");
                            string host = _configuration.GetValue<string>("EmailService:host");
                            int port = _configuration.GetValue<int>("EmailService:port");
                            string fromEmail = _configuration.GetValue<string>("EmailService:fromEmail");
                            string password = _configuration.GetValue<string>("EmailService:password");

                            //read emailTemplate file
                            StreamReader str = new StreamReader(emailTemplate);
                            string MailText = str.ReadToEnd();
                            str.Close();

                            //make changes for UserName and Birthdate in Template
                            MailText = MailText.Replace("[username]", emp.FirstName);
                            MailText = MailText.Replace("[birthday]", emp.DateofBirth.Value.Day + "/" + emp.DateofBirth.Value.Month + "/" + DateTime.Now.AddDays(30).Year);
                            
                            string subject = "Your Birthday is coming soon...!";

                            //Create Mail Message
                            MailMessage mailmsg = new MailMessage();
                            mailmsg.IsBodyHtml = true;
                            mailmsg.From = new MailAddress(fromEmail);
                            mailmsg.To.Add(emp.Address);
                            mailmsg.Subject = subject;
                            mailmsg.Body = MailText;


                            //set up SMTP for sending Email
                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = host;
                            smtp.Port = port;
                            smtp.EnableSsl = true;

                            //change Default NetworkCredential
                            NetworkCredential _network = new NetworkCredential(fromEmail, password);
                            smtp.Credentials = _network;

                            smtp.Send(mailmsg);


                            #region Send Email Without Template
                            //var fromAddress = new MailAddress("evanzandu@gmail.com", $"Hey! {emp.FirstName}...!");
                            //var toAddress = new MailAddress(emp.Address);
                            //var subject = "Your Birthday Is Coming Soon...!";
                            //var body = $"Hi, Your Birthday Is Coming Soon on {emp.DateofBirth?.ToString("dd-MM-yyyy")}";
                            //var message = new MailMessage(fromAddress, toAddress)
                            //{
                            //    Subject = subject,
                            //    Body = body,
                            //    IsBodyHtml = true
                            //};
                            //var smtpClient = new SmtpClient("smtp.gmail.com", 587)
                            //{
                            //    UseDefaultCredentials = false,
                            //    Credentials = new NetworkCredential("evanzandu@gmail.com", "timrrquqhqzvdpns"),
                            //    EnableSsl = true
                            //};
                            //smtpClient.Send(message);

                            #endregion


                            //make changes in database after sending email
                            emp.IsMailSent = true;
                            emp.MailSentDate = DateTime.Now;
                            _sqlPracticeContext.Employees.Update(emp);
                            _sqlPracticeContext.SaveChanges();
                        }
                    }
                    return StatusCode(200, $"{employees.Count()} emails sent..!");
                    //Ok($"{employees.Count()} emails sent..!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.InnerException.Message);
            }

        }
        #endregion
    }
}


