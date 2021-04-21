using System;
using System.Collections.Generic;
using System.Text;
using DatabaseBusinessLogic.BindingModels;
using DatabaseBusinessLogic.ViewModels;

namespace DatabaseBusinessLogic.Interfaces
{
    public interface IAgeLimitStorage
    {
        List<AgeLimitViewModel> GetFullList();

        List<AgeLimitViewModel> GetFilteredList(AgeLimitBindingModel model);

        AgeLimitViewModel GetElement(AgeLimitBindingModel model);

        void Insert(AgeLimitBindingModel model);

        void Update(AgeLimitBindingModel model);

        void Delete(AgeLimitBindingModel model);
    }
}
