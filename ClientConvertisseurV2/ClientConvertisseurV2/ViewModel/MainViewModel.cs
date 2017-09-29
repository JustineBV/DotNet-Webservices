﻿using ClientConvertisseurV2.Model;
using ClientConvertisseurV2.Service;
using ClientConvertisseurV2.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        public MainViewModel()
        {
            ActionGetData();
            BtnSetConversion = new RelayCommand(ActionSetConversion);
            BtnChangePage = new RelayCommand(ActionChangeConvertisseur);
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
                this.MontantDevise = Double.Parse(_montantEuros) * ComboBoxDeviseItem.Taux;
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
            catch(Exception e)
            {
                var messageDialog = new MessageDialog("Pas de connexion au webService concerné");
                await messageDialog.ShowAsync();
                // IL FAUDRAIT FERMER L'APPLICATION
            }
        }


        private void ActionChangeConvertisseur() {
            RootPage r = (RootPage)Window.Current.Content;
            SplitView sv = (SplitView)(r.Content);
            (sv.Content as Frame).Navigate(typeof(DeviseEuros));
        }

    }


}