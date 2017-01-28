using System.Collections.ObjectModel;

namespace WellsWithHelix.ViewModels
{
    public interface IPlot3DViewModel
    {
        string Title { get; set; }

        IAxisSettingsViewModel AxisXSettings { get; }

        IAxisSettingsViewModel AxisValueSettings { get; }

        IAxisSettingsViewModel AxisZSettings { get; }

        ObservableCollection<ISeries3DViewModel> Series { get; }
    }
}
