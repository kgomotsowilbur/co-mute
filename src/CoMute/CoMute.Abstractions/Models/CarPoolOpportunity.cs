using System;
using System.Collections.Generic;

namespace AeverPortal.Abstractions.Models;

public class Fund:BaseEntity
{
    public Fund()
    {
        this.PhysicalAddress = new();
        this.RegisteredAddress = new();
        this.InvestmentTerm = new();
        this.Investment = new();
        this.InvestmentTeamMember = new List<InvestmentTeamMember>();
    }

    public string Name { get; set; }
    public string WebsiteUrl { get; set; }
    public string ShortBlurb { get; set; }
    public string FundOverview { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
    public DateTime IncoporationDate { get; set; }
    public string InvestmentStrategy { get; set; }
    public string ValueCreation { get; set; }
    public string InvestmentProcess { get; set; }
    public string ValueProposition { get; set; }
    public string MarketOpportunity { get; set; }
    public string EnterpriceValue { get; set; }
    public string InvestmentTeamOverview { get; set; }
    public string ImpactSummary { get; set; }
    public string Logo { get; set; }
    public string ImpactThesis { get; set; }
    public Address PhysicalAddress { get; set; }
    public Address RegisteredAddress { get; set; }
    public FundInvestmentTerm InvestmentTerm { get; set; }
    public Investment Investment { get; set; }
    public virtual ICollection<InvestmentTeamMember> InvestmentTeamMember { get; set; }
    public Guid FundManagerId { get; set; }
}