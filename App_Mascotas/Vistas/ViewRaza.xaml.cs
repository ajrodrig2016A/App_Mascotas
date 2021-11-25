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
    public partial class ViewRaza : ContentPage
    {
        private SQLiteAsyncConnection con;
        private ObservableCollection<Raza> tablaRaza;
        public IList<Modelo.Raza> Mraza { get; private set; }
        public ViewRaza()
        {
            InitializeComponent();
            con = DependencyService.Get<Database>().GetConnection();
            Mraza = new List<Modelo.Raza>();
            consulta();
        }

        public async void consulta()
        {
            var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
            var db = new SQLiteConnection(documentPath);
            db.CreateTable<Raza>();
            var registro = await con.Table<Raza>().ToListAsync();
            tablaRaza = new ObservableCollection<Raza>(registro);
            MylistView.ItemsSource = tablaRaza;
            base.OnAppearing();

        }
        private void btnAgregar_Clicked(object sender, EventArgs e)
        {
            Modelo.Raza x = new Modelo.Raza();
            x.id = -1;
            Navigation.PushAsync(new RegistrarRaza(x));
        }

        private void MylistView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Modelo.Raza seleccion = e.Item as Modelo.Raza;
            Modelo.Raza x = new Modelo.Raza();
            x = MylistView.SelectedItem as Modelo.Raza;
            if (seleccion.nombre == x.nombre)
            {
                Navigation.PushAsync(new RegistrarRaza(x));
            }
        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}