using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities.Models.Mission.Mission_Response_Model
{
    public class ResponseMission
    {
        [Key]
        public int RowNum { get; set; }
        public long mission_id { get; set; }
        public long theme_id { get; set; }
        public string? themeName { get; set; }
        public long country_id { get; set; }
        public string? countryName { get; set; }
        public long city_id { get; set; }
        public string? cityName { get; set; }
        public string? title { get; set; }
        public string? short_description { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public string? mission_type { get; set; }
        public string? organization_name { get; set; }
        public string? availability { get; set; }
        public DateTime? deadline { get; set; }


        public int TotalCount { get; set; }
        public int? average_rating { get; set; }

    }
    public class CreateResponseMission
    {
        public long city_id { get; set; }
        public long country_id { get; set; }
        public long theme_id { get; set; }
        public string? title { get; set; }
        public string? short_description { get; set; }
        public string? description { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public string? mission_type { get; set; }
        public string? status { get; set; }
        public DateTime? deadline { get; set; }
        public string? organization_name { get; set; }
        public string? organization_detail { get; set; }
        public string? availability { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }


    }

    public class UpdateResponseMission
    {
        [Key]
        public long mission_id { get; set; }
        public long? city_id { get; set; }
        public long? country_id { get; set; }
        public long? theme_id { get; set; }
        public string? title { get; set; }
        public string? short_description { get; set; }
       // public string? description { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public string? mission_type { get; set; }
        public string? status { get; set; }
        public DateTime? deadline { get; set; }
        public string? organization_name { get; set; }
       // public string? organization_detail { get; set; }
        public string? availability { get; set; }
    }

}
