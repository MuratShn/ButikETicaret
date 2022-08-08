using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel_s
{
    public class GoogleLoginVm : IVM
    {
        public string Id { get; set; }
        public string IdToken { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string Provider { get; set; }

    }
}

//email: "muratsahh.37@gmail.com"
//firstName: "Murat"
//id: "107234872043967149716"
//idToken:
//lastName: "Åahin"
//name: "Murat Åahin"
//photoUrl: "https://lh3.googleusercontent.com/a-/AFdZuco6PuwcrMI3Xn7nAyP1ELlWz1zZCbMjILOagl7N3w=s96-c"
//provider: "GOOGLE"