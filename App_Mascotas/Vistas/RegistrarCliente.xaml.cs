using App_Mascotas.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace App_Mascotas.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarCliente : ContentPage
    {
        private SQLiteAsyncConnection con;

        private int validacion = 0;
        public string texto;
        public int id;
        IEnumerable<Cliente> ResultadoUpdate;
        IEnumerable<Cliente> ResultadoDelete;
        public RegistrarCliente(Cliente cliente)
        {
            InitializeComponent();
            con = DependencyService.Get<Database>().GetConnection();
            if (cliente.idCliente > -1)
            {
                txtCedula.Text = cliente.cedula;
                txtNombres.Text = cliente.nombres;
                txtApellidos.Text = cliente.apellidos;
                txtFecha.Text = cliente.fechaRegistro.ToString();
                txtDireccion.Text = cliente.direccion;
                txtEmail.Text = cliente.email;

                id = cliente.idCliente;
                validacion = 1;
                btnAgregar.Text = "Modificar";
                btnEliminar.IsVisible = true;
            }
            else
            {
                btnAgregar.Text = "Agregar";
                btnEliminar.IsVisible = false;
            }
        }

        private void dtFecha_DateSelected(object sender, DateChangedEventArgs e)
        {
            txtFecha.Text = dtFecha.Date.ToString("MMM dd, yyyy");
        }

        public static IEnumerable<Cliente> DELETE(SQLiteConnection db, int id)
        {
            return db.Query<Cliente>("DELETE FROM Cliente where idCliente = ?", id);
        }

        public static IEnumerable<Cliente> UPDATE(SQLiteConnection db, string cedula, string nombres, string apellidos,
                                                    DateTime fecha, string direccion, string email, int id)
        {
            return db.Query<Cliente>("UPDATE Cliente SET cedula = ?, nombres = ?, apellidos = ?, fechaRegistro = ?," +
                "direccion = ?, email = ? where idCliente = ?", cedula, nombres, apellidos, fecha, direccion, email, id);
        }

        private void btnAgregar_Clicked(object sender, EventArgs e)
        {
            if (validacion == 1)
            {
                var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(documentPath);
                ResultadoUpdate = UPDATE(db, txtCedula.Text, txtNombres.Text,txtApellidos.Text,Convert.ToDateTime(txtFecha.Text), txtDireccion.Text, txtEmail.Text, id);
                DisplayAlert("Alerta", "Datos Actualizados", "Ok");
                db.Close();
                Navigation.PopAsync();
                Navigation.PopAsync();
                Navigation.PushAsync(new ViewClient());
            }
            else
            {
                try
                {
                    var Registros = new Cliente { cedula = txtCedula.Text, nombres = txtNombres.Text, apellidos=txtApellidos.Text, fechaRegistro = Convert.ToDateTime(txtFecha.Text), direccion =txtDireccion.Text, email=txtEmail.Text };
                    con.InsertAsync(Registros);
                    DisplayAlert("Alerta", "Dato Ingresado", "ok");
                    txtCedula.Text = "";
                    txtNombres.Text = "";
                    txtApellidos.Text = "";
                    txtFecha.Text = "";
                    txtDireccion.Text = "";
                    txtEmail.Text = "";
                }
                catch (Exception ex)
                {
                    DisplayAlert("Alerta", "Error: " + ex.Message, "ok");

                }
            }
        }

        private void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(documentPath);
                ResultadoDelete = DELETE(db, id);
                DisplayAlert("Alerta", "Datos Eliminados", "Ok");
                db.Close();
                Navigation.PopAsync();
                Navigation.PopAsync();
                Navigation.PushAsync(new ViewClient());
            }
            catch (Exception ex)
            {

                DisplayAlert("Alerta", "Error: " + ex.Message, "ok");
            }
        }
    }
}