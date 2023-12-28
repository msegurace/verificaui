namespace VerificaApp.CustomControls;

public partial class FlyoutHeader : ContentView
{
	public FlyoutHeader()
	{
		InitializeComponent();
        labelUserName.Text = String.Concat(SecureStorage.GetAsync("username").Result, " - ", SecureStorage.GetAsync("phone").Result);
    }
}