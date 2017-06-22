using GalaSoft.MvvmLight.Ioc;
using OnmyojiHelper.Models;
using OnmyojiHelper.Models.Relations;
using OnmyojiHelper.Services;
using OnmyojiHelper.ViewModels.Battles;
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

namespace OnmyojiHelper.Views.Battles
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BattleEditPage : Page
    {
        private ISerializationService _serializationService;
        private IDataService _dataService => SimpleIoc.Default.GetInstance<IDataService>();
        private BattleEditPageViewModel _viewModel => (BattleEditPageViewModel)DataContext ?? null;

        public BattleEditPage()
        {
            this.InitializeComponent();
            _serializationService = SerializationService.Json;

            battleEditShikigami.ItemsSource = GetShikigamiBattle();
            battleEditStage.ItemsSource = _dataService.GetAllStages();
        }

        private IEnumerable<ShikigamiBattle> GetShikigamiBattle()
        {
            var shikigamis = _dataService.GetAllShikigamis().ToList();

            return (from s in shikigamis
                    select new ShikigamiBattle() { ShikigamiId = s.Id, Shikigami = s, Count = 0 })
                   .ToList();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var battle = _serializationService.Deserialize<Battle>(e.Parameter?.ToString());
            battleEditStage.SelectedItem = battleEditStage.Items.Cast<Stage>().FirstOrDefault(s => s.Id == battle.StageId);
            battleEditShikigami.SelectedItems.Clear();

            foreach (var item in battleEditShikigami.ItemsSource as IEnumerable<ShikigamiBattle>)
            {
                var target = battle.ShikigamiBattles.FirstOrDefault(a => a.ShikigamiId == item.ShikigamiId);

                if(target != null)
                {
                    item.Count = target.Count;
                    battleEditShikigami.SelectedItems.Add(item);
                }
            }

            base.OnNavigatedTo(e);
        }

        private void battleEditShikigami_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_viewModel != null)
                _viewModel.SelectedShikigamiBattles = battleEditShikigami.SelectedItems.Cast<ShikigamiBattle>().Where(sb => sb.Count > 0).Select(sb => new ShikigamiBattle() { ShikigamiId = sb.Shikigami.Id, Count = sb.Count }).ToList();
        }

        private void battleEditStage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_viewModel != null)
                _viewModel.SelectedStage = battleEditStage.SelectedItem as Stage;
        }
    }
}
