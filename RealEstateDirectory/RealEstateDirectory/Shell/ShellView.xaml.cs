using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RealEstateDirectory.MainFormTabs.Flat;

namespace RealEstateDirectory.Shell
{
	/// <summary>
	/// Interaction logic for ShellView.xaml
	/// </summary>
	public partial class ShellView : Window
	{
		public ShellView()
		{
			InitializeComponent();

			Closing += (sender, args) =>
				{
					Application.Current.Shutdown();
				};
		}

		private void Print(object sender, RoutedEventArgs e)
		{
			var selected = Tabs.SelectedContent;
			FlatListView view = selected as FlatListView;
			var grid = view.DataList;

			PrintDialog printDialog = new PrintDialog();
			if (printDialog.ShowDialog() == true)
			{

				// Определить поля, 
				int pageMargin = 5;
// Получить размер страницы. 
				Size pageSize = new Size(printDialog.PrintableAreaWidth-pageMargin*2,
				printDialog.PrintableAreaHeight - 20);
// Инициировать установку размера элемента, 
				grid.Measure(pageSize);
				grid.Arrange(new Rect(pageMargin, pageMargin,
				                        pageSize.Width, pageSize.Height));


				//printDialog.PrintDocument(((DataGridPaginator)grid).DocumentPaginator, "A Flow Document"); 


				printDialog.PrintVisual(grid, "Квартиры");
			}

		}
	}
}