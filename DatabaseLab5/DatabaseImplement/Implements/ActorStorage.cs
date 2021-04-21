using System;
using System.Collections.Generic;
using System.Text;
using DatabaseBusinessLogic.BindingModels;
using DatabaseBusinessLogic.Interfaces;
using DatabaseBusinessLogic.ViewModels;
using DatabaseImplement.Models;
using System.Linq;

namespace DatabaseImplement.Implements
{
    public class ActorStorage : IActorStorage
    {
        public List<ActorViewModel> GetFullList()
        {
            using (var context = new postgresContext())
            {
                return context.Actor.Select(CreateModel)
                .ToList();
            }
        }

        public List<ActorViewModel> GetFilteredList(ActorBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                return context.Actor
                .Where(rec => rec.Id == model.Id)
                .Select(CreateModel)
                .ToList();
            }
        }

        public ActorViewModel GetElement(ActorBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                var actor = context.Actor.FirstOrDefault(rec => rec.Id == model.Id);
                return actor != null ?
                new ActorViewModel
                {
                    Id = actor.Id,
                    Name = actor.Name,
                    Surname = actor.Surname,
                    MiddleName = actor.Middlename
                } : null;
            }
        }

        public void Insert(ActorBindingModel model)
        {
            using (var context = new postgresContext())
            {
                context.Actor.Add(CreateModel(model, new Actor()));
                context.SaveChanges();
            }
        }

        public void Update(ActorBindingModel model)
        {
            using (var context = new postgresContext())
            {
                var element = context.Actor.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Актёр не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(ActorBindingModel model)
        {
            using (var context = new postgresContext())
            {
                Actor element = context.Actor.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Actor.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Актёр не найден");
                }
            }
        }

        private Actor CreateModel(ActorBindingModel model, Actor actor)
        {
            actor.Name = model.Name;
            actor.Surname = model.Surname;
            actor.Middlename = model.MiddleName;          
            return actor;
        }

        private ActorViewModel CreateModel(Actor actor)
        {
            ActorViewModel model = new ActorViewModel();
            model.Id = actor.Id;
            model.Name = actor.Name;
            model.Surname = actor.Surname;
            model.MiddleName = actor.Middlename;
            return model;
        }
    }
}
