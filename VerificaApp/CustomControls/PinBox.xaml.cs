namespace VerificaApp.CustomControls;

public partial class PinBox : Grid
{
    public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(TextProperty), typeof(string), typeof(PinBox), String.Empty);

    public PinBox()
	{
		InitializeComponent();
	}

    public string Text
    {
        get { return GetValue(TextProperty).ToString(); }
        set 
        { 
            SetValue(TextProperty, value); 
            this.Entry.Text = value;
        }
    }
}