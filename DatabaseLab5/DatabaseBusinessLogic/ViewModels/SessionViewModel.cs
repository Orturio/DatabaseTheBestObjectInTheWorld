using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Runtime.Serialization;

namespace DatabaseBusinessLogic.ViewModels
{
    public class SessionViewModel
    {
        public int? Id { get; set; }

        public int? FilmId { get; set; }

        public int? UserId { get; set; }

        [DisplayName ("Дата начала просмотра")]
        public DateTime StartOfWatchingMovie { get; set; }

        [DisplayName ("Название фильма")]
        public string FilmName { get; set; }
    }
}
