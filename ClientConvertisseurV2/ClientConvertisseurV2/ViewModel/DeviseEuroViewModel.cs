using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientConvertisseurV2.Model;
using ClientConvertisseurV2.Service;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Popups;
using ClientConvertisseurV2.View;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace ClientConvertisseurV2.ViewModel
{
    // Attention, je n'ai pas encore changé les noms des variables (euro devient devise et devise devient euro), ni fais de la généricité
    public class DeviseEuroViewModel : ViewModelBase
    {
        private ObservableCollection<Devise> _comboBoxDevises;

        public ICommand BtnSetConversion { get; private set; }
   
        public ICommand BtnChangePage { get; private set; }

        private Devise _comboBoxDeviseItem;

        private double _montantEuros;

        private string _montantDevise;


        /// <summary>
        /// Properties of _comboBoxDeviseItem
        /// Get return the list of Devises
        /// Set sends a message RaisePropertyChanged();
        /// </summary>
        public Devise ComboBoxDeviseItem
        {
            get { return _comboBoxDeviseItem; }
            set
            {
                _comboBoxDeviseItem = value;
                RaisePropertyChanged();
            }
        }


        /// <summary>
        /// Property of _montantDevise
        /// Return string
        /// </summary>
        public string MontantDevise
        {
            get { return _montantDevise; }
            set
            {
                _montantDevise = value;
                RaisePropertyChanged();
            }
        }



        /// <summary>
        /// Property of _montantEuros
        /// Return double
        /// </summary>
        public double MontantEuros
        {
            get { return _montantEuros; }
            set
            {
                _montantEuros = value;
                RaisePropertyChanged();
            }
        }



        /// <summary>
        /// Property of  _comboBoxDevises
        /// Return ObservableCollection<Devise>
        /// </summary>
        public ObservableCollection<Devise> ComboBoxDevises
        {
            get { return _comboBoxDevises; }
            set
            {
                _comboBoxDevises = value;
                RaisePropertyChanged();// Pour notifier de la modification de ses données 
            }
        }


        /// <summary>
        /// Constructor, initialise the relayCommand of BtnSetConversion
        /// </summary>
        public DeviseEuroViewModel()
        {
            ActionGetData();
            BtnSetConversion = new RelayCommand(ActionSetConversion);
            BtnChangePage = new RelayCommand(ActionChangeConvertisseur);
        }


        /// <summary>
        /// Method asynchronous converts the first amount with the devise and displays in the MontantEuros TextBox
        /// </summary>
        private async void ActionSetConversion()
        {
            if (_montantDevise == null)
            {
                var messageDialog = new MessageDialog("Renseignez le montant à convertir");
                await messageDialog.ShowAsync();
            }
            else if (_comboBoxDeviseItem == null)
            {
                var messageDialog = new MessageDialog("Il n'y pas la devise de renseigner");
                await messageDialog.ShowAsync();
            }
            else
            {             
                this.MontantEuros = (Double.Parse(_montantDevise) / ComboBoxDeviseItem.Taux);
            }
        }


        private async void ActionGetData()
        {
            WSService wsService = WSService.GetInstance();
            try
            {
                var result = await wsService.GetAllDevisesAsync();
                ComboBoxDevises = new ObservableCollection<Devise>(result);
            }
            catch (Exception e)
            {
                var messageDialog = new MessageDialog("Pas de connexion au webService concerné");
                await messageDialog.ShowAsync();
                // IL FAUDRAIT FERMER L'APPLICATION
            }
        }


        /// <summary>
        /// Change of convertissor page : go to MainPage
        /// </summary>
        private void ActionChangeConvertisseur()
        {
            RootPage r = (RootPage)Window.Current.Content;
            SplitView sv = (SplitView)(r.Content);
            (sv.Content as Frame).Navigate(typeof(MainPage));
        }


    }


}