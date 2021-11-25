using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using App_Mascotas.Modelo;
using System.IO;

namespace App_Mascotas.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class login : ContentPage
    {
        private SQLiteAsyncConnection con;
        public login()
        {
            InitializeComponent();
            con = DependencyService.Get<Database>().GetConnection();
        }

        public static IEnumerable<Usuario> SELECT_WHERE(SQLiteConnection db,string usuario, string contra)
        {
            return db.Query<Usuario>("SELECT * FROM Usuario where usuario = ? and password = ?",usuario,contra);
        }
        private void btnIniciar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(documentPath);
                db.CreateTable<Usuario>();
                IEnumerable<Usuario> resultado = SELECT_WHERE(db, txtusuario.Text, txtcontra.Text);
                if (resultado.Count() > 0)
                {
                    Navigation.PushAsync(new MenuPrincipal());
                }
                else
                {
                    DisplayAlert("Alerta","Usuario no existe, por favor Registrese", "ok");
                }
            }
            catch (Exception ex)
            {

                DisplayAlert("Alerta", ex.Message, "ok");
            }
        }

        private void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            Modelo.Usuario x = new Modelo.Usuario();
            x.id = -1;
            Navigation.PushAsync(new RegistrarUsuario(x));   
        }
    }
}