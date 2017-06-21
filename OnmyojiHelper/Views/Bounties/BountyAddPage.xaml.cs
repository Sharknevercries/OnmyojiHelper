using GalaSoft.MvvmLight.Ioc;
using OnmyojiHelper.Models;
using OnmyojiHelper.Services;
using OnmyojiHelper.ViewModels.Bounties;
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

namespace OnmyojiHelper.Views.Bounties
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BountyAddPage : Page
    {
        private IDataService _dataService => SimpleIoc.Default.GetInstance<IDataService>();

        public BountyAddPage()
        {
            this.InitializeComponent();

            bountyEditClue.ItemsSource = _dataService.GetAllClues().ToList();
            bountyEditShikigami.ItemsSource = _dataService.GetAllShikigamis().ToList();
        }

        private void bountyEditClue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = (BountyAddPageViewModel)DataContext;
            viewModel.SelectedClues = bountyEditClue.SelectedItems.Cast<Clue>().ToList();
        }
    }
}
