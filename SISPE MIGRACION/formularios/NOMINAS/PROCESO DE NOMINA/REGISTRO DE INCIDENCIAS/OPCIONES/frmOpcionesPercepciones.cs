using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.PROCESO_DE_NOMINA.REGISTRO_DE_INCIDENCIAS.OPCIONES
{
    public partial class frmOpcionesPercepciones : Form
    {
        private List<string> arreglo;
        private string jubilado;
        private string total;
        internal metodo enviar;

        public frmOpcionesPercepciones(List<string> arreglo,string jubilado)
        {
            InitializeComponent();
            this.arreglo = arreglo;
            this.jubilado = jubilado;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void frmOpcionesPercepciones_Load(object sender, EventArgs e)
        {
            string jpp = this.jubilado.Substring(0,3);
            string numjpp = this.jubilado.Substring(3);

            string query = $"select * from nominas_catalogos.nominew where jpp = '{jpp}' and numjpp = {numjpp} and clave <= 69  and tipo_nomina = 'N' order by clave";
            List<Dictionary<string, object>> resultado = globales.consulta(query);

            

            resultado.ForEach(o => dtggrid.Rows.Add(true,o["clave"], o["descri"],o["monto"]));
            this.total = globales.convertMoneda(resultado.Sum(o => globales.convertDouble(Convert.ToString(o["monto"]))));
            txttotal.Text =  "Total: "+total;
            
            
        }

        private void select_CheckedChanged(object sender, EventArgs e)
        {
            double total = 0;
            foreach (DataGridViewRow dic in this.dtggrid.Rows) {
                dic.Cells[0].Value = select.Checked;
                if (select.Checked) {
                    total += globales.convertDouble(dic.Cells[3].Value.ToString());
                }
            }

            this.total = globales.convertMoneda(total);


            txttotal.Text = "Total: " + this.total;

        }

        private void dtggrid_EditModeChanged(object sender, EventArgs e)
        {

        }

        private void dtggrid_CurrentCellChanged(object sender, EventArgs e)
        {
           
        }

        private void dtggrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            

        }

        private void dtggrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
       
        }

        private void dtggrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            double suma = 0;

            if (e.ColumnIndex == 0)
            {
                foreach (DataGridViewRow item in dtggrid.Rows)
                {
                    bool chequeado = Convert.ToBoolean(item.Cells[0].Value);
                    if (chequeado)
                    {
                        suma += Convert.ToDouble(item.Cells[3].Value);
                    }
                }

                this.total = globales.convertMoneda(suma);
                txttotal.Text = "Total: " + this.total;
            }
        }

        private void dtggrid_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {

        }

        private void dtggrid_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            
           
        }

        private void dtggrid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            ActiveControl = label45;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.enviar(this.total);
            this.Owner.Close();
        }
    }
}
