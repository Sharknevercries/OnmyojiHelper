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
            SimpleIoc.Default.Register<Stages.StagePageViewModel>();
            SimpleIoc.Default.Register<SettingsPageViewModel>();
            SimpleIoc.Default.Register<EditPageViewModel>();
            SimpleIoc.Default.Register<Stages.StageEditPageViewModel>();
            SimpleIoc.Default.Register<Stages.StageAddPageViewModel>();
            SimpleIoc.Default.Register<Shikigamis.ShikigamiAddPageViewModel>();
            SimpleIoc.Default.Register<Shikigamis.ShikigamiEditPageViewModel>();
            SimpleIoc.Default.Register<Clues.ClueAddPageViewModel>();
            SimpleIoc.Default.Register<Clues.ClueEditPageViewModel>();
        }
    }
}
