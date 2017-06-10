using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Collections.Generic;

namespace ImageReader.Domain
{
	public class ImageRenderListener : IRenderListener
	{
		#region Fields
		Dictionary<System.Drawing.Image, string> images = new Dictionary<System.Drawing.Image, string>();
		#endregion Fields


		#region Properties
		public Dictionary<System.Drawing.Image, string> Images => images;
		#endregion Properties


		#region Methods
		public void BeginTextBlock() { }

		public void EndTextBlock() { }

		public void RenderImage(ImageRenderInfo renderInfo)
		{
			PdfImageObject image = renderInfo.GetImage();
			var filter = (PdfName) image.Get(PdfName.FILTER);

			if (filter != null)
			{
				System.Drawing.Image drawingImage = image.GetDrawingImage();
				string extension = ".";

				if (filter == PdfName.DCTDECODE)
				{ extension += PdfImageObject.ImageBytesType.JPG.FileExtension; }
				else if (filter == PdfName.JPXDECODE)
				{ extension += PdfImageObject.ImageBytesType.JP2.FileExtension; }
				else if (filter == PdfName.FLATEDECODE)
				{ extension += PdfImageObject.ImageBytesType.PNG.FileExtension; }
				else if (filter == PdfName.LZWDECODE)
				{ extension += PdfImageObject.ImageBytesType.CCITT.FileExtension; }


				this.Images.Add(drawingImage, extension);
				string path = System.IO.Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory);
				drawingImage.Save(path + "temp" + extension, drawingImage.RawFormat);

			}
		}

		public void RenderText(TextRenderInfo renderInfo) { }
		#endregion Methods
	}
}
