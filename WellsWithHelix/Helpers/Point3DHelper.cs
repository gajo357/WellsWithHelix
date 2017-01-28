using System;
using System.Windows.Media.Media3D;
using WellsWithHelix.ViewModels;

namespace WellsWithHelix.Helpers
{
    public class Point3DHelper
    {
        internal static Point3D CreateWpfPoint(double x, double y, double z, Vector3D xVector, Vector3D yVector, Vector3D zVector)
        {
            return new Point3D(0, 0, 0) + x * xVector + y * yVector + z * zVector;
        }

        internal static Point3D CreateWpfPoint(IPoint3DViewModel viewModel, Vector3D xVector, Vector3D yVector, Vector3D zVector)
        {
            return CreateWpfPoint(viewModel.X, viewModel.Z, viewModel.Value, xVector, yVector, zVector);
        }
    }
}
