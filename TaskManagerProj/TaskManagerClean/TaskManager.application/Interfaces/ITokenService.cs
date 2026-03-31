using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.application.Interfaces
{
    public interface ITokenService
    {
        public  string CreateToken(string userName, string passWord, string userId, List<string> roles);

        public string RefreshAccessToken(string expiredToken, string refreshToken);

        public string generateRefreshToken();

        public void SaveRefreshToken(string userId, string refreshToken);
    }
}
