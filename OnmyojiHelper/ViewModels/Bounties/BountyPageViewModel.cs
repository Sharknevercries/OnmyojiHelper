using OnmyojiHelper.Models;
using OnmyojiHelper.Models.Groups;
using OnmyojiHelper.Models.Relations;
using OnmyojiHelper.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private List<ShikigamiBattle> _bountyLocationCount;
        public List<ShikigamiBattle> BountyLocationCount
        {
            get { return _bountyLocationCount; }
            set { Set(ref _bountyLocationCount, value); }
        }

        public ObservableCollection<string> Suggestions { get; set; } 

        public DelegateCommand<Bounty> BountySelectionChangedCommand { get; private set; }
        public DelegateCommand<AutoSuggestBoxQuerySubmittedEventArgs> SearchAutoSuggestBoxQuerySubmittedCommand { get; private set; }
        public DelegateCommand<AutoSuggestBoxSuggestionChosenEventArgs> SearchAutoSuggestBoxQuerySuggestionChosenCommand { get; private set; }
        public DelegateCommand<AutoSuggestBoxTextChangedEventArgs> SearchAutoSuggestBoxQueryTextChangedCommand { get; private set; }

        public BountyPageViewModel(IDataService dataService)
        {
            this._dataService = dataService;

            BountySelectionChangedCommand = new DelegateCommand<Bounty>(BountySelectionChanged);
            SearchAutoSuggestBoxQuerySubmittedCommand = new DelegateCommand<AutoSuggestBoxQuerySubmittedEventArgs>(QuerySubmitted);
        }


        public void QuerySubmitted(AutoSuggestBoxQuerySubmittedEventArgs e)
        {
            var keyword = e.QueryText;

            var byName = from bg in _allBountyGroups
                         let bs = bg.Bounties
                         from b in bs
                         where b.Shikigami.Name.Contains(keyword)
                         select b;
            var byClue = from bg in _allBountyGroups
                         let bs = bg.Bounties
                         from b in bs
                         let bcs = b.BountyClues
                         from bc in bcs
                         where bc.Clue.Keyword.Contains(keyword)
                         select b;

            BountyGroups = (from b in byName.Union(byClue)
                            group b by b.Shikigami.Rarity into g
                            select new BountyGroup()
                            {
                                Rarity = g.Key,
                                Bounties = g.OrderBy(b => b.ShikigamiId).ToList(),
                            });
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
