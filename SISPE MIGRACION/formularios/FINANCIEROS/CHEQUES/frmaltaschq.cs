﻿using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.FINANCIEROS.CHEQUES
{
    public partial class frmaltaschq : Form
    {
        private int filaActual = 0;
        private int columnaActual = 0;
        private int rowActual = 0;
        private string cuenta, concepto, debe, haber, monto, sec, referencia, debehaber;
        List<Dictionary<string, object>> result;
        private string cuentacancelada;
        private bool error = false;
        private bool esInsertar = false;


        public frmaltaschq()
        {
            InitializeComponent();
            txtnumchq.Visible = true;
            label1.Visible = true;
            this.cuentacancelada = cuentacancelada;
            //this.KeyPreview = true;

            //this.KeyPress += new KeyPressEventHandler(frmaltaschq_KeyPress);
        }

        private void frmaltaschq_Load(object sender, EventArgs e)
        {
            btnbaja.Visible = false;
            bloquea();
            txtnumchq.Focus();
            cargaprimero();
            label14.Visible = false;
            txtcuentacancel.Visible = false;
            lblstatus.Text = "GENERANDO CHEQUE";

        }
        private void cargaprimero()
        {
            string query = "SELECT (max(numcheque)+1) as maxcheque FROM financieros.datoscheques";
            List<Dictionary<string, object>> resul = globales.consulta(query);
            string numcheque = Convert.ToString(resul[0]["maxcheque"]);
            txtnumchq.Text = numcheque;

        }

        private void bloquea()
        {
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
            txtbenefic.Visible = false;
            txtbanco.Visible = false;
            txtconcepto.Visible = false;
            txtcuenta.Visible = false;
            txtelaboro.Visible = false;
            txtmonto.Visible = false;
            txtpoliza.Visible = false;
            fec1.Visible = false;
            data.Visible = false;
            letramonto.Visible = false;
            linkLabel2.Visible = false;
            //     btnbaja.Visible = true;
        }

        private void desbloquea()
        {
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            label9.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            label12.Visible = true;
            label13.Visible = true;
            txtbenefic.Visible = true;
            txtbanco.Visible = true;
            txtconcepto.Visible = true;
            txtcuenta.Visible = true;
            txtelaboro.Visible = true;
            txtmonto.Visible = true;
            txtpoliza.Visible = true;
            fec1.Visible = true;
            letramonto.Visible = true;
        }

        private void txtnumchq_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnbaja.Visible = false;
                string query = "select * from financieros.datoscheques where numcheque ='{0}'";
                string detalle = string.Format(query, txtnumchq.Text);
                List<Dictionary<string, object>> result = globales.consulta(detalle);
                if (result.Count > 0)
                {
                    lblstatus.Text = "EDITANDO CHEQUE";
                    DialogResult per = globales.MessageBoxInformation("ACTUALIZANDO INFORMACIÓN DE CHEQUE", "MODIFICANDO CHEQUE", this);
                    btnbaja.Visible = true;
                    desbloquea();
                    rellenasihayinfo();
                }
                else
                {
                    lblstatus.Text = "GENERANDO CHEQUE";
                    DialogResult per = globales.MessageBoxInformation("GENERANDO NUEVO CHEQUE", "NUEVO FOLIO", this);
                    desbloquea();
                    entocesnuevo();
                }
            }
        }

        private void rellenasihayinfo()
        {
            txtnumchq.Enabled = false;
            txtbanco.Select();
            string query = "select * from financieros.datoscheques where numcheque ='{0}'";
            string ncheque = string.Format(query, txtnumchq.Text);
            List<Dictionary<string, object>> result = globales.consulta(ncheque);
            this.txtbanco.Text = Convert.ToString(result[0]["banco"]);
            this.txtcuenta.Text = Convert.ToString(result[0]["chequera"]);
            DateTime traefecha = Convert.ToDateTime(result[0]["fecha"]);
            this.txtbenefic.Text = Convert.ToString(result[0]["benefic"]);
            this.fec1.Text = string.Format("{0:dd/MM/yyyy}", traefecha);
            string monto = Convert.ToString(result[0]["impcheque"]);
            double montonum = Convert.ToDouble(monto);
            this.txtmonto.Text = string.Format("{0:C}", (montonum));    // PASO A PESOS 
            this.txtconcepto.Text = Convert.ToString(result[0]["concep1"]);
            this.txtelaboro.Text = Convert.ToString(result[0]["elaboro"]);
            this.txtpoliza.Text = Convert.ToString(result[0]["numpoliz"]);
            this.letramonto.Text = globales.convertirNumerosLetras(monto, true);
        }

        private void entocesnuevo()
        {
            txtnumchq.Enabled = false;
            txtbanco.Focus();
            this.txtbanco.Text = "BANORTE";
            DateTime fechaactual = DateTime.Now;
            this.fec1.Text = string.Format("{0:dd/MM/yyyy}", fechaactual);
            string numpoliz = "SELECT (max(numpoliz)+1) as numpoliz FROM financieros.datoscheques";
            List<Dictionary<string, object>> lis = globales.consulta(numpoliz);
            string ultimapoliza = Convert.ToString(lis[0]["numpoliz"]);
            this.txtpoliza.Text = ultimapoliza;
            this.txtelaboro.Text = "MLD";
            this.txtcuenta.Text = "0139891689";
        }

        private void txtmonto_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void frmaltaschq_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
            char S;

            S = Char.ToUpper(e.KeyChar);

            e.KeyChar = S;
        }

        private void btnbaja_Click(object sender, EventArgs e)
        {
            txtcuentacancel.Text = "111318010010030001";
            linkLabel1.Visible = false;
            string qry = "SELECT concep1 as concepto FROM financieros.datoscheques where numcheque='{0}'";
            string checa = string.Format(qry, txtnumchq.Text);
            List<Dictionary<string, object>> det = globales.consulta(checa);
            string sicancelado = "";

            if (det.Count == 0)
            {
                MessageBox.Show("ESTE CHEQUE NO EXISTE");
                return;
            }
            sicancelado = Convert.ToString(det[0]["concepto"]);

            if (sicancelado == "CANCELADO")
            {
                MessageBox.Show("ESTE CHEQUE YA SE ENCUENTRA CANCELADO");
                return;
            }
            txtnumchq.Enabled = false;

            DialogResult dialogo = globales.MessageBoxQuestion("Visualizando la información del cheque:" + txtnumchq.Text, this);
            if (dialogo == DialogResult.Yes)
            {
                label14.Visible = false;
                txtcuentacancel.Visible = false;
                desbloquea();
                rellenasihayinfo();
                label14.Visible = true;
                txtcuentacancel.Visible = true;
                txtcuentacancel.Select();
            }
            else
            {
                return;
            }
        }

        private void ocultayborra()
        {
            bloquea();
            txtbanco.Clear();
            txtcuenta.Clear();
            fec1.Clear();
            txtelaboro.Clear();
            txtpoliza.Clear();
            txtbenefic.Clear();
            txtmonto.Clear();
            txtconcepto.Clear();
            data.Visible = false;
            bloquea();
            txtnumchq.Select();
            txtcuentacancel.Clear();
            txtcuentacancel.Visible = false;
            label14.Visible = false;
            txtnumchq.Enabled = true;
            txtnumchq.Select();
            linkLabel1.Visible = true;
            cargaprimero();
            data.Rows.Clear();
            lblstatus.Text = "GENERANDO CHEQUE";

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                txtcuentacancel.Clear();
                frmcuentas cuentas = new frmcuentas();
                cuentas.enviar = rellenacuenta;
                cuentas.ShowDialog();
                this.txtcuentacancel.Text = cuentacancelada;
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(txtcuentacancel.Text))
                {
                    MessageBox.Show("Favor de capturar la cuenta de cancelación");
                }
                else

                {

                    string query = $"select * from financieros.detalle_cheque where numcheque={txtnumchq.Text} ";
                    List<Dictionary<string, object>> valida = globales.consulta(query);
                    if (valida.Count <= 0)
                    {
                        DialogResult dialogo12 = globales.MessageBoxExclamation("VERIFICAR", "EL CHEQUE A CANCELAR NO TIENE UN DETALLE DE CUENTA, FAVOR DE CAPTURARLO ANTES DE CANCELAR", globales.menuPrincipal);
                        return;
                    }
                    query = "update financieros.datoscheques set benefic='CANCELADO',impcheque='0.00',concep1='CANCELADO' WHERE numcheque='{0}'";
                    string actualiza = string.Format(query, txtnumchq.Text);
                    globales.consulta(actualiza);
                    string query1 = $"update financieros.detalle_cheque set ctacontab='{txtcuentacancel.Text}', concepto='CANCELADO',importe='0.00',cancelado='TRUE' where numcheque='{txtnumchq.Text}'";
                //    string actualiza2 = string.Format(query1, txtnumchq.Text);
                    globales.consulta(query1);

                    DialogResult dialogo = globales.MessageBoxInformation($"cheque {txtnumchq.Text} cancelado", "CHEQUE ELIMINADO", this);
                    ocultayborra();

                }
            }
        }
        public void rellenacuenta(Dictionary<string, object> obj, bool externo = false)
        {
            cuentacancelada = Convert.ToString(obj["cuenta"]);
        }

        private void frmaltaschq_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                ocultayborra();
            }
            if (e.KeyCode == Keys.F2)
            {
                Close();
            }

        }

        private void data_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void data_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void data_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtnumchq_TextChanged(object sender, EventArgs e)
        {
            lblstatus.Text = "-";
        }

        private void txtbanco_KeyDown(object sender, KeyEventArgs e)
        {
            //    if (e.KeyCode == Keys.Enter) txtcuenta.Select();
        }

        private void txtmonto_Leave(object sender, EventArgs e)
        {
            this.txtmonto.Text = globales.convertMoneda(globales.convertDouble(txtmonto.Text));
            this.letramonto.Text = globales.convertirNumerosLetras(Convert.ToString(globales.convertDouble(txtmonto.Text)), true);

        }

        private void data_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int c = e.RowIndex;

            if (c == -1) return;

            if (this.error)
            {
                this.error = false;
                return;
            }

            if (this.esInsertar)
            {
                return;
            }
            if (e.ColumnIndex == 3)
            {
                try
                {
                    double entrada = 0;
                    entrada = double.Parse(Convert.ToString(data.Rows[c].Cells[3].Value), System.Globalization.NumberStyles.Currency);
                    data.Rows[c].Cells[3].Value = string.Format("{0:C}", entrada).Replace("$", "");

                }
                catch
                {
                    globales.MessageBoxError("Ingresar la entrada en el formato correcto:\n$0.00", "Error entrada", globales.menuPrincipal);
                    data.Rows[c].Cells[3].Value = string.Format("{0:C}", 0).Replace("$", "");
                }
            }


            if (e.ColumnIndex == 4)
            {
                try
                {
                    double entrada = 0;
                    entrada = double.Parse(Convert.ToString(data.Rows[c].Cells[4].Value), System.Globalization.NumberStyles.Currency);
                    data.Rows[c].Cells[4].Value = string.Format("{0:C}", entrada).Replace("$", "");

                }
                catch
                {
                    globales.MessageBoxError("Ingresar la entrada en el formato correcto:\n$0.00", "Error entrada", globales.menuPrincipal);
                    data.Rows[c].Cells[4].Value = string.Format("{0:C}", 0).Replace("$", "");
                }
            }
        }

        private void txtmonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.numerico(e.KeyChar);
        }

        private void data_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            SendKeys.Send("{UP}");
            SendKeys.Send("{TAB}");
        }

        private void txtpoliza_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtcuentacancel.Visible == false)
            {

                if (e.KeyCode == Keys.Enter)
                {
                    data.Select();
                    DialogResult dialogo = globales.MessageBoxQuestion("¿Es correcta la información?", "Aviso", this);
                    if (dialogo == DialogResult.Yes)
                    {
                        data.Visible = true;
                        guardaOactualiza();
                        verificadetalle();
                        linkLabel2.Visible = true;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }


        private void guardaOactualiza()

        {
            string queryt = "SELECT * FROM financieros.datoscheques where numcheque='{0}'";
            string paso1 = string.Format(queryt, txtnumchq.Text);
            List<Dictionary<string, object>> resultados = globales.consulta(paso1);
            if (resultados.Count > 0)
            {
                string qry = "update  financieros.datoscheques set banco = '{0}',chequera='{1}',fecha = '{2}',benefic = '{3}',impcheque = '{4}',concep1 = '{5}',elaboro = '{6}',reviso = '{7}',autorizo = '{8}',numpoliz = '{9}' where numcheque='{10}'";
                string montoAux = string.IsNullOrWhiteSpace(txtmonto.Text) ? "0" : txtmonto.Text;
                double monto = double.Parse(montoAux, System.Globalization.NumberStyles.Currency);
                DateTime fecha = Convert.ToDateTime(fec1.Text);
                string paso = string.Format(qry, txtbanco.Text, txtcuenta.Text, String.Format("{0:yyyy-MM-dd}", fecha), txtbenefic.Text, monto, txtconcepto.Text, txtelaboro.Text, label12.Text, label13.Text, txtpoliza.Text, txtnumchq.Text);
                globales.consulta(paso);
            }
            else
            {// nuevo
                string qry = "insert into financieros.datoscheques (banco,chequera,fecha,numcheque,benefic,impcheque,concep1,elaboro,reviso,autorizo,numpoliz,folio,esfondo) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')";
                string montoAux = (string.IsNullOrWhiteSpace(txtmonto.Text)) ? "0" : txtmonto.Text;
                double monto = double.Parse(montoAux, System.Globalization.NumberStyles.Currency);
                DateTime fecha = Convert.ToDateTime(fec1.Text);
                string paso = string.Format(qry, txtbanco.Text, txtcuenta.Text, String.Format("{0:yyyy-MM-dd}", fecha), txtnumchq.Text, txtbenefic.Text, monto, txtconcepto.Text, txtelaboro.Text, label12.Text, label13.Text, txtpoliza.Text, txtpoliza.Text, false);
                globales.consulta(paso);

            }

        }
        private void verificadetalle()
        {
            data.Rows.Clear();
            string qry = "SELECT * FROM financieros.detalle_cheque where numcheque='{0}' order by secuencia";
            string checa = string.Format(qry, txtnumchq.Text);
            List<Dictionary<string, object>> listadetalle = globales.consulta(checa);
            if (listadetalle.Count > 0)
            {
                string debe_haber;
                if (listadetalle.Count > 0)
                {
                    foreach (var item in listadetalle)
                    {
                        string cuenta = Convert.ToString(item["ctacontab"]);
                        string descripcion = Convert.ToString(item["concepto"]);
                        string referencia = Convert.ToString(item["referencia"]);
                        debe_haber = Convert.ToString(item["debe_haber"]);
                        double importe = Convert.ToDouble(item["importe"]);
                        string secuencia = Convert.ToString(item["secuencia"]);
                        if (debe_haber == "D")
                        {
                            data.Rows.Add(cuenta, descripcion, referencia, string.Format("{0:C}", importe).Replace("$", ""), "", secuencia);
                        }
                        if (debe_haber == "H")
                        {
                            data.Rows.Add(cuenta, descripcion, referencia, "", string.Format("{0:C}", importe).Replace("$", ""), secuencia);

                        }

                        //if (cuenta == cuenta_bancaria)
                        //{
                        //    totalcheque = Convert.ToString(item["importe"]);
                        //}
                    }
                }
            }
        }


        private void data_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                if (this.columnaActual == 0)
                {
                    frmcuentas cuentas = new frmcuentas();
                    cuentas.enviar = rellenarDependencias;
                    cuentas.ShowDialog();
                }

            }
            if (e.KeyCode == Keys.F9)  // GUARDAR CAMBIOS     
            {
                string query01 = "select impcheque from financieros.datoscheques where numcheque='{0}'";
                string pasa01 = string.Format(query01, txtnumchq.Text);
                List<Dictionary<string, object>> sobr = globales.consulta(pasa01);
                string totalcheque = Convert.ToString(sobr[0]["impcheque"]);
                string cuenta_bancaria = "111318010010030001";
                const int columdebe = 3;
                const int columdhaber = 4;
                double sumadebe = 0.00;
                double sumahaber = 0.00;
                foreach (DataGridViewRow it in data.Rows)
                {
                    string cuenta = Convert.ToString(it.Cells[0].Value);
                    string v1 = Convert.ToString(it.Cells[columdebe].Value);
                    string v2 = Convert.ToString(it.Cells[columdhaber].Value);
                    sumadebe += (string.IsNullOrWhiteSpace(v1)) ? 0 : double.Parse(v1, System.Globalization.NumberStyles.Currency);
                    sumahaber += (string.IsNullOrWhiteSpace(v2)) ? 0 : double.Parse(v2, System.Globalization.NumberStyles.Currency);
                    if (cuenta == cuenta_bancaria) { totalcheque = v2; }
                }

                string montoaux = string.IsNullOrWhiteSpace(txtmonto.Text) ? "0" : txtmonto.Text;
                double monto = double.Parse(montoaux, System.Globalization.NumberStyles.Currency);
                double chequet = double.Parse(totalcheque, System.Globalization.NumberStyles.Currency);
                DialogResult dialogo1 = globales.MessageBoxInformation("DEBE: " + String.Format("{0:c}", (sumadebe)) + "HABER: " + String.Format("{0:c}", (sumahaber)), "", globales.menuPrincipal);
                double n1 = Math.Round(sumadebe);
                if (n1  == Math.Round(sumahaber) && Convert.ToString(Math.Round(chequet)) == Convert.ToString(Math.Round(monto)))
                {

                    string consulta = "SELECT * FROM financieros.detalle_cheque where numcheque='{0}'";
                    string pasa = string.Format(consulta, txtnumchq.Text);
                    List<Dictionary<string, object>> resu = globales.consulta(pasa);

                    if (resu.Count > 0)
                    {
                        string borra = "delete from financieros.detalle_cheque where numcheque='{0}'";
                        string borrapasa = string.Format(borra, txtnumchq.Text);
                        globales.consulta(borrapasa);
                    }
                    int secuencia = 0;
                    int contador = 1;

                    try {
                        foreach (DataGridViewRow item in data.Rows)
                        {
                            if (contador == data.Rows.Count) break;
                            string ctacontab = Convert.ToString(item.Cells[0].Value);
                            string concepto = Convert.ToString(item.Cells[1].Value);
                            string referencia = Convert.ToString(item.Cells[2].Value);
                            debe = Convert.ToString(item.Cells[3].Value);
                            haber = Convert.ToString(item.Cells[4].Value);

                            secuencia++;
                            contador++;

                            string query = "INSERT INTO financieros.detalle_cheque (numcheque,secuencia,ctacontab,concepto,referencia,debe_haber,importe) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
                            if (debe == "0.00" || string.IsNullOrWhiteSpace(debe))
                            {
                                string debehaber = "H";
                                string actualiza = string.Format(query, txtnumchq.Text, secuencia, ctacontab, concepto, referencia, debehaber, Convert.ToString(double.Parse(haber, System.Globalization.NumberStyles.Currency)));
                                globales.consulta(actualiza);

                            }
                            else
                            {
                                string debehaber = "D";
                                string actualiza = string.Format(query, txtnumchq.Text, secuencia, ctacontab, concepto, referencia, debehaber, Convert.ToString(double.Parse(debe, System.Globalization.NumberStyles.Currency)));
                                globales.consulta(actualiza);

                            }
                        }

                    }
                    catch
                    {
                        globales.MessageBoxError("EXISTE UN ERROR EN LOS REGISTROS, NO SE PROCESO LA INFORMACIÓN", "CONTACTE A SISTEMAS", globales.menuPrincipal);
                    }
                    DialogResult dialo = globales.MessageBoxSuccess("PROCESO HECHO CORRECTAMENTE", "INSERTANDO REGISTROS", this);
                    DialogResult dialogo3 = globales.MessageBoxQuestion("DESEA HACER OTRO MOVIMIENTO", "", this);
                    if (dialogo3 == DialogResult.Yes)
                    {
                        ocultayborra();
                    }
                    else
                    {
                        Close();
                    }

                }
                else
                {
                    DialogResult mesage = globales.MessageBoxError("LOS IMPORTES DEBE Y HABER NO COINCIDEN CON EL IMPORTE DEL CHEQUE", "FAVOR DE VERIFICAR", this);
                }
            }



            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                int iColumn = data.CurrentCell.ColumnIndex;
                int iRow = data.CurrentCell.RowIndex;
                if (iColumn == data.ColumnCount - 1)
                {
                    if (data.RowCount > (iRow + 1))
                    {
                        data.CurrentCell = data[1, iRow + 1];
                    }
                    else
                    {
                        //focus next control
                    }
                }
                else
                    data.CurrentCell = data[iColumn + 1, iRow];
            }
        }


        public void rellenarDependencias(Dictionary<string, object> obj, bool externo = false)
        {
            string cuenta = Convert.ToString(obj["cuenta"]);
            string descripcion = Convert.ToString(obj["descripcion"]);

            data.Rows[this.rowActual].Cells[0].Value = cuenta;
            data.Rows[this.rowActual].Cells[1].Value = descripcion;

        }

        private void data_CellEnter(object sender, DataGridViewCellEventArgs e)

        {
            this.columnaActual = e.ColumnIndex;
            this.rowActual = e.RowIndex;
            int c = e.RowIndex;
            if (c == -1) return;

            //inicio,FINAL,new_tipo,entrada,salida,cuenta,movimiento

            DataGridViewRow row = data.Rows[c];
            cuenta = Convert.ToString(row.Cells[0].Value);
            concepto = Convert.ToString(row.Cells[1].Value);
            referencia = Convert.ToString(row.Cells[2].Value);
            debe = Convert.ToString(row.Cells[3].Value);
            haber = Convert.ToString(row.Cells[4].Value);
        }

    }
}
