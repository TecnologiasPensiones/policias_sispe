﻿using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.QUIROGRAFARIO;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.CATÁLOGOS
{
    public partial class frmCatalogoP_quirog : Form
    {
        internal enviarDatos2 enviar;
        private int numeroMaximo = 0;
        private List<Dictionary<string, object>> resultado;
        private string folio = string.Empty;
        internal string tablaConsultar = string.Empty;
        internal rellenar enviar2;
        internal bool enviarBool = false;
        internal bool btnAceptarbool = false;
        public frmCatalogoP_quirog()
        {
            InitializeComponent();
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Close();
            return;
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.alfaNumerico(e.KeyChar);
           
            
            if (e.KeyChar == 13)
                btnseleccionar_Click(null, null);
     
        }

        private void frmCatalogoP_quirog_Load(object sender, EventArgs e)
        {
            string query = string.Format("select MAX(FOLIO) from datos.{0}", this.tablaConsultar);

            List<Dictionary<string, object>> resultado2 = globales.consulta(query);
            string maximo = Convert.ToString(resultado2[0]["max"]);
            numeroMaximo = maximo.Length;

            string query2 = string.Format("select * from datos.{0} order by folio desc limit 100",tablaConsultar);

            resultado = globales.consulta(query2);
            resultado.ForEach(o => datos.Rows.Add(o["folio"], o["rfc"], o["nombre_em"]));
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string query = string.Format("select * from datos.{0}", tablaConsultar);
            if (!string.IsNullOrWhiteSpace(txtBusqueda.Text))
            {
                query += " where ";
                if (txtBusqueda.Text.ElementAt(0) >= '0' && txtBusqueda.Text.ElementAt(0) <= '9')
                {
                    string cadenaAux = txtBusqueda.Text;
                    string cadenaAux2 = cadenaAux;
                    if (txtBusqueda.Text.Length != numeroMaximo)
                    {
                        for (int x = txtBusqueda.Text.Length; x <= numeroMaximo; x++)
                        {
                            query += string.Format("  folio >= {0} AND folio <= {1}  OR", cadenaAux, cadenaAux2);
                            cadenaAux += "0";
                            cadenaAux2 += "9";
                        }
                        query = query.Substring(0, query.Length - 2);
                    }
                    else
                    {
                        query += string.Format("  FOLIO = {0} ", txtBusqueda.Text);
                    }
                    
                }
                else
                {
                    query += string.Format("  RFC LIKE '{0}%' OR NOMBRE_EM LIKE '{1}%'", txtBusqueda.Text, txtBusqueda.Text);
                }
            }
            query += " order by FOLIO DESC limit 100";

            this.resultado = globales.consulta(query);
            datos.Rows.Clear();
            resultado.ForEach(o => datos.Rows.Add(o["folio"], o["rfc"], o["nombre_em"]));
        }

        private void btnseleccionar_Click(object sender, EventArgs e)
        {
            if (this.resultado.Count == 0) return;
            Close();
            this.btnAceptarbool = true;
            Dictionary<string, object> aux = null;
            try
            {
                foreach (var item in resultado)
                {
                    if (Convert.ToString(item["folio"]) == this.folio)
                    {
                        aux = item;
                        break;
                    }
                }
            }
            catch
            {

            }
            if (this.tablaConsultar == "p_quirog" && !enviarBool)
            {

                string query = string.Format("select * from datos.D_QUIROG where FOLIO = '{0}'", this.folio);
                List<Dictionary<string, object>> aux2 = null;
                aux2 = globales.consulta(query);

                enviar(aux, aux2);
                return;
            }
            enviar2(aux);
        }

        private void datos_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            folio = Convert.ToString(datos.Rows[e.Cell.RowIndex].Cells[0].Value);
        }

        private void frmCatalogoP_quirog_FormClosing(object sender, FormClosingEventArgs e)
        {
         
        }

        private void frmCatalogoP_quirog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Close();
            }
        }

        private void datos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsNumber(e.KeyChar))
            {
                txtBusqueda.Focus();
            }
        }

        private void datos_QueryAccessibilityHelp(object sender, QueryAccessibilityHelpEventArgs e)
        {

        }

        private void datos_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
           

        }

        private void datos_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) {
            //    if (resultado.Count == 0) return;
            //    Close();
            //    this.btnAceptarbool = true;
            //    Dictionary<string, object> aux = null;
            //    foreach (var item in resultado)
            //    {
            //        if (Convert.ToString(item["folio"]) == this.folio)
            //        {
            //            aux = item;
            //            break;
            //        }
            //    }
            //    if (this.tablaConsultar == "p_quirog" && !enviarBool)
            //    {

            //        string query = string.Format("select * from datos.D_QUIROG where FOLIO = '{0}'", this.folio);
            //        List<Dictionary<string, object>> aux2 = null;
            //        aux2 = globales.consulta(query);

            //        enviar(aux, aux2);
            //        return;
            //    }
            //    enviar2(aux);

            //}
     
            if (e.KeyCode == Keys.Back)
            {
                txtBusqueda.Focus();
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnseleccionar_Click(null, null);
            }
        }
        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Down)
            {
                datos.Focus();
            }
        }
    }
}
