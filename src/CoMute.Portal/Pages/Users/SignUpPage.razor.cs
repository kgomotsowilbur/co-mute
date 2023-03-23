using System;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using CoMute.Abstractions.Models;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Security.Principal;
using CoMute.Portal.Services;

namespace CoMute.Portal.Pages;

public class SignUpPageBase : ComponentBase
{
    #region [ CTOR ]
    public SignUpPageBase()
    {
        this.User = new();
    }
    #endregion

    #region [ Fields ]
    [Inject]
    public IUserDataService UserDataService { get; set; }
    [Inject]
    public NavigationManager Navigation { get; set; }

    public User User { get; set; }
    #endregion

    public async Task OnValidSubmit(EditContext context)
    {
        await this.UserDataService.AddUser(this.User);

    //    Navigation.NavigateTo("");
    }
}

