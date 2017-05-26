using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageReader.Domain
{
    public partial class TransparentForm : Form
    {

        public TransparentForm()
        {
            InitializeComponent();
        }

        private void TransparentForm_MouseMove(object sender, MouseEventArgs e)
        {

            CaptureImage capImg = new CaptureImage();
            OCR conversor = new OCR();
            Speech fala = new Speech();
            String caminhoArquivo = "";
            String textoImagem = "";

            caminhoArquivo = capImg.CaptImage(new Point(MousePosition.X - 20, MousePosition.Y - 10));
            conversor.setImagem(caminhoArquivo);
            conversor.setIdioma("por");

            textoImagem = conversor.extraiTextoImagem();
            Console.WriteLine(textoImagem);
            fala.SoletraTexto(textoImagem);
            
        }
    }
}
