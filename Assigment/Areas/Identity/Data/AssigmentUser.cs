using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Assigment.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the AssigmentUser class
    public class AssigmentUser : IdentityUser
    {
<<<<<<< Updated upstream
        public string address { get; set; }

        public DateTime RegisteredDate { get; set; }

        public string Ic { get; set; }

        public string FullName { get;set; }
=======
        [PersonalData] public string FullName { get; set; }
        [PersonalData] public string Ic { get; set; }
        [PersonalData] public string address { get; set; }
        [PersonalData] public DateTime RegisteredDate { get; set; }
        [PersonalData] public override string PhoneNumber { get; set; }

>>>>>>> Stashed changes
    }
}
