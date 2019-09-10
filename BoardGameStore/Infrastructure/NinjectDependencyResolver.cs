using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Moq;
using Ninject;
using LibraryBoardGameStore.Concreate;
using LibraryBoardGameStore.Abstract;
using LibraryBoardGameStore.Entites;
using BoardGameStore.Infrastructure.Abstract;
using BoardGameStore.Infrastructure.Concrete;
namespace BoardGameStore.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
        //         Mock<IBoardGameRepository> mock = new Mock<IBoardGameRepository>();
        //    mock.Setup(m => m.Games).Returns(new List<BoardGame>
        //    {
        //        new BoardGame{NameGame="Uno",Price=235M},
        //        new BoardGame{NameGame="Саботер",Price=320M},
        //        new BoardGame{NameGame="Метро 2033",Price=450M},
        //        new BoardGame{NameGame="Ксеркс", Price=345M}
        //    });
        //    kernel.Bind<IBoardGameRepository>().ToConstant(mock.Object);// Здесь размещаются привязки
            kernel.Bind<IBoardGameRepository>().To<EFGameRepository>();
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };
            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);
            //kernel.Bind<IAuthProvider>().To<FormAuthProvider>();
        }
       
    }
    
}