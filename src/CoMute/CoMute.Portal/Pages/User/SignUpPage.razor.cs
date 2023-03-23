using System;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using AeverPortal.Abstractions.Models;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Security.Principal;
using AeverPortal.Portal.Services;

namespace AeverPortal.Portal.Pages;

public class NewFundManagerPageBase : ComponentBase
{
    #region [ CTOR ]
    public NewFundManagerPageBase()
    {
        this.FundManager = new();
    }
    #endregion

    #region [ Fields ]
    [Inject]
    public IFundManagerDataService FundManagerDataService { get; set; }
    [Inject]
    public NavigationManager Navigation { get; set; }

    public User FundManager { get; set; }
    #endregion

    public async Task OnValidSubmit(EditContext context)
    {
        await this.FundManagerDataService.AddFundManager(this.FundManager);
        StateHasChanged();

        Navigation.NavigateTo("/fundManagersList");
    }
}

