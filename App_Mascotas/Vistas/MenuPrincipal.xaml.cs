using App_Mascotas.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_Mascotas.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPrincipal : ContentPage
    {
        public IList<Mmenu> menu { get; private set; }
        public MenuPrincipal()
        {
            InitializeComponent();
            menu = new List<Mmenu>();

            menu.Add(new Mmenu
            {
                nombre = "Usuario",
                imagen = "usuario.png"
            });
            menu.Add(new Mmenu
            {
                nombre = "Cliente",
                imagen = "cliente.png"
            });
            menu.Add(new Mmenu
            {
                nombre = "Citas",
                imagen = "cita.png"
            });
            menu.Add(new Mmenu
            {
                nombre ="Mascotas" ,
                imagen = "mascotas.png"
            });
            menu.Add(new Mmenu
            {
                nombre = "Raza",
                imagen = "raza.png"
            });


            BindingContext = this;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Mmenu seleccion=e.Item as Mmenu;
            if (seleccion.nombre == "Cliente")
            {
                Navigation.PushAsync(new ViewCliente());
            }
            else if (seleccion.nombre == "Raza")
            {
                Navigation.PushAsync(new ViewRaza());
            }
            else if (seleccion.nombre == "Mascotas")
            {
                Navigation.PushAsync(new ViewMascota());
            }
            else if (seleccion.nombre == "Citas")
            {
                Navigation.PushAsync(new ViewCita());
            }
            else if (seleccion.nombre == "Usuario")
            {
                Navigation.PushAsync(new ViewUsuario());
            }

        }
    }
}