using System;
using CoMute.Abstractions.Models;
using CoMute.Portal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace CoMute.Portal.Pages;

public class CreateCarPoolOpportunityPageBase : ComponentBase
{
	public CreateCarPoolOpportunityPageBase()
	{
        this.CarPoolOpportunity = new();
	}
    #region [ Fields ]
    [Parameter]
    public Guid UserId { get; set; }

    [Inject]
    public ICarPoolOpportunityDataService CarPoolOpportunityDataService { get; set; }
    [Inject]
    public NavigationManager Navigation { get; set; }

    public CarPoolOpportunity CarPoolOpportunity { get; set; }
    #endregion

    public async Task OnValidSubmit(EditContext context)
    {
        this.CarPoolOpportunity.UserId = UserId;
        await this.CarPoolOpportunityDataService.AddCarPoolOpportunity(this.CarPoolOpportunity);

        //Navigation.NavigateTo("");
    }
}

