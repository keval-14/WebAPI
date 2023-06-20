using CI_API.Common.CommonModels;
using CI_API.Core.CIDbContext;
using CI_API.Core.Models;
using CI_API.Core.ViewModel;
using CI_API.Data.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_API.Data.Repository
{
    public class AdminRepository: IAdminRepository
    {

        #region Dependency Injection of DbContext 

        private readonly CipContext _cIDbContext;

        public AdminRepository(CipContext cIDbContext)
        {
            _cIDbContext = cIDbContext;
        }
        #endregion

        //#region GetAllUser

        //public List<User> GetAllUser()
        //{
        //    var loginData = _cIDbContext.Users.ToList();
          
        //    return loginData;
        //}
        //#endregion

        #region GetAllUser

        public List<User> GetAllUser(string? search)
        {
            if (search != null)
            {
            var loginData = _cIDbContext.Users.Where(U=>U.FirstName.Contains(search)|| U.LastName.Contains(search)).ToList();
            return loginData;
            }
            else
            {
                var loginData = _cIDbContext.Users.ToList();
                return loginData;
            }
          
        }
        #endregion

        #region GetAllMission
        public async Task<JsonResult> GetAllMission(string? search)
        {
            try
            {
                List<Mission> AllMission =await Task.FromResult(_cIDbContext.Missions.ToList());
                List<MissionTheme> AllMissionThemes = await Task.FromResult(_cIDbContext.MissionThemes.ToList());
                List<MissionSkill> AllMissionSkills = await Task.FromResult(_cIDbContext.MissionSkills.ToList());

                LandingPageViewModel landingPageViewModel = new()
                {
                    missions = AllMission,
                    missionThemes = AllMissionThemes,
                    missionSkills = AllMissionSkills,

                };

                return new JsonResult(new apiResponse<LandingPageViewModel> { Message = ResponseMessages.LoginSuccess, StatusCode = responseStatusCode.Success, Data = landingPageViewModel, Result = true });
            }
            catch
            {
                return new JsonResult(new apiResponse<LandingPageViewModel> { Message = ResponseMessages.InvalidLoginCredentials, StatusCode = responseStatusCode.BadRequest, Result = true });

            }
        }
        #endregion
    }
}
