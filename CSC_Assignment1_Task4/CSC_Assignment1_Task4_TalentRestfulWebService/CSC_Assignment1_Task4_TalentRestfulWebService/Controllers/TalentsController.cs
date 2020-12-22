using CSC_Assignment1_Task4_TalentRestfulWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CSC_Assignment1_Task4_TalentRestfulWebService.Controllers
{
    //refactoring TalentRepository into an interface
    public interface ITalentRepository
    {
        IEnumerable<Talent> GetTalents();
        Talent Get(int id);
        void Add(Talent talent);
    }

    public class TalentsController : ApiController
    {
        //provide ITalentRepository as a constructor parameter
        private ITalentRepository _repository;

        public TalentsController(ITalentRepository repository)
        {
            _repository = repository;
        }
    }

    //Web API Dependency Resolver
    public interface IDependencyResolver : IDependencyScope, IDisposable
    {
        IDependencyScope BeginScope();
    }

    public interface IDependencyScope : IDisposable
    {
        object GetService(Type serviceType);
        IEnumerable<object> GetServices(Type serviceType);
    }
}
