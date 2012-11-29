using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using RealEstateDirectory.Services;
using Xceed.Wpf.DataGrid;

namespace RealEstateDirectory.MainFormTabs.House
{
    /// <summary>
    /// Interaction logic for HouseListView.xaml
    /// </summary>
    public partial class HouseListView  : UserControl
    {
        public HouseListView()
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