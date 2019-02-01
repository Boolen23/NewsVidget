using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using News.Model;
using News.View;
using News.View.Command;

namespace News.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public MainViewModel()
        {
            newsFactory = new NewsFactory();
            newsFactory.TimeToUpdate += NewsFactory_TimeToUpdate;
        }

        private async void NewsFactory_TimeToUpdate(object sender, EventArgs e) => await LoadData.ExecuteAsync();

        private NewsFactory newsFactory;
        private ObservableCollection<DailyhNews> newsFeed;
        public ObservableCollection<DailyhNews> NewsFeed
        {
            get => newsFeed;
            set
            {
                newsFeed = value;
                OnPropertyChanged();
            }
        }
        private DailyhNews selectedNews;
        public DailyhNews SelectedNews
        {
            get => selectedNews;
            set
            {
                selectedNews = value;
                if (selectedNews!=null)
                {
                    foreach (var i in NewsFeed) i.IsSelected = false;
                    newsFeed.First(i => i == value).IsSelected = true;
                }
                OnPropertyChanged();
            }
        }
        private ICommand _freeDescription;
        public ICommand FreeDescription => _freeDescription ?? (_freeDescription = new RelayCommand(FreeDescriptionMethod, CanExuteFreeDescription));
        private void FreeDescriptionMethod(object param)
        {
            foreach (var i in NewsFeed)
                i.IsSelected = false;
            SelectedNews = null;
        }
        private bool CanExuteFreeDescription(object parameter) => !LoadData.IsExecuting;

        private AsyncRelayCommand _loadData;
        public AsyncRelayCommand LoadData => _loadData ?? (_loadData = new AsyncRelayCommand(LoadDataMethod));
        private async Task LoadDataMethod(CancellationToken ct)
        {
            NewsFeed = await newsFactory.LoadAndParseAsync();
        }

        private ICommand _startBrowser;
        public ICommand StartBrowser => _startBrowser ?? (_startBrowser = new RelayCommand(StartBrowserMethod));
        private void StartBrowserMethod(object param)
        {
            Process.Start((string)param);
        }
    }
}
