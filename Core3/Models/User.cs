using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatterSite.Models
{
    public class User : IdentityUser
    {
        #region Constructor
        public User()
        {
            Messages = new HashSet<Message>();
        }
        #endregion

        public virtual ICollection<Message> Messages { get; set; }
    }
}
