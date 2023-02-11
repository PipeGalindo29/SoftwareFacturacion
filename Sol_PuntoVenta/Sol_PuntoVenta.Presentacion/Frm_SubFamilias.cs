using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sol_PuntoVenta.Entidades;
using Sol_PuntoVenta.Negocio;

namespace Sol_PuntoVenta.Presentacion
{
    public partial class Frm_SubFamilias : Form
    {
        public Frm_SubFamilias()
        {
            InitializeComponent();
        }

        #region "Mis variables"
        int nCodigo = 0;
        int nCodigo_fa = 0;
        int Estadoguarda = 0;
        
        #endregion

        #region "Mis metodos"

        private void Formato_sf()
        {
            Dgv_Listado.Columns[0].Width = 100;
            Dgv_Listado.Columns[0].HeaderText = "CODIGO_SF";
            Dgv_Listado.Columns[1].Width = 250;
            Dgv_Listado.Columns[1].HeaderText = "SUBFAMILIA";
            Dgv_Listado.Columns[2].Width = 250;
            Dgv_Listado.Columns[2].HeaderText = "FAMILIA";
            Dgv_Listado.Columns[3].Visible = false;
        }

        private void Formato_fa()
        {
            Dgv_1.Columns[0].Visible = false;
            Dgv_1.Columns[1].Width = 330;
            Dgv_1.Columns[1].HeaderText = "FAMILIA";
        }

