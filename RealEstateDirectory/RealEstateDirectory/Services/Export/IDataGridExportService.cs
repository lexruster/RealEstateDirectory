using System.Collections.Generic;
using System.Windows.Controls;

namespace RealEstateDirectory.Services.Export
{
	public interface IDataGridExportService
    {
        List<string> GetHeader(DataGrid grid);
		List<List<string>> GetData(DataGrid grid);
    }
}