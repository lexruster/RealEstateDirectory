using System.Collections.Generic;
using System.Windows.Controls;
using Misc.Miscellaneous;

namespace RealEstateDirectory.Services.Export
{
	public interface IDataGridExportService
    {
        List<Header> GetHeader(DataGrid grid);
		List<List<string>> GetData(DataGrid grid);
    }
}