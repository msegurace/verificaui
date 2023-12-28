namespace VerificaApp.ViewModels
{
    public partial class ItemsViewModel : BaseViewModel
    {
        #region Properties
        private Stopwatch StopWatch = new Stopwatch();
        private TimeSpan SessionDuration;

        private ItemDto _selectedItem;

        [ObservableProperty]
        private ItemDto item;

        [ObservableProperty]
        private ObservableCollection<ItemDto> items;

        [ObservableProperty]
        private String emptyViewLabel;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private bool isRefreshing;

        public int LastId { get; set; }

        IDispatcherTimer timer;
        #endregion
        
        #region Constructor
        private readonly IVerificaAppService _verificaService;
        public ItemsViewModel(IVerificaAppService verificaService)
        {
            _verificaService = verificaService;
            
            Title = "Autorizaciones por gestionar";
            Items = new ObservableCollection<ItemDto>();

            IsBusy = false;
            
            //Hace una petición cada minuto para ver si hay autorizaciones pendientes.
            //Device.StartTimer(new TimeSpan(0, App.RefreshTimer, 0), () =>
            SessionDuration = TimeSpan.FromMinutes(CommonConstants.RefreshTimer);
            StopWatch.Start();

            AppSessionManager.Instance.StartSession();

            timer = Application.Current.Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick += (s, e) => TimerTick();
            timer.Start();
        }

        #endregion

        #region Métodos
        [RelayCommand]
        public async Task LoadItems()
        {
            if (CurrentUser == null) return;

            EmptyViewLabel = "Autorizaciones por gestionar";

            try
            {
                Items.Clear();

                var response = await _verificaService.GetTokens(CurrentUser.id);

                //Respuesta
                foreach (var item in response)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await Shell.Current.DisplayAlert(CommonConstants.SERVER_ERROR_TITLE, ex.Message, "Aceptar");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
                EmptyViewLabel = String.Concat("Actualmente no tienes autorizaciones por gestionar.",
                    Environment.NewLine, Environment.NewLine, Environment.NewLine,
                    "Desliza hacia abajo para refrescar.");
            }
        }

        [RelayCommand]
        async Task Accept(ItemDto item)
        {
            IsBusy = true;
            try
            {

                if (!await _verificaService.AcceptToken(item.id))
                {
                    await AppShell.Current.DisplayAlert("Ha ocurrido un error.", "Ocurrió un error al validar el token.", "Aceptar");
                }
                else
                {
                    if (LoadItemsCommand.CanExecute(null))
                    {
                        LoadItemsCommand.Execute(null);
                        Console.WriteLine("****** Llamado desde ExecuteAutorizoCommand");
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("E201"))
                {
                    HandleCredentialsChanged();
                }
                else
                {
                    await AppShell.Current.DisplayAlert("Error obteniendo datos desde el servidor", ex.Message, "Aceptar");
                }
            }
            finally
            {
                IsBusy = false;
            }

        }

        [RelayCommand]
        async Task Reject(ItemDto item)
        {
            IsBusy = true;
            try
            {

                if (!await _verificaService.RejectToken(item.id))
                {
                    await AppShell.Current.DisplayAlert("Ha ocurrido un error.", "Ocurrió un error al validar el token.", "Aceptar");
                }
                else
                {
                    if (LoadItemsCommand.CanExecute(null))
                    {
                        LoadItemsCommand.Execute(null);
                        Console.WriteLine("****** Llamado desde ExecuteAutorizoCommand");
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("E201"))
                {
                    HandleCredentialsChanged();
                }
                else
                {
                    await AppShell.Current.DisplayAlert("Error obteniendo datos desde el servidor", ex.Message, "Aceptar");
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            StopWatch.Restart();

        }

        public void OnDissapearing()
        {
            StopWatch.Stop();
        }

        private void TimerTick()
        {
            if (StopWatch.IsRunning && StopWatch.Elapsed.Minutes >= SessionDuration.Minutes)
            {
                if (Items.Count == 0)
                {
                    if (LoadItemsCommand.CanExecute(null))
                    {
                        LoadItemsCommand.Execute(null);
                       // Console.WriteLine("****** Llamado desde constructor");
                    }
                }
                StopWatch.Restart();
            }
        }

        #endregion

    }
}