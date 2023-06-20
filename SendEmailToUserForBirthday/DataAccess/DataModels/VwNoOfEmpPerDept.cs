using System;
using System.Collections.Generic;

namespace DataAccess.DataModels;

public partial class VwNoOfEmpPerDept
{
    public int? DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public int? EmployeeCount { get; set; }
}
