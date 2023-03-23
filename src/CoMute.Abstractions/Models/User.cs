using System;
using System.Collections.Generic;

namespace CoMute.Abstractions.Models;

public class User : BaseEntity
{
    public User()
    {
        this.CarPoolOpportunities = new List<CarPoolOpportunity>();
    }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public virtual ICollection<CarPoolOpportunity> CarPoolOpportunities { get; set; }
}

