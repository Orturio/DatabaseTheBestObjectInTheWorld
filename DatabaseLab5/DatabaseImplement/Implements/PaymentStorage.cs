using System;
using System.Collections.Generic;
using DatabaseBusinessLogic.BindingModels;
using DatabaseBusinessLogic.Interfaces;
using DatabaseBusinessLogic.ViewModels;
using DatabaseImplement.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DatabaseImplement.Implements
{
    public class PaymentStorage : IPaymentStorage
    {
        public List<PaymentViewModel> GetFullList()
        {
            using (var context = new postgresContext())
            {
                return context.Payment.Select(CreateModel)
                .ToList();
            }
        }

        public List<PaymentViewModel> GetFilteredList(PaymentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                return context.Payment
                .Where(rec => rec.Id == model.Id)
                .Select(CreateModel)
                .ToList();
            }
        }

        public PaymentViewModel GetElement(PaymentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                var payment = context.Payment
                .FirstOrDefault(rec => rec.Id == model.Id);
                return payment != null ? CreateModel(payment) : null;
            }
        }

        public void Insert(PaymentBindingModel model)
        {
            using (var context = new postgresContext())
            {
                context.Payment.Add(CreateModel(model, new Payment()));
                context.SaveChanges();
            }
        }

        public void Update(PaymentBindingModel model)
        {
            using (var context = new postgresContext())
            {
                var element = context.Payment.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Оплата не найдена");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(PaymentBindingModel model)
        {
            using (var context = new postgresContext())
            {
                Payment element = context.Payment.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Payment.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Клиент не найден");
                }
            }
        }

        private Payment CreateModel(PaymentBindingModel model, Payment payment)
        {
            payment.Durationofpayment = model.DurationOfPayment;
            return payment;
        }

        private PaymentViewModel CreateModel(Payment payment)
        {
            PaymentViewModel model = new PaymentViewModel();
            model.Id = payment.Id;
            model.DurationOfPayment = payment.Durationofpayment;
            return model;
        }
    }
}
