using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class ViewUsuario : ContentPage
    {
        private SQLiteAsyncConnection con;
        private ObservableCollection <Usuario> tablaUsuario;
        public IList<Modelo.Usuario> Mcliente { get; private set; }
        public ViewUsuario()
        {
            InitializeComponent();
            con = DependencyService.Get<Database>().GetConnection();
            Mcliente = new List<Modelo.Usuario>();
            consulta();
        }

        public async void consulta()
        {
            var registro=await con.Table<Usuario>().ToListAsync();
            tablaUsuario=new ObservableCollection<Usuario>(registro);
            MylistView.ItemsSource = tablaUsuario;
            base.OnAppearing();

        }
        private void btnAgregar_Clicked(object sender, EventArgs e)
        {

            Modelo.Usuario x = new Modelo.Usuario();
            x.id = -1;
            Navigation.PushAsync(new RegistrarUsuario(x));
        }

        private void MylistView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Modelo.Usuario seleccion = e.Item as Modelo.Usuario;
            Modelo.Usuario x = new Modelo.Usuario();
            x = MylistView.SelectedItem as Modelo.Usuario;
            if (seleccion.nombre == x.nombre)
            {
                Navigation.PushAsync(new RegistrarUsuario(x));
            }
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}