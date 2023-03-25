using System;
using CoMute.Abstractions.Models;

namespace CoMute.Portal.Services;

public interface IUserDataService
{
    Task<Users> GetUser(Guid userId);
    Task<IEnumerable<Users>> GetAllUsers();
    Task AddUser(Users user);
    Task<Users> GetUserDetails(Guid userId);
}

