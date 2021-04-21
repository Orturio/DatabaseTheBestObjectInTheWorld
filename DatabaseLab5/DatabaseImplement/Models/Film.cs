using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DatabaseImplement.Models
{
    public partial class Film
    {
        public Film()
        {
            Session = new HashSet<Session>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int Agelimitid { get; set; }

        public virtual Agelimit Agelimit { get; set; }
        public virtual ICollection<Session> Session { get; set; }
    }
}
