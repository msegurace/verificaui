using System.Windows.Input;

namespace VerificaApp;

public partial class AppShell : Shell
{
    public ICommand HelpCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
    public ICommand ExitCommand => new Command(() => {
        MainThreadHelper.BeginInvokeOnMainThread(async () =>
        {
            await Shell.Current.GoToAsync($"///{nameof(LoginPage)}");
        });
    });

    public AppShell()
	{
		InitializeComponent();

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

    }



    private void MenuItem_Clicked(object sender, EventArgs e)
    {
        HelpCommand.Execute("https://www3.gobiernodecanarias.org/aplicaciones/gestionconocimiento/_recursos/infografias/6437ea0f81a23300115d7eff/genially.html");
    }

    private void MenuItem_Clicked_1(object sender, EventArgs e)
    {
        ExitCommand.Execute(null);
    }
}
