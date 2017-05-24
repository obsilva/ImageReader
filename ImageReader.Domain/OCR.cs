using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using Tesseract;

namespace ImageReader.Domain
{
	public class OCR
	{
		private String caminhoImagem;
		private String idioma;

		public void setArquivo(String caminho)
		{
			this.caminhoImagem = caminho;
		}
		public void setIdioma(String linguagem)
		{
			this.idioma = linguagem;
		}

		public bool extraiTextoImagem(System.Windows.Forms.RichTextBox txtImagem, System.Windows.Forms.Label lbPrecisao)
		{
			Speech fala = new Speech();
			try
			{
				using (var engine = new TesseractEngine(@"tessdata", idioma, EngineMode.Default))
				{
					using (var img = Pix.LoadFromFile(caminhoImagem))
					{
						using (var page = engine.Process(img))
						{
							var textoImagem = page.GetText();
							txtImagem.Text = textoImagem;
							lbPrecisao.Text = Convert.ToString(page.GetMeanConfidence());
							fala.SoletraTexto(textoImagem);
							return true;
						}
					}
				}
			}
			catch (Exception)
			{
				return false;
			}
		}

		/// <summary>Checks whether a specified page of a PDF file contains images.</summary>
		/// <returns>True if the page contains at least one image; false otherwise.</returns>
		public static bool PageContainsImages(string filename, int pageNumber)
		{
			using (var reader = new PdfReader(filename))
			{
				var parser = new PdfReaderContentParser(reader);
				ImageRenderListener listener = null;
				parser.ProcessContent(pageNumber, (listener = new ImageRenderListener()));
				return listener.Images.Count > 0;
			}
		}

		/// <summary>Extracts all images (of types that iTextSharp knows how to decode) from a PDF file.</summary>
		public void extraiImagens(System.Windows.Forms.RichTextBox txtImagem, System.Windows.Forms.Label lbPrecisao, string filename)
		{
			String caminho = "", extensaoImagem = ""; ;
			using (var reader = new PdfReader(filename))
			{
				var parser = new PdfReaderContentParser(reader);
				ImageRenderListener listener = null;

				for (var i = 1; i <= 2/*reader.NumberOfPages*/; i++)
				{
					parser.ProcessContent(i, (listener = new ImageRenderListener()));
					var index = 1;
					if (listener.Images.Count > 0)
					{
						foreach (var pair in listener.Images)
						{
							caminho = System.IO.Path.GetFullPath(@"..\files\temp.png");
							extensaoImagem = System.IO.Path.GetExtension(caminho);
							this.caminhoImagem = caminho;
							extraiTextoImagem(txtImagem, lbPrecisao);
							index++;
						}
					}
				}
			}
		}


		/*public string LeTextoPdf(string filename)
			{
				PdfReader reader = new PdfReader(filename);
				string pdfText = string.Empty;
				ITextExtractionStrategy extraction = new SimpleTextExtractionStrategy();

				for (int i = 1; i <= reader.NumberOfPages; i++)
				{
					string extractText = PdfTextExtractor.GetTextFromPage(reader, i, extraction);

					extractText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default,
						Encoding.UTF8, Encoding.Default.GetBytes(extractText)));
					pdfText = pdfText + extractText;       
				}
				reader.Close();
				return pdfText;
			}*/
	}
}
