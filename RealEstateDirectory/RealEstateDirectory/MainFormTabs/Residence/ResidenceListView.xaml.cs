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
            var excelService = new ExcelService(new MessageService());
            excelService.ExportToExcel(DataList);
        }
	}
}
