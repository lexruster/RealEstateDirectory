using System.Windows.Controls;
using Misc.Miscellaneous;

namespace RealEstateDirectory.Services.Export
{
	public interface IWordService
    {
        void ExportToWord(DataGrid grid, string fileName);
		void ExportToWord(ExportObject data);
    }
}