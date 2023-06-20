using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Entities.Models.Mission.Mission_Response_Model;
using Project.Entities.Models.Mission.Mission_Request_Model;
using System.Net;

namespace Project.Repository.Interface
{
    public interface IMissionRepository
    {
        public List<ResponseMission> GetMissions(out string? statusCode);
        public List<ResponseMission> GetMissionFilter(RequestMission missionFilter, out string? statusCode);

        public List<CreateResponseMission> CreateMission(CreateRequestMission createMission1, out string? statusCode);
        //public List<UpdateResponseMission> UpdateMission(UpdateRequestMission updateRequestMission, out string? statusCode);
        public UpdateResponseMission UpdateMission(UpdateRequestMission updateMission, out HttpStatusCode? statusCode, out string? errorText);
        public void DeleteMission(long missionId, out HttpStatusCode? statusCode, out string? errorText);

    }
}
