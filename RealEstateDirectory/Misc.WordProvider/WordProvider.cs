using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;

namespace Misc.WordProvider
{
	public class WordProvider
    {
		public static void Generate(string[] headers, string[,] data, string fileName)
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


			Object Nothing = System.Reflection.Missing.Value;
			Directory.CreateDirectory("C:/CNSI"); // Create file category 
			string name = "CNSI_" + "53asdf" + ".doc";
			object filename = "C://CNSI//" + name; // file save path
			// create Word file
			Word.Application WordApp = new Word.ApplicationClass();
			Word.Document WordDoc = WordApp.Documents.Add
				(ref Nothing, ref Nothing, ref Nothing, ref Nothing);
			
			
			

			// Add header
			WordApp.ActiveWindow.View.Type = Word.WdViewType.wdOutlineView;
			WordApp.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekPrimaryHeader;
			WordApp.ActiveWindow.ActivePane.Selection.InsertAfter("slon");
			WordApp.Selection.ParagraphFormat.Alignment =
				Word.WdParagraphAlignment.wdAlignParagraphRight; // Set right alignment 
			WordApp.ActiveWindow.View.SeekView =
				Word.WdSeekView.wdSeekMainDocument; // set pop up header 

			WordApp.Selection.ParagraphFormat.LineSpacing = 15f; // Set file line spacing
			WordApp.Selection.PageSetup.Orientation=Word.WdOrientation.wdOrientLandscape;

			// Move focus and newline
			object count = 14;
			object WdLine = Word.WdUnits.wdLine; //newline;
			WordApp.Selection.MoveDown(ref WdLine, ref count, ref Nothing); //move focus
			WordApp.Selection.TypeParagraph(); // insert paragraph

			// Create table in Word file
			Word.Table newTable = WordDoc.Tables.Add
				(WordApp.Selection.Range, 12, 3, ref Nothing, ref Nothing);
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
			WordApp.Selection.Cells.VerticalAlignment =
				Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter; // Center Vertically 
			WordApp.Selection.ParagraphFormat.Alignment =
				Word.WdParagraphAlignment.wdAlignParagraphCenter; // Center Horizontal 

			// Fill in table
			newTable.Cell(2, 1).Range.Text = " Product information table ";
			newTable.Cell(2, 1).Range.Font.Color =
				Word.WdColor.wdColorDarkBlue; // Set   text color in Cells
			//Merge Cells
			newTable.Cell(2, 1).Merge(newTable.Cell(2, 3));
			WordApp.Selection.Cells.VerticalAlignment =
				Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
			
			// Fill in table
			newTable.Cell(3, 1).Range.Text = "Brandname";
			newTable.Cell(3, 2).Range.Text = "BrandName";
			// merge cells longitudinal 
			newTable.Cell(3, 3).Select(); // choose a row
			object moveUnit = Word.WdUnits.wdLine;
			object moveCount = 5;
			object moveExtend = Word.WdMovementType.wdExtend;
			WordApp.Selection.MoveDown(ref moveUnit, ref moveCount, ref moveExtend);
			WordApp.Selection.Cells.Merge();
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
			WordDoc.Content.Tables[1].Rows.Add(ref Nothing);

			WordDoc.Paragraphs.Last.Range.Text = "Created date：" +
												 DateTime.Now.ToString(); //“Alignment”
			WordDoc.Paragraphs.Last.Alignment =
				Word.WdParagraphAlignment.wdAlignParagraphRight;

			// Save file
			WordDoc.SaveAs(ref filename, ref Nothing, ref Nothing,
						   ref Nothing, ref Nothing, ref Nothing, ref Nothing,
						   ref Nothing, ref Nothing, ref Nothing, ref Nothing,
						   ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);

			Type doctype = WordDoc.GetType();
			doctype.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, WordDoc, new
				object[] { @"d:\test\fd.rtf", Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatRTF });

			WordDoc.Close(ref Nothing, ref Nothing, ref Nothing);
			WordApp.Quit(ref Nothing, ref Nothing, ref Nothing);
		}
    }
}
