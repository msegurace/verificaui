namespace VerificaApp.CustomControls;

public partial class FlyoutFooter : ContentView
{
    public string VersionString { get; set; }
    public FlyoutFooter()
	{
		InitializeComponent();

        this.version.Text = string.Concat(AppInfo.VersionString, ".", AppInfo.BuildString);
    }
}