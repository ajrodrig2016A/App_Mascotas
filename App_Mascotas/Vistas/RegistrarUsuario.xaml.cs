using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_Mascotas.Modelo;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_Mascotas.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarUsuario : ContentPage
    {
        private SQLiteAsyncConnection con;
        
        private int validacion = 0;
        public string texto;
        public int id;
        IEnumerable<Usuario> ResultadoUpdate;
        IEnumerable<Usuario> ResultadoDelete;

        public RegistrarUsuario(Modelo.Usuario usuario)
        {
            InitializeComponent();
            con =DependencyService.Get<Database>().GetConnection();
            if (usuario.id > -1)
            {
                txtContrasenia.Text = usuario.password;
                txtNombre.Text = usuario.nombre;
                txtUsuario.Text = usuario.usuario;
                id = usuario.id;
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

        public static IEnumerable<Usuario> DELETE(SQLiteConnection db,int id)
        {
            return db.Query<Usuario>("DELETE FROM Usuario where id = ?", id);
        }

        public static IEnumerable<Usuario> UPDATE(SQLiteConnection db, string nombre, string usuario,
                                                    string contra, int id)
        {
            return db.Query<Usuario>("UPDATE Usuario SET nombre = ?, usuario = ?, password = ? where id = ?", nombre,usuario,contra, id);
        }

        
        private void btnAgregar_Clicked(object sender, EventArgs e)
        {
            if(validacion == 1) {
                var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(documentPath);
                ResultadoUpdate = UPDATE(db, txtNombre.Text, txtUsuario.Text, txtContrasenia.Text,id);
                DisplayAlert("Alerta", "Datos Actualizados", "Ok");
                db.Close();
                Navigation.PopAsync();
                Navigation.PopAsync();
                Navigation.PushAsync(new ViewUsuario());
            }
            else
            {
                try
                {
                    var Registros = new Usuario { nombre = txtNombre.Text, usuario = txtUsuario.Text, password = txtContrasenia.Text };
                    con.InsertAsync(Registros);
                    DisplayAlert("Alerta", "Dato Ingresado", "ok");
                    txtNombre.Text = "";
                    txtUsuario.Text = "";
                    txtContrasenia.Text = "";
                }
                catch (Exception ex)
                {
                    DisplayAlert("Alerta", "Error: "+ex.Message, "ok");

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
                Navigation.PushAsync(new ViewUsuario());
            }
            catch (Exception ex)
            {

                DisplayAlert("Alerta", "Error: " + ex.Message, "ok");
            }
        }
    }
}