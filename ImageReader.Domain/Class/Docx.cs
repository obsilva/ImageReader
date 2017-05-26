using System;
using System.Drawing;

namespace ImageReader.Domain
{
	public class Docx
	{
		/// <summary> Salva o texto em um arquivo .docx utilizando a biblioteca Docx </summary>	
		public bool exportToDocx(string texto)
		{
			String caminho = System.IO.Path.GetFullPath(@"..\files");
			caminho += "\\documentoDocX.docx";
			try
			{
				using (var docX = Novacode.DocX.Create(caminho))
				{
					var paragrafo1 = docX.InsertParagraph();
					paragrafo1.LineSpacingAfter = 8;
					paragrafo1.Append(texto);
					paragrafo1.Alignment = Novacode.Alignment.both;

					var paragrafo2 = docX.InsertParagraph();
					paragrafo2.LineSpacingAfter = 8;
					//paragrafo2.Append("Fonte Arial Negrito, Itálico, Sublinhado, Tamanho 18");
					paragrafo2.Font(new FontFamily("Arial"));
					paragrafo2.FontSize(18);
					paragrafo2.Bold();
					paragrafo2.Italic();
					paragrafo2.UnderlineStyle(Novacode.UnderlineStyle.singleLine);

					docX.Save();
					return true;
				}
			}
			catch
			{
				return false;
			}
		}
	}
}
