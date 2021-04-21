using System;
using System.Collections.Generic;
using DatabaseBusinessLogic.BindingModels;
using DatabaseBusinessLogic.Interfaces;
using DatabaseBusinessLogic.ViewModels;

namespace DatabaseBusinessLogic.BusinessLogics
{
    public class ActorLogic
    {
        private readonly IActorStorage _actorStorage;

        public ActorLogic(IActorStorage actorStorage)
        {
            _actorStorage = actorStorage;
        }

        public List<ActorViewModel> Read(ActorBindingModel model)
        {
            if (model == null)
            {
                return _actorStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<ActorViewModel> { _actorStorage.GetElement(model) };
            }

            return _actorStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(ActorBindingModel model)
        {
            var element = _actorStorage.GetElement(new ActorBindingModel { Id = model.Id });

            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Не найден такой актёр");
            }

            if (model.Id.HasValue)
            {
                _actorStorage.Update(model);
            }

            else
            {
                _actorStorage.Insert(model);
            }
        }

        public void Delete(ActorBindingModel model)
        {
            var element = _actorStorage.GetElement(new ActorBindingModel { Id = model.Id });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _actorStorage.Delete(model);
        }
    }
}
