using Ejercicio15.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ejercicio15.Repository
{
    public class EntradasRepository : IEntradasRepository
    {
        public Entrada Create(Entrada entrada)
        {
            return EntradasService.applicationDbContext.Entradas.Add(entrada);
            //throw new NotImplementedException();
        }
    }
}