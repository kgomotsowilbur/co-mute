using System;
using CoMute.Abstractions;
using CoMute.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoMute.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : PortalControllerBase
{
    public UserController(IPortalDataProvider dataProvider) : base(dataProvider)
    {

    }

    [HttpGet("{userId}")]
    public async Task<User> GetUser([FromRoute] Guid userId)
    {
        try
        {
            var result = await this._dataprovider.GetUser(userId);

            return result;
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        try
        {
            var result = await this._dataprovider.GetAllUsers();
            return result;
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }
        
    }

    [HttpPost]
    public async Task AddUser([FromBody] User user)
    {
        try
        {
            var result = await this._dataprovider.GetUser(user.Id);
            if (result == null) {
                await this._dataprovider.Insert(user);
            }
            else 
            { 
                await this._dataprovider.Update(user);
            }
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [Route("addCarPoolOpportunity")]
    [HttpPost]
    public async Task AddCarPoolOpportunityToUser([FromBody] CarPoolOpportunity carPoolOpportunity)
    {
        try
        {
            await this._dataprovider.Insert(carPoolOpportunity);
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpGet("{userId}/userDetails")]
    public async Task<User> GetUserDetails([FromRoute] Guid userId)
    {
        try
        {
            var result = await this._dataprovider.GetUserDetails(userId);
            return result;
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }
        
    }
}

