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
        public ClueEditPartViewModel ClueEditPartViewModel => new ClueEditPartViewModel(_dataService);
        public BountyEditPartViewModel BountyEditPartViewModel => new BountyEditPartViewModel(_dataService);
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

    public class ClueEditPartViewModel : Mvvm.ViewModelBase
    {
        private IDataService _dataService;

        private ObservableCollection<Clue> _clues;
        public ObservableCollection<Clue> Clues
        {
            get { return _clues; }
            set { Set(ref _clues, value); }
        }

        public DelegateCommand ClueAddCommand { get; private set; }
        public DelegateCommand<ItemClickEventArgs> ClueItemClickedCommand { get; private set; }

        public ClueEditPartViewModel(IDataService dataService)
        {
            this._dataService = dataService;

            _clues = new ObservableCollection<Clue>(_dataService.GetAllClues());

            ClueAddCommand = new DelegateCommand(GoToClueAdd);
            ClueItemClickedCommand = new DelegateCommand<ItemClickEventArgs>(GoToClueEdit);
        }

        public void GoToClueAdd()
        {
            var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
            nav.Navigate(typeof(Views.Clues.ClueAddPage));
        }

        public void GoToClueEdit(ItemClickEventArgs e)
        {
            var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
            nav.Navigate(typeof(Views.Clues.ClueEditPage), (Clue)e.ClickedItem);
        }
    }

    public class BountyEditPartViewModel : Mvvm.ViewModelBase
    {
        private IDataService _dataService;

        private ObservableCollection<Bounty> _bounties;
        public ObservableCollection<Bounty> Bounties
        {
            get { return _bounties; }
            set { Set(ref _bounties, value); }
        }

        public DelegateCommand BountyAddCommand { get; private set; }
        public DelegateCommand<ItemClickEventArgs> BountyItemClickedCommand { get; private set; }

        public BountyEditPartViewModel(IDataService dataService)
        {
            this._dataService = dataService;

            Bounties = new ObservableCollection<Bounty>(_dataService.GetAllBounties());

            BountyAddCommand = new DelegateCommand(GoToBountyAdd);
            BountyItemClickedCommand = new DelegateCommand<ItemClickEventArgs>(GoToBountyEdit);
        }

        public void GoToBountyAdd()
        {
            var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
            nav.Navigate(typeof(Views.Bounties.BountyAddPage));
        }

        public void GoToBountyEdit(ItemClickEventArgs e)
        {
            var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
            nav.Navigate(typeof(Views.Bounties.BountyEditPage), (Bounty)e.ClickedItem);
        }
    }
}
