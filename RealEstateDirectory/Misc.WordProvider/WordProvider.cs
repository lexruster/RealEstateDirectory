using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Misc.Miscellaneous;
using Word = Microsoft.Office.Interop.Word;

namespace Misc.WordProvider
{
	public static class WordProvider
    {
		public static void Generate(ExportObject exportObject, string fileName)
		{
			//for (int j = 0; j < headers.Length; j++)
			//{
			//	t[j, 0].Formatting = new RtfParagraphFormatting(8, RtfTextAlign.Center);
			//	t[j, 0].Formatting.TextColorIndex = 1;
			//	t[j, 0].AppendText(headers[j]);
			//}

			//for (int i = 0; i < data.GetLength(0); i++)
			//{
			//	for (int j = 0; j < headers.Length; j++)
			//	{
			//		t[j, i + 1].Definition.Style = new RtfTableCellStyle(RtfBorderSetting.All, LeftAligned12,
			//															 RtfTableCellVerticalAlign.Bottom);
			//		t[j, i + 1].AppendText(data[i, j] ?? "");
			//	}
			//}


			Object nothing = System.Reflection.Missing.Value;
			//Directory.CreateDirectory("C:/CNSI"); // Create file category 
			//string name = "CNSI_" + "53asdf" + ".doc";
			//object filename = "C://CNSI//" + name; // file save path
			// create Word file
			Word.Application wordApp = new Word.ApplicationClass();
			Word.Document wordDoc = wordApp.Documents.Add(ref nothing, ref nothing, ref nothing, ref nothing);

			
			// Add header
			wordApp.ActiveWindow.View.Type = Word.WdViewType.wdNormalView;
			wordApp.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekPrimaryHeader;
			wordApp.ActiveWindow.ActivePane.Selection.InsertAfter("E-8");
			wordApp.Selection.ParagraphFormat.Alignment =
				Word.WdParagraphAlignment.wdAlignParagraphCenter; // Set right alignment 
			wordApp.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekMainDocument; // set pop up header 


			wordApp.ActiveWindow.View.Type = Word.WdViewType.wdOutlineView;
			wordApp.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekPrimaryHeader;
			wordApp.ActiveWindow.ActivePane.Selection.InsertAfter(String.Format("{0:dd.MM.yyyy}", DateTime.Now));
			wordApp.Selection.ParagraphFormat.Alignment =
				Word.WdParagraphAlignment.wdAlignParagraphLeft; // Set right alignment 
			wordApp.ActiveWindow.View.SeekView =
				Word.WdSeekView.wdSeekMainDocument; // set pop up header 

			wordApp.Selection.ParagraphFormat.LineSpacing = 15f; // Set file line spacing
			wordApp.Selection.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;


			foreach (ExportTable table in exportObject.Tables)
			{
				var title = table.Title;
				var headers= table.Headers;
				var data= table.Data;

				//wordApp.ActiveWindow.View.Type = Word.WdViewType.wdNormalView;
				//wordApp.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekPrimaryHeader;
				wordApp.ActiveWindow.ActivePane.Selection.InsertAfter(title);
				wordApp.Selection.ParagraphFormat.Alignment =
					Word.WdParagraphAlignment.wdAlignParagraphCenter; // Set right alignment 
				wordApp.ActiveWindow.View.SeekView =
					Word.WdSeekView.wdSeekMainDocument; // set pop up header 

				// Move focus and newline
				object count = 2;
				object WdLine = Word.WdUnits.wdLine; //newline;
				wordApp.Selection.MoveDown(ref WdLine, ref count, ref nothing); //move focus
				wordApp.Selection.TypeParagraph(); // insert paragraph

				// Create table in Word file
				
				Word.Table newTable = wordDoc.Tables.Add
					(wordApp.Selection.Range, data.GetLength(0), headers.Length, ref nothing, ref nothing);
				// Set table style
				newTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
				newTable.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
				/*newTable.Columns[1].Width = 100f;
				newTable.Columns[2].Width = 220f;
				newTable.Columns[3].Width = 105f;*/
				newTable.AllowAutoFit = true;
				var t = new Word.WdAutoFitBehavior();
				
				newTable.AutoFitBehavior(t);

				// Fill in table
				for (int j = 0; j < headers.Length; j++)
				{
					newTable.Cell(1, j).Range.Text = headers[j];
					newTable.Cell(1, j).Range.Bold = 2;
				}

				for (int i = 0; i < data.GetLength(0); i++)
				{
					for (int j = 0; j < headers.Length; j++)
					{
						newTable.Cell(i + 1, j).Range.Text = data[i, j] ?? "";	
					}
				}

				wordApp.Selection.Cells.VerticalAlignment =Word.WdCellVerticalAlignment.wdCellAlignVerticalTop; // Center Vertically 
				wordApp.Selection.ParagraphFormat.Alignment =Word.WdParagraphAlignment.wdAlignParagraphCenter; // Center Horizontal 

				
				//// merge cells longitudinal 
				//newTable.Cell(3, 3).Select(); // choose a row
				//object moveUnit = Word.WdUnits.wdLine;
				//object moveCount = 5;
				//object moveExtend = Word.WdMovementType.wdExtend;
				//wordApp.Selection.MoveDown(ref moveUnit, ref moveCount, ref moveExtend);
				//wordApp.Selection.Cells.Merge();
				
				wordDoc.Paragraphs.Last.Range.Text = "Created date：" +
				                                     DateTime.Now.ToString(); //“Alignment”
				wordDoc.Paragraphs.Last.Alignment =
					Word.WdParagraphAlignment.wdAlignParagraphRight;
			}
			// Save file
			object fileNameL = fileName;
			wordDoc.SaveAs(ref fileNameL, ref nothing, ref nothing,
						   ref nothing, ref nothing, ref nothing, ref nothing,
						   ref nothing, ref nothing, ref nothing, ref nothing,
						   ref nothing, ref nothing, ref nothing, ref nothing, ref nothing);

			Type doctype = wordDoc.GetType();
			doctype.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, wordDoc, new
				object[] { fileName+".rtf", Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatRTF });

			wordDoc.Close(ref nothing, ref nothing, ref nothing);
			wordApp.Quit(ref nothing, ref nothing, ref nothing);
		}
    }
}
