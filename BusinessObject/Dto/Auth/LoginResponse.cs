using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Dto.Auth
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = null!;
        public int AccountId { get; set; }
    }
}
