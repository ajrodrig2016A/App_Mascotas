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
    public partial class ViewCita : ContentPage
    {
        private SQLiteAsyncConnection con;
        private ObservableCollection<Cita> tablaCita;
        public IList<Modelo.Cita> cita { get; private set; }
        public ViewCita()
        {
            InitializeComponent();
            con = DependencyService.Get<Database>().GetConnection();
            cita = new List<Modelo.Cita>();
            consulta();
        }

        public async void consulta()
        {
            var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
            var db = new SQLiteConnection(documentPath);
            db.CreateTable<Cita>();
            var registro = await con.Table<Cita>().ToListAsync();
            tablaCita = new ObservableCollection<Cita>(registro);
            MylistView.ItemsSource = tablaCita;
            base.OnAppearing();

        }

        private void btnAgregar_Clicked(object sender, EventArgs e)
        {
            Modelo.Cita x = new Modelo.Cita();
            x.idCita = -1;
            Navigation.PushAsync(new RegistrarCita(x));
        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void MylistView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Modelo.Cita seleccion = e.Item as Modelo.Cita;
            Modelo.Cita x = new Modelo.Cita();
            x = MylistView.SelectedItem as Modelo.Cita;
            if (seleccion.idCita == x.idCita)
            {
                Navigation.PushAsync(new RegistrarCita(x));
            }
        }
    }
}