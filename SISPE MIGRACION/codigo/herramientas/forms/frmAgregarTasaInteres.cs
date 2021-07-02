﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.codigo.herramientas.forms
{
    public partial class frmAgregarTasaInteres : Form
    {
        internal string txtFecha, txtInteres = string.Empty;
        internal bool aceptar = false;
        public frmAgregarTasaInteres()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.numerico(e.KeyChar);
        }

        private void frmAgregarTasaInteres_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) this.Owner.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.SendToBack();
            if (string.IsNullOrWhiteSpace(txtFecha1.Text)) {
                globales.MessageBoxExclamation("Se debe ingresar una fecha para aplicar tasa","Aviso",globales.menuPrincipal);
                txtFecha1.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtInteres1.Text)) {
                globales.MessageBoxExclamation("Se debe ingresar la tasa de interes a aplicar", "Aviso", globales.menuPrincipal);
                txtInteres1.Focus();
                return;
            }

            this.txtFecha = string.Format("{0}-{1}-{2}", this.txtFecha1.Value.Year, this.txtFecha1.Value.Month, this.txtFecha1.Value.Day);
            this.txtInteres = this.txtInteres1.Text;
            this.aceptar = true;
            this.Owner.Close();
        }
    }
}
