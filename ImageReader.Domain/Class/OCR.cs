using System;
using System.IO;
using Tesseract;

namespace ImageReader.Domain
{
	public class OCR
	{
		#region Properties
		public string Language { get; set; }

		public string ImagePath { get; set; }
		#endregion

		public string FromImage()
		{
			try
			{
				using (var engine = new TesseractEngine(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory)
					+ "tessdata", Language, EngineMode.Default))
				{
					using (Pix img = Pix.LoadFromFile(ImagePath))
					{
						using (Page page = engine.Process(img))
						{ return page.GetText(); }
					}
				}
			}
			catch (Exception e)
			{
				if (e.InnerException != null)
				{ return e.InnerException.Message; }
				return String.Empty;
			}
		}
	}
}
