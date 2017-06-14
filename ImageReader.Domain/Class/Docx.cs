using Novacode;
using System.Windows.Forms;

namespace ImageReader.Domain
{
	public class Docx
	{
		#region Methods
		/// <summary>Cria um novo arquivo no formato .docx a partir de um texto.</summary>
		/// <param name="text">Texto que irá popular o documento.</param>
		/// <returns>True caso o documento seja criado e salvo com sucesso, false caso contrário.</returns>
		public void Create(string text)
		{
			var saveFileDialog = new SaveFileDialog()
			{
				FileName = "imageReader",
				Filter = "Word Documents | *.docx"
			};

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				string documentFilePath = saveFileDialog.FileName;
				documentFilePath = documentFilePath.Replace(".docx", "");
				documentFilePath = documentFilePath + ".docx";

				using (var docx = DocX.Create(documentFilePath))
				{
					Paragraph paragraph = docx.InsertParagraph(text);
					paragraph.Alignment = Alignment.left;

					docx.Save();
				}
			}
		}
		#endregion
	}
}
