using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vibe_App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vibe_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClienteDetalhesPage : ContentPage
    {
        public ClienteDetalhesPage()
        {
            InitializeComponent();
            BindingContext = new ClienteDetalhesViewModel();
        }
    }
}