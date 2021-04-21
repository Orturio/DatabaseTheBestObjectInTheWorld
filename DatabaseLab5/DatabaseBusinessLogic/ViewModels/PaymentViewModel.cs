using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace DatabaseBusinessLogic.ViewModels
{
    public class PaymentViewModel
    {
        public int? Id { get; set; }

        [DisplayName ("Длительность подписки")]
        public int DurationOfPayment { get; set; }
    }
}
