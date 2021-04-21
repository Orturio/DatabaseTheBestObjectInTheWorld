using System;
using System.Collections.Generic;
using System.Text;
using DatabaseBusinessLogic.BindingModels;
using DatabaseBusinessLogic.ViewModels;

namespace DatabaseBusinessLogic.Interfaces
{
    public interface IPaymentStorage
    {
        List<PaymentViewModel> GetFullList();

        List<PaymentViewModel> GetFilteredList(PaymentBindingModel model);

        PaymentViewModel GetElement(PaymentBindingModel model);

        void Insert(PaymentBindingModel model);

        void Update(PaymentBindingModel model);

        void Delete(PaymentBindingModel model);
    }
}
