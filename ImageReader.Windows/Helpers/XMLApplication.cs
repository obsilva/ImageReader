namespace ImageReader.Windows.Helpers
{
	public class XMLApplication
	{
		#region Properties
		/// <summary> Recupera ou define o array de propriedades. </summary>
		[System.Xml.Serialization.XmlElement(ElementName = "Propriedade")]
		public XMLPropertyApplication[] Property { get; set; }
		#endregion


		#region Overrides
		/// <summary>
		/// Método sobrescrito. Verifica se dois objetos são iguais internamente.
		/// </summary>
		/// <param name="obj"> Objeto com o qual deve ser comparado. </param>
		/// <returns> Verdadeiro se os objetos forem iguais, falso caso contrário. </returns>
		public override bool Equals(object obj)
		{
			if ((obj == null) || (obj.GetType() != this.GetType()))
			{
				return false;
			}

			var aux = obj as XMLApplication;
			if (this.Property.Length != aux.Property.Length)
			{
				return false;
			}

			for (int index = 0; index < this.Property.Length; index++)
			{
				if (!this.Property[index].Equals(aux.Property[index]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Método sobrescrito. Gera o HashCode para verificação de igualdade entre objetos.
		/// </summary>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion
	}
}
