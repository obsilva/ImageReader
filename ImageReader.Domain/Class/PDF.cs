using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.IO;
using System.Windows.Forms;

namespace ImageReader.Domain
{
	public class PDF
	{
		/// <summary>Checks whether a specified page of a PDF file contains images.</summary>
		/// <returns>True if the page contains at least one image; false otherwise.</returns>
		public static bool PageContainsImages(string filename, int pageNumber)
		{
			using (var reader = new PdfReader(filename))
			{
				var parser = new PdfReaderContentParser(reader);
				var listener = new ImageRenderListener();

				parser.ProcessContent(pageNumber, listener);

				return listener.Images.Count > 0;
			}
		}

		/// <summary>Extrai todas as imagens de um arquivo PDF.</summary>
		public string ExtractImage(string filePath, string language)
		{
			var tesseract = new OCR() { Language = language };
			var imagePath = String.Empty;
			var imageText = String.Empty;

			using (var reader = new PdfReader(filePath))
			{
				var parser = new PdfReaderContentParser(reader);
				var listener = new ImageRenderListener();

				for (var index = 1; index <= reader.NumberOfPages; index++)
				{
					parser.ProcessContent(index, listener);

					if (listener.Images.Count > 0)
					{
						for (int count = 0; count < listener.Images.Count; count++)
						{
							imagePath = System.IO.Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory) + "temp.png";
							tesseract.ImagePath = imagePath;
							imageText += tesseract.FromImage();
						}
					}
				}

				if (!String.IsNullOrWhiteSpace(imagePath))
				{ File.Delete(imagePath); }

				return imageText;
			}
		}

		/// <summary>Exporta o texto para um arquivo PDF. </summary>
		public void Create(string text)
		{
			var saveFileDialog = new SaveFileDialog()
			{
				FileName = "imageReader",
				Filter = "PDF Documents | *.pdf"
			};

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				string documentFilePath = saveFileDialog.FileName;
				documentFilePath = documentFilePath.Replace(".pdf", "");
				documentFilePath = documentFilePath + ".pdf";


				var document = new Document(PageSize.A4); //criando e estipulando o tipo da folha usada
				document.SetMargins(40, 40, 40, 80); //estibulando o espaçamento das margens 
				document.AddCreationDate(); //adicionando as configuracoes

				//criando o arquivo pdf em branco
				var writer = PdfWriter.GetInstance(document, new FileStream(documentFilePath, FileMode.Create));

				document.Open();
				var paragrafo = new Paragraph(text, new Font(Font.NORMAL, 14)) { Alignment = Element.ALIGN_JUSTIFIED };
				document.Add(paragrafo);
				document.Close();
			}
		}
	}
}
