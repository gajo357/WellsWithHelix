using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using WellsWithHelix.Helpers;
using WellsWithHelix.ViewModels;
using WellsWithHelix.Enums;

namespace WellsWithHelix.Views
{
    public class InteractiveVisual3D : UIElement3D
    {
        #region Dependency properties

        public static readonly DependencyProperty AxisXVectorProperty = DependencyProperty.Register(nameof(AxisXVector),
            typeof(Vector3D), typeof(InteractiveVisual3D),
            new PropertyMetadata(new Vector3D(1, 0, 0)));
        public static readonly DependencyProperty AxisYVectorProperty = DependencyProperty.Register(nameof(AxisYVector),
            typeof(Vector3D), typeof(InteractiveVisual3D),
            new PropertyMetadata(new Vector3D(0, 1, 0)));
        public static readonly DependencyProperty AxisZVectorProperty = DependencyProperty.Register(nameof(AxisZVector),
            typeof(Vector3D), typeof(InteractiveVisual3D),
            new PropertyMetadata(new Vector3D(0, 0, -1)));

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
            typeof(ISeries3DViewModel),
            typeof(InteractiveVisual3D), new PropertyMetadata(null));

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(nameof(IsSelected),
            typeof(bool), typeof(InteractiveVisual3D),
            new PropertyMetadata(false, IsSelectedProperty_ChangedCallback));

        private static void IsSelectedProperty_ChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var uc = dependencyObject as InteractiveVisual3D;
            if (uc != null)
                uc.IsSelectedChanged();
        }

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

        public ISeries3DViewModel ViewModel
        {
            get { return (ISeries3DViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        #endregion

        private readonly GeometryModel3D _wells = new GeometryModel3D();
        private readonly TextVisual3D _billboard = new TextVisual3D
        {
            Background = null,
            Foreground = Brushes.White
        };

        public InteractiveVisual3D(ISeries3DViewModel viewModel, Vector3D axisX, Vector3D axisY, Vector3D axisZ)
        {
            AxisXVector = axisX;
            AxisYVector = axisY;
            AxisZVector = axisZ;

            ViewModel = viewModel;


            Visual3DModel = null;
            UpdateVisual();

            var group = new Model3DGroup();

            group.Children.Add(_wells);
            group.Children.Add(_billboard.Content);

            Visual3DModel = group;
        }

        protected void UpdateVisual()
        {
            var xVector = AxisXVector;
            var yVector = AxisYVector;
            var zVector = AxisZVector;

            var meshBuilder = new MeshBuilder(false, false);

            var path = new Point3DCollection(ViewModel.GetPoints().Select(viewModel => Point3DHelper.CreateWpfPoint(viewModel, xVector, yVector, zVector)));
            path.Freeze();
            if (ViewModel.Gallery == GalleryType3D.Line)
            {
                var diameter = 20;
                var thetaDiv = 12;

                meshBuilder.AddTube(path, diameter,
                    thetaDiv, false);
            }
            
            var point = path[0];
            if (ViewModel.MarkerType == PointMarkerType3D.Sphere)
            {
                int radius = 30;
                meshBuilder.AddSphere(point, radius);
            }
            else
            {
                var dimension = 30;
                meshBuilder.AddBox(point, dimension, dimension, dimension, MeshBuilder.BoxFaces.All ^ MeshBuilder.BoxFaces.Bottom);
            }

            var mesh = meshBuilder.ToMesh();
            mesh.Freeze();

            _wells.Geometry = mesh;
            SetWellsMaterial();
            //_wells.Freeze();

            _billboard.Text = ViewModel.Title;
            _billboard.Position = point - 50 * zVector;
            _billboard.Height = 50;
            _billboard.FontSize = 50;
            _billboard.UpDirection = -zVector;
            _billboard.TextDirection = xVector;
            _billboard.IsDoubleSided = true;
            _billboard.Background = NormalBrush;
        }

        private void SetWellsMaterial()
        {
            _wells.Material = IsSelected ? SelectedMaterial : NormalMaterial;
            _wells.BackMaterial = null;
        }

        private void IsSelectedChanged()
        {
            SetWellsMaterial();
        }

        private Brush _normalBrush;
        private Brush NormalBrush
        {
            get
            {
                if (_normalBrush != null)
                    return _normalBrush;

                _normalBrush = new SolidColorBrush(ViewModel.Color);
                _normalBrush.Freeze();

                return _normalBrush;
            }
        }

        private static Brush _selectedBrush;
        private static Brush SelectedBrush
        {
            get
            {
                if (_selectedBrush != null)
                    return _selectedBrush;

                _selectedBrush = Brushes.Yellow;
                _selectedBrush.Freeze();
                return _selectedBrush;
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

        private static Material _selectedMaterial;
        private static Material SelectedMaterial
        {
            get
            {
                if (_selectedMaterial != null)
                    return _selectedMaterial;

                _selectedMaterial = new DiffuseMaterial(SelectedBrush);
                _selectedMaterial.Freeze();
                return _selectedMaterial;
            }
        }
    }
}
