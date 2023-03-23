using System;
using AeverPortal.Abstractions.Models;
using AeverPortal.Portal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace AeverPortal.Portal.Pages;

public class FundsListPageBase : ComponentBase
{
    public FundsListPageBase()
	{
        this.AllFunds = new();
        this.Fund = new();

    }

    [Inject]
    public ICarPoolOpportunityDataService FundDataService { get; set; }

    protected List<CarPoolOpportunity> AllFunds;

    public CarPoolOpportunity Fund { get; set; }

    public bool SaveQuery { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var allFunds = await this.FundDataService.GetAllFunds();
        this.AllFunds = allFunds.OrderBy(c => c.Name).ToList();
    }

    public async Task OnValidSubmit()
    {
        if (this.SaveQuery)
        {
         //   this.QueryFund.UserId = UserId;
         //   await this.FundQueriesDataService.AddFundQuery(this.QueryFund);
        }

        if (Fund.Name == "" && Fund.Type == "" && Fund.Status == "" && Fund.InvestmentStrategy == "" && Fund.ValueCreation == "")
        {
            var allFunds = await this.FundDataService.GetAllFunds();
            this.AllFunds = allFunds.OrderBy(c => c.Name).ToList();
        }
            var queriedFunds = await this.FundDataService.QueryFunds(Fund);
            this.AllFunds = queriedFunds.OrderBy(c => c.Name).ToList();
        
    }
}

