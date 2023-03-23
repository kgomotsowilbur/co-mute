using System;
using CoMute.Abstractions.Models;

namespace CoMute.Portal.Services;

public interface IUserDataService
{
    Task<User> GetUser(Guid userId);
    Task<IEnumerable<User>> GetAllUsers();
    Task AddUser(User user);
    Task<User> GetUserDetails(Guid userId);
}

