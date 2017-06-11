using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using OnmyojiHelper.Models.Groups;
using OnmyojiHelper.Services;

namespace OnmyojiHelper.ViewModels
{
    public class StagePageViewModel : Mvvm.ViewModelBase
    {
        private IEnumerable<StageGroup> _stageGroups;
        public IEnumerable<StageGroup> StageGroups
        {
            get { return _stageGroups; }
            set { Set(ref _stageGroups, value); }
        }

        private IDataService _dataService;

        public StagePageViewModel(IDataService dataService)
        {
            _dataService = dataService;

            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                
            }
            else
            {
                StageGroups = _dataService.GetAllStageGroups().ToList();
            }

            RaisePropertyChanged("StageGroups");
        }
    }
}
