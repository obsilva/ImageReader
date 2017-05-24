using System;


namespace ImageReader.Windows.Helpers
{
    public class XMLConfiguration
    {
        #region Properties      
        /// <summary> Recupera ou define as configuraçãos da aplicação. </summary>
        [System.Xml.Serialization.XmlElement(ElementName = "Aplicacao")]
        public XMLApplication Application { get; set; }
        #endregion

        #region Overries
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

            var aux = obj as XMLConfiguration;
            return (object.ReferenceEquals(this.Application, aux.Application) ||
                   (this.Application != null) && this.Application.Equals(aux.Application));
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
