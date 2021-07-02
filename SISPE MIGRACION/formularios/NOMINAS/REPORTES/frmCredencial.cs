using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.REPORTES
{
    public partial class frmCredencial : Form
    {
        public frmCredencial()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string jpp = string.Empty;
            string tipo = string.Empty;

            if (radioButton1.Checked == true)
            {
                jpp = "JUP";
                tipo = "Jubilado";
            }
            if (radioButton2.Checked == true) 
            {
                jpp = "PDP";
                tipo = "Pensionado";

            }
            if (radioButton3.Checked == true)
            {
                jpp = "PTP";
                tipo = "Pensionista";

            }
            if (radioButton4.Checked == true)
            {
                jpp = "PEA";
                tipo = "Pensión alimenticia";

            }

            string numero=txtnum.Text;
            if (string.IsNullOrWhiteSpace(numero))
            {
                DialogResult dialogo = globales.MessageBoxExclamation("INGRESE LOS NUMEROS DE LOS JUBILADOS", "CAMPO VACÍO", globales.menuPrincipal); return;
            }
            string textop = "SUMA DE PROTECCIÓN : $    0.00 (PESOS 00/100 M.N) Este monto puede variar conforme a lo que al respecto acuerde el Consejo Directivo de Pensiones.";
            string quer = $"SELECT rfc,jpp,num,fching,telefono,nombre,domicilio FROM nominas_catalogos.maestro where num in ({numero}) and jpp='{jpp}' ;";
            List<Dictionary<string, object>> resultado = globales.consulta(quer);

            object[] aux2 = new object[resultado.Count];
            int contador = 0;
            string nombre = string.Empty;
            string domicilio = string.Empty;
            string rfc = string.Empty;
            string num = string.Empty;
            string fec = string.Empty;
            string tel = string.Empty;
            string fecha = string.Empty;
            string factualcred = string.Empty;
            foreach (Dictionary<string, object> item in resultado)
            {
                try
                {
                    
                    nombre = Convert.ToString(item["nombre"]);
                    domicilio = Convert.ToString(item["domicilio"]);
                    rfc = Convert.ToString(item["rfc"]);
                    num = Convert.ToString(item["jpp"] + Convert.ToString(item["num"]));
                    fec = Convert.ToString(item["fching"]);
                    string query1 = $"select * from datos.fechaletra('{string.Format("{0:d}",Convert.ToDateTime(fec))}')";
                    List<Dictionary<string, object>> f1 = globales.consulta(query1);
                    fecha = Convert.ToString(f1[0]["fechaletra"]).Replace("Oaxaca de Juárez,Oax.,a ", "");
                    tel = Convert.ToString(item["telefono"]);
                    DateTime feactual = DateTime.Now;
                    factualcred = string.Format("{0:d}", feactual).Replace("12:00:00 a. m.", "");

                }
                catch
                {

                }
                object[] tt1 = { nombre,domicilio,rfc,num,fecha,tel, factualcred , tipo };
                aux2[contador] = tt1;
                contador++;
            }
            object[] parametros = { "textop" };
            object[] valor = { textop };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("ReportCredenciales", "crede", aux2, "", false, enviarParametros);
            this.Cursor = Cursors.Default;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }

}
    
