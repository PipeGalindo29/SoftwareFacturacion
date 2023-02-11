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
    public partial class Frm_Rpt_Unidades_Medidas : Form
    {
        public Frm_Rpt_Unidades_Medidas()
        {
            InitializeComponent();
        }

        private void Frm_Rpt_Unidades_Medidas_Load(object sender, EventArgs e)
        {
            this.uSP_Listado_umTableAdapter.Fill(this.dataSet_DatosMaestros.USP_Listado_um,
                                                cTexto: Txt_p1.Text);
            this.reportViewer1.RefreshReport();
        }
    }
}
