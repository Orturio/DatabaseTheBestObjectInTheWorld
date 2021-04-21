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
    public class FilmStorage : IFilmStorage
    {
        public List<FilmViewModel> GetFullList()
        {
            using (var context = new postgresContext())
            {
                return context.Film.Include(x => x.Agelimit).Select(CreateModel)
                .ToList();
            }
        }

        public List<FilmViewModel> GetFilteredList(FilmBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                return context.Film.Include(x => x.Agelimit)
                .Where(rec => rec.Name == model.Name)
                .Select(CreateModel)
                .ToList();
            }
        }

        public FilmViewModel GetElement(FilmBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                var film = context.Film.Include(x => x.Agelimit)
                .FirstOrDefault(rec => rec.Id == model.Id || rec.Name == model.Name);
                return film != null ? CreateModel(film) : null;
            }
        }

        public void Insert(FilmBindingModel model)
        {
            using (var context = new postgresContext())
            {
                context.Film.Add(CreateModel(model, new Film()));
                context.SaveChanges();
            }
        }

        public void Update(FilmBindingModel model)
        {
            using (var context = new postgresContext())
            {
                var element = context.Film.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Фильм не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(FilmBindingModel model)
        {
            using (var context = new postgresContext())
            {
                Film element = context.Film.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Film.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Фильм не найден");
                }
            }
        }

        private Film CreateModel(FilmBindingModel model, Film film)
        {
            film.Agelimitid = model.AgeLimitId;
            film.Name = model.Name;
            film.Description = model.Description;
            film.Rating = model.Rating;         
            return film;
        }

        private FilmViewModel CreateModel(Film film)
        {
            FilmViewModel model = new FilmViewModel();
            model.Id = film.Id;
            model.AgeLimitId = film.Agelimitid;
            model.Name = film.Name;
            model.Description = film.Description;
            model.Rating = film.Rating;
            return model;
        }
    }
}
