using System;
using System.Collections.Generic;

namespace DataAccess.DataModels;

public partial class UserAuth
{
    public long UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Role { get; set; } = null!;
}
