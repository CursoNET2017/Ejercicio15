using Ejercicio15.Models;
using Ejercicio15.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ejercicio15.Repository
{
    public class EntradasRepository : IEntradasRepository
    {
        public Entrada Buscar(long id)
        {
            return ApplicationDbContext.applicationDbContext.Entradas.Find(id);
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
                        ApplicationDbContext.applicationDbContext.Entradas.Add(entrada);

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
            return entrada;
            //return ApplicationDbContext.applicationDbContext.Entradas.Add(entrada);
            //throw new NotImplementedException();
        }

        // GET: api/Entradas
        public IQueryable<Entrada> GetEntradas()
        {
            IList<Entrada> lista = new List<Entrada>(ApplicationDbContext.applicationDbContext.Entradas);
            return lista.AsQueryable();//Si devuelves el IQueryable casca en el lado del cliente.
        }
    }
}