        private void Listado_sf(string cTexto)
        {
            try
            {
                Dgv_Listado.DataSource = N_SubFamilias.Listado_sf(cTexto);
                this.Formato_sf();
                Lbl_Totalregistros.Text = "Total Registros: " + Convert.ToString(Dgv_Listado.Rows.Count);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Listado_fa(string cTexto)
        {
            try
            {
                Dgv_1.DataSource = N_SubFamilias.Listado_fa(cTexto);
                this.Formato_fa();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Limpia_Texto()
        {
            Txt_Descripcion.Text = "";
            Txt_familia.Text = "";
        }

        private void Estado_BotonesPrincipales(bool LEstado)
        {
            Btn_Nuevo.Enabled = LEstado;
            Btn_Actualizar.Enabled = LEstado;
            Btn_Eliminar.Enabled = LEstado;
            Btn_Reporte.Enabled = LEstado;
            Btn_Salir.Enabled = LEstado;
        
        }

        private void Estado_Texto(bool LEstado)
        {
            Txt_Descripcion.ReadOnly = !LEstado;
        }

        private void Estado_BotonesProcesos(bool LEstado)
        {
            Btn_Cancelar.Visible = LEstado;
            Btn_Guardar.Visible = LEstado;
            Btn_Retomar.Visible = !LEstado;
            Btn_Lupa1.Visible = LEstado;
        } 

        private void Selecciona_item()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Dgv_Listado.CurrentRow.Cells["codigo_sf"].Value)))
            {
                MessageBox.Show("Selecciona un registro",
                                "Aviso del sistema",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
            else
            {
                this.nCodigo = Convert.ToInt32(Dgv_Listado.CurrentRow.Cells["codigo_sf"].Value);
                Txt_Descripcion.Text = Convert.ToString(Dgv_Listado.CurrentRow.Cells["descripcion_sf"].Value);
                Txt_familia.Text = Convert.ToString(Dgv_Listado.CurrentRow.Cells["descripcion_fa"].Value);
                this.nCodigo_fa = Convert.ToInt32(Dgv_Listado.CurrentRow.Cells["codigo_fa"].Value);
            }
        }

        private void Selecciona_item_fa()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Dgv_1.CurrentRow.Cells["codigo_fa"].Value)))
            {
                MessageBox.Show("Selecciona un registro",
                                "Aviso del sistema",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
            else
            {
                Txt_familia.Text = Convert.ToString(Dgv_1.CurrentRow.Cells["descripcion_fa"].Value);
                this.nCodigo_fa = Convert.ToInt32(Dgv_1.CurrentRow.Cells["codigo_fa"].Value);
            }
        }
        #endregion


        // ------------------------------------------------------------------------

        private void Frm_SubFamilias_Load(object sender, EventArgs e)
        {
            this.Listado_sf("%");
            this.Listado_fa("%");  
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Pnl_Titulo_Formulario_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Nuevo_Click(object sender, EventArgs e)
        {
            this.Estadoguarda = 1; //Nuevo Registro
            this.Estado_BotonesPrincipales(false);
            this.Estado_BotonesProcesos(true);
            this.Limpia_Texto();
            this.Estado_Texto(true);
            Tbc_principal.SelectedIndex = 1;
            Btn_Lupa1.Focus();

        }

        private void Btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Limpia_Texto();
            this.Estado_Texto(false);
            this.Estado_BotonesPrincipales(true);
            this.Estado_BotonesProcesos(false);
            Tbc_principal.SelectedIndex = 0;
        }

        private void Btn_Retomar_Click(object sender, EventArgs e)
        {
            Tbc_principal.SelectedIndex = 0;
        }

        private void Btn_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Txt_Descripcion.Text == String.Empty || Txt_familia.Text == string.Empty)
                {
                    MessageBox.Show("Falta ingresar datos requeridos (*)", 
                                    "Aviso del Sistema",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                }
                else
                {

                    string Rpta = "";
                    E_SubFamilias oPropiedad = new E_SubFamilias();
                    oPropiedad.Codigo_sf = this.nCodigo;
                    //oPropiedad.Descripcion_sf = Txt_Descripcion.Text.Trim();
                    oPropiedad.Codigo_fa = this.nCodigo_fa;
                    Rpta = N_SubFamilias.Guardar_sf(this.Estadoguarda, oPropiedad);
                    if (Rpta.Equals("OK"))
                    {
                        MessageBox.Show("Los datos han sido guardados correctamente", 
                                        "Aviso del sistema", 
                                        MessageBoxButtons.OK, 
                                        MessageBoxIcon.Information);
                        this.Limpia_Texto();
                        this.Estado_Texto(false);
                        this.Estado_BotonesPrincipales(true);
                        this.Estado_BotonesProcesos(false);
                        this.Estadoguarda = 0;
                        this.nCodigo = 0;
                        this.nCodigo_fa = 0;
                        this.Listado_sf("%");
                        Tbc_principal.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show(Rpta, "Aviso del sistema", 
                                        MessageBoxButtons.OK, 
                                        MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Actualizar_Click(object sender, EventArgs e)
        {
            if (Dgv_Listado.Rows.Count>0)
            {
                this.Estadoguarda = 2; //Actualiza Registro
                this.Estado_BotonesPrincipales(false);
                this.Estado_BotonesProcesos(true);
                this.Estado_Texto(true);
                this.Limpia_Texto();
                this.Selecciona_item();
                Tbc_principal.SelectedIndex = 1;
                Btn_Lupa1.Focus();
            }
        }

        private void Dgv_Listado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.Estadoguarda == 0)
            {
                this.Selecciona_item();
                Tbc_principal.SelectedIndex = 1;
            }
        }

        private void Btn_Eliminar_Click(object sender, EventArgs e)
        {
            if (Dgv_Listado.Rows.Count > 0)
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("¿Estas seguro de eliminar el registro seleccionado?", 
                                         "Aviso del sistema", 
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

                if (Opcion == DialogResult.Yes)
                {

                string Rpta = "";
                this.Selecciona_item();
                Rpta = N_SubFamilias.Eliminar_sf(this.nCodigo);
                if (Rpta.Equals("OK"))
                {
                    this.Listado_sf("%");
                    MessageBox.Show("El registro ha sido eliminado", 
                                    "Aviso del sistema", 
                                    MessageBoxButtons.OK, 
                                    MessageBoxIcon.Exclamation);
                    this.nCodigo = 0;
                }
                else
                {
                    MessageBox.Show(Rpta,"Aviso del sistema",
                                    MessageBoxButtons.OK, 
                                    MessageBoxIcon.Exclamation);
                }
                this.Limpia_Texto();

                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Listado_sf(Txt_Buscar.Text.Trim());
        }

        private void Btn_Reporte_Click(object sender, EventArgs e)
        {
            /*if (Dgv_Listado.Rows.Count>0)
            {
                Reportes.Frm_Rpt_Familias oRpt_fa = new Reportes.Frm_Rpt_Familias();
                oRpt_fa.Txt_p1.Text = Txt_Buscar.Text.Trim();
                oRpt_fa.ShowDialog();
            }*/
        }

        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Dgv_1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Selecciona_item_fa();
            Panel_Listado1.Visible = false;
            Txt_Descripcion.Focus();
        }

        private void Btn_Retornar1_Click(object sender, EventArgs e)
        {
            Panel_Listado1.Visible = false;
        }

        private void Btn_Buscar1_Click(object sender, EventArgs e)
        {
            this.Listado_fa(Txt_Buscar1.Text.Trim());
        }

        private void Btn_Lupa1_Click(object sender, EventArgs e)
        {
            Panel_Listado1.Location = Btn_Lupa1.Location;
            Panel_Listado1.Visible=true;
            Txt_Buscar1.Focus();
        }
    }
}
