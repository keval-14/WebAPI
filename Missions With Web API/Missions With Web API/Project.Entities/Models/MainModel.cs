using Project.Entities.Models.Mission.Mission_Request_Model;
using Project.Entities.Models.Mission.Mission_Response_Model;

namespace MissionWebApi.Models
{
    public class MainModel
    {
        public List<ResponseMission>? Missions { get; set; }
        public List<RequestMission>? MissionList { get; set; }
    }
}
