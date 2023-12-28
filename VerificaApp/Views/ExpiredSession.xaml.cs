namespace VerificaApp.Views;

public partial class ExpiredSession : ContentPage
{
	public ExpiredSession()
	{
		InitializeComponent();
	}

    private void btnVolver_Clicked(object sender, EventArgs e)
    {
        MainThreadHelper.BeginInvokeOnMainThread(async () =>
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        });
    }
}