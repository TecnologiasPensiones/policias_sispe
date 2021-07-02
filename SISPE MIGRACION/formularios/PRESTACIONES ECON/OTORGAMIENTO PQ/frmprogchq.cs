﻿using SISPE_MIGRACION.codigo.baseDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ
{
    public partial class frmprogchq : Form
    {
        private DataGridViewRow elemento2;
        public frmprogchq()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void frmprogchq_Load(object sender, EventArgs e)
        {
            string movimientos = "select folio,rfc,nombre_em,f_solicitud,f_emischeq from datos.p_quirog order by folio desc limit 100;";
            var elemento = baseDatos.consulta(movimientos);
            foreach (var item in elemento)
            {
                string folio = Convert.ToString(item["folio"]);
                string rfc = Convert.ToString(item["rfc"]);
                string nombre_em = Convert.ToString(item["nombre_em"]);
                string f_solicitud = Convert.ToString(item["f_solicitud"]).Replace(" 12:00:00 a. m.", ""); ;
                string f_emischeq = Convert.ToString(item["f_emischeq"]).Replace(" 12:00:00 a. m.", ""); ;
                datoscheque.Rows.Add(folio, rfc, nombre_em, f_solicitud, f_emischeq);
            }




        }

        private void datoscheque_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void datoscheque_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var mod = datoscheque.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                string actualiza = "update datos.p_quirog set ";
                switch (e.ColumnIndex)
                {
                    case 1:
                        actualiza += " rfc = '{0}'";
                        break;
                    case 2:
                        actualiza += " nombre_em = '{0}'";
                        break;
                    case 4:
                        actualiza += " f_emischeq = '{0}'";
                        break;

                }
                actualiza += " where folio = '{1}'";
                string cambia = string.Format(actualiza, mod, datoscheque.Rows[e.RowIndex].Cells[0].Value);
                if (baseDatos.consulta(cambia, true))
                {
                    globales.MessageBoxSuccess("Registro actualizado correctamente","Registro actualizado",globales.menuPrincipal);
                }
                else
                {
                    globales.MessageBoxError("Error en la actualización","Error",globales.menuPrincipal);
                }


            }
            catch
            {

            }

        }

        private void datoscheque_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                elemento2 = datoscheque.Rows[e.RowIndex];


            }
            catch
            {
                globales.MessageBoxError("Error en los datos del dataGrid.. contacte a sistemas para dar solución","Error",globales.menuPrincipal);
            }

        }

        private void frmprogchq_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Close();
            }
        }

        private void frmprogchq_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            SISPE_MIGRACION.formularios.CATÁLOGOS.frmCatalogoP_quirog p = new SISPE_MIGRACION.formularios.CATÁLOGOS.frmCatalogoP_quirog();
            p.tablaConsultar = "p_quirog";
            p.enviarBool = true;
            p.enviar2 = traerRegistro;
            p.ShowDialog();
        }
        private void traerRegistro(Dictionary<string,object> item) {
            datoscheque.Rows.Clear();
            string folio = Convert.ToString(item["folio"]);
            string rfc = Convert.ToString(item["rfc"]);
            string nombre_em = Convert.ToString(item["nombre_em"]);
            string f_solicitud = Convert.ToString(item["f_solicitud"]).Replace(" 12:00:00 a. m.", ""); ;
            string f_emischeq = Convert.ToString(item["f_emischeq"]).Replace(" 12:00:00 a. m.", ""); ;
            datoscheque.Rows.Add(folio, rfc, nombre_em, f_solicitud, f_emischeq);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult p = globales.MessageBoxQuestion("¿Deseas cerrar el modulo?", globales.menuPrincipal);
            if (p == DialogResult.Yes)
                this.Close();
        }
    }
}
