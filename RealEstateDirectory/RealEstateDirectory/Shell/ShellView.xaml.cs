using System;
using System.Collections.Generic;
using System.Printing;
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
using RealEstateDirectory.MainFormTabs.House;
using RealEstateDirectory.MainFormTabs.Plot;
using RealEstateDirectory.MainFormTabs.Residence;
using RealEstateDirectory.MainFormTabs.Room;

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
			try
			{
				var selected = Tabs.SelectedContent;
				DataGrid grid = null;
				var name = "";
				if (selected is FlatListView)
				{
					grid = (selected as FlatListView).DataList;
					name = "Квартиры";
				}
				if (selected is RoomListView)
				{
					grid = (selected as RoomListView).DataList;
					name = "Комнаты";
				}
				if (selected is PlotListView)
				{
					grid = (selected as PlotListView).DataList;
					name = "Участки";
				}
				if (selected is HouseListView)
				{
					grid = (selected as HouseListView).DataList;
					name = "Дома";
				}
				if (selected is ResidenceListView)
				{
					grid = (selected as ResidenceListView).DataList;
					name = "Помещения";
				}

				PrintDialog printDialog = new PrintDialog();

				printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;


				if (printDialog.ShowDialog() == false)
					return;
				string documentTitle = name;
				int pageMargin = 10;
				Size pageSize = new Size(printDialog.PrintableAreaWidth - pageMargin*2,
				                         printDialog.PrintableAreaHeight - 20);
				//// Инициировать установку размера элемента, 
				grid.Measure(pageSize);
				grid.Arrange(new Rect(pageMargin, pageMargin, pageSize.Width, pageSize.Height));

				CustomDataGridDocumentPaginator paginator = new CustomDataGridDocumentPaginator(grid, documentTitle, pageSize,
				                                                                                new Thickness(30, 20, 30, 20));
				printDialog.PrintDocument(paginator, "Квартиры");
			}
			catch (Exception)
			{
				MessageBox.Show("Произошла ошибка при печати", "Ошибка");
			}

		}
	}
}