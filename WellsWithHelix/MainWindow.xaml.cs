using HelixToolkit.Wpf;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using WellsWithHelix.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace WellsWithHelix
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel ViewModel => DataContext as MainViewModel;

        public MainWindow()
        {
            InitializeComponent();
            
            var dx = MainViewModel.Create();
            ((INotifyPropertyChanged)dx).PropertyChanged += MainViewModel_PropertyChanged;

            DataContext = dx;

            PlotChanged();
        }

        private void MainViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainViewModel.Plot))
            {
                PlotChanged();
            }
        }

        private void PlotChanged()
        {
            Dispatcher.Invoke(() => AxisGridModel.Children.Clear());
            Dispatcher.Invoke(() => AxisLabelsModel.Children.Clear());
            Dispatcher.Invoke(() => SeriesModel.Children.Clear());

            // Add axis labels
            Dispatcher.Invoke(() => Billboard.Items = CreateGridLabels(ViewModel.Plot));
            Dispatcher.Invoke(() => AxisLabelsModel.Children.Add(Billboard));

            // add axis grid

            // add series


            ZoomExtents();
        }

        public void ZoomExtents(object sender, EventArgs e)
        {
            ZoomExtents();
        }
        
        public void ZoomExtents()
        {
            Dispatcher.Invoke(() => ViewPort.ZoomExtents(0.5));
        }

        private static List<BillboardTextItem> CreateGridLabels(PlotViewModel plot)
        {
            var items = new List<BillboardTextItem>();

            var xVector = new Vector3D(1, 0, 0);
            var yVector = new Vector3D(0, 1, 0);
            var zVector = new Vector3D(0, 0, -1);

            items.AddRange(CraeteAxisLabels(plot.AxisX, plot.AxisY.Maximum, 
                xVector, yVector, new Point3D(0, 0, -plot.AxisZ.Maximum)));

            items.AddRange(CraeteAxisLabels(plot.AxisY, plot.AxisX.Maximum, 
                yVector, xVector, new Point3D(0, 0, -plot.AxisZ.Maximum)));

            items.AddRange(CraeteAxisLabels(plot.AxisZ, plot.AxisY.Maximum, 
                zVector, yVector, new Point3D(plot.AxisX.Minimum, 0, 0)));
            
            return items;
        }

        private static IEnumerable<BillboardTextItem> CraeteAxisLabels(AxisViewModel axis, double widthMaximum, 
            Vector3D lengthVector, Vector3D widthVector, Point3D offset)
        {
            var eps = axis.MinorGrid / 10;
            for(var length = axis.Minimum; length <= axis.Maximum + eps; length += axis.MajorGrid)
            {
                var item = new BillboardTextItem();
                item.Position = GetPoint(offset, length, widthMaximum + 20, lengthVector, widthVector);
                item.Text = FormatNumber(length, axis.NumberFormat);

                yield return item;
            }

            var midPoint = axis.Minimum + (axis.Maximum - axis.Minimum) / 2;
            var axisTitle = new BillboardTextItem();
            axisTitle.Position = GetPoint(offset, midPoint, widthMaximum + 50, lengthVector, widthVector);
            axisTitle.Text = axis.Title;
            yield return axisTitle;
        }

        private static string FormatNumber(double length, string numberFormat)
        {
            return string.Format("{0:" + numberFormat + "}", length);
        }

        private static Point3D GetPoint(Point3D offset, double length, double width, Vector3D lengthVector, Vector3D widthVector)
        {
            return offset + (lengthVector * length) + (widthVector * width);
        }
    }
}
