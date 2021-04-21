using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DatabaseBusinessLogic.ViewModels
{
    public class ActorViewModel
    {
        public int? Id { get; set; }

        [DisplayName ("Имя актёра")]
        public string Name { get; set; }

        [DisplayName("Фамилия актёра")]
        public string Surname { get; set; }

        [DisplayName("Отчество актёра")]
        public string MiddleName { get; set; }
    }
}
