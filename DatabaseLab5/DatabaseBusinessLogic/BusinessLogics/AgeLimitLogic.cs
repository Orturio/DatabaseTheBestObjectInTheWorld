using System;
using System.Collections.Generic;
using DatabaseBusinessLogic.BindingModels;
using DatabaseBusinessLogic.Interfaces;
using DatabaseBusinessLogic.ViewModels;

namespace DatabaseBusinessLogic.BusinessLogics
{
    public class AgeLimitLogic
    {
        private readonly IAgeLimitStorage _ageLimitStorage;

        public AgeLimitLogic(IAgeLimitStorage ageLimitStorage)
        {
            _ageLimitStorage = ageLimitStorage;
        }

        public List<AgeLimitViewModel> Read(AgeLimitBindingModel model)
        {
            if (model == null)
            {
                return _ageLimitStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<AgeLimitViewModel> { _ageLimitStorage.GetElement(model) };
            }
            return _ageLimitStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(AgeLimitBindingModel model)
        {
            var element = _ageLimitStorage.GetElement(new AgeLimitBindingModel { Id = model.Id });

            if (element == null)
            {
                throw new Exception("Не найдено возрастное ограничение");
            }

            if (model.Id.HasValue)
            {
                _ageLimitStorage.Update(model);
            }

            else
            {
                _ageLimitStorage.Insert(model);
            }
        }

        public void Delete(AgeLimitBindingModel model)
        {
            var element = _ageLimitStorage.GetElement(new AgeLimitBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("возрастное ограничение не найдено");
            }
            _ageLimitStorage.Delete(model);
        }
    }
}
