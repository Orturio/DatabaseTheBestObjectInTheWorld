using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace DatabaseBusinessLogic.ViewModels
{
    public class FilmViewModel
    {
        public int? Id { get; set; }

        public int? AgeLimitId { get; set; }

        [DisplayName ("Название фильма")]
        public string Name { get; set; }

        [DisplayName("Описание фильма")]
        public string Description { get; set; }

        [DisplayName("Рейтинг фильма")]
        public int Rating { get; set; }
    }
}
