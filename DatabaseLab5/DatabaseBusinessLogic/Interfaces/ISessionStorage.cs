using System;
using System.Collections.Generic;
using System.Text;
using DatabaseBusinessLogic.BindingModels;
using DatabaseBusinessLogic.ViewModels;

namespace DatabaseBusinessLogic.Interfaces
{
    public interface ISessionStorage
    {
        List<SessionViewModel> GetFullList();

        List<SessionViewModel> GetFilteredList(SessionBindingModel model);

        SessionViewModel GetElement(SessionBindingModel model);

        void Insert(SessionBindingModel model);

        void Update(SessionBindingModel model);

        void Delete(SessionBindingModel model);
    }
}
