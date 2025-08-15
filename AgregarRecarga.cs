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
    public partial class AgregarRecarga : Form
    {
        public AgregarRecarga()
        {
            InitializeComponent();
        }

        private void AgregarRecarga_Load(object sender, EventArgs e)
        {
            EstiloUI.RedondearBoton(btnGuardar, 10);
            EstiloUI.RedondearBoton(btnCancelar, 10);
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            string descripcion = txtDescripcion.Text.Trim();
            decimal monto = numMonto.Value;

            if (string.IsNullOrWhiteSpace(descripcion) || monto <= 0)
            {
                MessageBox.Show("Ingrese una descripción valida y un monto mayor a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var nuevaRecarga = new
            {
                descripcion = descripcion,
                monto = monto
            };

            using (HttpClient client = new HttpClient()) ///api/AgregarRecargas/AgregarRecarga
            {
                var response = await client.PostAsJsonAsync("https://localhost:7019/api/AgregarRecargas/AgregarRecarga", nuevaRecarga);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Recarga agregada correctamente." ,"Exito",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    this.Close();

                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error al agregar: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }



        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
