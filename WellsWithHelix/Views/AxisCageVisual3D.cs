using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using WellsWithHelix.Helpers;

namespace WellsWithHelix.Views
{
    public class AxisCageVisual3D : UIElement3D
    {
        #region Dependency properties

        public static readonly DependencyProperty AxisXVectorProperty = DependencyProperty.Register(nameof(AxisXVector),
            typeof(Vector3D), typeof(AxisCageVisual3D),
            new PropertyMetadata(new Vector3D(1, 0, 0), GeometryChanged));
        public static readonly DependencyProperty AxisYVectorProperty = DependencyProperty.Register(nameof(AxisYVector),
            typeof(Vector3D), typeof(AxisCageVisual3D),
            new PropertyMetadata(new Vector3D(0, 1, 0), GeometryChanged));
        public static readonly DependencyProperty AxisZVectorProperty = DependencyProperty.Register(nameof(AxisZVector),
            typeof(Vector3D), typeof(AxisCageVisual3D),
            new PropertyMetadata(new Vector3D(0, 0, -1), GeometryChanged));

        public static readonly DependencyProperty MinXProperty = DependencyProperty.Register(nameof(MinX),
            typeof(double), typeof(AxisCageVisual3D), new PropertyMetadata(0.0, DimensionChanged));
        public static readonly DependencyProperty MaxXProperty = DependencyProperty.Register(nameof(MaxX),
            typeof(double), typeof(AxisCageVisual3D), new PropertyMetadata(0.0, DimensionChanged));

        public static readonly DependencyProperty MinYProperty = DependencyProperty.Register(nameof(MinY),
            typeof(double), typeof(AxisCageVisual3D), new PropertyMetadata(0.0, DimensionChanged));
        public static readonly DependencyProperty MaxYProperty = DependencyProperty.Register(nameof(MaxY),
            typeof(double), typeof(AxisCageVisual3D), new PropertyMetadata(0.0, DimensionChanged));

        public static readonly DependencyProperty MinZProperty = DependencyProperty.Register(nameof(MinZ),
            typeof(double), typeof(AxisCageVisual3D), new PropertyMetadata(0.0, DimensionChanged));
        public static readonly DependencyProperty MaxZProperty = DependencyProperty.Register(nameof(MaxZ),
            typeof(double), typeof(AxisCageVisual3D), new PropertyMetadata(0.0, DimensionChanged));

        public static readonly DependencyProperty GridMajorXProperty = DependencyProperty.Register(nameof(GridMajorX),
            typeof(double), typeof(AxisCageVisual3D), new PropertyMetadata(1000.0, DimensionChanged));
        public static readonly DependencyProperty GridMinorXProperty = DependencyProperty.Register(nameof(GridMinorX),
            typeof(double), typeof(AxisCageVisual3D), new PropertyMetadata(500.0, DimensionChanged));

        public static readonly DependencyProperty GridMajorYProperty = DependencyProperty.Register(nameof(GridMajorY),
            typeof(double), typeof(AxisCageVisual3D), new PropertyMetadata(1000.0, DimensionChanged));
        public static readonly DependencyProperty GridMinorYProperty = DependencyProperty.Register(nameof(GridMinorY),
            typeof(double), typeof(AxisCageVisual3D), new PropertyMetadata(500.0, DimensionChanged));

        public static readonly DependencyProperty GridMajorZProperty = DependencyProperty.Register(nameof(GridMajorZ),
            typeof(double), typeof(AxisCageVisual3D), new PropertyMetadata(100.0, DimensionChanged));
        public static readonly DependencyProperty GridMinorZProperty = DependencyProperty.Register(nameof(GridMinorZ),
            typeof(double), typeof(AxisCageVisual3D), new PropertyMetadata(50.0, DimensionChanged));

        public static readonly DependencyProperty AxisTitleXProperty = DependencyProperty.Register(nameof(AxisTitleX),
            typeof(string), typeof(AxisCageVisual3D), new PropertyMetadata("", UnitTextChanged));
        public static readonly DependencyProperty AxisTitleYProperty = DependencyProperty.Register(nameof(AxisTitleY),
            typeof(string), typeof(AxisCageVisual3D), new PropertyMetadata("", UnitTextChanged));
        public static readonly DependencyProperty AxisTitleZProperty = DependencyProperty.Register(nameof(AxisTitleZ),
            typeof(string), typeof(AxisCageVisual3D), new PropertyMetadata("", UnitTextChanged));

