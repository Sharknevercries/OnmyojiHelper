using OnmyojiHelper.Models;
using OnmyojiHelper.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Common;
using Template10.Mvvm;
using Windows.UI.Xaml.Controls;

namespace OnmyojiHelper.ViewModels
{
    public class EditPageViewModel : Mvvm.ViewModelBase
    {
        private IDataService _dataService;

        public EditPageViewModel (IDataService dataService)
        {
            this._dataService = dataService;
        }

        public StageEditPartViewModel StageEditPartViewModel => new StageEditPartViewModel(_dataService);
        public ShikigamiEditPartViewModel ShikigamiEditPartViewModel => new ShikigamiEditPartViewModel(_dataService);
    }

    public class StageEditPartViewModel : Mvvm.ViewModelBase
    {
        private IDataService _dataService;

        private ObservableCollection<Stage> _stages;
        public ObservableCollection<Stage> Stages
        {
            get { return _stages; }
            set { Set(ref _stages, value); }
        }

        public DelegateCommand<ItemClickEventArgs> StageItemClickedCommand { get; private set; }
        public DelegateCommand StageAddCommand { get; private set; }
           

        public StageEditPartViewModel(IDataService dataService)
        {
            this._dataService = dataService;

            Stages = new ObservableCollection<Stage>(_dataService.GetAllStages());
            StageItemClickedCommand = new DelegateCommand<ItemClickEventArgs>(GoToStageEdit);
            StageAddCommand = new DelegateCommand(GoToStageAdd);
        }

        public void GoToStageEdit(ItemClickEventArgs e)
        {
            var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
            nav.Navigate(typeof(Views.Stages.StageEditPage), (Stage)e.ClickedItem);
        }

        public void GoToStageAdd()
        {
            var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
            nav.Navigate(typeof(Views.Stages.StageAddPage));
        }
    }

    public class ShikigamiEditPartViewModel : Mvvm.ViewModelBase
    {
        private IDataService _dataService;

        private ObservableCollection<Shikigami> _shikigamis;
        public ObservableCollection<Shikigami> Shikigamis
        {
            get { return _shikigamis; }
            set { Set(ref _shikigamis, value); }
        }

        public DelegateCommand ShikigamiAddCommand { get; private set; }
        public DelegateCommand<ItemClickEventArgs> ShikigamiItemClickedCommand { get; private set; }

        public ShikigamiEditPartViewModel(IDataService dataService)
        {
            this._dataService = dataService;

            Shikigamis = new ObservableCollection<Shikigami>(_dataService.GetAllShikigamis());

            ShikigamiAddCommand = new DelegateCommand(GoToShikigamiAdd);
            ShikigamiItemClickedCommand = new DelegateCommand<ItemClickEventArgs>(GoToShikigamiEdit);
        }

        public void GoToShikigamiAdd()
        {
            var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
            nav.Navigate(typeof(Views.Shikigamis.ShikigamiAddPage));
        }

        public void GoToShikigamiEdit(ItemClickEventArgs e)
        {
            var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
            nav.Navigate(typeof(Views.Shikigamis.ShikigamiEditPage), (Shikigami)e.ClickedItem);
        }
    }
}
