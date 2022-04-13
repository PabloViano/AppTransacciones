using Entidades;
namespace Logica
{
    public class PrincipalLogica
    {
        List<Movimiento> Movimientos = new List<Movimiento>();
        List<Usuario> Usuarios = new List<Usuario>();

        public string CrearMovimientoNuevo(int dniEmisor, int dniReceptor, string descripcion, decimal monto)
        {
            if (ValidarUsuariosYSaldoEmisor(dniEmisor, dniReceptor, monto) == "1")
            {
                Movimiento movimientoE = new Movimiento(Movimientos.Count() + 1,descripcion, monto, dniEmisor, dniReceptor);
                Movimientos.Add(movimientoE);
                Movimiento movimientoR = new Movimiento(Movimientos.Count() + 1, descripcion, -monto, dniEmisor, dniReceptor);
                Movimientos.Add(movimientoR);
                AsignarMovimiento(dniEmisor, -monto, movimientoE);
                AsignarMovimiento(dniReceptor, monto, movimientoR);
                return "201";
            }
            if (ValidarUsuariosYSaldoEmisor(dniEmisor, dniReceptor, monto) == "2")
            {
                return "404 (El usuario emisor no cuenta con saldo suficiente)";
            }
            else
            {
                return "404 (El usuario no se encuentra en nuestra base de datos";
            }
        }
        public string CancelarMovimiento (int id)
        {
            Movimiento? movimiento = Movimientos.Find(x => x.Id == id);
            if (movimiento != null)
            {
                Usuario? Emisor = Usuarios.Find( x => x.DNI == movimiento.DNIEmisor);
                Usuario? Receptor = Usuarios.Find( x => x.DNI == movimiento.DNIReceptor);
                if (Emisor != null && Receptor != null)
                {
                    Movimientos.Remove(movimiento);
                    Movimiento movimientoE = new Movimiento(Movimientos.Count + 1, $"Cancelacion {movimiento.Descripcion}", movimiento.Monto, Receptor.DNI, Emisor.DNI);
                    Movimientos.Add(movimientoE);
                    AsignarMovimiento(Emisor.DNI, movimiento.Monto, movimientoE);
                    Movimiento movimientoR = new Movimiento(Movimientos.Count + 1, $"Cancelacion {movimiento.Descripcion}", -(movimiento.Monto), Receptor.DNI, Emisor.DNI);
                    Movimientos.Add(movimientoR);
                    AsignarMovimiento(Receptor.DNI, -(movimiento.Monto), movimientoR);
                    return "Cancelado con exito";
                }
                return "Error (El movimiento no esta asignado a ningun Usuario)";
            }
            else
            {
                return "Error (El movimiento buscado no existe)";
            }
        }
        public string ValidarUsuariosYSaldoEmisor(int dniEmisor, int dniReceptor, decimal monto)
        {
            if (Usuarios.Exists(x => x.DNI == dniEmisor) == true && Usuarios.Exists(x => x.DNI == dniReceptor) == true)
            {
                if (Usuarios.Find(x => x.DNI == dniEmisor).Saldo >= monto)
                {
                    return "1";
                }
                return "2";
            }
            return "0";
        }
        public bool AsignarMovimiento(int dni, decimal monto, Movimiento movimiento)
        {
            Usuario? usuario = Usuarios.Find(x => x.DNI == dni);
            if (usuario != null)
            {
                usuario.Saldo += monto;
                if (usuario.Movimientos != null)
                {
                    usuario.Movimientos.Add(movimiento);
                }
                else
                {
                    List<Movimiento> movimientos = new List<Movimiento>();
                    movimientos.Add(movimiento);
                    usuario.Movimientos = movimientos;
                }
                return true;
            }
            return false;
        }
        public List<Movimiento> RetornarListaMovimientos (int dni)
        {
            Usuario? user = Usuarios.Find(x => x.DNI == dni);
            if (user != null)
            {
                return user.Movimientos.OrderByDescending(x => x.Fecha).ToList();
            }
            throw new Exception ("404");
        }
    }
}