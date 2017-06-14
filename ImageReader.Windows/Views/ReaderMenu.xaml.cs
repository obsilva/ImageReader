using ImageReader.Domain;
using Microsoft.Win32;
using System;
using System.Threading;
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
		private new string Language { get; set; }

		private Speech Speak { get; set; }

		private string TextBoxContent { get { rtxtTexto.SelectAll(); return rtxtTexto.Selection.Text; } }
		#endregion


		#region Constructors
		public ReaderMenu()
		{
			InitializeComponent();
			Speak = new Speech();
		}
		#endregion


		#region Methods
		private void Play() => Speak.FromText(rtxtTexto.Selection.Text);

		private void Pause() => Speak.Pause();

		private void ExportaAudio() => Speak.ExportAudio(TextBoxContent);
		#endregion


		#region Events
		private void btnExportToAudio_Click(object sender, RoutedEventArgs e) => ExportaAudio();

		private void btnExportToDocx_Click(object sender, RoutedEventArgs e) => new Docx().Create(TextBoxContent);

		private void btnExportToPDF_Click(object sender, RoutedEventArgs e) => new PDF().Create(TextBoxContent);

		private void btnOpenImage_Click(object sender, RoutedEventArgs e)
		{
			var file = new OpenFileDialog() { Filter = "Image files |*.jpg;*.jpeg;*.png" };

			if (file.ShowDialog() == true)
			{
				var ocr = new OCR() { ImagePath = file.FileName, Language = Language };

				rtxtTexto.Document.Blocks.Clear();
				rtxtTexto.Document.Blocks.Add(new Paragraph(new Run(ocr.FromImage())));
			}

			Speak = new Speech();
		}

		private void btnOpenPDF_Click(object sender, RoutedEventArgs e)
		{
			var file = new OpenFileDialog() { Filter = "PDF files | *.pdf" };

			if (file.ShowDialog() == true)
			{
				var textoImagem = String.Empty;
				var pdf = new PDF();

				rtxtTexto.Document.Blocks.Clear();
				rtxtTexto.Document.Blocks.Add(new Paragraph(new Run(pdf.ExtractImage(file.FileName, Language))));
			}

			Speak = new Speech();
		}

		private void btnPauseSpeech_Click(object sender, RoutedEventArgs e) => Pause();

		private void btnPlaySpeech_Click(object sender, RoutedEventArgs e)
		{
			rtxtTexto.SelectAll();
			var t = new Thread(Play);
			t.Start();
		}

		private void btnSpeechWithMouse_Click(object sender, RoutedEventArgs e) => new ImageCapture().Enable();

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
		#endregion
	}
}
