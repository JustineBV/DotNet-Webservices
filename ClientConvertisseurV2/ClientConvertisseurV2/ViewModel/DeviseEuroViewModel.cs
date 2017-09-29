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

namespace ClientConvertisseurV2.ViewModel
{
    public class DeviseEuroViewModel : ViewModelBase
    {
        private ObservableCollection<Devise> _comboBoxDevises;

        public ICommand BtnSetConversion { get; private set; }

        private Devise _comboBoxDeviseItem;

        private double _montantDevise;

        private string _montantEuros;


        public Devise ComboBoxDeviseItem
        {
            get { return _comboBoxDeviseItem; }
            set
            {
                _comboBoxDeviseItem = value;
                RaisePropertyChanged();
            }
        }


        public double MontantDevise
        {
            get { return _montantDevise; }
            set
            {
                _montantDevise = value;
                RaisePropertyChanged();
            }
        }


        public string MontantEuros
        {
            get { return _montantEuros; }
            set
            {
                _montantEuros = value;
                RaisePropertyChanged();
            }
        }


        public ObservableCollection<Devise> ComboBoxDevises
        {
            get { return _comboBoxDevises; }
            set
            {
                _comboBoxDevises = value;
                RaisePropertyChanged();// Pour notifier de la modification de ses données 
            }
        }


        public DeviseEuroViewModel()
        {
            ActionGetData();
            BtnSetConversion = new RelayCommand(ActionSetConversion);
        }


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
                this.MontantDevise = (Double.Parse(_montantEuros) / ComboBoxDeviseItem.Taux);
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
        

    }


}