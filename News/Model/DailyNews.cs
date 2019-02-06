using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Model
{
    public class DailyhNews : BindableBase
    {
        public DailyhNews(string title, string desc, string link, byte[] img)
        {
            Title = title;
            Description = desc;
            Link = link;
            Image = img;
            IsSelected = false;
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public byte[] Image { get; set; }
        private bool _IsSelected;
        public bool IsSelected
        {
            get => _IsSelected;
            set
            {
                _IsSelected = value;
                OnPropertyChanged();
            }
        }
        public static DailyhNews Exeption(string Exeption)
        {
            return new DailyhNews("Ошибка загрузки!" + Environment.NewLine + Exeption, string.Empty, null, null);
        }
    }
}
