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
    public class UserStorage : IUserStorage
    {
        public List<UserViewModel> GetFullList()
        {
            using (var context = new postgresContext())
            {
                return context.Users.Include(x => x.Payment).Select(CreateModel)
                .ToList();
            }
        }

        public List<UserViewModel> GetFilteredList(UserBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                return context.Users.Include(x => x.Payment)
                .Where(rec => rec.Id == model.Id)
                .Select(CreateModel)
                .ToList();
            }
        }

        public UserViewModel GetElement(UserBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                var user = context.Users.Include(x => x.Payment)
                .FirstOrDefault(rec => rec.Id == model.Id || rec.Name == model.Name);
                return user != null ? CreateModel(user) : null;
            }
        }

        public void Insert(UserBindingModel model)
        {
            using (var context = new postgresContext())
            {
                context.Users.Add(CreateModel(model, new Users()));
                context.SaveChanges();
            }
        }

        public void Update(UserBindingModel model)
        {
            using (var context = new postgresContext())
            {
                var element = context.Users.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("пользователь не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(UserBindingModel model)
        {
            using (var context = new postgresContext())
            {
                Users element = context.Users.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Users.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Пользователь не найден");
                }
            }
        }

        private Users CreateModel(UserBindingModel model, Users user)
        {
            user.Name = model.Name;
            user.Password = model.Password;
            user.Paymentid = model.PaymentId;
            return user;
        }

        private UserViewModel CreateModel(Users user)
        {
            UserViewModel model = new UserViewModel();
            model.Id = user.Id;
            model.PaymentId = user.Paymentid;
            model.Name = user.Name;
            model.Password = user.Password;
            return model;
        }
    }
}
