using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IETTJWT.Core.Models
{
    public class UserRefreshToken
    {


        public string UserId { get; set; }

        public string RefreshToken { get; set; }

        public DateTime Expiration { get; set; }

    }
}
