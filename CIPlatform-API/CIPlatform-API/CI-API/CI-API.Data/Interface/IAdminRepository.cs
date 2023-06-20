using CI_API.Core.Models;
using CI_API.Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_API.Data.Interface
{
    public interface IAdminRepository
    {
        //public List<User> GetAllUser();
        public List<User> GetAllUser(string? search);
        public Task<JsonResult> GetAllMission(string? search);
    }
}
