using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace ClientConvertisseurV2.ViewModel
{     /// <summary>     
      /// This class contains static references to all the view models in the     
      /// application and provides an entry point for the bindings.    
      /// <para> 

    /// See http://www.mvvmlight.net    
    /// </para>     
    ///</summary>   


    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<DeviseEuroViewModel>();
        }       
        


        /// <summary>         
        /// Gets the Main property.         
        /// </summary>         
        /// 
        public DeviseEuroViewModel DeviseEuros => ServiceLocator.Current.GetInstance<DeviseEuroViewModel>();
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

    }
}