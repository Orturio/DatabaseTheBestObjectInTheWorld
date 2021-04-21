using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DatabaseImplement.Models
{
    public partial class Session
    {
        public int Id { get; set; }
        public DateTime Startofwatchingmovie { get; set; }
        public int Filmid { get; set; }
        public int Usersid { get; set; }

        public virtual Film Film { get; set; }
        public virtual Users Users { get; set; }
    }
}
