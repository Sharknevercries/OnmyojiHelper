using GalaSoft.MvvmLight.Ioc;
using OnmyojiHelper.Models;
using OnmyojiHelper.Models.Relations;
using OnmyojiHelper.Services;
using OnmyojiHelper.ViewModels.Bounties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Template10.Services.SerializationService;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OnmyojiHelper.Views.Bounties
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BountyEditPage : Page
    {
        private ISerializationService _serializationService;
        private IDataService _dataSerivce => SimpleIoc.Default.GetInstance<IDataService>();

        public BountyEditPage()
        {
            this.InitializeComponent();
            _serializationService = SerializationService.Json;

            bountyEditClue.ItemsSource = _dataSerivce.GetAllClues().ToList();
            bountyEditShikigami.ItemsSource = _dataSerivce.GetAllShikigamis().ToList();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var bounty = _serializationService.Deserialize<Bounty>(e.Parameter?.ToString());
            bountyEditShikigami.SelectedItem = bountyEditShikigami.Items.Cast<Shikigami>().FirstOrDefault(s => s.Id == bounty.ShikigamiId);
            bountyEditClue.SelectedItems.Clear();

            foreach (var item in bountyEditClue.ItemsSource as IEnumerable<Clue>)
            {
                if (bounty.BountyClues.Count(a => a.ClueId == item.Id) > 0)
                {
                    bountyEditClue.SelectedItems.Add(item);
                }
            }

            base.OnNavigatedTo(e);
        }

        private void bountyEditClue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = (BountyEditPageViewModel)DataContext;
            if(viewModel != null)
                viewModel.SelectedClues = bountyEditClue.SelectedItems.Cast<Clue>().ToList();
        }

        private void bountyEditShikigami_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = (BountyEditPageViewModel)DataContext;
            if (viewModel != null)
                viewModel.SelectedShikigami = bountyEditShikigami.SelectedItem as Shikigami;
        }
    }
}
