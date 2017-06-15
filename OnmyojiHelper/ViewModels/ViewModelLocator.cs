using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using OnmyojiHelper.Mvvm;
using OnmyojiHelper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnmyojiHelper.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
            }
            else
            {
                SimpleIoc.Default.Register<IDataService, DataService>();
            }

            //Register your services used here
            SimpleIoc.Default.Register<StagePageViewModel>();
            SimpleIoc.Default.Register<SettingsPageViewModel>();
        }
    }
}
