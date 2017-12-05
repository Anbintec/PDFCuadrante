using CrearPDFPorCuadranteLibrary.Controller.DAONumeroOrden;
using CrearPDFPorCuadranteLibrary.Controller.DAOPDF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrearPDFPorCuadrante
{
    public partial class FormPDFCuadrante : Form
    {
        private static FolderBrowserDialog folderBrowserdialog = new FolderBrowserDialog();
      
        object ProyectoSeleccionado = new object();
        object CuadranteSeleccionado = new object();
        bool esTrineo = false;
        string url = "";
        private FormLoading _waitForm;


        public FormPDFCuadrante()
        {
            InitializeComponent();
        }

        private void FormPDFCuadrante_Load(object sender, EventArgs e)
        {
           
            comboBoxProyectos.DataSource = DAOPDF.Instance.ObtieneProyectos();
        }

      

        private void comboBoxProyectos_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxCuadrante.Text = "";
            if ((int)comboBoxProyectos.SelectedValue != 0)
            {
                if (checkBoxEsTrineo.Checked)
                   comboBoxCuadrante.DataSource= DAOPDF.Instance.ObtieneCuadrantes((int)comboBoxProyectos.SelectedValue,1);
                else
                    comboBoxCuadrante.DataSource = DAOPDF.Instance.ObtieneCuadrantes((int)comboBoxProyectos.SelectedValue, 0);
                
            }
        }

        private void checkBoxEsTrineo_CheckedChanged(object sender, EventArgs e)
        {
            if ((int)comboBoxProyectos.SelectedValue > 0)
            {
                comboBoxCuadrante.DataSource = DAOPDF.Instance.ObtieneCuadrantes((int)comboBoxProyectos.SelectedValue, checkBoxEsTrineo.Checked ? 1:0);
            }
        }

        private void ButtonSeleccionaCarpeta_Click(object sender, EventArgs e)
        {
            if (folderBrowserdialog.ShowDialog() == DialogResult.OK)
            {
                labelCarpetaSeleccionada.Text = folderBrowserdialog.SelectedPath;
            }
        }

        private void ButtonSubmit_Click(object sender, EventArgs e)
        {
          
            this.ProyectoSeleccionado = comboBoxProyectos.SelectedItem;
            this.CuadranteSeleccionado = comboBoxCuadrante.SelectedItem;
            this.esTrineo = checkBoxEsTrineo.Checked;
            this.url = folderBrowserdialog.SelectedPath;
            backgroundWorkerGenerarPDF.RunWorkerAsync();
            
        }

        private void ShowFormLoading()
        {
            _waitForm = new FormLoading();
            _waitForm.SetMessage(""); // "Loading data. Please wait..."
            _waitForm.TopMost = true;
            _waitForm.StartPosition = FormStartPosition.CenterScreen;
            
            _waitForm.Show();
            _waitForm.Refresh();
        }
        private void IniciaProceso()
        {
            string mensaje = "";
            
            if (!DAONumeroOrden.Instance.CrearPFDPorCuadrante(ProyectoSeleccionado, CuadranteSeleccionado, esTrineo, url, out mensaje))
                MessageBox.Show(mensaje);
            else
                MessageBox.Show("Se genero el archivo con " + mensaje + " travelers", "Atención", MessageBoxButtons.OK);
            _waitForm.Close();
        }

        private void backgroundWorkerGenerarPDF_DoWork(object sender, DoWorkEventArgs e)
        {
            ShowFormLoading();
            IniciaProceso();
        }

        private void backgroundWorkerGenerarPDF_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorkerGenerarPDF_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("The task has been cancelled");
            }
            else if (e.Error != null)
            {
                MessageBox.Show("Error. Details: " + (e.Error as Exception).ToString());
            }
            else
            {
                //MessageBox.Show("The task has been completed. Results: " + e.Result.ToString());
                //MessageBox.Show("Se termino el proceso");
               
            }

        }
    }
}
