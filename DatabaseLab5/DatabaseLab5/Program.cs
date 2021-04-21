using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseBusinessLogic.Interfaces;
using DatabaseBusinessLogic.BusinessLogics;
using DatabaseImplement.Implements;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;
using DatabaseBusinessLogic.ViewModels;

namespace DatabaseLab5
{
    static class Program
    {

        public static UserViewModel User { get; set; }
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new UnityContainer().AddExtension(new Diagnostic());
            Application.Run(container.Resolve<FormMain>());
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IUserStorage, UserStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IFilmStorage, FilmStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISessionStorage, SessionStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IPaymentStorage, PaymentStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IAgeLimitStorage, AgeLimitStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IActorStorage, ActorStorage>(new
            HierarchicalLifetimeManager());

            currentContainer.RegisterType<UserLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<FilmLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<SessionLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<PaymentLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<AgeLimitStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ActorLogic>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
