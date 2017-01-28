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

        public virtual IPlot3DViewModel Plot { get; set; }

        public void GeneratePlot()
        {
            var plot = Plot3DViewModel.Create();

            Plot = plot;
        }
    }
}
