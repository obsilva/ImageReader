namespace ImageReader.Windows.Helpers
{
	public class XMLPropertyApplication
	{
		#region Properties
		/// <summary> Recupera ou define o nome da propriedade. </summary>
		[System.Xml.Serialization.XmlAttribute(AttributeName = "Nome")]
		public string Name { get; set; }

		/// <summary> Recupera ou deinfe o valor da propriedade. </summary>
		[System.Xml.Serialization.XmlText()]
		public string Value { get; set; }
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

			var aux = obj as XMLPropertyApplication;
			return (object.ReferenceEquals(this.Name, aux.Name) ||
				   (this.Name != null) && this.Name.Equals(aux.Name)) &&
				   (object.ReferenceEquals(this.Value, aux.Value) ||
				   (this.Value != null) && this.Value.Equals(aux.Value));
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
