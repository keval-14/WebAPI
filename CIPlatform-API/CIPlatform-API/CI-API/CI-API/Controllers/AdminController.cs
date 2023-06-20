using CI_API.Application.ServiceInterface;
using CI_API.Common.CommonModels;
using CI_API.Core.Models;
using CI_API.Core.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        #region Dependancy Injection
        private readonly IAdminService AdminService;

        public AdminController(IAdminService _AdminService)
        {
            AdminService = _AdminService;
        }
        #endregion


        //#region getAllUser

        //[Authorize]
        //[HttpGet("GetUser")]
        //public async Task<List<User>> GetAllUser()
        //{
        //    return AdminService.GetAllUser();

        //}
        //#endregion

        #region getAllUserPost

        [Authorize]
        [HttpPost("GetAllUser")]
        public async Task<List<User>> GetAllUser([FromBody]searchViewModel search)
        {
            return AdminService.GetAllUser(search.search);
        }
        #endregion

        #region GetAllMission
        [Authorize]
        [HttpPost("GetAllMission")]
        public async Task<JsonResult> GetAllMission([FromBody] searchViewModel search)
        {
            if (ModelState.IsValid)
            {
                return await AdminService.GetAllMission(search.search);
            }
            return new JsonResult(new apiResponse<string> { Message = ResponseMessages.InternalServerError, StatusCode = responseStatusCode.BadRequest, Result = false });
        }
        #endregion
    }
}
