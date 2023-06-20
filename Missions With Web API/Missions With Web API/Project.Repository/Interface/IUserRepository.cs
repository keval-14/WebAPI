using Project.Entities.Models.Mission.Mission_Request_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Interface
{
    public interface IUserRepository
    {
        public List<Login> Login(Login login, out string? statusCode);
    }
}
