﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios
{
    public partial class subirinstalador : Form
    {
        private string base64;

        public subirinstalador()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                string ruta = openFileDialog1.FileName;
                byte[] bytes = File.ReadAllBytes(ruta);
                base64 = Convert.ToBase64String(bytes);

                txt1.Text = ruta;
               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string version = SISPE_MIGRACION.Properties.Resources.version;
            globales.consulta(string.Format("update catalogos.version set instalador = '{0}',version = '{1}'", base64, version));
            MessageBox.Show("Instalador subio correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
