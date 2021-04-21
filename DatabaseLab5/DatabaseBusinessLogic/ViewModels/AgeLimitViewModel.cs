using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DatabaseBusinessLogic.ViewModels
{
    public class AgeLimitViewModel
    {
        public int? Id { get; set; }

        [DisplayName ("Возрастное ограничение")]
        public string Name { get; set; }
    }
}
