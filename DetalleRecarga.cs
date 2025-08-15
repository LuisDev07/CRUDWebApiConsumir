using CRUDWebApiConsumir.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CRUDWebApiConsumir
{
    public partial class DetalleRecarga : Form
    {
        private Principal.RecargasDto recarga;
        // Pon esto como campo para reutilizar:
        private static readonly HttpClient http = new HttpClient();
        private const string BASE_URL = "https://localhost:7019";
        public DetalleRecarga(Principal.RecargasDto recargaSeleccionada)
        {
            InitializeComponent();
            recarga = recargaSeleccionada;
            CargarDetalle();
        }

        private void DetalleRecarga_Load(object sender, EventArgs e)
        {



            EstiloUI.RedondearBoton(btnActivar, 10);
            EstiloUI.RedondearBoton(btnCancelar, 10);
            EstiloUI.RedondearBoton(btnDesactivar, 10);
            EstiloUI.RedondearBoton(btnEliminar, 10);
            EstiloUI.RedondearBoton(Editarbtn, 10);
            if (lblEstado.Text == "Activo")
            {
                lblEstado.ForeColor = Color.Green;
            }
            else
            {
                lblEstado.ForeColor = Color.Red; // Cambiar a rojo si no es activo
            }
            CargarDetalle();

        }
        private void CargarDetalle()
        {
            lblID.Text = recarga.ID_Recarga.ToString();
            lblDescripcion.Text = recarga.descripcion;
            lblMonto.Text = recarga.monto.ToString("C");
            lblEstado.Text = recarga.estadoRecarga;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void Editarbtn_Click(object sender, EventArgs e)
        {
            using (EditarRecarga formEditar = new EditarRecarga(recarga))
            {
                if (formEditar.ShowDialog() == DialogResult.OK)
                {
                    // Actualizamos los datos en este formulario después de editar
                    recarga = formEditar.RecargaEditada;
                    CargarDetalle();
                }
            }
        }
      

        private async void btnDesactivar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que deseas desactivar esta recarga?", "Confirmar",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                var req = new HttpRequestMessage(HttpMethod.Put,
                    $"{BASE_URL}/api/ActivarDesactivar/DesactivarRecarga/{recarga.ID_Recarga}");

                var resp = await http.SendAsync(req);
                if (resp.IsSuccessStatusCode)
                {
                    MessageBox.Show("Recarga desactivada correctamente.");
                    // Cerramos el formulario y devolvemos DialogResult.OK
                    this.DialogResult = DialogResult.OK;
                    this.Close();

                }
                else
                {
                    var body = await resp.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error al desactivar: {resp.ReasonPhrase}\n{body}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al desactivar: " + ex.Message);
            }
        }



        private async void btnActivar_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("¿Seguro que deseas activar esta recarga?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                var req = new HttpRequestMessage(HttpMethod.Put,
                    $"{BASE_URL}/api/ActivarDesactivar/ActivarRecarga/{recarga.ID_Recarga}");

                var resp = await http.SendAsync(req);
                if (resp.IsSuccessStatusCode)
                {
                    MessageBox.Show("Recarga activada correctamente.");
                    // Cerramos el formulario y devolvemos DialogResult.OK
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    var body = await resp.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error al activar: {resp.ReasonPhrase}\n{body}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al activar: " + ex.Message);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("⚠️ Esta acción es permanente. ¿Seguro que deseas eliminar?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                var resp = await http.DeleteAsync(
                    $"{BASE_URL}/api/EleminarRecarga/EleminarRecarga/{recarga.ID_Recarga}"); // verifica el nombre del endpoint

                if (resp.IsSuccessStatusCode)
                {
                    MessageBox.Show("Recarga eliminada correctamente.");
                    // Cerramos el formulario y devolvemos DialogResult.OK
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    var body = await resp.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error al eliminar: {resp.ReasonPhrase}\n{body}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }
    }
}
