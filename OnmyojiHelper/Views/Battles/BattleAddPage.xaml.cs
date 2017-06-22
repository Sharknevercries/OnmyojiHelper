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
    public sealed partial class BattleAddPage : Page
    {
        private IDataService _dataService => SimpleIoc.Default.GetInstance<IDataService>();

        public BattleAddPage()
        {
            this.InitializeComponent();

            battleEditShikigami.ItemsSource = GetShikigamiBattle();
            battleEditStage.ItemsSource = _dataService.GetAllStages();
        }

        private IEnumerable<ShikigamiBattle> GetShikigamiBattle()
        {
            var shikigamis = _dataService.GetAllShikigamis().ToList();

            return (from s in shikigamis
                    select new ShikigamiBattle() { Shikigami = s, Count = 0 })
                   .ToList();
        }

        private void battleEditShikigami_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = (BattleAddPageViewModel)DataContext;
            if (viewModel != null)
                viewModel.SelectedShikigamiBattles = battleEditShikigami.SelectedItems.Cast<ShikigamiBattle>().Where(sb => sb.Count > 0).Select(sb => new ShikigamiBattle() { ShikigamiId = sb.Shikigami.Id, Count = sb.Count }).ToList();
        }
    }
}
