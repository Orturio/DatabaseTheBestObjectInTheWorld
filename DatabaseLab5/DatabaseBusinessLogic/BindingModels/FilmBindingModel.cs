using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseBusinessLogic.BindingModels
{
    public class FilmBindingModel
    {
        public int? Id { get; set; }

        public int AgeLimitId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }
    }
}
