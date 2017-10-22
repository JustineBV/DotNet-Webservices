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

        // Add each new ModelView
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<DeviseEuroViewModel>();
        }       
        


        /// <summary>         
        /// Gets the Main property.         
        /// </summary>   
        /// For each new page we need to add this line which made link between the view and the modelview.
        public DeviseEuroViewModel DeviseEuros => ServiceLocator.Current.GetInstance<DeviseEuroViewModel>();
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

    }
}