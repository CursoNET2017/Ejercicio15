using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio15.Servicios
{
    public interface IEntradasService
    {
        Entrada Create(Entrada entrada);
        Entrada Buscar(long id);
        IQueryable<Entrada> GetEntradas();
    }
}
