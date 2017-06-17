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
}
