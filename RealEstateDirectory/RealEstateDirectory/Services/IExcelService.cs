using System.Windows.Controls;

namespace RealEstateDirectory.Services
{
    public interface IExcelService
    {
        void ExportToExcel(DataGrid grid);
    }
}