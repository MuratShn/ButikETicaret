using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel_s
{
    public class UserLoginVM : IVM
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
