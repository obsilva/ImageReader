using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.IO;

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
				ImageRenderListener listener = null;
				parser.ProcessContent(pageNumber, (listener = new ImageRenderListener()));
				return listener.Images.Count > 0;
			}
		}

		/// <summary>Extrai todas as imagens de um arquivo PDF.</summary>
		public String extraiImagens(string caminhoArquivo, string idioma)
		{
			OCR tesseract = new OCR();
			String caminhoImagem = "", extensaoImagem = "", textoImagem = "";
			tesseract.setIdioma(idioma);

			using (var reader = new PdfReader(caminhoArquivo))
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
							caminhoImagem = System.IO.Path.GetFullPath(@"..\files\temp.png");
							extensaoImagem = System.IO.Path.GetExtension(caminhoImagem);
							tesseract.setImagem(caminhoImagem);
							textoImagem += tesseract.extraiTextoImagem();
							index++;
						}
					}
				}
				File.Delete(caminhoImagem);
				return textoImagem;
			}
		}

		/// <summary>Exporta o texto para um arquivo PDF. </summary>
		public bool exportToPdf(string texto)
		{
			try
			{
				Document doc = new Document(PageSize.A4); //criando e estipulando o tipo da folha usada
				doc.SetMargins(40, 40, 40, 80); //estibulando o espaçamento das margens 
				doc.AddCreationDate(); //adicionando as configuracoes

				//caminho onde sera criado o pdf + nome desejado       
				String caminho = System.IO.Path.GetFullPath(@"..\files");
				caminho += "\\documentoPdf.pdf";

				//criando o arquivo pdf em branco, passando como parametro a variavel doc criada acima e a variavel caminho 
				//tambem criada acima.
				PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));

				doc.Open();
				Paragraph paragrafo = new Paragraph(texto, new Font(Font.NORMAL, 14));
				paragrafo.Alignment = Element.ALIGN_JUSTIFIED;
				doc.Add(paragrafo);
				doc.Close();

				return true;
			}
			catch
			{
				return false;
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
