using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities.Models.Mission.Mission_Request_Model
{
    [BindProperties]
    public class RequestMission
    {
        public string? Search { get; set; }
        public long? CityId { get; set; }
        public long? CountryId { get; set; }
        public long? ThemeId { get; set; }
        public string? MissionSkill { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? Deadline { get; set; }
        public string? MissionType { get; set; }
        public string? shortDescription { get; set; }
        public string? MissionAvailibility { get; set; }
        public string? OrganizationName { get; set; }
        public int PageNumber { get; set; } = 1;
        public string? Rating { get; set; }

        public int PageSize { get; set; } = 10;
        public int IsSortByAsc { get; set; } = 1;
        public string? Expression { get; set; } = "mission_id";
    }
    public class CreateRequestMission
    {

        //[Required(ErrorMessage = "City Is Required")]
        public long? CityId { get; set; }


        //[Required(ErrorMessage = "Country Is Required")]
        public long? CountryId { get; set; }


        //[Required(ErrorMessage = "Theme Is Required")]
        public long? ThemeId { get; set; }

        //[Required(ErrorMessage = "Title is a Required field.")]
        //[DataType(DataType.Text)]
        //[Display(Order = 1, Name = "Title")]
        //[RegularExpression("^((?!^Title)[a-zA-Z '])+$", ErrorMessage = "Title must be properly formatted.")]
        public string? title { get; set; }


        //[MaxLength(800), MinLength(100)]
        //[Required(ErrorMessage = "shortDescription Is Required")]
        public string? shortDescription { get; set; }


        //[MaxLength(1200), MinLength(100)]
        //[Required(ErrorMessage = "description Is Required")]
        public string? description { get; set; }

        //[Required(ErrorMessage = "StartDate is a Required field.")]
        public DateTime? startDate { get; set; }

        //[Required(ErrorMessage = "EndDate is a Required field.")]
        public DateTime? endDate { get; set; }
        public string? missionType { get; set; }
        public long? missionStatus { get; set; }
        public DateTime? deadline { get; set; }
        //[Required(ErrorMessage = "OrganizationName is a Required field.")]
        public string? organizationName { get; set; }

        //[MaxLength(1000), MinLength(100)]
        //[Required(ErrorMessage = "OrganizationDetail Is Required")]
        public string? organizationDetail { get; set; }
        public string? missionAvailibility { get; set; }

    }
    public class UpdateRequestMission
    {
        //[Required(ErrorMessage = "MissionId Is Required")]
        //[RegularExpression("^[0-9]+$", ErrorMessage = "MissionId must be properly formatted.")]
        public long missionId { get; set; }
        public long? CityId { get; set; }
        public long? CountryId { get; set; }
        public long? ThemeId { get; set; }
        public string? title { get; set; }
        public string? shortDescription { get; set; }
        public string? description { get; set; }

        //[Required(ErrorMessage = "StartDate is a Required field.")]
        public DateTime? startDate { get; set; }

        //[Required(ErrorMessage = "EndDate is a Required field.")]
        public DateTime? endDate { get; set; }

        //[Required(ErrorMessage = "MissionType is a Required field.")]
        public string? missionType { get; set; }

        //[Required(ErrorMessage = "MissionStatus is a Required field.")]
        public long? missionStatus { get; set; }
        public DateTime? deadline { get; set; }
        public string? organizationName { get; set; }
        public string? organizationDetail { get; set; }
        public string? missionAvailibility { get; set; }



    }

    public class missionIdModel
    {
        public int missionId { get; set; }
        public string name { get; set; }
    }

    public class Login
    {
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        public string? Role { get; set; }

    }

}
