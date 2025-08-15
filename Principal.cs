using CRUDWebApiConsumir.models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows.Forms;

namespace CRUDWebApiConsumir
{
    public partial class Principal : Form
    {
        //creamos una lista de recargas donde se almacenaran  los elementos extraidos de la api 
        private List<RecargasDto> recargas = new List<RecargasDto>();

        // creamos una clase para mapear o extraer los datos de la api 
        public class ApiResponse
        {
            public string mensaje { get; set; }
            public List<RecargasDto> data { get; set; }
        }

        // Extraer los Dto que sean identiticos a los de la api 
        public class RecargasDto
        {
            public int ID_Recarga { get; set; }
            public string descripcion { get; set; }
            public decimal monto { get; set; }
            public string estadoRecarga { get; set; }
        }
        public Principal()
        {
           
            InitializeComponent();
        }
        private void Principal_Load(object sender, EventArgs e)
        {
            EstiloUI.RedondearBoton(btnActualizar, 10);
            EstiloUI.RedondearBoton(btnRegistrar, 10);
            CargarCatalogo();

            //inicializamos con datagridview para agregarle un boton 
            dataGridView1.DataSource = recargas;

            // Verifica si ya existe la columna de botón para no duplicarla
            if (!dataGridView1.Columns.Contains("btnMostrarMas"))
            {
                DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
                btnColumn.HeaderText = "Detalles";
                btnColumn.Name = "btnMostrarMas";
                btnColumn.Text = "Ver Detalles";
                btnColumn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btnColumn);
            }
            dataGridView1.RowHeadersVisible = false;


            // Cambiar ancho
            dataGridView1.Columns["ID_Recarga"].Width = 30;
            dataGridView1.Columns["Descripcion"].Width = 320;
            dataGridView1.Columns["Monto"].Width = 60;
            dataGridView1.Columns["EstadoRecarga"].Width = 70;
            dataGridView1.Columns["btnMostrarMas"].Width = 100;

            // Estilo moderno por columna
            dataGridView1.Columns["ID_Recarga"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#E9ECEF"); // Gris claro
            dataGridView1.Columns["ID_Recarga"].DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#212529"); // Gris oscuro

            dataGridView1.Columns["Descripcion"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FFF3CD"); // Amarillo suave
            dataGridView1.Columns["Descripcion"].DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#212529");

            dataGridView1.Columns["Monto"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#D1ECF1"); // Celeste suave
            dataGridView1.Columns["Monto"].DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#0C5460"); // Azul oscuro

            dataGridView1.CellFormatting += dataGridView1_CellFormatting;

        }


        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return; // Ignorar encabezados

            var columnName = dataGridView1.Columns[e.ColumnIndex].Name;


            var btnColumn = (DataGridViewButtonColumn)dataGridView1.Columns["btnMostrarMas"];
            btnColumn.DefaultCellStyle.BackColor = Color.DarkCyan; // fondo del botón
            btnColumn.DefaultCellStyle.ForeColor = Color.White;     // color del texto

            btnColumn.DefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);



            if (columnName.Equals("EstadoRecarga", StringComparison.OrdinalIgnoreCase) && e.Value != null)
            {
                string valor = e.Value.ToString().Trim();

                if (valor.Equals("Activo", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.BackColor = ColorTranslator.FromHtml("#28A745"); // Verde
                    e.CellStyle.ForeColor = Color.White;
                }
                else if (valor.Equals("Inactivo", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.BackColor = ColorTranslator.FromHtml("#DC3545"); // Rojo
                    e.CellStyle.ForeColor = Color.White;
                }
            }

        }

        //Metodo utilizado para traer todos los catalogos de Nuestra Api 
        private async void CargarCatalogo()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // URL DE LA NUESTRA API
                    string url = "https://localhost:7019/api/MostrarCatalogo/ObtenerCatalogo";

                    // Llamada a la API y deserialización
                    var response = await client.GetFromJsonAsync<ApiResponse>(url);

                    if (response != null)
                    {
                        // Si hay datos, asignarlos al DataGridView
                        if (response.data != null)
                        {
                            recargas = response.data;
                            dataGridView1.DataSource = recargas;
                        }

                        // Mostrar mensaje en texto plano
                        if (!string.IsNullOrEmpty(response.mensaje))
                        {
                            try
                            {
                                // Intentar deserializar mensaje JSON
                                var msgObj = JsonSerializer.Deserialize<ApiResponse>(response.mensaje);
                                MessageBox.Show(msgObj?.mensaje ?? "No hay mensaje");
                            }
                            catch
                            {
                                // Si no es JSON, mostrar tal cual
                                lblmensaje.Text=(response.mensaje);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No hay datos");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener datos: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica que no sea el header
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["btnMostrarMas"].Index)
            {
               
                var recargaSeleccionada = (RecargasDto)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                DetalleRecarga detalle = new DetalleRecarga (recargaSeleccionada);
                detalle.ShowDialog(); // Abre modal
                CargarCatalogo();


            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarCatalogo();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            AgregarRecarga frm = new AgregarRecarga();
            frm.ShowDialog(); // Modal para que espere a cerrarse
            CargarCatalogo();
        }

       
    }
}
