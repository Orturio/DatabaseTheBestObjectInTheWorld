using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace DatabaseBusinessLogic.ViewModels
{
    public class UserViewModel
    {
        public int? Id { get; set; }

        public int? PaymentId { get; set; }

        [DisplayName ("Имя пользователя")]
        public string Name { get; set; }

        [DisplayName("Пароль пользователя")]
        public string Password { get; set; }
    }
}
