using CSC_Assignment1_Task4_TalentRestfulWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Dependencies;
using Unity;

namespace CSC_Assignment1_Task4_TalentRestfulWebService.Controllers
{
    public class TalentsController : ApiController
    {
        //not the best approach to call the repository
        static readonly TalentRepository repository = new TalentRepository();

        ////provide ITalentRepository as a constructor parameter
        //private ITalentRepository _repository;

        //public TalentsController(ITalentRepository repository)
        //{
        //    _repository = repository;
        //}

        [EnableCors(origins: "*", headers: "*", methods: "*")]

        //GET all talents
        [Route("api/talents")]
        public IEnumerable<Talent> GetAllTalents()
        {
            return repository.GetAll();
        }

        //GET talent by id
        [Route("api/talents/{id:int}")]
        public Talent GetTalent(int id)
        {
            Talent item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }
    }

    ////Dependency Injection using Unity
    //public class UnityResolver : IDependencyResolver
    //{
    //    protected IUnityContainer container;

    //    public UnityResolver(IUnityContainer container)
    //    {
    //        if (container == null)
    //        {
    //            throw new ArgumentNullException(nameof(container));
    //        }
    //        this.container = container;
    //    }

    //    public object GetService(Type serviceType)
    //    {
    //        try
    //        {
    //            return container.Resolve(serviceType);
    //        }
    //        catch (ResolutionFailedException exception)
    //        {
    //            throw new InvalidOperationException(
    //                $"Unable to resolve service for type {serviceType}.",
    //                exception);
    //        }
    //    }

    //    public IEnumerable<object> GetServices(Type serviceType)
    //    {
    //        try
    //        {
    //            return container.ResolveAll(serviceType);
    //        }
    //        catch (ResolutionFailedException exception)
    //        {
    //            throw new InvalidOperationException(
    //                $"Unable to resolve service for type {serviceType}.",
    //                exception);
    //        }
    //    }

    //    public IDependencyScope BeginScope()
    //    {
    //        var child = container.CreateChildContainer();
    //        return new UnityResolver(child);
    //    }

    //    public void Dispose()
    //    {
    //        Dispose(true);
    //    }

    //    protected virtual void Dispose(bool disposing)
    //    {
    //        container.Dispose();
    //    }
    //}
}
