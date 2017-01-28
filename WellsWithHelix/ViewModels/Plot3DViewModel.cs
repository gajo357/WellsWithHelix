using System.Collections.ObjectModel;
using DevExpress.Mvvm.POCO;
using System.IO;
using System;

namespace WellsWithHelix.ViewModels
{
    public class Plot3DViewModel : IPlot3DViewModel
    {
        protected Plot3DViewModel()
        {
            InitializeProperties();

            AxisZSettings.Title = "North";
            AxisZSettings.MajorGrid = 1000;
            AxisZSettings.MinorGrid = 500;

            AxisXSettings.Title = "East";
            AxisXSettings.MajorGrid = 1000;
            AxisXSettings.MinorGrid = 500;

            AxisValueSettings.Title = "Depth";
            AxisValueSettings.Minimum = 0;
            AxisValueSettings.Maximum = 2000;
            AxisValueSettings.MajorGrid = 500;
            AxisValueSettings.MinorGrid = 250;
            AxisValueSettings.DisplayFormat = "F1";

            var count = 0;
            foreach (var file in Directory.GetFiles(@"..\..\Resources"))
            {
                var lines = File.ReadAllLines(file);

                var serie = Series3DViewModel.Create();
                serie.Title = Path.GetFileNameWithoutExtension(file);
                foreach(var line in lines)
                {
                    var values = line.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    var depth = double.Parse(values[4]) * 0.3048;
                    var east = double.Parse(values[1]);
                    var north = double.Parse(values[2]);
                    serie.AddPoint(east, depth, north);

                    CheckValues(depth, AxisValueSettings);
                    CheckValues(east, AxisXSettings);
                    CheckValues(north, AxisZSettings);
                }

                Series.Add(serie);
                count++;

                if (count > 20)
                    break;
            }
        }

        private void CheckValues(double value, IAxisSettingsViewModel axisSettings)
        {
            if (value < axisSettings.Minimum)
                axisSettings.Minimum = value;

            if (value > axisSettings.Maximum)
                axisSettings.Maximum = value;
        }

        public static Plot3DViewModel Create()
        {
            var factory = ViewModelSource.Factory(() => new Plot3DViewModel());

            return factory.Invoke();
        }

        public virtual string Title { get; set; }

        public ObservableCollection<ISeries3DViewModel> Series { get; private set; }

        public IAxisSettingsViewModel AxisXSettings { get; private set; }
        public IAxisSettingsViewModel AxisValueSettings { get; private set; }
        public IAxisSettingsViewModel AxisZSettings { get; private set; }

        private void InitializeProperties()
        {
            AxisXSettings = AxisSettingsViewModel.Create();
            AxisZSettings = AxisSettingsViewModel.Create();
            AxisValueSettings = AxisSettingsViewModel.Create();
            
            Series = new ObservableCollection<ISeries3DViewModel>();
        }
    }
}
