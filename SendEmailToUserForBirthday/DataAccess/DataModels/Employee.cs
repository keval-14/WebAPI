﻿using System;
using System.Collections.Generic;

namespace DataAccess.DataModels;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string Gender { get; set; } = null!;

    public DateTime? DateofBirth { get; set; }

    public int? DepartmentId { get; set; }

    public decimal Salary { get; set; }

    public string? Address { get; set; }

    public int? CityId { get; set; }

    public string? PinCode { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsMailSent { get; set; }

    public DateTime? MailSentDate { get; set; }

    public virtual City? City { get; set; }

    public virtual Department? Department { get; set; }
}