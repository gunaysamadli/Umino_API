using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umino.Authentication.Models;
using UminoWeb.DAL.Entities;

namespace Umino.Authentication.Services.Contracts
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterModel model);
        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<IList<ApplicationUser>> GetAllAsync();
    }
}
