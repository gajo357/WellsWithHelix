using HelixToolkit.Wpf;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using WellsWithHelix.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;
using WellsWithHelix.Views;
using System.Linq;

namespace WellsWithHelix
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty AxisXVectorProperty = DependencyProperty.Register(nameof(AxisXVector),
            typeof(Vector3D), typeof(MainWindow),
            new PropertyMetadata(new Vector3D(1, 0, 0)));
        public static readonly DependencyProperty AxisYVectorProperty = DependencyProperty.Register(nameof(AxisYVector),
            typeof(Vector3D), typeof(MainWindow),
            new PropertyMetadata(new Vector3D(0, 1, 0)));
        public static readonly DependencyProperty AxisZVectorProperty = DependencyProperty.Register(nameof(AxisZVector),
            typeof(Vector3D), typeof(MainWindow),
            new PropertyMetadata(new Vector3D(0, 0, -1)));

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
            // update axis grid
            Dispatcher.Invoke(() => ViewPort.Children.Remove(AxisCage));
            Dispatcher.Invoke(() => UpdateAxisCage(AxisCage, ViewModel.Plot));
            Dispatcher.Invoke(() => ViewPort.Children.Add(AxisCage));

            // add series
            Dispatcher.Invoke(() => ViewPort.Children.Remove(SeriesModel));
            Dispatcher.Invoke(() => CreateSeries(SeriesModel, ViewModel.Plot));
            Dispatcher.Invoke(() => ViewPort.Children.Add(SeriesModel));

            ZoomExtents();
        }

        private void CreateSeries(ModelVisual3D seriesModel, IPlot3DViewModel plot)
        {
            seriesModel.Children.Clear();
            foreach (var series in plot.Series)
            {
                seriesModel.Children.Add(new InteractiveVisual3D(series, AxisXVector, AxisYVector, AxisZVector));
            }
        }

        private void UpdateAxisCage(AxisCageVisual3D axisCage, IPlot3DViewModel plot)
        {
            axisCage.BeginEdit();

            axisCage.AxisTitleX = plot.AxisXSettings.Title;
            axisCage.NumberFormatX = plot.AxisXSettings.DisplayFormat;
            axisCage.MinX = plot.AxisXSettings.Minimum;
            axisCage.MaxX = plot.AxisXSettings.Maximum;
            axisCage.GridMajorX = plot.AxisXSettings.MajorGrid;
            axisCage.GridMinorX = plot.AxisXSettings.MinorGrid;

            axisCage.AxisTitleY = plot.AxisZSettings.Title;
            axisCage.NumberFormatY = plot.AxisZSettings.DisplayFormat;
            axisCage.MinY = plot.AxisZSettings.Minimum;
            axisCage.MaxY = plot.AxisZSettings.Maximum;
            axisCage.GridMajorY = plot.AxisZSettings.MajorGrid;
            axisCage.GridMinorY = plot.AxisZSettings.MinorGrid;

            axisCage.AxisTitleZ = plot.AxisValueSettings.Title;
            axisCage.NumberFormatZ = plot.AxisValueSettings.DisplayFormat;
            axisCage.MinZ = plot.AxisValueSettings.Minimum;
            axisCage.MaxZ = plot.AxisValueSettings.Maximum;
            axisCage.GridMajorZ = plot.AxisValueSettings.MajorGrid;
            axisCage.GridMinorZ = plot.AxisValueSettings.MinorGrid;

            axisCage.EndEdit();
        }

        public void ZoomExtents(object sender, EventArgs e)
        {
            ZoomExtents();
        }
        
        public void ZoomExtents()
        {
            // time in miliseconds
            Dispatcher.Invoke(() => ViewPort.ZoomExtents(500));
        }

        private void ViewPort_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void ViewPort_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var visual = ViewPort.FindNearestVisual(e.GetPosition(ViewPort));
            if (visual == null)
                return;

            foreach (var serie in SeriesModel.Children.Cast<InteractiveVisual3D>())
            {
                serie.IsSelected = serie == visual;
            }
        }
    }
}