        public static readonly DependencyProperty NumberFormatXProperty = DependencyProperty.Register(nameof(NumberFormatX),
            typeof(string), typeof(AxisCageVisual3D), new PropertyMetadata("F2", NumberFormatChanged));
        public static readonly DependencyProperty NumberFormatYProperty = DependencyProperty.Register(nameof(NumberFormatY),
            typeof(string), typeof(AxisCageVisual3D), new PropertyMetadata("F2", NumberFormatChanged));
        public static readonly DependencyProperty NumberFormatZProperty = DependencyProperty.Register(nameof(NumberFormatZ),
            typeof(string), typeof(AxisCageVisual3D), new PropertyMetadata("F2", NumberFormatChanged));

        /// <summary>
        /// Identifies the <see cref="Thickness"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register(nameof(Thickness),
            typeof(double), typeof(AxisCageVisual3D), new PropertyMetadata(5.0, GeometryChanged));

        public Vector3D AxisXVector
        {
            get { return (Vector3D)GetValue(AxisXVectorProperty); }
            set { SetValue(AxisXVectorProperty, value); }
        }
        public Vector3D AxisYVector
        {
            get { return (Vector3D)GetValue(AxisYVectorProperty); }
            set { SetValue(AxisYVectorProperty, value); }
        }
        public Vector3D AxisZVector
        {
            get { return (Vector3D)GetValue(AxisZVectorProperty); }
            set { SetValue(AxisZVectorProperty, value); }
        }

        public double MinX
        {
            get { return (double)GetValue(MinXProperty); }
            set { SetValue(MinXProperty, value); }
        }
        public double MaxX
        {
            get { return (double)GetValue(MaxXProperty); }
            set { SetValue(MaxXProperty, value); }
        }

        public double MinY
        {
            get { return (double)GetValue(MinYProperty); }
            set { SetValue(MinYProperty, value); }
        }
        public double MaxY
        {
            get { return (double)GetValue(MaxYProperty); }
            set { SetValue(MaxYProperty, value); }
        }

        public double MinZ
        {
            get { return (double)GetValue(MinZProperty); }
            set { SetValue(MinZProperty, value); }
        }
        public double MaxZ
        {
            get { return (double)GetValue(MaxZProperty); }
            set { SetValue(MaxZProperty, value); }
        }

        public double GridMajorX
        {
            get { return (double)GetValue(GridMajorXProperty); }
            set { SetValue(GridMajorXProperty, value); }
        }
        public double GridMinorX
        {
            get { return (double)GetValue(GridMinorXProperty); }
            set { SetValue(GridMinorXProperty, value); }
        }

        public double GridMajorY
        {
            get { return (double)GetValue(GridMajorYProperty); }
            set { SetValue(GridMajorYProperty, value); }
        }
        public double GridMinorY
        {
            get { return (double)GetValue(GridMinorYProperty); }
            set { SetValue(GridMinorYProperty, value); }
        }

        public double GridMajorZ
        {
            get { return (double)GetValue(GridMajorZProperty); }
            set { SetValue(GridMajorZProperty, value); }
        }
        public double GridMinorZ
        {
            get { return (double)GetValue(GridMinorZProperty); }
            set { SetValue(GridMinorZProperty, value); }
        }

        public string AxisTitleX
        {
            get { return (string)GetValue(AxisTitleXProperty); }
            set { SetValue(AxisTitleXProperty, value); }
        }
        public string AxisTitleY
        {
            get { return (string)GetValue(AxisTitleYProperty); }
            set { SetValue(AxisTitleYProperty, value); }
        }
        public string AxisTitleZ
        {
            get { return (string)GetValue(AxisTitleZProperty); }
            set { SetValue(AxisTitleZProperty, value); }
        }

