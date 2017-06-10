using Novacode;
using System;
using System.IO;

namespace ImageReader.Domain
{
	public class Docx
	{
		#region Methods
		/// <summary>Cria um novo arquivo no formato .docx a partir de um texto.</summary>
		/// <param name="text">Texto que irá popular o documento.</param>
		/// <returns>True caso o documento seja criado e salvo com sucesso, false caso contrário.</returns>
		public bool Create(string text)
		{
			string filePath = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory) + "imageReader.docx";

			try
			{
				using (var docx = DocX.Create(filePath))
				{
					Paragraph paragraph = docx.InsertParagraph(text);
					paragraph.Alignment = Alignment.left;

					docx.Save();
					return true;
				}
			}
			catch
			{ return false; }
		}
		#endregion
	}
}
