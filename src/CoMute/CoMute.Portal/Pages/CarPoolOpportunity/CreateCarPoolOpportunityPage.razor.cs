using System;
using AeverPortal.Abstractions.Models;
using AeverPortal.Portal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace AeverPortal.Portal.Pages;

public class FundsNewPageBase : ComponentBase
{
	public FundsNewPageBase()
	{
        this.Entity = new();
	}
    #region [ Fields ]
    [Parameter]
    public Guid FundManagerId { get; set; }

    [Inject]
    public ICarPoolOpportunityDataService FundDataService { get; set; }
    [Inject]
    public NavigationManager Navigation { get; set; }

    public CarPoolOpportunity Entity { get; set; }
    #endregion

    public async Task OnValidSubmit(EditContext context)
    {
        this.Entity.FundManagerId = FundManagerId;
        await this.FundDataService.AddFund(this.Entity);
        StateHasChanged();

        Navigation.NavigateTo("/funds");
    }
}

