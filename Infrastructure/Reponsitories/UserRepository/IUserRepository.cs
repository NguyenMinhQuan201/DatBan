using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Reponsitories.UserReponsitories
{
    public interface IRoleRepository : IRepositoryBase<AppUser>
    {
        /*Tokens GenerateToken(string userName);
        Tokens GenerateRefreshToken(string userName);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);*/
    }
}
