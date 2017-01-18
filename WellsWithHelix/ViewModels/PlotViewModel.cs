namespace WellsWithHelix.ViewModels
{
    public class PlotViewModel
    {
        public PlotViewModel()
        {
            AxisNorth = new AxisViewModel()
            {
                Title = "North",
                MajorGrid = 1000,
                MinorGrid = 500
            };

            AxisEast = new AxisViewModel()
            {
                Title = "East",
                MajorGrid = 1000,
                MinorGrid = 500
            };

            AxisDepth = new AxisViewModel()
            {
                Title = "Depth",
                MajorGrid = 100,
                MinorGrid = 50
            };
        }

        public AxisViewModel AxisNorth { get; }
        public AxisViewModel AxisEast { get; }
        public AxisViewModel AxisDepth { get; }
    }
}
