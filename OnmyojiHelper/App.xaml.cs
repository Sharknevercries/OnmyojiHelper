using Windows.UI.Xaml;
using System.Threading.Tasks;
using OnmyojiHelper.Services.SettingsServices;
using Windows.ApplicationModel.Activation;
using Template10.Controls;
using Template10.Common;
using System;
using System.Linq;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Template10.Services.NavigationService;
using OnmyojiHelper.Views;
using GalaSoft.MvvmLight.Ioc;
using OnmyojiHelper.ViewModels;
using Template10.Services.LoggingService;
using System.Runtime.CompilerServices;
using OnmyojiHelper.ViewModels.Stages;
using OnmyojiHelper.ViewModels.Shikigamis;

namespace OnmyojiHelper
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    [Bindable]
    sealed partial class App : BootStrapper
    {
        public App()
        {
            InitializeComponent();
            SplashFactory = (e) => new Views.Splash(e);

            #region app settings

            // some settings must be set in app.constructor
            var settings = SettingsService.Instance;
            RequestedTheme = settings.AppTheme;
            CacheMaxDuration = settings.CacheMaxDuration;
            ShowShellBackButton = settings.UseShellBackButton;

            #endregion

            LoggingService.WriteLine = new DebugWriteDelegate(LogHandler);

            EnsureDataBaseAsync().Wait();

            using (var db = new OnmyojiContext())
            {
                db.Database.Migrate();
            }
        }

        public void LogHandler(string text = null, Severities severity = Severities.Info, Targets target = Targets.Debug, [CallerMemberName]string caller = null)
        {
            System.Diagnostics.Debug.WriteLine($"{DateTime.Now.TimeOfDay.ToString()} {severity} {caller} {text}");
        }

        private async Task EnsureDataBaseAsync()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            try
            {
                var localFile = await localFolder.GetFileAsync("Onmyoji.db");
            }
            catch (FileNotFoundException ex)
            {
                var dbFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/Onmyoji.db"));
                await dbFile.CopyAsync(localFolder);
            }
        }

        public override UIElement CreateRootElement(IActivatedEventArgs e)
        {
            var service = NavigationServiceFactory(BackButton.Attach, ExistingContent.Exclude);
            return new ModalDialog
            {
                DisableBackButtonWhenModal = true,
                Content = new Views.Shell(service),
                ModalContent = new Views.Busy(),
            };
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            // TODO: add your long-running task here
            await NavigationService.NavigateAsync(typeof(Views.Stages.StagePage));
        }

        public override INavigable ResolveForPage(Page page, NavigationService navigationService)
        {
            switch (page)
            {
                case Views.Stages.StagePage p:
                    return SimpleIoc.Default.GetInstance<StagePageViewModel>();
                case SettingsPage p:
                    return SimpleIoc.Default.GetInstance<SettingsPageViewModel>();
                case EditPage p:
                    return SimpleIoc.Default.GetInstance<EditPageViewModel>();
                case Views.Stages.StageEditPage p:
                    return SimpleIoc.Default.GetInstance<StageEditPageViewModel>();
                case Views.Stages.StageAddPage p:
                    return SimpleIoc.Default.GetInstance<StageAddPageViewModel>();
                case Views.Shikigamis.ShikigamiAddPage p:
                    return SimpleIoc.Default.GetInstance<ShikigamiAddPageViewModel>();
                case Views.Shikigamis.ShikigamiEditPage p:
                    return SimpleIoc.Default.GetInstance<ShikigamiEditPageViewModel>();
            }
            return base.ResolveForPage(page, navigationService);
        }
    }
}
