namespace CrearPDFPorCuadrante
{
    partial class FormPDFCuadrante
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
            this.components = new System.ComponentModel.Container();
            this.lMensaje = new System.Windows.Forms.Label();
            this.ButtonSubmit = new System.Windows.Forms.Button();
            this.ButtonSeleccionaCarpeta = new System.Windows.Forms.Button();
            this.labelCuadrante = new System.Windows.Forms.Label();
            this.labelProyecto = new System.Windows.Forms.Label();
            this.Error = new System.Windows.Forms.ErrorProvider(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.labelRutaGuardar = new System.Windows.Forms.Label();
            this.labelCarpetaSeleccionada = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.checkBoxEsTrineo = new System.Windows.Forms.CheckBox();
            this.labelTipo = new System.Windows.Forms.Label();
            this.comboBoxCuadrante = new System.Windows.Forms.ComboBox();
            this.comboBoxProyectos = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.backgroundWorkerGenerarPDF = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.Error)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lMensaje
            // 
            this.lMensaje.AutoSize = true;
            this.lMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lMensaje.ForeColor = System.Drawing.Color.Lime;
            this.lMensaje.Location = new System.Drawing.Point(170, 186);
            this.lMensaje.Name = "lMensaje";
            this.lMensaje.Size = new System.Drawing.Size(0, 20);
            this.lMensaje.TabIndex = 20;
            // 
            // ButtonSubmit
            // 
            this.ButtonSubmit.Location = new System.Drawing.Point(23, 284);
            this.ButtonSubmit.Name = "ButtonSubmit";
            this.ButtonSubmit.Size = new System.Drawing.Size(109, 23);
            this.ButtonSubmit.TabIndex = 19;
            this.ButtonSubmit.Text = "Generar PDF";
            this.ButtonSubmit.UseVisualStyleBackColor = true;
            this.ButtonSubmit.Click += new System.EventHandler(this.ButtonSubmit_Click);
            // 
            // ButtonSeleccionaCarpeta
            // 
            this.ButtonSeleccionaCarpeta.Location = new System.Drawing.Point(21, 225);
            this.ButtonSeleccionaCarpeta.Name = "ButtonSeleccionaCarpeta";
            this.ButtonSeleccionaCarpeta.Size = new System.Drawing.Size(111, 23);
            this.ButtonSeleccionaCarpeta.TabIndex = 18;
            this.ButtonSeleccionaCarpeta.Text = "Seleccionar carpeta";
            this.ButtonSeleccionaCarpeta.UseVisualStyleBackColor = true;
            this.ButtonSeleccionaCarpeta.Click += new System.EventHandler(this.ButtonSeleccionaCarpeta_Click);
            // 
            // labelCuadrante
            // 
            this.labelCuadrante.AutoSize = true;
            this.labelCuadrante.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCuadrante.Location = new System.Drawing.Point(20, 150);
            this.labelCuadrante.Name = "labelCuadrante";
            this.labelCuadrante.Size = new System.Drawing.Size(55, 16);
            this.labelCuadrante.TabIndex = 17;
            this.labelCuadrante.Text = "Cuadrante";
            // 
            // labelProyecto
            // 
            this.labelProyecto.AutoSize = true;
            this.labelProyecto.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProyecto.Location = new System.Drawing.Point(20, 48);
            this.labelProyecto.Name = "labelProyecto";
            this.labelProyecto.Size = new System.Drawing.Size(49, 16);
            this.labelProyecto.TabIndex = 16;
            this.labelProyecto.Text = "Proyecto";
            // 
            // Error
            // 
            this.Error.ContainerControl = this;
            // 
            // labelRutaGuardar
            // 
            this.labelRutaGuardar.AutoSize = true;
            this.labelRutaGuardar.Location = new System.Drawing.Point(20, 206);
            this.labelRutaGuardar.Name = "labelRutaGuardar";
            this.labelRutaGuardar.Size = new System.Drawing.Size(80, 13);
            this.labelRutaGuardar.TabIndex = 26;
            this.labelRutaGuardar.Text = "Ruta a Guardar";
            // 
            // labelCarpetaSeleccionada
            // 
            this.labelCarpetaSeleccionada.AutoSize = true;
            this.labelCarpetaSeleccionada.Location = new System.Drawing.Point(21, 254);
            this.labelCarpetaSeleccionada.Name = "labelCarpetaSeleccionada";
            this.labelCarpetaSeleccionada.Size = new System.Drawing.Size(152, 13);
            this.labelCarpetaSeleccionada.TabIndex = 25;
            this.labelCarpetaSeleccionada.Text = "Ninguna carpeta seleccionada";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 32);
            this.label4.TabIndex = 24;
            this.label4.Text = "PDF Cuadrante\r\nGenerar PDF";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(317, 45);
            this.splitter1.TabIndex = 23;
            this.splitter1.TabStop = false;
            // 
            // checkBoxEsTrineo
            // 
            this.checkBoxEsTrineo.AutoSize = true;
            this.checkBoxEsTrineo.Location = new System.Drawing.Point(23, 122);
            this.checkBoxEsTrineo.Name = "checkBoxEsTrineo";
            this.checkBoxEsTrineo.Size = new System.Drawing.Size(79, 17);
            this.checkBoxEsTrineo.TabIndex = 22;
            this.checkBoxEsTrineo.Text = "¿Es trineo?";
            this.checkBoxEsTrineo.UseVisualStyleBackColor = true;
            this.checkBoxEsTrineo.CheckedChanged += new System.EventHandler(this.checkBoxEsTrineo_CheckedChanged);
            // 
            // labelTipo
            // 
            this.labelTipo.AutoSize = true;
            this.labelTipo.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTipo.Location = new System.Drawing.Point(20, 102);
            this.labelTipo.Name = "labelTipo";
            this.labelTipo.Size = new System.Drawing.Size(30, 16);
            this.labelTipo.TabIndex = 21;
            this.labelTipo.Text = "Tipo";
            // 
            // comboBoxCuadrante
            // 
            this.comboBoxCuadrante.DisplayMember = "NombreCuadrante";
            this.comboBoxCuadrante.FormattingEnabled = true;
            this.comboBoxCuadrante.Location = new System.Drawing.Point(23, 169);
            this.comboBoxCuadrante.Name = "comboBoxCuadrante";
            this.comboBoxCuadrante.Size = new System.Drawing.Size(212, 21);
            this.comboBoxCuadrante.TabIndex = 15;
            this.comboBoxCuadrante.ValueMember = "CuadranteID";
            // 
            // comboBoxProyectos
            // 
            this.comboBoxProyectos.DisplayMember = "NombreProyecto";
            this.comboBoxProyectos.FormattingEnabled = true;
            this.comboBoxProyectos.Location = new System.Drawing.Point(21, 67);
            this.comboBoxProyectos.Name = "comboBoxProyectos";
            this.comboBoxProyectos.Size = new System.Drawing.Size(214, 21);
            this.comboBoxProyectos.TabIndex = 27;
            this.comboBoxProyectos.ValueMember = "ProyectoID";
            this.comboBoxProyectos.SelectedIndexChanged += new System.EventHandler(this.comboBoxProyectos_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CrearPDFPorCuadrante.Resource.logo;
            this.pictureBox1.InitialImage = global::CrearPDFPorCuadrante.Resource.logo;
            this.pictureBox1.Location = new System.Drawing.Point(111, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(206, 45);
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
            // 
            // backgroundWorkerGenerarPDF
            // 
            this.backgroundWorkerGenerarPDF.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerGenerarPDF_DoWork);
            this.backgroundWorkerGenerarPDF.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerGenerarPDF_ProgressChanged);
            this.backgroundWorkerGenerarPDF.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerGenerarPDF_RunWorkerCompleted);
            // 
            // FormPDFCuadrante
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(317, 321);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.comboBoxProyectos);
            this.Controls.Add(this.lMensaje);
            this.Controls.Add(this.ButtonSubmit);
            this.Controls.Add(this.ButtonSeleccionaCarpeta);
            this.Controls.Add(this.labelCuadrante);
            this.Controls.Add(this.labelProyecto);
            this.Controls.Add(this.comboBoxCuadrante);
            this.Controls.Add(this.labelRutaGuardar);
            this.Controls.Add(this.labelCarpetaSeleccionada);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.checkBoxEsTrineo);
            this.Controls.Add(this.labelTipo);
            this.MaximizeBox = false;
            this.Name = "FormPDFCuadrante";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FormPDFCuadrante_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Error)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lMensaje;
        private System.Windows.Forms.Button ButtonSubmit;
        private System.Windows.Forms.Button ButtonSeleccionaCarpeta;
        private System.Windows.Forms.Label labelCuadrante;
        private System.Windows.Forms.Label labelProyecto;
        private System.Windows.Forms.ErrorProvider Error;
        private System.Windows.Forms.ComboBox comboBoxProyectos;
        private System.Windows.Forms.ComboBox comboBoxCuadrante;
        private System.Windows.Forms.Label labelRutaGuardar;
        private System.Windows.Forms.Label labelCarpetaSeleccionada;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.CheckBox checkBoxEsTrineo;
        private System.Windows.Forms.Label labelTipo;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorkerGenerarPDF;
    }
}

