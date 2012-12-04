using System.Windows.Controls;

namespace RealEstateDirectory.Services.Export
{
	public interface IDataExportService
    {
        string[] GetHeader(DataGrid grid);
		string[,] GetData(DataGrid grid);
    }
}