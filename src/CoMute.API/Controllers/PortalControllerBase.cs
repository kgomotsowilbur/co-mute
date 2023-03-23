using System;
using CoMute.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoMute.API.Controllers;

public abstract class PortalControllerBase : ControllerBase
{
	public const string JSON_MEDIA_TYPE = "application/json";

	protected readonly IPortalDataProvider _dataprovider;

	public PortalControllerBase(IPortalDataProvider dataProvider)
	{
		this._dataprovider = dataProvider;
	}

	protected StringContent PrepareJsonPayload(object payloadContentObject)
	{
		var serialiazedPayload = JsonConvert.SerializeObject(payloadContentObject);
		var result = new StringContent(serialiazedPayload, System.Text.Encoding.UTF8, JSON_MEDIA_TYPE);

		return result;
	}
}

