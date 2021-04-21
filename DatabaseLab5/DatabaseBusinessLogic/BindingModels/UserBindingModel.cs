using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseBusinessLogic.BindingModels
{
    public class UserBindingModel
    {
        public int? Id { get; set; }

        public int PaymentId { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
    }
}
