using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using App_Mascotas.Modelo;
using Newtonsoft.Json;
using RestSharp;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace App_Mascotas.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewCliente : ContentPage
    {
        private const string Url = "http://192.168.200.7/doctorvetWebApi/postCliente.php";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<Cliente> _post;
        public Cliente PreviousSelectedCustomer { get; set; }
        private Cliente _selectedCustomer { get; set; }
        public Cliente SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    ExpandOrCollapseSelectedItem();
                }
            }
        }
        public ViewCliente()
        {
            InitializeComponent();
            GetDatos();
        }
        private async void GetDatos()
        {
            var content = await client.GetStringAsync(Url);
            List<Cliente> posts = JsonConvert.DeserializeObject<List<Cliente>>(content);
            _post = new ObservableCollection<Cliente>(posts);

            clienteListView.ItemsSource = _post;
        }

        private void ExpandOrCollapseSelectedItem()
        {
            if (PreviousSelectedCustomer != null)
            {
                _post.Where(t => t.idCliente == PreviousSelectedCustomer.idCliente).FirstOrDefault().IsVisible =
                false;
            }

            _post.Where(t => t.idCliente == SelectedCustomer.idCliente).FirstOrDefault().IsVisible =
                true;
            PreviousSelectedCustomer = SelectedCustomer;
        }

        private void btnAgregar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegistrarCliente());
        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuPrincipal());
        }

        private void clienteListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var objDatos = ((Xamarin.Forms.ListView)sender).SelectedItem as Cliente;

            if (objDatos == null)
            {
                return;
            }
            SelectedCustomer = objDatos;
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            Cliente datos = SelectedCustomer;

            if (datos == null)
            {
                await DisplayAlert("Mensaje informativo", "Favor seleccione un cliente del ListView", "OK");
                return;
            }
            await Navigation.PushAsync(new ActualizarCliente(datos));
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                Cliente datos = SelectedCustomer;

                if (datos == null)
                {
                    await DisplayAlert("Mensaje informativo", "Favor seleccione un cliente del ListView", "OK");
                    return;
                }

                var action = await DisplayAlert("Mensaje de confirmación", "Usted está seguro de eliminar el registro", "Aceptar", "Cancelar");

                if (action.Equals(true))
                {
                    var client = new RestClient("http://192.168.200.7/doctorvetWebApi/postCliente.php?idCliente=" + datos.idCliente);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.DELETE);
                    request.AddHeader("Accept", "application/json");
                    var body = @"";
                    request.AddParameter("text/plain", body, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);

                    if (response.StatusCode.Equals(HttpStatusCode.OK))
                    {
                        await DisplayAlert("Administracion de Clientes", "Cliente correctamente eliminado.", "OK");
                        GetDatos();
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }
    }
}