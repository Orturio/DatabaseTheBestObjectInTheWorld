using System;
using System.Collections.Generic;
using DatabaseBusinessLogic.BindingModels;
using DatabaseBusinessLogic.ViewModels;
using System.Text;

namespace DatabaseBusinessLogic.Interfaces
{
    public interface IUserStorage
    {
        List<UserViewModel> GetFullList();

        List<UserViewModel> GetFilteredList(UserBindingModel model);

        UserViewModel GetElement(UserBindingModel model);

        void Insert(UserBindingModel model);

        void Update(UserBindingModel model);

        void Delete(UserBindingModel model);
    }
}
