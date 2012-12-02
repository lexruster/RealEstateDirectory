using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.MainFormTabs.Flat
{
    /// <summary>
    /// Interaction logic for FlatListView.xaml
    /// </summary>
    public partial class FlatListView : UserControl
    {
        public FlatListView()
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