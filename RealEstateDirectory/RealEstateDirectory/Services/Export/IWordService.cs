using System.Windows.Controls;

namespace RealEstateDirectory.Services.Export
{
	public interface IWordService
    {
        void ExportToWord(DataGrid grid, string fileName);
		void ExportToWord(string[] headers, string[,] data, string fileName);
    }
}