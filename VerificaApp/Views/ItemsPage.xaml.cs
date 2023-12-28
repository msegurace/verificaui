namespace VerificaApp.Views
{
    [QueryProperty(nameof(user), "user")]
    public partial class ItemsPage : BasePage
    {
        private ItemsViewModel _viewModel;

        public VerificaAppUser user
        {
            set
            {
                _viewModel.CurrentUser = value;
                if (_viewModel.LoadItemsCommand.CanExecute(null))
                {
                    _viewModel.LoadItemsCommand.Execute(null);
                }
            }
        }

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