using ImageReader.Domain;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ImageReader.Windows.Views
{
	/// <summary>
	/// Interaction logic for ReaderMenu.xaml
	/// </summary>
	public partial class ReaderMenu : UserControl
	{
		#region Properties
		private string Language { get; set; }
		#endregion

		#region Constructors
		public ReaderMenu()
		{ InitializeComponent(); }
        #endregion

        Speech fala = new Speech();


        #region Methods
        private void Play()
		{
			rtbTexto.SelectAll();
			fala.FromText(rtbTexto.Selection.Text);
		}

		private void Pause()
		{			
			fala.Pause();
		}
		#endregion


		#region Events
		private void button_Click(object sender, RoutedEventArgs e)
		{
			var file = new OpenFileDialog() { Filter = "Image files |*.jpg;*.jpeg;*.png" };

			if (file.ShowDialog() == true)
			{
				var ocr = new OCR() { ImagePath = file.FileName, Language = Language };

				rtbTexto.Document.Blocks.Clear();
				rtbTexto.Document.Blocks.Add(new Paragraph(new Run(ocr.FromImage())));
			}
		}

		private void btnExportToDocx_Click(object sender, RoutedEventArgs e)
		{
			var docx = new Docx();

			rtbTexto.SelectAll();
			if (docx.Create(rtbTexto.Selection.Text))
			{ MessageBox.Show("Arquivo salvo com sucesso!"); }
			else
			{ MessageBox.Show("Erro ao salvar arquivo!"); }
		}

		private void btnExportToPdf_Click(object sender, RoutedEventArgs e)
		{
			var pdf = new PDF();

			rtbTexto.SelectAll();
			if (pdf.Create(rtbTexto.Selection.Text))
			{ MessageBox.Show("Arquivo salvo com sucesso!"); }
			else
			{ MessageBox.Show("Erro ao salvar arquivo!"); }
		}

		private void btnMouse_Click(object sender, RoutedEventArgs e)
		{ new ImageCapture().Enable(); }

		private void cmbIdioma_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			switch (cmbIdioma.SelectedIndex)
			{
				case 0:
					Language = "por";
					break;
				case 1:
					Language = "eng";
					break;
				default:
					Language = "por";
					break;
			}
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			var file = new OpenFileDialog() { Filter = "PDF files | *.pdf" };

			if (file.ShowDialog() == true)
			{
				var textoImagem = String.Empty;
				var pdf = new PDF();

				rtbTexto.Document.Blocks.Clear();
				rtbTexto.Document.Blocks.Add(new Paragraph(new Run(pdf.ExtractImage(file.FileName, Language))));
			}
		}

        private void btn_pause_Click(object sender, RoutedEventArgs e)
        {
            Pause();
        }

        private void btn_play_Click(object sender, RoutedEventArgs e)
        {
            Play();
        }
        #endregion


    }
}
