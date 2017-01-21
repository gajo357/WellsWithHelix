using System;
using DevExpress.Mvvm.POCO;

namespace WellsWithHelix.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            GeneratePlot();
        }

        public static MainViewModel Create()
        {
            return ViewModelSource.Create(() => new MainViewModel());
        }

        public virtual PlotViewModel Plot { get; set; }

        public void GeneratePlot()
        {
            var plot = PlotViewModel.Create();

            Plot = plot;
        }
    }
}
