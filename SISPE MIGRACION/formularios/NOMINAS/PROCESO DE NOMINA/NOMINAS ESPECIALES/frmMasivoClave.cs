using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.PROCESO_DE_NOMINA.NOMINAS_ESPECIALES
{
    public partial class frmMasivoClave : Form
    {
        public frmMasivoClave()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtClave.Text == "") return;
            if (comboJpp.Text == "") return;

            string jpp = string.Empty;
            if (comboJpp.Text == "TODOS") jpp = "'JUB,'PDO','PTA','PEA'";
            if (comboJpp.Text == "PDO") jpp = "'PDO'";
            if (comboJpp.Text == "PTA") jpp = "'PTA'";
            if (comboJpp.Text == "PEA") jpp = "'PEA'";
            if (comboJpp.Text == "JUB") jpp = "'JUB'";
            string query = string.Empty;

            string tipo_nomina = string.Empty;

            if (comboBox1.SelectedIndex == 0) tipo_nomina = "AG";
            if (comboBox1.SelectedIndex == 1) tipo_nomina = "CA";
            if (comboBox1.SelectedIndex == 2) tipo_nomina = "DM";
            if (comboBox1.SelectedIndex == 3) tipo_nomina = "UT";
            if (comboBox1.SelectedIndex == 4) tipo_nomina = "CAN2";//PTA,PDO






            if (checkBox1.Checked == true)
            {
                DialogResult dialo = globales.MessageBoxQuestion("DESEA ELIMINAR LA CLAVE?", "ELIMINAR", globales.menuPrincipal);
                if (dialo == DialogResult.No) return;
                if (checkBox4.Checked)
                {
                    query = $"delete from nominas_catalogos.nominew where clave='{txtClave.Text}' and jpp in '{jpp}' and tipo_nomina={tipo_nomina} ";

                }
                else
                {
                    if (checkBox4.Checked)
                    query = $"delete from nominas_catalogos.nominew where clave='{txtClave.Text}' and jpp in '{jpp}' and tipo_nomina='N' ";

                }

            }
            else
            {
                if (checkBox2.Checked)
                {
                    string trae = $"select descri from nominas_catalogos.perded where clave={txtClave.Text}";
                    List<Dictionary<string, object>> ret = globales.consulta(trae);
                    string desci = Convert.ToString(ret[0]["descri"]);

                    DialogResult po = globales.MessageBoxQuestion($"¿Desea insertar la clave {txtClave.Text} a los {jpp} directo a la nómina", globales.menuPrincipal);
                    if (po == DialogResult.No) return;

                    if (checkBox3.Checked == true)
                    {
                        query = $"CREATE TEMP TABLE  MUEVE AS(SELECT * FROM nominas_catalogos.nominew where clave = 1 and jpp in ({jpp}) AND tipopago = 'N');" +
                       $"UPDATE MUEVE SET CLAVE = {txtClave.Text}, MONTO = {maskedMonto.Text}, DESCRI = '{desci}', TIPO_NOMINA = '{tipo_nomina}';" +
                        $"INSERT INTO nominas_catalogos.nominew(SELECT * FROM MUEVE);";
                    }
                    else
                    {
                        query = $"CREATE TEMP TABLE  MUEVE AS(SELECT * FROM nominas_catalogos.nominew where clave = 1 and jpp in ({jpp}) AND tipopago = 'N');" +
             $"UPDATE MUEVE SET CLAVE = {txtClave.Text}, MONTO = {maskedMonto.Text}, DESCRI = '{desci}', TIPO_NOMINA = 'N';" +
              $"INSERT INTO nominas_catalogos.nominew(SELECT * FROM MUEVE);";
                    }

                }
               

            }
            globales.consulta(query);
            DialogResult dialoN = globales.MessageBoxSuccess("SE TERMINO EL PROCESO CORRECTAMENTE", "TERMINADO", globales.menuPrincipal);
        }








        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox3.Visible = true;
                checkBox4.Visible = true;

            }
            else
            {
                if (checkBox2.Checked)
                {
                    checkBox3.Visible = true;
                    checkBox4.Visible = true;

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked )
            {
                groupBox2.Visible = true;
                checkBox3.Visible = true;
                checkBox4.Visible = true;

            }
            else
            {
                if (checkBox1.Checked )
                {
                    groupBox2.Visible = true;
                    checkBox3.Visible = true;
                    checkBox4.Visible = true;

                }
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                comboBox1.Visible = true;
            }
            else
            {
                comboBox1.Visible = false;
            }
        }
    }
}





