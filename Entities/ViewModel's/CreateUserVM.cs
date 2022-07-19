using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel_s
{
    public class CreateUserVM :IVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; }
        public string Password{ get; set; }
        public string PasswordConfirm { get; set; }
    }
}
