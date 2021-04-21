using System;
using System.Collections.Generic;
using System.Text;
using DatabaseBusinessLogic.BindingModels;
using DatabaseBusinessLogic.ViewModels;

namespace DatabaseBusinessLogic.Interfaces
{
    public interface IActorStorage
    {
        List<ActorViewModel> GetFullList();

        List<ActorViewModel> GetFilteredList(ActorBindingModel model);

        ActorViewModel GetElement(ActorBindingModel model);

        void Insert(ActorBindingModel model);

        void Update(ActorBindingModel model);

        void Delete(ActorBindingModel model);
    }
}
