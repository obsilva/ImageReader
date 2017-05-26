using System;
using System.Drawing;

namespace ImageReader.Domain
{
    public class CaptureImage
    {
        //private System.Windows.Forms.OpenFileDialog svdTela;
        private Graphics g;

        public void StartMouse()
        {
            TransparentForm frm = new TransparentForm();
            frm.Show();
        }

        public string CaptImage(Point p1)
        {
            //armazena a imagem no bitmap
            Bitmap b = new Bitmap(60, 30);
            //copia  a tela no bitmap            
            g = Graphics.FromImage(b);
            g.CopyFromScreen(p1, new Point(0, 0), new Size(1366, 768));
            //salva imagem
            string strCaminho = SaveImage(b);
            return strCaminho;
        }

        public string SaveImage(Bitmap img)
        {
            string strCaminho = System.IO.Path.GetTempPath() + "/myScreenShot.jpg";
            img.Save(strCaminho, System.Drawing.Imaging.ImageFormat.Jpeg);
            return strCaminho;
        }
    }
}
