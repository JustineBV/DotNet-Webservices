using ClientConvertisseurV2.Model;
using ClientConvertisseurV2.Service;
using ClientConvertisseurV2.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ClientConvertisseurV2.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Devise> _comboBoxDevises;

        public ICommand BtnSetConversion { get; private set; }
        public ICommand BtnChangePage { get; private set; }

        private Devise _comboBoxDeviseItem; 

        private double _montantDevise;

        private string _montantEuros;


        /// <summary>
        /// Property ComboBoxDeviseItem avec notification du changement de property
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
        /// Property MontantDevise avec notification du changement de property
        /// </summary>
        public double MontantDevise
        {
            get { return _montantDevise; }
            set
            {
                _montantDevise = value;
                RaisePropertyChanged();
            }
        }


        /// <summary>
        /// Property MontantEuros avec notification du changement de property
        /// </summary>
        public string MontantEuros
        {
            get { return _montantEuros; }
            set
            {
                _montantEuros = value;
                RaisePropertyChanged();
            }
        }


        /// <summary>
        /// Property ComboBoxDevises avec notification de la modification
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
        /// Constructeur sans paramètre
        /// Ajoute les deux RelayCommand aux boutons BtnSetConversion et BtnChangePage
        /// </summary>
        public MainViewModel()
        {
            ActionGetData();
            BtnSetConversion = new RelayCommand(ActionSetConversion);
            BtnChangePage = new RelayCommand(ActionChangeConvertisseur);
        }


        /// <summary>
        /// Méthode aynchrone permettant de renvoyer dans le textBox MontantDevise, la conversion du montant en fonction de la devise donnés par les box
        /// </summary>
        private async void ActionSetConversion()
        {
            if (_montantEuros == null)
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
                this.MontantDevise = Double.Parse(_montantEuros) * ComboBoxDeviseItem.Taux;
            }
        }



        /// <summary>
        /// Essaie de récupérer les devises dans un appel asynchrone
        /// Erreur deconnexion gérée par un try catch
        /// </summary>
        private async void ActionGetData()
        {
            WSService wsService = WSService.GetInstance();
            try
            {
                var result = await wsService.GetAllDevisesAsync();
                ComboBoxDevises = new ObservableCollection<Devise>(result);
            }
            catch(Exception e)
            {
                var messageDialog = new MessageDialog("Pas de connexion au webService concerné");
                await messageDialog.ShowAsync();
                Application.Current.Exit();
            }
        }


        /// <summary>
        /// Change to convertissor page = go to DeviseEuros
        /// </summary>
        private void ActionChangeConvertisseur() {
            RootPage r = (RootPage)Window.Current.Content;
            SplitView sv = (SplitView)(r.Content);
            (sv.Content as Frame).Navigate(typeof(DeviseEuros));
        }

    }


}
