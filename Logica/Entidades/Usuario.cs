using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuario
    {
        public int DNI { get; set; }
        public string? NombreYApellido { get; set; }
        public decimal Saldo { get; set; }
        public List<Movimiento>? Movimientos { get; set; }

    }
}