        public string NumberFormatX
        {
            get { return (string)GetValue(NumberFormatXProperty); }
            set { SetValue(NumberFormatXProperty, value); }
        }
        public string NumberFormatY
        {
            get { return (string)GetValue(NumberFormatYProperty); }
            set { SetValue(NumberFormatYProperty, value); }
        }
        public string NumberFormatZ
        {
            get { return (string)GetValue(NumberFormatZProperty); }
            set { SetValue(NumberFormatZProperty, value); }
        }

        /// <summary>
        /// Gets or sets the thickness of the grid lines.
        /// </summary>
        /// <value>The thickness.</value>
        public double Thickness
        {
            get
            {
                return (double)GetValue(ThicknessProperty);
            }

            set
            {
                SetValue(ThicknessProperty, value);
            }
        }

        #endregion

        #region Dependency props event handlers

        private static void DimensionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GeometryChanged(d, e);
        }

        private static void UnitTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GeometryChanged(d, e);
        }

        private static void NumberFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GeometryChanged(d, e);
        }

        private static void GeometryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cage = d as AxisCageVisual3D;
            cage?.ReinitVisual();
        }

        #endregion

        private readonly GeometryModel3D _walls = new GeometryModel3D();
        private readonly TextGroupVisual3D _billboard = new TextGroupVisual3D
        {
            Background = null,
            Foreground = Brushes.Black,
            IsDoubleSided = true
        };

        /// <summary>
        /// Initializes a new instance of the <see cref = "AxisCageVisual3D" /> class.
        /// </summary>
        public AxisCageVisual3D()
        {
            ReinitVisual();
        }

        private bool _isEditing = false;
        public void BeginEdit()
        {
            _isEditing = true;
        }

        public void EndEdit()
        {
            _isEditing = false;

            ReinitVisual();
        }

        private void ReinitVisual()
        {
            if (_isEditing)
                return;

            Visual3DModel = null;

            if(!UpdateVisual())
                return;

            var group = new Model3DGroup();

            group.Children.Add(_walls);
            group.Children.Add(_billboard.Content);

            Visual3DModel = group;
        }

        protected bool UpdateVisual()
        {
            if (MinX == MaxX ||
                MinY == MaxY ||
                MinZ == MaxZ)
                return false;

            var meshBuilder = new MeshBuilder(true, false);
            var xVector = AxisXVector;
            var yVector = AxisYVector;
            var zVector = AxisZVector;

            // XY plane
            // length along the X axis
            // normal goes along the Z axis
            var xyPlaneCenter = Point3DHelper.CreateWpfPoint(0, 0, MaxZ, xVector, yVector, zVector);
            CreateGridLines(meshBuilder, xVector, yVector, zVector, xyPlaneCenter, MinX, MaxX, MinY, MaxY, GridMajorX, GridMinorX,
                GridMajorY, GridMinorY, Thickness);

            // XZ plane
            // length along the Z axis
            // normal goes along the Y axis
            var xzPlaneCenter = Point3DHelper.CreateWpfPoint(0, MinY, 0, xVector, yVector, zVector);
            CreateGridLines(meshBuilder, zVector, xVector, yVector, xzPlaneCenter, MinZ, MaxZ, MinX, MaxX, GridMajorZ, GridMinorZ,
                GridMajorX, GridMinorX, Thickness);

            // YZ plane
            // length along the Y axis
            // normal goes along the X axis
            var yzPlaneCenter = Point3DHelper.CreateWpfPoint(MinX, 0, 0, xVector, yVector, zVector);
            CreateGridLines(meshBuilder, yVector, zVector, xVector, yzPlaneCenter, MinY, MaxY, MinZ, MaxZ, GridMajorY, GridMinorY,
                GridMajorZ, GridMinorZ, Thickness);

            var mesh = meshBuilder.ToMesh();
            mesh.Freeze();

            _walls.Geometry = mesh;
            _walls.Material = NormalMaterial;
            _walls.BackMaterial = _walls.Material;

            _billboard.Items = CreateGridLabels(
                MinX, MaxX, GridMajorX, xVector,
                MinY, MaxY, GridMajorY, yVector,
                MinZ, MaxZ, GridMajorZ, zVector);
            _billboard.Height = 100;

            return true;
        }

        #region Grid Labels

        private IList<SpatialTextItem> CreateGridLabels(
            double minX, double maxX, double gridMajorX, Vector3D xVector, 
            double minY, double maxY, double gridMajorY, Vector3D yVector, 
            double minZ, double maxZ, double gridMajorZ, Vector3D zVector)
        {
            var items = new List<SpatialTextItem>();

            items.AddRange(CraeteAxisLabels(minX, maxX, gridMajorX,
                NumberFormatX, AxisTitleX,
                maxY, xVector, yVector, Point3DHelper.CreateWpfPoint(0, 0, maxZ, xVector, yVector, zVector)));

            items.AddRange(CraeteAxisLabels(minY, maxY, gridMajorY,
                NumberFormatY, AxisTitleY,
                maxX, yVector, xVector, Point3DHelper.CreateWpfPoint(0, 0, maxZ, xVector, yVector, zVector)));

            items.AddRange(CraeteAxisLabels(minZ, maxZ, gridMajorZ,
                NumberFormatZ, AxisTitleZ,
                maxY, zVector, yVector, Point3DHelper.CreateWpfPoint(minX, 0, 0, xVector, yVector, zVector)));

            return items;
        }

        private static IEnumerable<SpatialTextItem> CraeteAxisLabels(double minimum, double maximum,
            double gridSpacing, string displayFormat, string title, double widthMaximum,
            Vector3D lengthVector, Vector3D widthVector, Point3D offset)
        {
            var eps = gridSpacing / 10;
            for (var length = minimum; length <= maximum + eps; length += gridSpacing)
            {
                var item = new SpatialTextItem();
                item.Position = GetPoint(length, widthMaximum + 20, lengthVector, widthVector, offset);
                item.Text = FormatNumber(length, displayFormat);
                item.TextDirection = widthVector;
                item.UpDirection = -lengthVector;

                yield return item;
            }

            var midPoint = minimum + (maximum - minimum) / 2;
            var axisTitle = new SpatialTextItem();
            axisTitle.Position = GetPoint(midPoint, widthMaximum + 500, lengthVector, widthVector, offset);
            axisTitle.Text = title;
            axisTitle.TextDirection = lengthVector;
            axisTitle.UpDirection = -widthVector;
            yield return axisTitle;
        }

        private static string FormatNumber(double length, string numberFormat)
        {
            return string.Format("{0:" + numberFormat + "}", length);
        }

        #endregion

        private static void CreateGridLines(MeshBuilder meshBuilder, Vector3D lengthDirection, Vector3D widthDirection, Vector3D normal, 
            Point3D center, double minLength,
            double maxLength, double minWidth, double maxWidth, double gridMajorLength, double gridMinorLength, double gridMajorWidth,
            double gridMinorWidth, double thickness)
        {
            double eps = gridMinorLength / 10;
            for (double length = minLength; length <= maxLength + eps; length += gridMinorLength)
            {
                double t = thickness;
                if (IsMultipleOf(length, gridMajorLength))
                {
                    t *= 2;
                }

                AddLineLenght(meshBuilder, length, minWidth, maxWidth, t, widthDirection, lengthDirection, center, normal);
            }

            eps = gridMinorWidth / 10;
            for (double width = minWidth; width <= maxWidth + eps; width += gridMinorWidth)
            {
                double t = thickness;
                if (IsMultipleOf(width, gridMajorWidth))
                {
                    t *= 2;
                }

                AddLineWidth(meshBuilder, width, minLength, maxLength, t, widthDirection, lengthDirection, center, normal);
            }
        }

        /// <summary>
        /// Determines whether y is a multiple of d.
        /// </summary>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <param name="d">
        /// The d.
        /// </param>
        /// <returns>
        /// The is multiple of.
        /// </returns>
        private static bool IsMultipleOf(double y, double d)
        {
            var y2 = d * (int)(y / d);
            return Math.Abs(y - y2) < 1e-3;
        }

        /// <summary>
        /// The add line x.
        /// </summary>
        /// <param name="mesh">
        /// The mesh.
        /// </param>
        /// <param name="length">
        /// The x.
        /// </param>
        /// <param name="minWidth">
        /// The min y.
        /// </param>
        /// <param name="maxWidth">
        /// The max y.
        /// </param>
        /// <param name="thickness">
        /// The thickness.
        /// </param>
        /// <param name="widthDirection"></param>
        /// <param name="lengthDirection"></param>
        /// <param name="center"></param>
        /// <param name="normal"></param>
        private static void AddLineLenght(MeshBuilder mesh, double length, double minWidth, double maxWidth, double thickness,
            Vector3D widthDirection, Vector3D lengthDirection, Point3D center, Vector3D normal)
        {
            int i0 = mesh.Positions.Count;
            mesh.Positions.Add(GetPoint(length - (thickness / 2), minWidth, lengthDirection, widthDirection, center));
            mesh.Positions.Add(GetPoint(length - (thickness / 2), maxWidth, lengthDirection, widthDirection, center));
            mesh.Positions.Add(GetPoint(length + (thickness / 2), maxWidth, lengthDirection, widthDirection, center));
            mesh.Positions.Add(GetPoint(length + (thickness / 2), minWidth, lengthDirection, widthDirection, center));
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);
            mesh.TriangleIndices.Add(i0);
            mesh.TriangleIndices.Add(i0 + 1);
            mesh.TriangleIndices.Add(i0 + 2);
            mesh.TriangleIndices.Add(i0 + 2);
            mesh.TriangleIndices.Add(i0 + 3);
            mesh.TriangleIndices.Add(i0);
        }

        /// <summary>
        /// The add line y.
        /// </summary>
        /// <param name="mesh">
        /// The mesh.
        /// </param>
        /// <param name="width">
        /// The y.
        /// </param>
        /// <param name="minLength">
        /// The min x.
        /// </param>
        /// <param name="maxLength">
        /// The max x.
        /// </param>
        /// <param name="thickness">
        /// The thickness.
        /// </param>
        /// <param name="widthDirection"></param>
        /// <param name="lengthDirection"></param>
        /// <param name="center"></param>
        /// <param name="normal"></param>
        private static void AddLineWidth(MeshBuilder mesh, double width, double minLength, double maxLength, double thickness,
            Vector3D widthDirection, Vector3D lengthDirection, Point3D center, Vector3D normal)
        {
            int i0 = mesh.Positions.Count;
            mesh.Positions.Add(GetPoint(minLength, width + (thickness / 2), lengthDirection, widthDirection, center));
            mesh.Positions.Add(GetPoint(maxLength, width + (thickness / 2), lengthDirection, widthDirection, center));
            mesh.Positions.Add(GetPoint(maxLength, width - (thickness / 2), lengthDirection, widthDirection, center));
            mesh.Positions.Add(GetPoint(minLength, width - (thickness / 2), lengthDirection, widthDirection, center));
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);
            mesh.TriangleIndices.Add(i0);
            mesh.TriangleIndices.Add(i0 + 1);
            mesh.TriangleIndices.Add(i0 + 2);
            mesh.TriangleIndices.Add(i0 + 2);
            mesh.TriangleIndices.Add(i0 + 3);
            mesh.TriangleIndices.Add(i0);
        }

        /// <summary>
        /// Gets a point on the plane.
        /// </summary>
        /// <param name="length">The x coordinate.</param>
        /// <param name="width">The y coordinate.</param>
        /// <param name="lengthDirection"></param>
        /// <param name="widthDirection"></param>
        /// <param name="center"></param>
        /// <returns>A <see cref="Point3D"/>.</returns>
        public static Point3D GetPoint(double length, double width, Vector3D lengthDirection, Vector3D widthDirection, Point3D center)
        {
            return center + (lengthDirection * length) + (widthDirection * width);
        }


        private Brush _normalBrush;
        private Brush NormalBrush
        {
            get
            {
                if (_normalBrush != null)
                    return _normalBrush;

                _normalBrush = new SolidColorBrush(Colors.Gray);
                _normalBrush.Freeze();

                return _normalBrush;
            }
        }

        private Material _normalMaterial;
        private Material NormalMaterial
        {
            get
            {
                if (_normalMaterial != null)
                    return _normalMaterial;

                _normalMaterial = new DiffuseMaterial(NormalBrush);
                _normalMaterial.Freeze();
                return _normalMaterial;
            }
        }
    }
}
