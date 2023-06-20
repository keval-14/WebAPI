using CI_API.Application.ServiceInterface;
using CI_API.Core.Models;
using CI_API.Core.ViewModel;
using CI_API.Data.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_API.Application.Services
{
    public class AdminService: IAdminService
    {
        #region Dependency Injection of LandingPageRepository Interface

        private readonly IAdminRepository AdminRepository;
        public AdminService(IAdminRepository _AdminRepository)
        {
            AdminRepository = _AdminRepository;
        }
        #endregion

        #region GetAllUser
        public List<User> GetAllUser(string? search)
        {
            return AdminRepository.GetAllUser(search);
        }
        #endregion

        #region GetAllMission
        public async Task<JsonResult> GetAllMission(string? search)
        {
            return await AdminRepository.GetAllMission(search);
        }
        #endregion
    }
}
