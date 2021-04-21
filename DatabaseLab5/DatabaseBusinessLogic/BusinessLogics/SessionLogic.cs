using System;
using System.Collections.Generic;
using DatabaseBusinessLogic.BindingModels;
using DatabaseBusinessLogic.Interfaces;
using DatabaseBusinessLogic.ViewModels;

namespace DatabaseBusinessLogic.BusinessLogics
{
    public class SessionLogic
    {
        private readonly ISessionStorage _sessionStorage;

        public SessionLogic(ISessionStorage cannedStorage)
        {
            _sessionStorage = cannedStorage;
        }

        public List<SessionViewModel> Read(SessionBindingModel model)
        {
            if (model == null)
            {
                return _sessionStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<SessionViewModel> { _sessionStorage.GetElement(model) };
            }

            return _sessionStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(SessionBindingModel model)
        {
            var element = _sessionStorage.GetElement(new SessionBindingModel { Id = model.Id });

            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Не найдена сессия");
            }

            if (model.Id.HasValue)
            {
                _sessionStorage.Update(model);
            }

            else
            {
                _sessionStorage.Insert(model);
            }
        }

        public void Delete(SessionBindingModel model)
        {
            var element = _sessionStorage.GetElement(new SessionBindingModel { Id = model.Id });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _sessionStorage.Delete(model);
        }
    }
}
