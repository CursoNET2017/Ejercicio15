using Ejercicio15.Models;
using Ejercicio15.Servicios;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Ejercicio15.Repository
{
    public class EntradasRepository : IEntradasRepository
    {
        public Entrada Get(long id)
        {
            return ApplicationDbContext.applicationDbContext.Entradas.Find(id);
        }

        public Entrada Create(Entrada entrada)
        {            
            return ApplicationDbContext.applicationDbContext.Entradas.Add(entrada);
        }

        public IQueryable<Entrada> GetEntradas()
        {
            IList<Entrada> lista = new List<Entrada>(ApplicationDbContext.applicationDbContext.Entradas);
            return lista.AsQueryable();//Si devuelves el IQueryable casca en el lado del cliente.
        }

        public Entrada Delete(long id)
        {
            Entrada entrada = ApplicationDbContext.applicationDbContext.Entradas.Find(id);
            if (entrada == null)
            {
                throw new NoEncontradoException("No he encontrado la entidad");
            }
            ApplicationDbContext.applicationDbContext.Entradas.Remove(entrada);
            return entrada;
        }

        public void Put(Entrada entrada)
        {
            if (ApplicationDbContext.applicationDbContext.Entradas.Count(e => e.Id == entrada.Id) == 0)// El private bool EntradaExists(long id) del anterior controlador
            {
                throw new NoEncontradoException("No he encontrado la entidad");
            }
            ApplicationDbContext.applicationDbContext.Entry(entrada).State = EntityState.Modified;
        }
    }
}