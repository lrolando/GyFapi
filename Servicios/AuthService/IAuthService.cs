using Commons.DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthService
{
    public interface IAuthService
    {
        public Task<Response<User>> Authenticate(AuthRequest userReq);

    }
}
