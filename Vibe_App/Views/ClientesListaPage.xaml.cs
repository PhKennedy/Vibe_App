using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Vibe_App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vibe_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientesListaPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public ClientesListaPage()
        {
            InitializeComponent();
            BindingContext = new ClientesListaViewModel();

        }
    }
}
