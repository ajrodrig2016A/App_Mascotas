using App_Mascotas.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_Mascotas.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PutPostCliente : ContentPage
    {
        private int validacion=0;
        public PutPostCliente(Modelo.Cliente Mcliente)
        {
            InitializeComponent();
            if (Mcliente.idCliente != -1)
            {
                txtCedula.Text=Mcliente.numeroDocumento.ToString();
                txtNombre.Text=Mcliente.nombres.ToString();
                txtApellido.Text=Mcliente.apellidos.ToString();
                txtFecha.Text=Mcliente.fechaRegistro.ToString();
                txtDireccion.Text=Mcliente.direccion.ToString();
                txtemail.Text=Mcliente.email.ToString();
                validacion = 1;
            }
        }

        private async void btnOk_Clicked(object sender, EventArgs e)
        {
            if(validacion == 0)
            {
                try
                {
                    WebClient client = new WebClient();
                    var parametros = new System.Collections.Specialized.NameValueCollection();
                    parametros.Add("cedula", txtCedula.Text);
                    parametros.Add("nombres", txtNombre.Text);
                    parametros.Add("apellidos", txtApellido.Text);
                    parametros.Add("fechaRegistro", txtFecha.Text);
                    parametros.Add("direccion", txtDireccion.Text);
                    parametros.Add("email", txtemail.Text);
                    
                    client.UploadValues("http://192.168.100.39/Services_veterinaria/ws_cliente.php", "POST", parametros);
                    await DisplayAlert("Mensaje", "Ingreso Correcto", "Ok");

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Mensaje", "Error. " + ex.Message, "Ok");
                }

            }
            else
            {
                await DisplayAlert("mensaje", "editando", "Ok");
            }
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}