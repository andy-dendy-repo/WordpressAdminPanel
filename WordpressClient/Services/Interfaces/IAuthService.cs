using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WordpressClient.Services.Interfaces
{
    public interface IAuthService
    {
        bool SignIn(string username, string password);
    }
}
