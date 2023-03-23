using System;
using AeverPortal.Abstractions.Models;
using AeverPortal.Portal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace AeverPortal.Portal.Pages;

public class FundManagerDetailsPageBase : ComponentBase
{
    public FundManagerDetailsPageBase()
    {
        this.FundManager = new();
        this.Address = new();
    }

    [Parameter]
    public Guid FundManagerId { get; set; }
    [Inject]
    public IFundManagerDataService FundManagerDataService { get; set; }
    protected User FundManager { get; set; }
    protected Address Address { get; set; }
    protected CarPoolOpportunity Fund { get; set; }

    public bool FundRegisteredIsOpen = false;
    public bool FundManagerRegisteredIsOpen = false;
    public bool FundPhysicalIsOpen = false;
    public bool FundManagerPhysicalIsOpen = false;
    public string FundName = "";

    protected override async Task OnInitializedAsync()
    {
        this.FundManager = await this.FundManagerDataService.GetFundManagerDetails(FundManagerId);
    }

    public void ChangeFund(CarPoolOpportunity fund)
    {
        this.Fund = fund;
    }
    public async Task Submit(EditContext context)
    {
        var model = context.Model;

        if (this.FundManagerRegisteredIsOpen)
        {
            this.FundManager.RegisteredAddress = (Address)model;
        }
        else if (this.FundRegisteredIsOpen)
        {
            foreach (var fund in FundManager.Funds)
            {
                if (fund.Name == this.FundName)
                    fund.RegisteredAddress = (Address)model;
            }

        }
        else if (this.FundPhysicalIsOpen)
        {
            foreach (var fund in FundManager.Funds)
            {
                if (fund.Name == this.FundName)
                    fund.PhysicalAddress = (Address)model;
            }

        }
        else if (this.FundManagerPhysicalIsOpen)
        {
            this.FundManager.PhysicalAddress = (Address)model;
        }
        await this.FundManagerDataService.AddFundManager(this.FundManager);

        this.FundManagerPhysicalIsOpen = false;
        this.FundPhysicalIsOpen = false;
        this.FundManagerRegisteredIsOpen = false;
        this.FundRegisteredIsOpen = false;
    }

    public void Cancel()
    {
        this.FundManagerPhysicalIsOpen = false;
        this.FundPhysicalIsOpen = false;
        this.FundManagerRegisteredIsOpen = false;
        this.FundRegisteredIsOpen = false;
    }

    public void FundManagerPhysicalPopover(User fundManager)
    {
        this.FundManagerPhysicalIsOpen = true;
        this.Address = fundManager.PhysicalAddress;
    }
    public void FundManagerRegisteredPopover(User fundManager)
    {
        this.FundManagerRegisteredIsOpen = true;
        this.Address = fundManager.RegisteredAddress;
    }
    public void FundPhysicalPopover(CarPoolOpportunity fund)
    {
        this.FundPhysicalIsOpen = true;
        this.Address = fund.PhysicalAddress;
        this.FundName = fund.Name;
    }
    public void FundRegisteredPopover(CarPoolOpportunity fund)
    {
        this.FundRegisteredIsOpen = true;
        this.Address = fund.RegisteredAddress;
        this.FundName = fund.Name;
    }
}

