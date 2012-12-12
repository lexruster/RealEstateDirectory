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

			wordApp.Selection.ParagraphFormat.LineSpacing = 15f; // Set file line spacing
			wordApp.Selection.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;

			//wordApp.ActiveWindow.View.Type = Word.WdViewType.wdNormalView;
			//wordApp.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekPrimaryHeader;
			wordApp.ActiveWindow.ActivePane.Selection.InsertAfter("E-8");
			wordApp.Selection.ParagraphFormat.Alignment =
				Word.WdParagraphAlignment.wdAlignParagraphRight; // Set right alignment 
			wordApp.ActiveWindow.View.SeekView =
				Word.WdSeekView.wdSeekMainDocument; // set pop up header 

			wordApp.Selection.ParagraphFormat.LineSpacing = 15f; // Set file line spacing
			wordApp.Selection.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;

			// Move focus and newline
			object count = 14;
			object WdLine = Word.WdUnits.wdLine; //newline;
			wordApp.Selection.MoveDown(ref WdLine, ref count, ref nothing); //move focus
			wordApp.Selection.TypeParagraph(); // insert paragraph

			// Create table in Word file
			Word.Table newTable = wordDoc.Tables.Add
				(wordApp.Selection.Range, 12, 3, ref nothing, ref nothing);
			// Set table style
			newTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleThickThinLargeGap;
			newTable.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
			newTable.Columns[1].Width = 100f;
			newTable.Columns[2].Width = 220f;
			newTable.Columns[3].Width = 105f;

			// Fill in table
			newTable.Cell(1, 1).Range.Text = "Product information table";
			newTable.Cell(1, 1).Range.Bold = 2; // Set text font as bold in Cells
			// Merge cells
			newTable.Cell(1, 1).Merge(newTable.Cell(1, 3));
			wordApp.Selection.Cells.VerticalAlignment =
				Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter; // Center Vertically 
			wordApp.Selection.ParagraphFormat.Alignment =
				Word.WdParagraphAlignment.wdAlignParagraphCenter; // Center Horizontal 

			// Fill in table
			newTable.Cell(2, 1).Range.Text = " Product information table ";
			newTable.Cell(2, 1).Range.Font.Color =
				Word.WdColor.wdColorDarkBlue; // Set   text color in Cells
			//Merge Cells
			newTable.Cell(2, 1).Merge(newTable.Cell(2, 3));
			wordApp.Selection.Cells.VerticalAlignment =
				Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

			// Fill in table
			newTable.Cell(3, 1).Range.Text = "Brandname";
			newTable.Cell(3, 2).Range.Text = "BrandName";
			// merge cells longitudinal 
			newTable.Cell(3, 3).Select(); // choose a row
			object moveUnit = Word.WdUnits.wdLine;
			object moveCount = 5;
			object moveExtend = Word.WdMovementType.wdExtend;
			wordApp.Selection.MoveDown(ref moveUnit, ref moveCount, ref moveExtend);
			wordApp.Selection.Cells.Merge();
			// insert image
			//string FileName = @"d:\Media\Musik\Infected Mushroom\Infected Mushroom-BP Empire 2001\BP Empire.jpg"; // image path
			//object LinkToFile = false;
			//object SaveWithDocument = true;
			//object Anchor = WordDoc.Application.Selection.Range;
			//WordDoc.Application.ActiveDocument.InlineShapes.AddPicture
			//				(FileName, ref LinkToFile, ref SaveWithDocument, ref Anchor);
			//WordDoc.Application.ActiveDocument.InlineShapes[1].Width = 100f; //image width
			//WordDoc.Application.ActiveDocument.InlineShapes[1].Height = 100f; //image height
			// Set image layout as Square 
			//Word.Shape s =
			//				WordDoc.Application.ActiveDocument.InlineShapes[1].ConvertToShape();
			//		s.WrapFormat.Type = Word.WdWrapType.wdWrapSquare;

			newTable.Cell(12, 1).Range.Text = "product special attribute";
			newTable.Cell(12, 1).Merge(newTable.Cell(12, 3));
			// Add rows in table
			wordDoc.Content.Tables[1].Rows.Add(ref nothing);

			wordDoc.Paragraphs.Last.Range.Text = "Created date：" +
												 DateTime.Now.ToString(); //“Alignment”
			wordDoc.Paragraphs.Last.Alignment =
				Word.WdParagraphAlignment.wdAlignParagraphRight;

			// Save file
			object fileNameL = fileName;
			wordDoc.SaveAs(ref fileNameL, ref nothing, ref nothing,
						   ref nothing, ref nothing, ref nothing, ref nothing,
						   ref nothing, ref nothing, ref nothing, ref nothing,
						   ref nothing, ref nothing, ref nothing, ref nothing, ref nothing);

			Type doctype = wordDoc.GetType();
			doctype.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, wordDoc, new
				object[] { fileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatRTF });

			wordDoc.Close(ref nothing, ref nothing, ref nothing);
			wordApp.Quit(ref nothing, ref nothing, ref nothing);
		}
    }
}
