using DevExpress.Mvvm.POCO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using WellsWithHelix.Enums;
using WellsWithHelix.Helpers;

namespace WellsWithHelix.ViewModels
{
    public class Series3DViewModel : ISeries3DViewModel
    {
        public int? DataId { get; set; }

        public Series3DViewModel()
        {
            NativePoints = new List<Point3D>();

            SetDefaults();
        }

        public static Series3DViewModel Create()
        {
            return
                ViewModelSource.Create(
                    () => new Series3DViewModel());
        }

        public virtual bool IsSelected { get; set; }

        public virtual string Title { get; set; }

        public virtual Color Color { get; set; }

        public virtual int Thickness { get; set; }

        public virtual PointMarkerType3D MarkerType { get; set; }

        public virtual GalleryType3D Gallery { get; set; }
        public virtual bool ShowInLegend { get; set; }

        private IList<Point3D> NativePoints { get; }

        public IEnumerable<IPoint3DViewModel> GetPoints()
        {
            foreach (var point3D in NativePoints)
            {
                yield return new Point3DViewModel(point3D.X, point3D.Y, point3D.Z);
            }
        }

        public bool AnyNonEmptyPoints()
        {
            return NativePoints.Any();//p => p.Value.HasValue);
        }


        public void AddPoint(double xNative, double valueNative, double zNative)
        {
            // check for consecutive empty points
            NativePoints.Add(new Point3D(xNative, valueNative, zNative));
        }

        private void SetDefaults()
        {
            Title = string.Empty;
            Color = RandomColorGenerator.GenerateColor();
            Thickness = 2;
            ShowInLegend = true;

            Gallery = GalleryType3D.Line;
            MarkerType = PointMarkerType3D.Sphere;
        }

        private class Point3DViewModel : IPoint3DViewModel
        {
            public Point3DViewModel(double x, double value, double z)
            {
                X = x;
                Value = value;
                Z = z;
            }


            public double X { get; }
            public double Value { get; }
            public double Z { get; }
        }
    }
}
