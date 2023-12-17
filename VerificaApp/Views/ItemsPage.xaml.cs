namespace VerificaApp.Views
{
    public partial class ItemsPage : BasePage
    {
        private ItemsViewModel _viewModel;


        public ItemsPage(ItemsViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _viewModel.OnDissapearing();
        }
    }
}