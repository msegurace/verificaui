namespace VerificaApp.Views
{
    public partial class AboutPage : BasePage
    {

        AboutViewModel _viewModel;
        public AboutPage(AboutViewModel aboutViewModel) :base(aboutViewModel)
        {
            InitializeComponent();
            _viewModel = aboutViewModel;
        }
    }
}