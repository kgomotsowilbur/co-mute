using System;
using AeverPortal.Abstractions.Models;
using AeverPortal.Portal.Services;
using Microsoft.AspNetCore.Components;

namespace AeverPortal.Portal.Pages
{
	public class FundsDetailsBase : ComponentBase
	{
		public FundsDetailsBase()
		{
            this.Fund = new();
            this.FundOwner = new();
		}

        [Parameter]
        public Guid FundId { get; set; }

        [Inject]
        public ICarPoolOpportunityDataService FundDataService { get; set; }
        [Inject]
        public IFundManagerDataService FundManagerDataService { get; set; }

        protected CarPoolOpportunity Fund { get; set; }
        protected User FundOwner { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.Fund = await this.FundDataService.GetFund(FundId);
            this.FundOwner = await this.FundManagerDataService.GetFundManager(this.Fund.FundManagerId);
        }
    }
}

