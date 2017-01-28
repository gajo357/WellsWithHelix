using System;
using System.Collections.Generic;
using System.Windows.Media;
using WellsWithHelix.Enums;

namespace WellsWithHelix.ViewModels
{
    public interface ISeries3DViewModel
    {
        bool IsSelected { get; set; }

        string Title { get; set; }
        Color Color { get; set; }
        int Thickness { get; set; }

        PointMarkerType3D MarkerType { get; set; }
        GalleryType3D Gallery { get; set; }

        IEnumerable<IPoint3DViewModel> GetPoints();
        void AddPoint(double xNative, double valueNative, double zNative);
    }
}
