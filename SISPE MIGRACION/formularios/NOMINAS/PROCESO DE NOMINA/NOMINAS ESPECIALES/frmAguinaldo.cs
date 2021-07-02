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
    public partial class frmAguinaldo : Form
    {
        public frmAguinaldo()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string jppCombo = string.Empty;

            if (comboBox1.SelectedIndex==0)
            {
                jppCombo = "JUB";
                  }
            else
            {
                jppCombo = "PDO";
            }
            string query = $"CREATE TEMP TABLE BASE  AS(SELECT a1.jpp, a1.num, a2.monto, a1.tiporel, a1.fching FROM nominas_catalogos.maestro a1 LEFT JOIN nominas_catalogos.nominew a2 ON a1.jpp = a2.jpp and a1.num = a2.numjpp where a1.jpp = '{jppCombo}' and a2.clave = 1 and a2.tipopago = 'N'order by a1.tiporel);" +
              " CREATE TEMP TABLE tabladias  AS(SELECT * , '2020-06-29' - fching as dias FROM BASE ORDER BY num);" +
              " update tabladias set monto = (monto / 30) * 35 where tiporel<> 'BASE' AND DIAS >= 180;" +  // actualiza confianza que si cumplen "+
               "update tabladias set monto = (monto / 30) * 35 / 180 * dias  where tiporel<> 'BASE' AND DIAS <= 179;" +  //actualiza confianza que No cumplen 
               "update tabladias set monto = (monto / 30) * 39  where tiporel = 'BASE' AND DIAS >= 180; " +  // BASE que si cUMPLEN 
               "update tabladias set monto = (monto / 30) * 39 / 180 * dias  where tiporel = 'BASE' AND DIAS <= 179;" +   //base que no cumplen
               "select * from tabladias";
            List<Dictionary<string, object>> resultado = globales.consulta(query);

            foreach(var item in resultado)
            {
                string jpp = Convert.ToString(item["jpp"]);
                string num = Convert.ToString(item["num"]);
                string monto = Convert.ToString(item["monto"]);


                string inserta = $"INSERT INTO nominas_catalogos.nominew (jpp,numjpp,clave,secuen,descri,monto,pagon,pagot,tipopago,tipo_nomina) values ('{jpp}',{num},59,1,'AGUINALDO',{monto},0,0,'N','AG');";
                try
                {
                    globales.consulta(inserta, true);
                }
                catch
                {

                }
            }
        }
    }
}
