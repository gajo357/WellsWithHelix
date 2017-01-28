using System;

namespace WellsWithHelix.ViewModels
{
    public interface IAxisSettingsViewModel
    {
        string Title { get; set; }
        string DisplayFormat { get; set; }

        double MajorGrid { get; set; }
        double MinorGrid { get; set; }

        bool Reverse { get; set; }

        double Minimum { get; set; }
        double Maximum { get; set; }
    }
}
