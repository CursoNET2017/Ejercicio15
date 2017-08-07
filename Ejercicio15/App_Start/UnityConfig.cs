using Ejercicio15.Repository;
using Ejercicio15.Servicios;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System.Web.Http;
using Unity.WebApi;
using System;
using System.Collections.Generic;
using Ejercicio15.Models;

namespace Ejercicio15
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            //Añadimos un Interceptor
            container.AddNewExtension<Interception>();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            //container.RegisterType<IEntradasService, EntradasService>();
            container.RegisterType<IEntradasRepository, EntradasRepository>();

            //Añadimos 
            container.RegisterType<IEntradasService, EntradasService>(
              new Interceptor<InterfaceInterceptor>(),
              new InterceptionBehavior<LoggingInterceptionBehavior>());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        class LoggingInterceptionBehavior : IInterceptionBehavior
        {
            public IMethodReturn Invoke(IMethodInvocation input,
              GetNextInterceptionBehaviorDelegate getNext)
            {
                IMethodReturn result;
                //// Before invoking the method on the original target.
                //WriteLog(String.Format(
                //  "Invoking method {0} at {1}",
                //  input.MethodBase, DateTime.Now.ToLongTimeString()));

                //// Invoke the next behavior in the chain.

                using (var context = new ApplicationDbContext())
                {
                    ApplicationDbContext.applicationDbContext = context;
                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            //entrada = entradasRepository.Get(id);

                            
                            result = getNext()(input, getNext);

                            //// After invoking the method on the original target.
                            //if (result.Exception != null)
                            //{
                            //    WriteLog(String.Format(
                            //      "Method {0} threw exception {1} at {2}",
                            //      input.MethodBase, result.Exception.Message,
                            //      DateTime.Now.ToLongTimeString()));
                            //}
                            //else
                            //{
                            //    WriteLog(String.Format(
                            //      "Method {0} returned {1} at {2}",
                            //      input.MethodBase, result.ReturnValue,
                            //      DateTime.Now.ToLongTimeString()));
                            //}

                            //return result;
                            if(result.Exception != null)
                            {
                                throw result.Exception;
                            }

                            context.SaveChanges();

                            dbContextTransaction.Commit();
                        }
                        catch (Exception e)
                        {
                            dbContextTransaction.Rollback();
                            throw new Exception("He hecho rollback de la transacción", e);
                        }
                    }
                }
                ApplicationDbContext.applicationDbContext = null;
                return result;
            }

            public IEnumerable<Type> GetRequiredInterfaces()
            {
                return Type.EmptyTypes;
            }

            public bool WillExecute
            {
                get { return true; }
            }

            private void WriteLog(string message)
            {

            }
        }
    }
}