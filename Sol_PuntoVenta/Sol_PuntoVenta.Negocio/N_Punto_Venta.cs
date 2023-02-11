using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Sol_PuntoVenta.Entidades;
using Sol_PuntoVenta.Datos;

namespace Sol_PuntoVenta.Negocio
{
    public class N_Punto_Venta
    {
         public static DataTable Listado_pv(string cTexto)
         {
            D_Punto_Venta Datos = new D_Punto_Venta();
            return Datos.Listado_pv(cTexto);
         }

        public static string Guardar_pv(int nOpcion, E_Punto_Venta oPropiedad)
        {
            D_Punto_Venta Datos = new D_Punto_Venta();
            return Datos.Guardar_pv(nOpcion, oPropiedad);   
        }

        public static string Eliminar_pv(int nCodigo)
        {
            D_Punto_Venta Datos = new D_Punto_Venta();
            return Datos.Eliminar_pv(nCodigo);
        }
    }
}
