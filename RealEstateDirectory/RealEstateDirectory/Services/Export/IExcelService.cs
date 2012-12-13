using System.Windows.Controls;
using Misc.Miscellaneous;

namespace RealEstateDirectory.Services.Export
{
    public interface IExcelService
    {
        void ExportToExcel(DataGrid grid);
		void ExportToExcel(ExportObject data);
    }
}