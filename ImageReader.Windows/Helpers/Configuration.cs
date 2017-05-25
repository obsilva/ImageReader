using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ImageReader.Windows.Helpers
{
	public class Configuration
	{
		#region Properties
		/// <summary> Recupera o caminho para a pasta da aplicação, onde ficam as configurações. </summary>
		public static string ApplicationFolder
		{ get { return Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory); } }

		/// <summary> Recupera ou define a imagem de fundo da tela inicial. </summary>
		public static string BackgroundImage
		{
			get
			{
				string[] image = Directory.GetFiles(ApplicationFolder, "backgroundImage.*");

				if (image.Length < 1)
				{
					return String.Empty;
				}
				return Path.GetFullPath(image[0]);
			}
			set
			{
				/* Remove as imagens antigas. */
				string[] images = Directory.GetFiles(ApplicationFolder, "backgroundImage.*");
				foreach (var image in images)
				{
					File.Delete(Path.GetFullPath(image));
				}

				if (!String.IsNullOrWhiteSpace(value) && File.Exists(value))
				{
					string imagemFullPath = Path.GetFullPath(value);
					string imageExtension = Path.GetExtension(imagemFullPath);
					string destiny = Path.Combine(ApplicationFolder, ("backgroundImage" + imageExtension));

					/* Faz a cópia da nova imagem. */
					File.Copy(imagemFullPath, destiny, true);
				}
			}
		}

		/// <summary> Recupera a configuração padrão do XML de configuração. </summary>
		public static XMLConfiguration DefaultSettings
		{
			get
			{
				XMLConfiguration settings = new XMLConfiguration();
				XMLApplication application = new XMLApplication();

				settings.Application = application;
				application.Property = new XMLPropertyApplication[] {
				new XMLPropertyApplication() {
					Name = "TituloJanelaPrincipal",
					Value = "Gerenciador Inteligente"
				},
				new XMLPropertyApplication() {
					Name = "ExibirTarefasJanelaPrincipal",
					Value = "true"
				},
				new XMLPropertyApplication() {
					Name = "Tema",
					Value = "Dark"
				},
				new XMLPropertyApplication() {
					Name = "Cor",
					Value = "Blue"
				}
			};

				return settings;
			}
		}

		/// <summary> Recupera ou define as informações contidas no arquivo XML de configuração. </summary>
		public static XMLConfiguration Settings
		{
			get
			{
				using (XmlTextReader XMLReader = new XmlTextReader(SettingsFile))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(XMLConfiguration));

					try
					{
						XMLReader.Read();
						return serializer.Deserialize(XMLReader) as XMLConfiguration;
					}
					catch (FileNotFoundException)
					{
						Settings = null;
						return DefaultSettings;
					}
				}
			}
			set
			{
				File.Delete(SettingsFile);

				if (value == null)
				{ value = DefaultSettings; }

				using (FileStream file = new FileStream(SettingsFile, FileMode.Create))
				{
					XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
					XmlSerializer serializer = new XmlSerializer(typeof(XMLConfiguration));

					namespaces.Add(String.Empty, String.Empty);
					serializer.Serialize(file, value, namespaces);
				}
			}
		}

		/// <summary> Recupera o caminho para o arquivo de configurações. </summary>
		public static string SettingsFile { get { return ApplicationFolder + "settings.xml"; } }
		#endregion
	}
}
