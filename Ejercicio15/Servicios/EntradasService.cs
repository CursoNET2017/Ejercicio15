using Ejercicio15.Models;
using Ejercicio15.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ejercicio15.Servicios
{
    public class EntradasService : IEntradasService
    {
        //[ThreadStatic] public static ApplicationDbContext applicationDbContext;// Para poder usarlo en el repository

        private IEntradasRepository entradasRepository;
        public EntradasService(IEntradasRepository _entradasRepository)
        {
            this.entradasRepository = _entradasRepository;
        }

        public Entrada Get(long id)
        {
            Entrada entrada;
            using (var context = new ApplicationDbContext())
            {
                ApplicationDbContext.applicationDbContext = context;
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        entrada = entradasRepository.Get(id);

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
            return entrada;

            //return entradasRepository.Get(id);
        }

        public Entrada Create(Entrada entrada)
        {
            using (var context = new ApplicationDbContext())
            {
                //applicationDbContext = context;// La asigno al valor guardado anteriormente. Para poder usarlo en el repository
                ApplicationDbContext.applicationDbContext = context;// La asigno al valor guardado anteriormente. Para poder usarlo en el repository
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        entrada = entradasRepository.Create(entrada);

                        context.SaveChanges();

                        dbContextTransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        dbContextTransaction.Rollback();
                        //throw e;
                        throw new Exception("He hecho rollback de la transaccion", e);// La 'e' dice la linea de la excepcion
                    }
                }
            }
            ApplicationDbContext.applicationDbContext = null;// Borro el valor
            return entrada;
            //return entradasRepository.Create(entrada);
        }


        public IQueryable<Entrada> GetEntradas()
        {
            IQueryable<Entrada> resultado;
            using (var context = new ApplicationDbContext())
            {
                ApplicationDbContext.applicationDbContext = context;
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        resultado = entradasRepository.GetEntradas();

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
            return resultado;            
        }

        public Entrada Delete(long id)
        {
            Entrada entrada;
            using (var context = new ApplicationDbContext())
            {
                //applicationDbContext = context;// La asigno al valor guardado anteriormente. Para poder usarlo en el repository
                ApplicationDbContext.applicationDbContext = context;// La asigno al valor guardado anteriormente. Para poder usarlo en el repository
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        entrada = entradasRepository.Delete(id);

                        context.SaveChanges();

                        dbContextTransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        dbContextTransaction.Rollback();
                        throw new Exception("He hecho rollback de la transaccion", e);// La 'e' dice la linea de la excepcion
                    }
                }
            }
            ApplicationDbContext.applicationDbContext = null;// Borro el valor
            return entrada;
        }

        public void Put(Entrada entrada)
        {
            using (var context = new ApplicationDbContext())
            {
                //applicationDbContext = context;// La asigno al valor guardado anteriormente. Para poder usarlo en el repository
                ApplicationDbContext.applicationDbContext = context;// La asigno al valor guardado anteriormente. Para poder usarlo en el repository
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        entrada = entradasRepository.Put(entrada);

                        context.SaveChanges();

                        dbContextTransaction.Commit();
                    }
                    catch (NoEncontradoException)
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                    catch (Exception e)
                    {
                        dbContextTransaction.Rollback();
                        throw new Exception("He hecho rollback de la transacción", e);
                    }
                }
            }
            ApplicationDbContext.applicationDbContext = null;// Borro el valor
        }
    }
}