using DevExpress.Mvvm.POCO;

namespace WellsWithHelix.ViewModels
{
    public class AxisSettingsViewModel : IAxisSettingsViewModel
    {
        public AxisSettingsViewModel()
        {
            SetDefaults();
        }

        public static IAxisSettingsViewModel Create()
        {
            var factory = ViewModelSource.Factory(() => new AxisSettingsViewModel());

            return factory.Invoke();
        }

        public virtual string Title { get; set; }

        public virtual double MajorGrid { get; set; }
        public virtual double MinorGrid { get; set; }

        public virtual string DisplayFormat { get; set; }

        public virtual bool Reverse { get; set; }

        public virtual double Minimum { get; set; }

        public virtual double Maximum { get; set; }

        private void SetDefaults()
        {
            DisplayFormat = "F2";

            MajorGrid = 1000;
            MinorGrid = 500;

            Reverse = false;

            Minimum = double.MaxValue;
            Maximum = double.MinValue;
        }
    }
}
