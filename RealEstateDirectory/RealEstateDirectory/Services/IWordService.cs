using System.Windows.Controls;

namespace RealEstateDirectory.Services
{
	public interface IWordService
    {
        void ExportToWord(DataGrid grid, string fileName);
    }
}