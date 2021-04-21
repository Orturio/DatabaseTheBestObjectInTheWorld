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
    public class SessionStorage : ISessionStorage
    {
        public List<SessionViewModel> GetFullList()
        {
            using (var context = new postgresContext())
            {
                return context.Session.Include(x => x.Film).Include(x => x.Users).Select(CreateModel)
                .ToList();
            }
        }

        public List<SessionViewModel> GetFilteredList(SessionBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                return context.Session.Include(x => x.Film).Include(x => x.Users)
                .Where(rec => rec.Id == model.Id && rec.Startofwatchingmovie == model.StartOfWatchingMovie)
                .Select(CreateModel)
                .ToList();
            }
        }

        public SessionViewModel GetElement(SessionBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                var session = context.Session.Include(x => x.Film).Include(x => x.Users)
                .FirstOrDefault(rec => rec.Id == model.Id);
                return session != null ? CreateModel(session) : null;
            }
        }

        public void Insert(SessionBindingModel model)
        {
            using (var context = new postgresContext())
            {
                context.Session.Add(CreateModel(model, new Session()));
                context.SaveChanges();
            }
        }

        public void Update(SessionBindingModel model)
        {
            using (var context = new postgresContext())
            {
                var element = context.Session.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Сессия не найденa");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(SessionBindingModel model)
        {
            using (var context = new postgresContext())
            {
                Session element = context.Session.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Session.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Сессия не найден");
                }
            }
        }

        private Session CreateModel(SessionBindingModel model, Session session)
        {
            session.Filmid = model.FilmId;
            session.Usersid = model.UserId;
            session.Startofwatchingmovie = model.StartOfWatchingMovie;
            return session;
        }

        private SessionViewModel CreateModel(Session session)
        {
            SessionViewModel model = new SessionViewModel();
            model.Id = session.Id;
            model.FilmId = session.Filmid;
            model.UserId = session.Usersid;
            model.FilmName = session.Film.Name;            
            return model;
        }
    }
}
