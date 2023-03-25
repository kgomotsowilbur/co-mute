using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CoMute.Abstractions.Models;

public class Users : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public virtual ICollection<CarPoolOpportunity> CarPoolOpportunities { get; set; }
}

