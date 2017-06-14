using System.Drawing;

namespace ImageReader.Domain
{
	public class ImageCapture
	{
		#region Methods		
		public void Enable() => new TransparentForm().Show();

		public string Capture(Point point)
		{
			var bitmapImage = new Bitmap(80, 30);

			Graphics.FromImage(bitmapImage).CopyFromScreen(point, new Point(0, 0), new Size(1366, 768));

			return Save(bitmapImage);
		}

		public string Save(Bitmap image)
		{
			string path = System.IO.Path.GetTempPath() + "imageReader.jpg";
			image.Save(path);

			return path;
		}
		#endregion
	}
}
