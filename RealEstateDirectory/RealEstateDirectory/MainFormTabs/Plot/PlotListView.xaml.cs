using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using RealEstateDirectory.Services;
using RealEstateDirectory.Services.Export;

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
			var messageService = new MessageService();
			var excelService = new ExcelService(messageService, new DataExportService(messageService));
			excelService.ExportToExcel(DataList);
		}

		private void ExportToRTF(object sender, RoutedEventArgs e)
		{
			Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
			dlg.DefaultExt = "rich text format file |*.rtf";
			dlg.Filter = "rtf file |*.rtf";
			if (dlg.ShowDialog() == true)
			{
				string filename = dlg.FileName;
				var messageService = new MessageService();
				var wordService = new WordService(messageService, new DataExportService(messageService));
				wordService.ExportToWord(DataList, filename);
			}
		}
    }
}