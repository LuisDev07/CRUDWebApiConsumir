using CRUDWebApiConsumir.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDWebApiConsumir
{
    public partial class EditarRecarga : Form
    {
        public Principal.RecargasDto RecargaEditada { get; private set; }
       

        public EditarRecarga(Principal.RecargasDto recargaSeleccionada)
        {
            InitializeComponent();
            // Mostrar valores actuales
            lblID.Text = recargaSeleccionada.ID_Recarga.ToString();
            lblEstado.Text = recargaSeleccionada.estadoRecarga;

            lblDescripcion.Text = recargaSeleccionada.descripcion;
            lblMonto.Text = recargaSeleccionada.monto.ToString();

            RecargaEditada = recargaSeleccionada;
        }

       
        private void EditarRecarga_Load(object sender, EventArgs e)
        {
            EstiloUI.RedondearBoton(btnGuardar, 10);
            EstiloUI.RedondearBoton(btnCancelar, 10);
        }
        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            string descripcion = txtDescripcion.Text.Trim();
            bool descripcionValida = !string.IsNullOrEmpty(descripcion);

            decimal montoActualizado;
            bool montoValido = decimal.TryParse(txtMonto.Text, out montoActualizado);

            // Si el monto es cero, considerarlo como no ingresado
            if (montoValido && montoActualizado == 0)
            {
                montoValido = false;
            }

            // Validar que al menos uno tenga valor
            if (!descripcionValida && !montoValido)
            {
                MessageBox.Show("Ingrese al menos una descripción o un monto válido.");
                return;
            }

           
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"https://localhost:7019/api/ActualizarRecarga/ActualizarRecarga/{RecargaEditada.ID_Recarga}";

                    // Construir request dinámicamente según los campos válidos
                    // Construir request dinámico según los campos ingresados
                    var requestBody = new Dictionary<string, object>();
                    if (descripcionValida)
                        requestBody.Add("descripcion", descripcion);

                    if (montoValido)
                        requestBody.Add("monto", montoActualizado);


                    var response = await client.PutAsJsonAsync(url, requestBody);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Recarga actualizada correctamente");

                        // Actualizar solo los campos enviados
                        if (descripcionValida)
                            RecargaEditada.descripcion = descripcion;

                        if (montoValido)
                            RecargaEditada.monto = montoActualizado;

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("Error al actualizar: " + error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        private void btnCancelar_Click(object sender, EventArgs e)
        {
            
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
