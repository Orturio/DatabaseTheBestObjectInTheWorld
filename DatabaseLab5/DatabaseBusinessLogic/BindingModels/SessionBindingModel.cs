using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseBusinessLogic.BindingModels
{
    public class SessionBindingModel
    {
        public int? Id { get; set; }

        public int FilmId { get; set; }

        public int UserId { get; set; }

        public DateTime StartOfWatchingMovie { get; set; }

        public string FilmName { get; set; }
    }
}
