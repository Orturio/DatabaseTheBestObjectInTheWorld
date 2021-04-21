using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DatabaseImplement.Models
{
    public partial class Users
    {
        public Users()
        {
            Session = new HashSet<Session>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Paymentid { get; set; }

        public virtual Payment Payment { get; set; }
        public virtual ICollection<Session> Session { get; set; }
    }
}
