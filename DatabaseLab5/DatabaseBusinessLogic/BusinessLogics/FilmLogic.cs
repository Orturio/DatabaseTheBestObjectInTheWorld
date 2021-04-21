using System;
using System.Collections.Generic;
using DatabaseBusinessLogic.BindingModels;
using DatabaseBusinessLogic.Interfaces;
using DatabaseBusinessLogic.ViewModels;

namespace DatabaseBusinessLogic.BusinessLogics
{
    public class FilmLogic
    {
        private readonly IFilmStorage _filmStorage;

        public FilmLogic(IFilmStorage purchasesStorage)
        {
            _filmStorage = purchasesStorage;
        }

        public List<FilmViewModel> Read(FilmBindingModel model)
        {
            if (model == null)
            {
                return _filmStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<FilmViewModel> { _filmStorage.GetElement(model) };
            }
            return _filmStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(FilmBindingModel model)
        {
            var element = _filmStorage.GetElement(new FilmBindingModel { Name = model.Name });

            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Не найден фильм");
            }

            if (model.Id.HasValue)
            {
                _filmStorage.Update(model);
            }

            else
            {
                _filmStorage.Insert(model);
            }
        }

        public void Delete(FilmBindingModel model)
        {
            var element = _filmStorage.GetElement(new FilmBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Фильм не найден");
            }
            _filmStorage.Delete(model);
        }
    }
}
