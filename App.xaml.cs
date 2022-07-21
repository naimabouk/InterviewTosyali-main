using System;
using System.Reflection;
using Prism.Ioc;
using System.Windows;
using JobInterview.ViewModels;
using JobInterview.Views;
using Prism.Mvvm;

namespace JobInterview
{

    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.Register(typeof(MainWindow).ToString(), typeof(MainWindowViewModel));
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

    }
}