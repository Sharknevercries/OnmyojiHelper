using OnmyojiHelper.Models;
using OnmyojiHelper.Models.Groups;
using OnmyojiHelper.Models.Relations;
using OnmyojiHelper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace OnmyojiHelper.ViewModels.Bounties
{
    public class BountyPageViewModel : Mvvm.ViewModelBase
    {
        private IDataService _dataService;

        private IEnumerable<BountyGroup> _allBountyGroups;

        private IEnumerable<BountyGroup> _bountyGroups;
        public IEnumerable<BountyGroup> BountyGroups
        {
            get { return _bountyGroups; }
            set { Set(ref _bountyGroups, value); }
        }

        public DelegateCommand<Bounty> BountySelectionChangedCommand { get; private set; }

        private List<ShikigamiBattle> _bountyLocationCount;
        public List<ShikigamiBattle> BountyLocationCount
        {
            get { return _bountyLocationCount; }
            set { Set(ref _bountyLocationCount, value); }
        }

        public BountyPageViewModel(IDataService dataService)
        {
            this._dataService = dataService;

            BountySelectionChangedCommand = new DelegateCommand<Bounty>(BountySelectionChanged);
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            _allBountyGroups = _dataService.GetAllBountyGroups();

            BountyGroups = _allBountyGroups;

            await Task.CompletedTask;
        }

        public void BountySelectionChanged(Bounty b)
        {
            if(b != null)
                BountyLocationCount = _dataService.GetBountyLocationCountFrom(b.ShikigamiId).ToList();
        }
    }
}
