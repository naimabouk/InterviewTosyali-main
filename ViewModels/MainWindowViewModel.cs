using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using JobInterview.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace JobInterview.ViewModels
{
    public class MainWindowViewModel :  BindableBase
    {
        public MainWindowViewModel()
        {
            HtmlDocument web = new HtmlDocument();
            web.Load(@"C:\Users\Kasozi\Desktop\Scrap\ConsoleApp1\ConsoleApp1\html.html");
            var marqueeText = web.DocumentNode.SelectNodes("//html/body/table/tbody/tr/td/font/table/tr/td/table/tr/td/font/b/font/marquee").First().InnerText;
            var rates = marqueeText.Split("-");
            var processed = rates.Select(a => a.Trim()).Where(a => a.Contains("USD") || a.Contains("EURO")).ToList();
            var objects = CreateDomainObjects(processed);

            var dbcontext = new KASOZIContext();
            foreach (var rate in objects)
                dbcontext.Add(rate);
            dbcontext.SaveChangesAsync();
        }
        private List<Rate> _rates = new List<Rate>();
        public List<Rate> Rates
        {
            get { return _rates; }
            set { SetProperty(ref _rates, value); }
        }
        
        public void FetchData()
        {
            var dbContext = new KASOZIContext();
            var rs = dbContext.Rates.ToList();
            Rates = rs;
        }
        
        public DelegateCommand RefreshCommand => new DelegateCommand(ExecuteCommandName);

        void ExecuteCommandName()
        {
            FetchData();
        }
        
        private List<Rate> CreateDomainObjects(IEnumerable<string> rates)
        {
            var tempList = new List<Rate>();
            foreach (var rate in rates)
            {
                if (rate.Split().Contains("EURO"))
                {
                    var domainModel = new Rate() {Id = 1, From = Double.Parse(rate.Split()[2].Split("/")[0]), To = Double.Parse(rate.Split()[2].Split("/")[1]), Currency = "EURO", Date = DateTime.Now.ToShortDateString().ToString()};
                    tempList.Add(domainModel);
                }
                else
                {
                    var domainModel = new Rate() {Id = 2, From = Double.Parse(rate.Split()[2].Split("/")[0]), To = Double.Parse(rate.Split()[2].Split("/")[1]), Currency = "USD", Date = DateTime.Now.ToShortDateString().ToString()};
                    tempList.Add(domainModel);
                }
            }

            return tempList;
        }
    }
}