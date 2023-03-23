using System;
using System.Collections.Generic;

namespace AeverPortal.Abstractions.Models;

public class FundManager : BaseEntity
{
    public FundManager()
    {
        this.Funds = new List<Fund>();
        this.TeamMembers = new List<TeamMember>();
        this.Advisors = new List<Advisors>();
        this.AboutUs = new List<AboutUs>();
        this.PhysicalAddress = new();
        this.RegisteredAddress = new();
    }
    public string Name { get; set; }
    public string ShortBlurb { get; set; }
    public string LogoUrl { get; set; }
    public string Overview { get; set; }
    public Address PhysicalAddress { get; set; }
    public Address RegisteredAddress { get; set; }
    public virtual ICollection<TeamMember> TeamMembers { get; set; }
    public virtual ICollection<Advisors> Advisors { get; set; }
    public List<AboutUs> AboutUs { get; set; }
    public virtual ICollection<Fund> Funds { get; set; }
}

