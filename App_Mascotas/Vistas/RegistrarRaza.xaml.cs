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
    public partial class RegistrarRaza : ContentPage
    {
        private SQLiteAsyncConnection con;

        private int validacion = 0;
        public string texto;
        public int id;
        IEnumerable<Raza> ResultadoUpdate;
        IEnumerable<Raza> ResultadoDelete;
        public RegistrarRaza(Raza raza)
        {
            InitializeComponent();
            con = DependencyService.Get<Database>().GetConnection();
            if (raza.id > -1)
            {
                txtCaracter.Text = raza.caracteristicas;
                txtNombre.Text = raza.nombre;
                id = raza.id;
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

        public static IEnumerable<Raza> DELETE(SQLiteConnection db, int id)
        {
            return db.Query<Raza>("DELETE FROM Raza where id = ?", id);
        }

        public static IEnumerable<Raza> UPDATE(SQLiteConnection db, string nombre, string caracter,
                                                    int id)
        {
            return db.Query<Raza>("UPDATE Raza SET nombre = ?, caracteristicas = ? where id = ?", nombre, caracter, id);
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
                Navigation.PushAsync(new ViewRaza());
            }
            catch (Exception ex)
            {

                DisplayAlert("Alerta", "Error: " + ex.Message, "ok");
            }
        }

        private void btnAgregar_Clicked(object sender, EventArgs e)
        {
            if (validacion == 1)
            {
                var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(documentPath);
                ResultadoUpdate = UPDATE(db, txtNombre.Text, txtCaracter.Text, id);
                DisplayAlert("Alerta", "Datos Actualizados", "Ok");
                db.Close();
                Navigation.PopAsync();
                Navigation.PopAsync();
                Navigation.PushAsync(new ViewRaza());
            }
            else
            {
                try
                {
                    var Registros = new Raza { nombre = txtNombre.Text, caracteristicas = txtCaracter.Text };
                    con.InsertAsync(Registros);
                    DisplayAlert("Alerta", "Dato Ingresado", "ok");
                    txtNombre.Text = "";
                    txtCaracter.Text = "";
                }
                catch (Exception ex)
                {
                    DisplayAlert("Alerta", "Error: " + ex.Message, "ok");

                }
            }
        }
    }
}