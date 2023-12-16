namespace VerificaApp.ViewModels
{
    public partial class AboutViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string version;

        [ObservableProperty]
        private string build;


        public AboutViewModel()
        {
            Title = "Acerca de Verifica";
            Version = AppInfo.Current.VersionString;
            Build = AppInfo.Current.BuildString;
        }

        [RelayCommand]
        public async Task Tap(string url)
        {
            await Launcher.OpenAsync(url);
        }        
    }
}