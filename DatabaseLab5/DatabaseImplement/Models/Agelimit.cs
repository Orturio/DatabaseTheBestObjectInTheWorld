using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DatabaseImplement.Models
{
    public partial class Agelimit
    {
        public Agelimit()
        {
            Film = new HashSet<Film>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Film> Film { get; set; }
    }
}
