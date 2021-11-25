using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class ViewClient : ContentPage
    {
        private SQLiteAsyncConnection con;
        private ObservableCollection<Cliente> tablaCliente;
        public IList<Modelo.Cliente> Mcliente { get; private set; }
        public ViewClient()
        {
            InitializeComponent();
            con = DependencyService.Get<Database>().GetConnection();
            Mcliente = new List<Modelo.Cliente>();
            consulta();
        }

        public async void consulta()
        {
            var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
            var db = new SQLiteConnection(documentPath);
            db.CreateTable<Cliente>();
            var registro = await con.Table<Cliente>().ToListAsync();
            tablaCliente = new ObservableCollection<Cliente>(registro);
            MylistView.ItemsSource = tablaCliente;
            base.OnAppearing();

        }
        private void btnAgregar_Clicked(object sender, EventArgs e)
        {
            Modelo.Cliente x = new Modelo.Cliente();
            x.idCliente = -1;
            Navigation.PushAsync(new RegistrarCliente(x));
        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void MylistView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Modelo.Cliente seleccion = e.Item as Modelo.Cliente;
            Modelo.Cliente x = new Modelo.Cliente();
            x = MylistView.SelectedItem as Modelo.Cliente;
            if (seleccion.nombres == x.nombres)
            {
                Navigation.PushAsync(new RegistrarCliente(x));
            }
        }
    }
}