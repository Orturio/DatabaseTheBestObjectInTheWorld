using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseBusinessLogic.BindingModels
{
    public class PaymentBindingModel
    {
        public int? Id { get; set; }

        public int DurationOfPayment { get; set; }
    }
}
