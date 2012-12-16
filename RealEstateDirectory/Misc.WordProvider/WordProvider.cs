using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Misc.Miscellaneous;
using Novacode;
using Header = Misc.Miscellaneous.Header;
using Word = Microsoft.Office.Interop.Word;
using XmlDocument = System.Xml.XmlDocument;

namespace Misc.WordProvider
{
	public static class WordProvider
	{
		public static void Generate(ExportObject exportObject, string fileName)
		{
			var onlyName = Path.GetFileNameWithoutExtension(fileName);
			var path = Path.GetDirectoryName(fileName) ?? "";
			var resultDocx = Path.Combine(path, onlyName + ".back.docx");
			var newDocx = Path.Combine(path, onlyName + ".docx");
			var rtf = Path.Combine(path, onlyName + ".rtf");
			using (DocX document = DocX.Create(resultDocx))
			{
				document.PageLayout.Orientation = Orientation.Landscape;
				var font = new FontFamily("Times New Roman");
				
				Paragraph p = document.InsertParagraph();
				
				p.Append("E-8").Font(font).FontSize(9).Bold();
				p.Alignment = Alignment.center;

				Paragraph pDate = document.InsertParagraph();
				pDate.Append(String.Format("{0:dd.MM.yyyy}", DateTime.Now)).Font(font).FontSize(9);
				pDate.Alignment = Alignment.left;

				foreach (ExportTable dataTable in exportObject.Tables)
				{
					var title = dataTable.Title;
					var headers = dataTable.Headers;
					var data = dataTable.Data;

					var tableHeader = document.InsertParagraph();

					tableHeader.Append(title).Font(new FontFamily("Times New Roman")).Bold();
					tableHeader.Alignment = Alignment.center;

					Table table = document.AddTable(data.Count + 1, headers.Count);
					table.AutoFit = AutoFit.Window;
					table.Alignment = Alignment.center;

					for (int j = 0; j < headers.Count; j++)
					{
						var cell = table.Rows[0].Cells[j];
						cell.Paragraphs.First().Append(headers[j].Name).Font(font).FontSize(9).Bold();
						SetParams(cell, headers[j]);
					}

					for (int i = 0; i < data.Count; i++)
					{
						for (int j = 0; j < headers.Count; j++)
						{
							var cell = table.Rows[i + 1].Cells[j];
							cell.Paragraphs.First().Append(data[i][j] ?? "").Font(font).FontSize(9);
							SetParams(cell, headers[j]);
						}
					}

					//table.AutoFit = AutoFit.ColumnWidth;
					table.AutoFit = AutoFit.Contents;
					document.InsertTable(table);
					document.InsertParagraph();
				}

				document.Save();
				ConvertMod(resultDocx, newDocx, Word.WdSaveFormat.wdFormatXMLDocument);
				ConvertMod(resultDocx, rtf, Word.WdSaveFormat.wdFormatRTF);
			}

			if(File.Exists(resultDocx) && File.Exists(newDocx))
			{
				File.Delete(resultDocx);
			}
		}

		private static void SetParams(Cell cell, Header header)
		{
			if (header.Width > 0)
			{
				//cell.Width = header.Width;
			}
			
			cell.MarginTop = 1;
			cell.MarginRight = 1;
			cell.MarginBottom = 1;
			cell.MarginLeft = 1;
		}

		// Convert a Word 2008 .docx to Word 2003 .doc
		public static void Convert(string input, string output, Word.WdSaveFormat format)
		{
			// Create an instance of Word.exe
			Word._Application oWord = new Word.Application();
			object oMissing = System.Reflection.Missing.Value;
			object isVisible = true;
			object readOnly = false;
			object oInput = input;
			object oOutput = output;
			object oFormat = format;
			try
			{
				// Make this instance of word invisible (Can still see it in the taskmgr).
				oWord.Visible = false;

				// Load a document into our instance of word.exe
				Word._Document oDoc = oWord.Documents.Open(ref oInput, ref oMissing, ref readOnly, ref oMissing, ref oMissing,
				                                           ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
				                                           ref oMissing, ref isVisible, ref oMissing, ref oMissing, ref oMissing,
				                                           ref oMissing);

				// Make this document the active document.
				oDoc.Activate();

				// Save this document in Word 2003 format.
				oDoc.SaveAs(ref oOutput, ref oFormat, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
				            ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
				            ref oMissing, ref oMissing);

				// Always close Word.exe.
				oWord.Quit(ref oMissing, ref oMissing, ref oMissing);
			}
			catch (Exception e)
			{
				oWord.Quit(ref oMissing, ref oMissing, ref oMissing);
				throw e;
			}
		}

		public static void ConvertMod(string input, string output, Word.WdSaveFormat format)
		{
			// Create an instance of Word.exe
			Word._Application oWord = new Word.Application();
			object oMissing = System.Reflection.Missing.Value;
			object isVisible = true;
			object readOnly = false;
			object oInput = input;
			object oOutput = output;
			object oFormat = format;
			try
			{
				// Make this instance of word invisible (Can still see it in the taskmgr).
				oWord.Visible = false;

				// Load a document into our instance of word.exe
				Word._Document oDoc = oWord.Documents.Open(ref oInput, ref oMissing, ref readOnly, ref oMissing, ref oMissing,
														   ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
														   ref oMissing, ref isVisible, ref oMissing, ref oMissing, ref oMissing,
														   ref oMissing);

				// Make this document the active document.
				oDoc.Activate();

				oDoc.PageSetup.LeftMargin = 15;
				oDoc.PageSetup.RightMargin = 15;
				oDoc.PageSetup.TopMargin = 15;
				oDoc.PageSetup.BottomMargin = 15;


				// Save this document in Word 2003 format.
				oDoc.SaveAs(ref oOutput, ref oFormat, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
							ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
							ref oMissing, ref oMissing);

				// Always close Word.exe.
				oWord.Quit(ref oMissing, ref oMissing, ref oMissing);
			}
			catch (Exception e)
			{
				oWord.Quit(ref oMissing, ref oMissing, ref oMissing);
				throw e;
			}
		}
	}
}