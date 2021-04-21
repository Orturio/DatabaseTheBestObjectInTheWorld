using System;
using System.Collections.Generic;
using System.Text;
using DatabaseBusinessLogic.BindingModels;
using DatabaseBusinessLogic.ViewModels;

namespace DatabaseBusinessLogic.Interfaces
{
    public interface IFilmStorage
    {
        List<FilmViewModel> GetFullList();

        List<FilmViewModel> GetFilteredList(FilmBindingModel model);

        FilmViewModel GetElement(FilmBindingModel model);

        void Insert(FilmBindingModel model);

        void Update(FilmBindingModel model);

        void Delete(FilmBindingModel model);
    }
}
