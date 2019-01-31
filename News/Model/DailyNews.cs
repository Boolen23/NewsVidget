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
        private string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;
               // description = string.Join(Environment.NewLine, value.Split(new char[] { '.', ',' }));
            }
        }
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
    }
}
