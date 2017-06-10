using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageReader.Domain
{
	public partial class TransparentForm : Form
	{
		public TransparentForm()
		{ InitializeComponent(); }

		private void TransparentForm_MouseMove(object sender, MouseEventArgs e)
		{
			var imageCapture = new ImageCapture();
			var ocr = new OCR() { Language = "por" };
			var speech = new Speech();
			var filePath = String.Empty;
			var imageText = String.Empty;

			filePath = imageCapture.Capture(new Point(MousePosition.X - 20, MousePosition.Y - 10));
			ocr.ImagePath = filePath;

			imageText = ocr.FromImage();
			Console.WriteLine(imageText);
			speech.FromText(imageText);
		}
	}
}
