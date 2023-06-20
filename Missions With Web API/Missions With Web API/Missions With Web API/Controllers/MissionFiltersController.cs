using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MissionWebApi.Models;

using System.Net.Http;
using Project.Repository.Interface;
using Project.Entities.Models.Mission.Mission_Response_Model;
using Project.Entities.Models.Mission.Mission_Request_Model;
using Microsoft.AspNetCore.Authorization;

namespace MissionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionFiltersController : ControllerBase
    {
        private readonly IMissionRepository _missionRepository;
        private readonly CIDBContext _context;
        private string? statusCode;

        public MissionFiltersController(CIDBContext context, IMissionRepository missionRepository)
        {
            _context = context;
            _missionRepository = missionRepository;
        }

        #region Get MissionList
        // GET: api/MissionFilters
        [Authorize]
        [HttpGet]
        [Route("GetAllMissions")]
        public async Task<ActionResult<IEnumerable<ResponseMission>>> GetMissionFilter()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                List<ResponseMission> mission;
                //string? StatusCode;
                mission = _missionRepository.GetMissions(out statusCode);
                if (statusCode == HttpStatusCode.OK.ToString())
                {
                    return await Task.FromResult(mission);

                }
                else
                {
                    return Problem($"Something Went Wrong in the {nameof(GetMissionFilter)}", statusCode: (int)HttpStatusCode.InternalServerError);
                }


            }
            catch (Exception ex)
            {
                return Problem($"Something Went Wrong in the {nameof(GetMissionFilter)}", statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region Get Mission With Filter

        // GET: api/MissionFilters/5
        [Authorize]
        [HttpGet]
        [Route("MissionFilter")]
        public async Task<ActionResult<IEnumerable<ResponseMission>>> GetMissionFilter([FromQuery] RequestMission missionFilter)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                List<ResponseMission> missions;
                //string? StatusCode;
                missions = _missionRepository.GetMissionFilter(missionFilter, out statusCode);
                if (statusCode == HttpStatusCode.OK.ToString())
                {
                    return Ok(missions);
                }
                else if (statusCode == HttpStatusCode.NotFound.ToString())
                {
                    return Problem($"No Missions to show  {nameof(GetMissionFilter)}", statusCode: (int)HttpStatusCode.NotFound);

                }
                else
                {
                    return Problem($"Something Went Wrong in the {nameof(GetMissionFilter)}", statusCode: (int)HttpStatusCode.InternalServerError);

                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
                //return Problem($"Something Went Wrong in the {nameof(GetMissionFilter)}", statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region Update Mission

        // PUT: api/MissionFilters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("UpdateMission")]
        public IActionResult UpdateMission([FromQuery] UpdateRequestMission updateMission)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (updateMission.missionId != null && updateMission.missionId != 0)
                {
                    UpdateResponseMission response = _missionRepository.UpdateMission(updateMission, out HttpStatusCode? StatusCode, out string? errorText);

                    if (StatusCode == HttpStatusCode.NotFound)
                    {
                        return BadRequest("No mission was  updated as you didnt gave any parameter");
                    }

                    // return Ok(response);
                    return Ok("Mission Updated Successful");
                }
                else
                {
                    return Ok("MissionId is Required...!");
                }
            }
            catch (Exception ex)
            {
                return Problem($"Something Went Wrong in the GetDetails with Error : " + ex.Message, statusCode: (int?)HttpStatusCode.InternalServerError);
            }
        }


        #endregion

        #region Create Mission
        // POST: api/MissionFilters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("AddMission")]
        public async Task<ActionResult<CreateResponseMission>> CreateMission([FromQuery] CreateRequestMission createMission)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (createMission.CityId != null && createMission.CityId != 0)
                {

                    //string? StatusCode;
                    var missions = _missionRepository.CreateMission(createMission, out statusCode);
                    if (statusCode == HttpStatusCode.OK.ToString())
                    {
                        return Ok(missions);

                    }
                    else if (statusCode == HttpStatusCode.NotFound.ToString())
                    {
                        return Problem($"one  or more field is not filled  {nameof(CreateMission)}", statusCode: (int)HttpStatusCode.NotFound);

                    }
                    else
                    {
                        return Problem($"Something Went Wrong in the {nameof(CreateMission)}", statusCode: (int)HttpStatusCode.InternalServerError);

                    }
                }
                else
                {
                    return Ok("MissionId is not added...!");
                }

            }
            catch (Exception ex)
            {
                return Problem($"Something Went Wrong in the {nameof(CreateMission)}", statusCode: (int)HttpStatusCode.InternalServerError);
            }


        }
        #endregion

        #region Delete Mission

        // DELETE: api/MissionFilters/5
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("DeleteMission")]
        public IActionResult DeleteMission([FromQuery] long missionId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (missionId != 0 && missionId != null)
                {

                    _missionRepository.DeleteMission(missionId, out HttpStatusCode? StatusCode, out string? errorText);
                    if (StatusCode == HttpStatusCode.NotFound)
                    {
                        return BadRequest("Currently this mission is in use so cant delete it");
                    }
                    return Ok("Mission Deleted Successfully");
                }
                else
                {
                    return Ok("MissionId is required...!");
                }

            }
            catch (Exception ex)
            {
                return Problem($"Something Went Wrong in the GetDetails with Error : " + ex.Message, statusCode: (int?)HttpStatusCode.InternalServerError);
            }
        }


        #endregion

        #region
        [HttpPost("GetDataAll")]
        public IActionResult GetDataAll([FromBody] missionIdModel missionId)
        {
            return Ok(missionId);
        }


        #endregion











        private bool MissionFilterExists(string id)
        {
            return (_context.MissionFilter?.Any(e => e.Search == id)).GetValueOrDefault();
        }
    }
}
