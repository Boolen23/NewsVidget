using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace News.Model
{
    public static class XMLParser
    {
        private static WebClient web;
        public static ObservableCollection<DailyhNews> Parse(this XDocument document)
        {
            web = new WebClient();
            ObservableCollection<DailyhNews> nws = new ObservableCollection<DailyhNews>();
            XElement channel = document.Root.Elements().First();
            foreach (XElement el in channel.Elements().Where(i => i.Name == "item"))
            {
                
                XElement title = el.Element("title");
                XElement desc = el.Element("description");
                XElement link = el.Element("link");
                XElement img = el.Element("enclosure");
                nws.Add(new DailyhNews(title.Value, desc.Value, link.Value, ByteFromUrl(img.Attribute("url").Value)));
            }
            return nws;
        }
        private static byte[] ByteFromUrl(string url) => web.DownloadData(url);
    }
}
