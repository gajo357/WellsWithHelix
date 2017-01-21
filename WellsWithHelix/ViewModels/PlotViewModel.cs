using DevExpress.Mvvm.POCO;

namespace WellsWithHelix.ViewModels
{
    public class PlotViewModel
    {
        public PlotViewModel()
        {
            AxisY = AxisViewModel.Create();
            AxisY.Title = "North";
            AxisY.Minimum = 40000;
            AxisY.Maximum = 50000;
            AxisY.MajorGrid = 1000;
            AxisY.MinorGrid = 500;

            AxisX = AxisViewModel.Create();
            AxisX.Title = "East";
            AxisX.Minimum = 20000;
            AxisX.Maximum = 30000;
            AxisX.MajorGrid = 1000;
            AxisX.MinorGrid = 500;

            AxisZ = AxisViewModel.Create();
            AxisZ.Title = "Depth";
            AxisZ.Minimum = 0;
            AxisZ.Maximum = 2000;
            AxisZ.MajorGrid = 100;
            AxisZ.MinorGrid = 50;
            AxisZ.UnitText = "m";
            AxisZ.NumberFormat = "F1";   
        }

        public static PlotViewModel Create()
        {
            return ViewModelSource.Create(() => new PlotViewModel());
        }

        public AxisViewModel AxisY { get; }
        public AxisViewModel AxisX { get; }
        public AxisViewModel AxisZ { get; }
    }
}
