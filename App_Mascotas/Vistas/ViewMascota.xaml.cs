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
    public partial class ViewMascota : ContentPage
    {
        private SQLiteAsyncConnection con;
        private ObservableCollection<Mascota> tablaMascota;
        public IList<Modelo.Mascota> mascota { get; private set; }
        public ViewMascota()
        {
            InitializeComponent();
            con = DependencyService.Get<Database>().GetConnection();
            mascota = new List<Modelo.Mascota>();
            consulta();
        }

        public async void consulta()
        {
            var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
            var db = new SQLiteConnection(documentPath);
            db.CreateTable<Mascota>();
            var registro = await con.Table<Mascota>().ToListAsync();
            tablaMascota = new ObservableCollection<Mascota>(registro);
            MylistView.ItemsSource = tablaMascota;
            base.OnAppearing();

        }

        private void btnAgregar_Clicked(object sender, EventArgs e)
        {
            Modelo.Mascota x = new Modelo.Mascota();
            x.idMascota = -1;
            Navigation.PushAsync(new RegistrarMascota(x));
        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void MylistView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Modelo.Mascota seleccion = e.Item as Modelo.Mascota;
            Modelo.Mascota x = new Modelo.Mascota();
            x = MylistView.SelectedItem as Modelo.Mascota;
            if (seleccion.nombre == x.nombre)
            {
                Navigation.PushAsync(new RegistrarMascota(x));
            }
        }
    }
}