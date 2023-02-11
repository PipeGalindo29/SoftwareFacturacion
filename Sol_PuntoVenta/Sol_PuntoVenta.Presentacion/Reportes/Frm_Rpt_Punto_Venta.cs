using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sol_PuntoVenta.Presentacion.Reportes
{
    public partial class Frm_Rpt_Punto_Venta : Form
    {
        public Frm_Rpt_Punto_Venta()
        {
            InitializeComponent();
        }

        private void Frm_Rpt_Punto_Venta_Load(object sender, EventArgs e)
        {
            this.uSP_Listado_pvTableAdapter.Fill(this.dataSet_DatosMaestros.USP_Listado_pv,
                                                 cTexto: Txt_p1.Text);
            this.reportViewer1.RefreshReport();
        }
    }
}
