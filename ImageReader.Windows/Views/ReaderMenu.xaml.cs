using ImageReader.Domain;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
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
		String idioma = "";

		#region Constructors
		public ReaderMenu()
		{
			InitializeComponent();
			ObservableCollection<string> list = new ObservableCollection<string>();
			list.Add("Português");
			list.Add("Inglês");
			cmbIdioma.ItemsSource = list;

			cmbIdioma.SelectedIndex = 0;
		}
		#endregion

		#region Methods
		private void ouvirTexto()
		{
			Speech fala = new Speech();
			rtbTexto.SelectAll();
			String texto = rtbTexto.Selection.Text;
			fala.SoletraTexto(texto);
		}

		private void pausaTexto()
		{
			Speech fala = new Speech();
			fala.Pausa();
		}
		#endregion

		#region Events
		private void button_Click(object sender, RoutedEventArgs e)
		{
			String caminhoArquivo = "";
			OpenFileDialog file = new OpenFileDialog();
			file.Filter = "Image files |*.jpg;*.jpeg;*.png";

			if (file.ShowDialog() == true)
			{
				caminhoArquivo = file.FileName;
				String textoImagem = "";
				OCR conversor = new OCR();
				conversor.setImagem(caminhoArquivo);
				conversor.setIdioma(this.idioma);

				textoImagem = conversor.extraiTextoImagem();
				rtbTexto.Document.Blocks.Clear();
				rtbTexto.Document.Blocks.Add(new Paragraph(new Run(textoImagem)));
			}
		}

		private void btnExportToDocx_Click(object sender, RoutedEventArgs e)
		{
			Docx writer = new Docx();
			rtbTexto.SelectAll();
			if (writer.exportToDocx(rtbTexto.Selection.Text))
				MessageBox.Show("Arquivo salvo com sucesso!");
			else
				MessageBox.Show("Erro ao salvar arquivo!");
		}

		private void btnExportToPdf_Click(object sender, RoutedEventArgs e)
		{
			PDF writer = new PDF();
			rtbTexto.SelectAll();
			if (writer.exportToPdf(rtbTexto.Selection.Text))
				MessageBox.Show("Arquivo salvo com sucesso!");
			else
				MessageBox.Show("Erro ao salvar arquivo!");
		}

		private void cmbIdioma_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int index = cmbIdioma.SelectedIndex;
			if (index == 0)
				idioma = "por";
			else if (index == 1)
				idioma = "eng";
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			String caminhoArquivo = "";
			OpenFileDialog file = new OpenFileDialog();
			file.Filter = "PDF files | *.pdf";

			if (file.ShowDialog() == true)
			{
				caminhoArquivo = file.FileName;
				String textoImagem = "";
				PDF pdf = new PDF();

				textoImagem = pdf.extraiImagens(caminhoArquivo, idioma);
				rtbTexto.Document.Blocks.Clear();
				rtbTexto.Document.Blocks.Add(new Paragraph(new Run(textoImagem)));
			}
		}

		private void checkBox_Checked(object sender, RoutedEventArgs e)
		{
			if (chkOuvirTexto.IsChecked == true)
				ouvirTexto();
			else
				pausaTexto();
		}
		#endregion
	}
}
