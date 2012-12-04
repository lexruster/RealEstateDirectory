using System.Windows.Controls;

namespace RealEstateDirectory.Services.Export
{
    public interface IExcelService
    {
        void ExportToExcel(DataGrid grid);
	    void ExportToExcel(string[] headers, string[,] data);
    }
}