namespace SISPE_MIGRACION.formularios.NOMINAS.CATALOGOS.modal
{
    partial class frmModalJubilados
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGuardarSecundario = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtggrid = new System.Windows.Forms.DataGridView();
            this.JPP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PAGO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.label45 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtggrid)).BeginInit();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnGuardarSecundario);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.txtBuscar);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(589, 534);
            this.panel1.TabIndex = 2;
            // 
            // btnGuardarSecundario
            // 
            this.btnGuardarSecundario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(173)))), ((int)(((byte)(104)))));
            this.btnGuardarSecundario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarSecundario.FlatAppearance.BorderSize = 0;
            this.btnGuardarSecundario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarSecundario.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarSecundario.ForeColor = System.Drawing.Color.White;
            this.btnGuardarSecundario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardarSecundario.Location = new System.Drawing.Point(479, 488);
            this.btnGuardarSecundario.Name = "btnGuardarSecundario";
            this.btnGuardarSecundario.Size = new System.Drawing.Size(97, 34);
            this.btnGuardarSecundario.TabIndex = 75;
            this.btnGuardarSecundario.Text = "✓ Aceptar ";
            this.btnGuardarSecundario.UseVisualStyleBackColor = false;
            this.btnGuardarSecundario.Click += new System.EventHandler(this.btnGuardarSecundario_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.dtggrid);
            this.panel2.Location = new System.Drawing.Point(8, 113);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(569, 361);
            this.panel2.TabIndex = 63;
            // 
            // dtggrid
            // 
            this.dtggrid.AllowUserToAddRows = false;
            this.dtggrid.AllowUserToDeleteRows = false;
            this.dtggrid.AllowUserToResizeColumns = false;
            this.dtggrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            this.dtggrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dtggrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtggrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dtggrid.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dtggrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtggrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedVertical;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtggrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dtggrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtggrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.JPP,
            this.Column1,
            this.Column2,
            this.PAGO});
            this.dtggrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtggrid.EnableHeadersVisualStyles = false;
            this.dtggrid.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dtggrid.Location = new System.Drawing.Point(0, 0);
            this.dtggrid.MultiSelect = false;
            this.dtggrid.Name = "dtggrid";
            this.dtggrid.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtggrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dtggrid.RowHeadersVisible = false;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.dtggrid.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dtggrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtggrid.Size = new System.Drawing.Size(567, 359);
            this.dtggrid.TabIndex = 0;
            this.dtggrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtggrid_CellEnter);
            this.dtggrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtggrid_KeyDown);
            this.dtggrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtggrid_KeyPress);
            // 
            // JPP
            // 
            this.JPP.FillWeight = 55.94296F;
            this.JPP.HeaderText = "JPP";
            this.JPP.Name = "JPP";
            this.JPP.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 99.07464F;
            this.Column1.HeaderText = "NUMERO";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 90.364F;
            this.Column2.HeaderText = "RFC";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // PAGO
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.PAGO.DefaultCellStyle = dataGridViewCellStyle8;
            this.PAGO.FillWeight = 195.449F;
            this.PAGO.HeaderText = "NOMBRE";
            this.PAGO.Name = "PAGO";
            this.PAGO.ReadOnly = true;
            // 
            // txtBuscar
            // 
            this.txtBuscar.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBuscar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBuscar.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.txtBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.txtBuscar.Location = new System.Drawing.Point(13, 71);
            this.txtBuscar.Multiline = true;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(319, 20);
            this.txtBuscar.TabIndex = 62;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            this.txtBuscar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscar_KeyDown);
            this.txtBuscar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscar_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label2.Location = new System.Drawing.Point(10, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(270, 18);
            this.label2.TabIndex = 18;
            this.label2.Text = "BUSCAR (No. JUBILADOS, RFC O NOMBRE):";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.Control;
            this.panel6.Controls.Add(this.button2);
            this.panel6.Controls.Add(this.label45);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(589, 38);
            this.panel6.TabIndex = 17;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(36)))), ((int)(((byte)(0)))));
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            this.button2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Gainsboro;
            this.button2.Location = new System.Drawing.Point(483, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 32);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cerrar   X";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label45.Location = new System.Drawing.Point(3, 6);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(177, 26);
            this.label45.TabIndex = 1;
            this.label45.Text = "Catálogos jubilados";
            // 
            // frmModalJubilados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 534);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmModalJubilados";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmModalJubilados";
            this.Load += new System.EventHandler(this.frmModalJubilados_Load);
            this.Shown += new System.EventHandler(this.frmModalJubilados_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtggrid)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button btnGuardarSecundario;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dtggrid;
        internal System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.DataGridViewTextBoxColumn JPP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PAGO;
    }
}