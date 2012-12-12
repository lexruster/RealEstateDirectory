using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RealEstateDirectory.Services;
using RealEstateDirectory.Services.Export;

namespace RealEstateDirectory.MainFormTabs.Residence
{
	/// <summary>
	/// Interaction logic for ResidenceListView.xaml
	/// </summary>
	public partial class ResidenceListView : UserControl
	{
		public ResidenceListView()
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
