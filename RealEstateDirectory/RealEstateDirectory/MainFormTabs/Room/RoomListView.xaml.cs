using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.Services;
using RealEstateDirectory.Services.Export;

namespace RealEstateDirectory.MainFormTabs.Room
{
    /// <summary>
    /// Interaction logic for RoomListView.xaml
    /// </summary>
    public partial class RoomListView : UserControl
    {
        public RoomListView()
        {
            InitializeComponent();
        }
        //אגעמ נאסקוע רטנטםû סעמכבצמג
        //private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        //{
        //    //Create a dispatcher to autofit all column widths
        //    Dispatcher.BeginInvoke(new Action(() =>
        //        {
        //            delayed_dataGrid_Loaded();
        //        }), DispatcherPriority.Background);
        //}

        //private void delayed_dataGrid_Loaded()
        //{
        //    //Autofit Column widths
        //    foreach (ColumnBase col in dataGrid.Columns)
        //        if (col.GetFittedWidth() != -1)
        //            if (col.ReadOnly)
        //                col.Width = col.GetFittedWidth();
        //            else
        //                col.Width = col.GetFittedWidth() < 64 ? 64 : col.GetFittedWidth();
        //}
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