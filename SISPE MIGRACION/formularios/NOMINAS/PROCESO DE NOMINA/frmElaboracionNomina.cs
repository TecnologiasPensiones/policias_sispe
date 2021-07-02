using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.PROCESO_DE_NOMINA
{
    public partial class frmElaboracionNomina : Form
    {
        string mes = string.Empty;
        string archivo = string.Empty;
        public frmElaboracionNomina()
        {
            InitializeComponent();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
        //por 1
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas generar la nomina?", "Aviso", globales.menuPrincipal);
            if (dialogo == DialogResult.No)
                return;
            string jpp = string.Empty;
            string jppdescripcion = string.Empty;
             mes = seleccionarMes();

          

            if (jubilados.Checked)
            {
                jpp = "JUP";
                jppdescripcion = "JUBILADOS  POLICIAS";
            }
            else if (pensionado.Checked)
            {
                jpp = "PDP";
                jppdescripcion = "PENSIONADOS POLICIAS";
            }
            else if (pensionistas.Checked)
            {
                jpp = "PTP";
                jppdescripcion = "PENSIONISTAS POLICIAS";
            }
            else if (alimenticia.Checked)
            {
                jpp = "PEA";
                jppdescripcion = "PENSION ALIMENTICIA";
            }
            string query = string.Empty;

            string tipo_nomina = string.Empty;

            if (radioButton1.Checked) tipo_nomina = "AG";
            if (radioButton2.Checked) tipo_nomina = "CA";
            if (radioButton3.Checked) tipo_nomina = "DM";
            if (radioButton4.Checked) tipo_nomina = "UT";
            if (radioButton5.Checked) tipo_nomina = "CAN2"; //PDOPTA

            if (groupBox2.Visible == false) tipo_nomina = "N";

            if (chknomina.Checked)
            {
                query = "SELECT	CONCAT(a1.jpp,a1.num) as proyecto,	a1.nombre,	a1.curp,	a1.rfc,	a1.imss,	a1.categ,	a2.clave,	a2.descri,	a2.monto,	a2.pagon, " +
             " a2.pagot,	a2.leyen FROM	nominas_catalogos.maestro a1 JOIN nominas_catalogos.nominew a2 ON a1.num = a2.numjpp and a1.jpp = a2.jpp WHERE " +
             $" a1.superviven = 'S' AND a1.jpp = a2.jpp AND a1.jpp = '{jpp}' and a2.tipo_nomina='{tipo_nomina}' ORDER BY 	a1.jpp,a1.num,a2.clave ";
            }
            else
            {
                query = "SELECT	CONCAT(a1.jpp,a1.num) as proyecto,	a1.nombre,	a1.curp,	a1.rfc,	a1.imss,	a1.categ,	a2.clave,	a2.descri,	a2.monto,	a2.pagon, " +
             " a2.pagot,	a2.leyen FROM	nominas_catalogos.maestro a1 JOIN nominas_catalogos.nominew a2 ON a1.num = a2.numjpp and a1.jpp = a2.jpp WHERE " +
             $" a1.superviven = 'S' AND a1.jpp = a2.jpp AND a1.jpp = '{jpp}' and a2.tipo_nomina='{tipo_nomina}' ORDER BY 	a1.jpp,a1.num,a2.clave ";
            }

          

            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count<=0)
            {
                DialogResult dialog = globales.MessageBoxExclamation("NO SE ENCUENTRA CARGADA UNA NÓMINA ESPECIAL", "UPS", globales.menuPrincipal);
                return;
            }

            string que = $"CREATE TEMP TABLE BASE AS  (SELECT CONCAT (a1.jpp, a1.num) AS proyecto,a1.nombre,	a1.curp,	a1.rfc,	a1.imss,	a1.categ,	a2.clave,	a2.descri,	a2.monto,	a2.pagon,	 a2.pagot,	a2.leyen FROM	nominas_catalogos.maestro a1 JOIN nominas_catalogos.nominew a2 ON a1.num = a2.numjpp AND a1.jpp = a2.jpp WHERE  a1.superviven = 'S' AND a1.jpp = a2.jpp AND a2.tipo_nomina='{tipo_nomina}' ORDER BY a1.jpp, a1.num,a2.clave);"
                + "CREATE temp table per as SELECT proyecto , sum(monto) FROM BASE WHERE CLAVE >= 59  GROUP BY proyecto;" +
              " CREATE temp table ded as SELECT proyecto , sum(monto) FROM BASE WHERE CLAVE <= 60  GROUP BY proyecto;"  +
            "  CREATE TEMP TABLE liquidos as select a1.proyecto, (a2.sum - a1.sum) as liquido from per a1 JOIN ded a2 on a1.proyecto = a2.proyecto order by a1.proyecto;"+
          "  select* from liquidos where liquido <= 1500; ";

            List<Dictionary<string, object>> liquidos = globales.consulta(que);

            if (liquidos.Count>=1)

            {
                DialogResult dialo = globales.MessageBoxExclamation("HAY LIQUIDOS MENORES AL LIMITE, GENERANDO REPORTE", "VERIFICAR", globales.menuPrincipal);
                object[] aux1 = new object[liquidos.Count];
                int contador1 = 0;
                foreach (var teim in liquidos)
                {
                    string proyecto = Convert.ToString(teim["proyecto"]);
                    string liquido = Convert.ToString(teim["liquido"]);
                    object[] tt1 = { proyecto,liquido  };
                    aux1[contador1] = tt1;
                    contador1++;

                }

                object[] parametros1 = { "vacio" };
                object[] valor1 = { ""};
                object[][] enviarParametros1 = new object[2][];

                enviarParametros1[0] = parametros1;
                enviarParametros1[1] = valor1;

                globales.reportes("valida_nomina", "va_nom", aux1, "", false, enviarParametros1);
                this.Cursor = Cursors.Default;

              //  return;
            }
            string quer = string.Empty;
        if (chknomina.Checked)
                
            {
                quer = $"SELECT DISTINCT(rfc),a1.num FROM 	nominas_catalogos.maestro a1 JOIN nominas_catalogos.nominew a2 ON a1.num = a2.numjpp AND a1.jpp = a2.jpp WHERE 	a1.superviven = 'S' AND a1.jpp = a2.jpp AND a1.jpp = '{jpp}' and a2.tipo_nomina='{tipo_nomina}'  ORDER BY a1.num";

            }
            else
            {
                quer = $"SELECT DISTINCT(rfc),a1.num FROM 	nominas_catalogos.maestro a1 JOIN nominas_catalogos.nominew a2 ON a1.num = a2.numjpp AND a1.jpp = a2.jpp WHERE 	a1.superviven = 'S' AND a1.jpp = a2.jpp AND a1.jpp = '{jpp}' and a2.tipo_nomina='{tipo_nomina}' ORDER BY a1.num";

            }

            List<Dictionary<string, object>>ado = globales.consulta(quer);
            int cantidad = ado.Count();
            query = "select  clave,descri from nominas_catalogos.perded order by clave";
            List<Dictionary<string, object>> perded = globales.consulta(query);

            resultado.ForEach(o =>
            {
                o["descri"] = perded.Where(p => Convert.ToString(o["clave"]) == Convert.ToString(p["clave"])).First()["descri"];
                //  o["descri"] += " (RETROACTIVO)";
            });

            object[] aux2 = new object[resultado.Count];
            int contadorPercepcion = 0;
            int contadorDeduccion = 0;



            string archivoPrimero = string.Empty;


            archivoPrimero = resultado[0]["proyecto"].ToString();


            foreach (var item in resultado)
            {
                string proyecto = string.Empty;
                string nombre = string.Empty;
                string curp = string.Empty;
                string rfc = string.Empty;
                string imss = string.Empty;
                string categ = string.Empty;
                string clave = string.Empty;
                string descri = string.Empty;
                double monto = 0;
                //  fecha = fec2.ToString();
                string archivo = string.Empty;
                string pago4 = string.Empty;
                string pagot = string.Empty;

                string descripcionleyenda = string.IsNullOrWhiteSpace(Convert.ToString(item["leyen"])) ? "" : "(" + Convert.ToString(item["leyen"] + ")");

                proyecto = Convert.ToString(item["proyecto"]);
                nombre = Convert.ToString(item["nombre"]);
                curp = Convert.ToString(item["curp"]);
              
                rfc = Convert.ToString(item["rfc"]);
             
                imss = Convert.ToString(item["imss"]);
                categ = Convert.ToString(item["categ"]);
                clave = Convert.ToString(item["clave"]);
                descri = Convert.ToString(item["descri"]) + $"{descripcionleyenda}";
                monto = globales.convertDouble(Convert.ToString(item["monto"]));
                archivo = Convert.ToString(item["proyecto"]);
                pago4 = Convert.ToString(item["pagon"]);
                pagot = Convert.ToString(item["pagot"]);




                object[] tt1 = { proyecto, rfc, nombre, categ, "", "", "", "", "", "", "" };

                if (archivoPrimero != archivo)
                {
                    archivoPrimero = archivo;
                    int tope = contadorDeduccion <= contadorPercepcion ? contadorPercepcion : contadorDeduccion;
                    contadorDeduccion = tope;
                    contadorPercepcion = tope;
                }

                if (Convert.ToInt32(clave) < 60)
                {
                    if (aux2[contadorPercepcion] == null)
                    {
                        tt1[4] = clave;
                        tt1[5] = descri;
                        tt1[6] = monto;
                        aux2[contadorPercepcion] = tt1;
                    }
                    else
                    {
                        object[] tmp = (object[])aux2[contadorPercepcion];
                        tmp[4] = clave;
                        tmp[5] = descri;
                        tmp[6] = monto;
                    }
                    contadorPercepcion++;
                }
                else
                {

                    if (aux2[contadorDeduccion] == null)
                    {
                        tt1[7] = clave;
                        tt1[8] = descri;
                        tt1[10] = (string.IsNullOrWhiteSpace(pago4) || pago4 == "0") ? "" : $"{pago4}/{pagot}";
                        tt1[9] = monto;
                        aux2[contadorDeduccion] = tt1;
                    }
                    else
                    {
                        object[] tmp = (object[])aux2[contadorDeduccion];
                        tmp[7] = clave;
                        tmp[8] = descri;
                        tmp[10] = (string.IsNullOrWhiteSpace(pago4) || pago4 == "0") ? "" : $"{pago4}/{pagot}";
                        tmp[9] = monto;
                    }
                    contadorDeduccion++;
                }

            }



            int contador = 0;

            List<object> lista = new List<object>();
            foreach (object item in aux2)
            {
                if (item == null)
                    break;
                lista.Add(item);
            }


            aux2 = new object[lista.Count];

            int x = 0;
            foreach (object item in lista)
            {
                aux2[x] = item;
                x++;
            }



            string descripcion = $"REPORTE DE INCIDENCIAS ELECTRÓNICO PARA EL PAGO A {jppdescripcion} DEL MES DE {mes} DE {txtAño.Text}";

            object[] parametros = { "descripcion" };
            object[] valor = { descripcion };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;
            //Restablece los objetos para evitar el break del reporteador
            globales.reportes("reporteGeneracionNominas", "nomina", aux2, "", false, enviarParametros);

            string queryresumen = string.Empty;
            if (chknomina.Checked)
            {
                queryresumen = $"SELECT a2.clave,a2.tipopago, max(a3.descri) as descri, sum(monto) as monto FROM nominas_catalogos.maestro a1 LEFT JOIN nominas_catalogos.nominew a2 ON a1.jpp = a2.jpp JOIN nominas_catalogos.perded a3 ON a3.clave=a2.clave AND a1.num = a2.numjpp WHERE 	a1.superviven = 'S' AND a1.jpp = '{jpp}' AND a2.tipopago in  ('N','R') AND a2.tipo_nomina='{tipo_nomina}' GROUP BY a2.clave,a2.tipopago ORDER BY a2.clave,a2.tipopago;";

            }
            else
            {
                queryresumen = $"SELECT a2.clave,a2.tipopago, max(a3.descri) as descri, sum(monto) as monto FROM nominas_catalogos.maestro a1 LEFT JOIN nominas_catalogos.nominew a2 ON a1.jpp = a2.jpp JOIN nominas_catalogos.perded a3 ON a3.clave=a2.clave AND a1.num = a2.numjpp WHERE 	a1.superviven = 'S' AND a1.jpp = '{jpp}' AND a2.tipopago in  ('N','R') AND a2.tipo_nomina ='{tipo_nomina}' GROUP BY a2.clave,a2.tipopago ORDER BY a2.clave,a2.tipopago;";

            }

            List<Dictionary<string, object>> resumen = globales.consulta(queryresumen);
            object[] aux4 = new object[resumen.Count];
            int cont = 0;
            int veces = resumen.Count;
            double sumap = 0;
            double sumad = 0;

            foreach (var itemr in resumen)
            {

                string claveR = string.Empty;
                string deducR = string.Empty;
                string MontoR = string.Empty;
                string MooR = string.Empty;

                string LeyenR = string.Empty;
                string tipopago = string.Empty;
                string retro = "RETROACTIVO";
            
                object[] tt1 = { "", "", "", "", "", "", "", "" };

                claveR = Convert.ToString(itemr["clave"]);
                deducR = Convert.ToString(itemr["descri"]);
                MooR = Convert.ToString(itemr["monto"]);
                MontoR = string.Format("{0:c}", Convert.ToDouble(MooR));
                tipopago = Convert.ToString(itemr["tipopago"]);
                

                if (Convert.ToInt32(claveR) < 60)
                {
                    tt1[0] = claveR;
                    tt1[1] = deducR;
                    tt1[2] = MontoR;
                    if (tipopago == "R")
                        tt1[3] = retro;
                    aux4[cont] = tt1;
                    sumap = Convert.ToDouble(MooR) + sumap;
                }
                else
                {
                    tt1[4] = claveR;
                    tt1[5] = deducR;
                    tt1[6] = MontoR;
                    if (tipopago == "R")
                        tt1[7] = retro;
                    aux4[cont] = tt1;
                    sumad = Convert.ToDouble(MooR) + sumad;

                }
                cont++;

            }

            List<object> listaResumen = new List<object>();
            foreach (object item in aux4)
            {
                if (item == null)
                    break;
                listaResumen.Add(item);
            }


            aux4 = new object[listaResumen.Count];

            int y = 0;
            foreach (object item in listaResumen)
            {
                aux4[y] = item;
                y++;
            }

            string desc = $"RESUMEN CONTABLE DE LAS INCIDENCIAS ELECTRÓNICA PARA EL PAGO A {jppdescripcion}  DEL MES DE {comboBox1.Text}";
            
            string fecha = "";
            double operacion = Convert.ToDouble(sumap) - Convert.ToDouble(sumad);
            string letra = $"LA PRESENTE NOMINA AMPARA LA CANTIDAD DE:{globales.convertirNumerosLetras(Convert.ToString(operacion),true)}";
            object[] parametrosR = { "descripcion" ,"sumap","sumad","liquido","conteo","letra"};
            object[] valorR = { desc,string.Format("{0:C}",Convert.ToDouble(sumap)) , string.Format("{0:C}", Convert.ToDouble(sumad)), string.Format("{0:C}", Convert.ToDouble(sumap)-Convert.ToDouble(sumad)),Convert.ToString(cantidad),letra };
            object[][] enviarParametrosR = new object[2][];

            enviarParametrosR[0] = parametrosR;
            enviarParametrosR[1] = valorR;
            //Restablece los objetos para evitar el break del reporteador
            globales.reportes("frmResumenNomina", "resumenNomina", aux4, "", false, enviarParametrosR);

        }

        private string seleccionarMes()
        {



            return this.comboBox1.Text;
        }

        private void frmElaboracionNomina_Load(object sender, EventArgs e)
        {
            txtAño.Text = DateTime.Now.Year.ToString();
            comboBox1.SelectedIndex = DateTime.Now.Month - 1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
         
            string mes = string.Empty;
            if (comboBox1.Text == "ENERO") mes = "01";
            if (comboBox1.Text == "FEBRERO") mes = "02";
            if (comboBox1.Text == "MARZO") mes = "03";
            if (comboBox1.Text == "ABRIL") mes = "04";
            if (comboBox1.Text == "MAYO") mes = "05";
            if (comboBox1.Text == "JUNIO") mes = "06";
            if (comboBox1.Text == "JULIO") mes = "07";
            if (comboBox1.Text == "AGOSTO") mes = "08";
            if (comboBox1.Text == "SEPTIEMBRE") mes = "09";
            if (comboBox1.Text == "OCTUBRE") mes = "10";
            if (comboBox1.Text == "NOVIEMBRE") mes = "11";
            if (comboBox1.Text == "DICIEMBRE") mes = "12";

            string tipo_nomina = string.Empty;

            if (radioButton1.Checked) tipo_nomina = "AG";
            if (radioButton2.Checked)  tipo_nomina = "CA";
            if (radioButton3.Checked) tipo_nomina = "DM";
            if (radioButton4.Checked) tipo_nomina = "UT";
            if (radioButton5.Checked) tipo_nomina = "CAN2";

            if (groupBox2.Visible == false) tipo_nomina = "N";

            DateTime anio = DateTime.Now;
            string anios =Convert.ToString(anio.Year).Substring(2,2);

            string query = string.Empty;
            if (chknomina.Checked)
            {
                query = $"SELECT a2.jpp,a2.numjpp,a2.clave,a2.secuen,a2.descri,a2.pagon as npago,a2.pagot,a2.tipopago as tipo_pago,a2.leyen,a2.fechaini,a2.fechafin,a2.monto,a2.folio FROM nominas_catalogos.maestro a1 LEFT JOIN nominas_catalogos.nominew a2 ON a1.jpp = a2.jpp AND a1.num = a2.numjpp WHERE a1.superviven = 'S'  AND a2.tipopago in ('N','R') AND tipo_nomina='{tipo_nomina}'";

            }
            else
            {
                query = "SELECT a2.jpp,a2.numjpp,a2.clave,a2.secuen,a2.descri,a2.pagon as npago,a2.pagot,a2.tipopago as tipo_pago,a2.leyen,a2.fechaini,a2.fechafin,a2.monto,a2.folio FROM nominas_catalogos.maestro a1 LEFT JOIN nominas_catalogos.nominew a2 ON a1.jpp = a2.jpp AND a1.num = a2.numjpp WHERE a1.superviven = 'S'  AND a2.tipopago in ('N','R') AND tipo_nomina='N'";

            }

            List<Dictionary<string, object>> resultado = globales.consulta(query);
            string entrada = Convert.ToString(resultado.Count);
            DialogResult p = globales.MessageBoxQuestion($"DESEA CERRAR NÓMINA.SE GENERARA CIERRE DEFINITIVO Y RESPAlDARÁ INFORMACIÓN, SE IMPORTARÁN {entrada} MOVIMIENTOS", "AVISO IMPORTANTE", globales.menuPrincipal);
            if (p == DialogResult.No) return;
            string inserta = string.Empty;
            foreach (var item in resultado)
            {
                string jpp = Convert.ToString(item["jpp"]);
                string numjpp = Convert.ToString(item["numjpp"]);
                string clave = Convert.ToString(item["clave"]);
                string secuen = Convert.ToString(item["secuen"]);
                string descri = Convert.ToString(item["descri"]);
                string pagon = Convert.ToString(item["npago"]);
                string pagot = Convert.ToString(item["pagot"]);
                if (pagon == "") pagon = "0"; if (pagot == "") pagot = "0";
                string tipo_pago = Convert.ToString(item["tipo_pago"]);
                string leyen = Convert.ToString(item["leyen"]);
                string fechaini = Convert.ToString(item["fechaini"]).Replace(" 12:00:00 a. m.","");
                fechaini = $"'{fechaini}'";

                if (fechaini == "''") fechaini = "null";

                string fechafin = Convert.ToString(item["fechafin"]).Replace(" 12:00:00 a. m.", "");
                fechafin = $"'{fechafin}'";

                if (fechafin=="''") fechafin = "null";

                string monto = Convert.ToString(item["monto"]);

                this.archivo = anios + mes;

                string folio = Convert.ToString(item["folio"]);
                if (folio == "") folio = "0";
               


                if (chknomina.Checked)
                {
                    inserta = $"INSERT INTO nominas_catalogos.respaldos_nominas (jpp,numjpp,clave,secuen,descri,pago4,pagot,leyen,fechaini,fechafin,archivo,tipo_nomina,monto,tipo_pago,folio)" +
                                                                                $"values ('{jpp}',{numjpp},{clave},{secuen},'{descri}',{pagon},{pagot},'{leyen}',{fechaini},{fechafin},'{this.archivo}','{tipo_nomina}',{monto},'{tipo_pago}',{folio});";

                }
                else
                {
                    inserta = $"INSERT INTO nominas_catalogos.respaldos_nominas (jpp,numjpp,clave,secuen,descri,pago4,pagot,leyen,fechaini,fechafin,archivo,tipo_nomina,monto,tipo_pago,folio)" +
                                                                                $"values ('{jpp}',{numjpp},{clave},{secuen},'{descri}',{pagon},{pagot},'{leyen}',{fechaini},{fechafin},'{this.archivo}','N',{monto},'{tipo_pago}',{folio});";

                }

                Cursor.Current = Cursors.WaitCursor;
                
                globales.consulta(inserta);


            }
            string confirma = string.Empty;

            if (chknomina.Checked)
            {
                confirma = $"select count(*)as conteo  from nominas_catalogos.respaldos_nominas where archivo='{this.archivo}' and tipo_nomina='{tipo_nomina}'";

            }
            else
            {
                confirma = $"select count(*)as conteo  from nominas_catalogos.respaldos_nominas where archivo='{this.archivo}' and tipo_nomina='N'";

            }
            List<Dictionary<string, object>> res = globales.consulta(confirma);
            string salida = Convert.ToString(res[0]["conteo"]);
            if (entrada==salida)
            {
                DialogResult dia = globales.MessageBoxSuccess("SE RESPALDO TODA LA INFORMACIÓN DE FORMA CORRECTA","TERMIANDO",globales.menuPrincipal);

            
            }
            else
            {
                DialogResult dialogerror = globales.MessageBoxError("SE PRESENTO UN ERROR , NO SE TRAPASO TODA LA INFORMACIÓN","CONTACTE A SISTEMAS", globales.menuPrincipal);
            }
            Cursor.Current = Cursors.Default;



        }

        private void chknomina_CheckedChanged(object sender, EventArgs e)
        {
            if (chknomina.Checked)
            {
                groupBox2.Visible = true;

            }
            else
            {
                groupBox2.Visible = false;
            }
        }

        private void frmElaboracionNomina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.F5)
            {
                button4.Visible = true;
            }
        }
    }
}
