using DevExpress.Mvvm.POCO;

namespace WellsWithHelix.ViewModels
{
    public class AxisViewModel
    {
        public AxisViewModel()
        {
            NumberFormat = "F2";
        }

        public static AxisViewModel Create()
        {
            return ViewModelSource.Create(() => new AxisViewModel());
        }

        public virtual string Title { get; set; }

        public virtual double Minimum { get; set; }
        public virtual double Maximum { get; set; }

        public virtual double MajorGrid { get; set; }
        public virtual double MinorGrid { get; set; }

        public virtual string UnitText { get; set; }
        public virtual string NumberFormat { get; set; }
    }

}
