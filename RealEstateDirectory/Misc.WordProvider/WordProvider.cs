using System;
using System.Drawing;
using System.Linq;
using Misc.Miscellaneous;
using Novacode;
using Word = Microsoft.Office.Interop.Word;

namespace Misc.WordProvider
{
	public static class WordProvider
	{
		public static void Generate(ExportObject exportObject, string fileName)
		{
			using (DocX document = DocX.Create(fileName + ".docx"))
			{
				document.PageLayout.Orientation = Orientation.Landscape;

				Paragraph p = document.InsertParagraph();
				p.Append("E-8").Font(new FontFamily("Times New Roman")).Bold();
				p.Alignment = Alignment.center;

				Paragraph pDate = document.InsertParagraph();
				pDate.Append(String.Format("{0:dd.MM.yyyy}", DateTime.Now)).Font(new FontFamily("Times New Roman"));
				pDate.Alignment = Alignment.left;

				foreach (ExportTable table in exportObject.Tables)
				{
					var title = table.Title;
					var headers = table.Headers;
					var data = table.Data;

					var tableHeader = document.InsertParagraph();

					tableHeader.Append(title).Font(new FontFamily("Times New Roman")).Bold();
					tableHeader.Alignment = Alignment.center;

					Table tab = document.AddTable(data.Count + 1, headers.Count);
					tab.Alignment = Alignment.center;

					for (int j = 0; j < headers.Count; j++)
					{
						tab.Rows[0].Cells[j].Paragraphs.First().Append(headers[j]).Font(new FontFamily("Times New Roman")).Bold();
					}

					for (int i = 0; i < data.Count; i++)
					{
						for (int j = 0; j < headers.Count; j++)
						{
							tab.Rows[i + 1].Cells[j].Paragraphs.First().Append(data[i][j] ?? "").Font(new FontFamily("Times New Roman"));
						}
					}

					tab.AutoFit = AutoFit.Contents;
					document.InsertTable(tab);
					document.InsertParagraph();
				}

				document.Save();
				Convert(fileName + ".docx", fileName, Word.WdSaveFormat.wdFormatRTF);
			}
		}

		// Convert a Word 2008 .docx to Word 2003 .doc
		public static void Convert(string input, string output, Word.WdSaveFormat format)
		{
			// Create an instance of Word.exe
			Word._Application oWord = new Word.Application();

			// Make this instance of word invisible (Can still see it in the taskmgr).
			oWord.Visible = false;

			// Interop requires objects.
			object oMissing = System.Reflection.Missing.Value;
			object isVisible = true;
			object readOnly = false;
			object oInput = input;
			object oOutput = output;
			object oFormat = format;

			// Load a document into our instance of word.exe
			Word._Document oDoc = oWord.Documents.Open(ref oInput, ref oMissing, ref readOnly, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref isVisible, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

			// Make this document the active document.
			oDoc.Activate();

			// Save this document in Word 2003 format.
			oDoc.SaveAs(ref oOutput, ref oFormat, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

			// Always close Word.exe.
			oWord.Quit(ref oMissing, ref oMissing, ref oMissing);
		}
	}
}