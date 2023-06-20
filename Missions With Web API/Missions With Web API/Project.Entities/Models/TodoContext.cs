using Microsoft.EntityFrameworkCore;
using MissionWebApi.Models;
using Project.Entities.Models.Mission.Mission_Request_Model;
using Project.Entities.Models.Mission.Mission_Response_Model;

namespace MissionWebApi.Models
{
    public class CIDBContext : DbContext
    {
        public CIDBContext(DbContextOptions<CIDBContext> options)
       : base(options)
        {
        }
        public DbSet<ResponseMission> Missions { get; set; } = null!;
        public DbSet<RequestMission>? MissionFilter { get; set; }

    }
}
