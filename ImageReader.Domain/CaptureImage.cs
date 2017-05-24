using System;
using System.Drawing;

namespace ImageReader.Domain
{
	public class CaptureImage
	{
		//private System.Windows.Forms.OpenFileDialog svdTela;
		private Graphics g;

		public void CaptImage(Point p1, Point p2)
		{
			Console.WriteLine("Entrou no captura imagem");
			//armazena a imagem no bitmap
			Bitmap b = new Bitmap(200, 200);
			//copia  a tela no bitmap            
			g = Graphics.FromImage(b);
			g.CopyFromScreen(p1, p2, new Size(100, 100));
			//salva imagem
			SaveImage(b);
		}

		public void SaveImage(Bitmap img)
		{
			//abre a janela de dialogo SaveDialog para salvar o arquivo gerado na captura
			/*svdTela = new OpenFileDialog();
			svdTela.InitialDirectory = "C:\\";
			if (svdTela.ShowDialog() == DialogResult.OK)
			{
				//obtem a extensão do arquivo salvo
				string ext = System.IO.Path.GetExtension(svdTela.FileName);
				if (ext == ".jpg")
					img.Save(svdTela.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
				else if (ext == ".gif")
					img.Save(svdTela.FileName, System.Drawing.Imaging.ImageFormat.Gif);
				else if (ext == ".png")
					img.Save(svdTela.FileName, System.Drawing.Imaging.ImageFormat.Png);
			}
			*/
			Console.WriteLine("Entrou no salva imagem");
			string strCaminho = System.IO.Path.GetTempPath() + "/myScreenShot.jpg";
			img.Save(strCaminho, System.Drawing.Imaging.ImageFormat.Jpeg);
		}
	}
}
