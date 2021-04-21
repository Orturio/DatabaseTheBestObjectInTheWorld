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
    public class AgeLimitStorage : IAgeLimitStorage
    {
        public List<AgeLimitViewModel> GetFullList()
        {
            using (var context = new postgresContext())
            {
                return context.Agelimit.Select(CreateModel)
                .ToList();
            }
        }

        public List<AgeLimitViewModel> GetFilteredList(AgeLimitBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                return context.Agelimit
                .Where(rec => rec.Id == model.Id)
                .Select(CreateModel)
                .ToList();
            }
        }

        public AgeLimitViewModel GetElement(AgeLimitBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                var agelimit = context.Agelimit
                .FirstOrDefault(rec => rec.Id == model.Id);
                return agelimit != null ? CreateModel(agelimit) : null;
            }
        }

        public void Insert(AgeLimitBindingModel model)
        {
            using (var context = new postgresContext())
            {
                context.Agelimit.Add(CreateModel(model, new Agelimit()));
                context.SaveChanges();
            }
        }

        public void Update(AgeLimitBindingModel model)
        {
            using (var context = new postgresContext())
            {
                var element = context.Agelimit.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("возрастное ограничение не найдено");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(AgeLimitBindingModel model)
        {
            using (var context = new postgresContext())
            {
                Agelimit element = context.Agelimit.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Agelimit.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Пользователь не найден");
                }
            }
        }

        private Agelimit CreateModel(AgeLimitBindingModel model, Agelimit ageLimit)
        {
            ageLimit.Name = model.Name;
            return ageLimit;
        }

        private AgeLimitViewModel CreateModel(Agelimit user)
        {
            AgeLimitViewModel model = new AgeLimitViewModel();
            model.Id = user.Id;
            model.Name = user.Name;            
            return model;
        }
    }
}