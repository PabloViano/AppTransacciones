using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Movimiento
    {
        public int Id { get; set; }
        public int DNIEmisor { get; set; }
        public int DNIReceptor { get; set; }
        public DateTime Fecha { get; set; }
        public string? Descripcion { get; set; }
        public decimal Monto { get; set; }
        public Movimiento() { }
        public Movimiento(int id, string desc, decimal monto, int dniEmisor, int dniReceptor)
        {
            this.Id = id;
            this.Fecha = DateTime.Now.Date;
            this.Descripcion = desc;
            this.Monto = monto;
        }
    }
}
