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
        [ThreadStatic] public static ApplicationDbContext applicationDbContext;// Para poder usarlo en el repository

        private IEntradasRepository entradasRepository;
        public EntradasService(IEntradasRepository _entradasRepository)
        {
            this.entradasRepository = _entradasRepository;
        }

        public Entrada Create(Entrada entrada)
        {
            using (var context = new ApplicationDbContext())
            {
                applicationDbContext = context;// La asigno al valor guardado anteriormente. Para poder usarlo en el repository
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        entradasRepository.Create(entrada);

                        context.SaveChanges();

                        dbContextTransaction.Commit();
                    } catch (Exception e)
                    {
                        dbContextTransaction.Rollback();
                        //throw e;
                        throw new Exception("He hecho rollback de la transaccion", e);// La 'e' dice la linea de la excepcion
                    }                    
                }
            }
            return entrada;
        }
    }
}