using ClientConvertisseurV1.Model;
using ClientConvertisseurV1.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ClientConvertisseurV1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static WSService wsService;

        public MainPage()
        {
            this.InitializeComponent();

            wsService = WSService.GetInstance();
            ActionGetData();
        }


        /// <summary>
        /// Permet de récupérer toutes les devises par le webservice de manière asynchrone
        /// </summary>
        private async void ActionGetData() {
            var result = await wsService.GetAllDevisesAsync();
            this.cbxDevise.DataContext = new List<Devise>(result);
        }


        /// <summary>
        /// Fait appel au webservice pour convertir le montant donné dan la textbox par la devise donnée dans la combobox et affiche le résultat dans la dernière textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonConvertisseur(object sender, RoutedEventArgs e)
        {
            string montantStr = MontantBox.Text;
            Devise devise = (Devise) cbxDevise.SelectedItem;
            if (( montantStr != "") && (devise != null) )
            {
                double result = wsService.CalculConversionDevise(Double.Parse(montantStr), devise);
                MontantDevBox.Text = result.ToString();
            }        
           
        }


    }
}
