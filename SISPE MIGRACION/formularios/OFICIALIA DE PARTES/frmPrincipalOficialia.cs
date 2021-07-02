﻿ using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using WIA;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace SISPE_MIGRACION.formularios.OFICIALIA_DE_PARTES
{
    public partial class frmPrincipalOficialia : Form
    {
        string folio;
        public frmPrincipalOficialia()
        {
            InitializeComponent();
        }

        private void frmPrincipalOficialia_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbInterno.Checked)
            //{
            //    label9.Visible = true;
            //    txtNomRemitente.Visible = true;
            //}
            //else
            //{
            //    label9.Visible = false;
            //    txtNomRemitente.Visible = false;
            //}
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbExterno.Checked)

            //{
            //    label9.Visible = true;
            //    txtNomRemitente.Visible = true;
            //    label10.Visible = true;
            //    txtDependRemit.Visible = true;
            //}
            //else
            //{
            //    label9.Visible = false;
            //    txtNomRemitente.Visible = false;
            //    label10.Visible = false;
            //    txtDependRemit.Visible = false;
            //}
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

            Process.Start(@"C:\Program Files\HP\HP ScanJet Pro 2000 s1\Bin\HP ScanJet Pro 2000 s1"); // Se abre paint


        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            DialogResult p = open1.ShowDialog();
            if (p == DialogResult.OK)
            {
                string ruta = open1.FileName;
                string nomArch = Path.GetFileName(ruta);
                string server = @"\\192.168.100.103\pensiones\2020\OFICIOS";
                try
                {
                    File.Copy(ruta, Path.Combine(server, (txtFolio.Text).Replace("/", "") + ".pdf"));

                }
                catch
                {
                    DialogResult dialogo = globales.MessageBoxExclamation("YA EXISTE UN ARCHIVO RELACIONADO A ESTE FOLIO", "AVISO", globales.menuPrincipal);
                    return;
                }
                globales.MessageBoxSuccess("DOCUMENTO CARGADO CON ÉXITO AL SERVER", "AVISO", globales.menuPrincipal);
                txtRuta.Text = (server + @"\" + txtFolio.Text.Replace("/", "") + ".pdf");


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFolio.Text) || string.IsNullOrWhiteSpace(txtAsunto.Text) || string.IsNullOrWhiteSpace(comboArea.Text) || string.IsNullOrWhiteSpace(txtDestinario.Text) || string.IsNullOrWhiteSpace(comboPrioridad.Text))
            {
                DialogResult dialogo = globales.MessageBoxExclamation("NO DEJE CAMPOS VACIOS", "AVSIO", globales.menuPrincipal);
                return;
            }

            string oki = $"select id from oficialia.tramites where nombre='{txtTipo.Text}'";
            List<Dictionary<string, object>> res = globales.consulta(oki);
            string id = Convert.ToString(res[0]["id"]);
            // externo =true

            string checa = $"select id from oficialia.areas_pensiones  where nombre='{comboArea.Text}'";
            List<Dictionary<string, object>> rs = globales.consulta(checa);
            string id_area = Convert.ToString(rs[0]["id"]);

            string ficherosSeleccionados = "";


            if (ListDependencias.CheckedItems.Count != 0)
            {
                //recorremos todos los elementos activados
                //CheckedItems sólo devuelve los elementos activados/chequeados
                for (int i = 0; i <= ListDependencias.CheckedItems.Count - 1; i++)
                {
                    if (ficherosSeleccionados != "")
                    {
                        ficherosSeleccionados =
                             ficherosSeleccionados + "," + ListDependencias.CheckedItems[i].ToString()+",";
                    }
                    else
                    {
                        ficherosSeleccionados =
                             ListDependencias.CheckedItems[i].ToString();
                    }
                }
            }
            int Nivel_Prioridad = comboPrioridad.SelectedIndex;
            string remitente = string.Empty;
            //if (rbExterno.Checked)
            //{
            //    remitente = "true";
            //}
            //else
            //{
            //    remitente = "false";
            //}

            DateTime fecha = DateTime.Now;
            string fec = string.Format("{0:dd/MM/yy}", fecha);
            string hora = string.Format("{0:hh:mm:ss}", fecha).Replace("a. m.", "");
            hora = string.Format("{0:hh:mm:ss}", fecha).Replace("p. m.", "");

            string año = Convert.ToString(fecha.Year);
            string insert = $"insert into  oficialia.detalle_recepcion_oficio (folio,area_destinatario,tipo_documento,destinatario,fecha_limite, prioridad, remitente,nombre_remitente,dependencia_remitente,asunto,captura,copias,ruta_archivo,fecha_captura,hora_captura,estatus) values ({txtFolio.Text.Replace($"/{año}", "")},{id_area},{id},'{txtDestinario.Text}','{txtFcehaLimite.Text}',{Nivel_Prioridad}, false,'{txtNomRemitente.Text}','{txtDependRemit.Text}','{txtAsunto.Text}','{globales.id_usuario}','{ficherosSeleccionados}','{txtRuta.Text}','{fec}','{hora}','INICIAL')";
            try
            {
                globales.consulta(insert);
                DialogResult dialogo = globales.MessageBoxSuccess("SE REGISTRO CORRECTAMENTE LA INFORMACIÓN", "AVISO", globales.menuPrincipal);

                string query = $"insert into oficialia.historial (folio,tramite,evidencia_ruta,tipo,responsable,fecha ) values ({txtFolio.Text.Replace($"/{año}", "")},{id},'{txtRuta.Text}','REGISTRO','{globales.usuario}',current_date)";
                globales.consulta(query);

                LimpiaFormulario();
            }
            catch
            {
                DialogResult dialogo = globales.MessageBoxError("LO SENTIMOS, OCURRIO UN ERROR", "UPS", globales.menuPrincipal);
            }
        }

        private void frmPrincipalOficialia_Shown(object sender, EventArgs e)
        {
            LimpiaFormulario();
            CargaInicial();
            //linkLabel1.Text = Convert.ToString(globales.usuario);
            label9.Visible = true;
            label10.Visible = true;
            txtDependRemit.Visible = true;
            txtNomRemitente.Visible = true;

        }

        public void LimpiaFormulario()
        {
            comboArea.Items.Clear();
            txtTipo.Text = "";
            txtDestinario.Text = "";
            txtFcehaLimite.Clear();
            comboPrioridad.SelectedIndex = -1;
            //rbExterno.Checked = false;
          //  rbInterno.Checked = false;
            txtNomRemitente.Text = "";
            txtDependRemit.Text = "";
            txtFechaRecepcion.Text = "";
            txtRuta.Text = "";
            ListDependencias.Items.Clear();
            txtFolio.Clear();
            comboArea.Items.Clear();
            CargaInicial();
        }

        public void CargaInicial()
        {
            string query = "select max(folio) +1 as folio from oficialia.detalle_recepcion_oficio  ";
            List<Dictionary<string, object>> res = globales.consulta(query);
             folio = string.Empty;

            folio = Convert.ToString(res[0]["folio"]);

            DateTime fechaA = DateTime.Now;
            string año = Convert.ToString(fechaA.Year);
            if (folio == "")
            {
                folio = "1/" + año;
            }
            else
            {
                folio = folio + "/" + año;
            }

            txtFolio.Text = folio;
            txtFolio.ReadOnly = true;
            DateTime fecha = DateTime.Now;

            txtFechaRecepcion.Text = Convert.ToString(fecha);

            comboArea.Items.Clear();
            string busca = "select nombre from oficialia.areas_pensiones ";
            List<Dictionary<string, object>> resultados = globales.consulta(busca);
            foreach (var item in resultados)
            {
                string nombre = Convert.ToString(item["nombre"]);

                comboArea.Items.Add(nombre);
                ListDependencias.Items.Add(nombre);

            }




        }

        //jo


        private void comboArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void txtTipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void comboPrioridad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void ListDependencias_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void txtTipo_Enter(object sender, EventArgs e)
        {
            txtTipo.Items.Clear();
            string busca = $"select id from oficialia.areas_pensiones  where nombre='{comboArea.Text}'";
            List<Dictionary<string, object>> res = globales.consulta(busca);
            if (res.Count <= 0) return;
            string id = Convert.ToString(res[0]["id"]);
            string query = $"select nombre from oficialia.tramites where id_depto='{id}'";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            foreach (var item in resultado)
            {
                string nombre = Convert.ToString(item["nombre"]);
                txtTipo.Items.Add(nombre);
            }
        }

        private void frmPrincipalOficialia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void frmPrincipalOficialia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
