using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Xml.Linq;

namespace News.Model
{
    public class NewsFactory
    {
        public NewsFactory()
        {
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e) => TimeToUpdate?.Invoke(null, null);

        public event EventHandler TimeToUpdate;

        private DispatcherTimer timer;
        private string url = @"https://lenta.ru/rss/news";
        public ObservableCollection<DailyhNews> NewsFeed;
        public async Task<ObservableCollection<DailyhNews>> LoadAndParseAsync()
        {
           return await Task.Factory.StartNew(() =>
            {
                XDocument document = XDocument.Parse(new WebClient() { Encoding = Encoding.UTF8 }.DownloadString(url));
                return XMLParser.Parse(document);
            });
        }
    }
}
