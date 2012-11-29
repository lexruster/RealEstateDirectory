using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using RealEstateDirectory.Services;
using Xceed.Wpf.DataGrid;

namespace RealEstateDirectory.MainFormTabs.Plot
{
    /// <summary>
    /// Interaction logic for RoomListView.xaml
    /// </summary>
    public partial class PlotListView : UserControl
    {
        public PlotListView()
        {
            InitializeComponent();
        }

        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            var excelService = new ExcelService(new MessageService());
            excelService.ExportToExcel(DataList);
        }
    }
}