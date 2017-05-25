using ImageReader.Windows.Helpers;
using MahApps.Metro;
using MahApps.Metro.Controls;
using System;
using System.IO;
using System.Windows;

namespace ImageReader.Windows.Windows
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : MetroWindow
	{
		#region Constructors
		/// <summary> Inicializa uma nova instância da classe <see cref="MainWindow"/>. </summary>
		public MainWindow()
		{
			Current = this;

			InitializeComponent();

			InterfaceConfiguration();

			verificaDiretorios();

		}
		#endregion

		#region Properties
		/// <summary> Recupera a instância corrente da janela. </summary>
		public static MainWindow Current { get; private set; }
		#endregion


		#region Methods
		private void verificaDiretorios()
		{
			String path = Path.GetFullPath(@"..\files");
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
		}
		private void InterfaceConfiguration()
		{
			XMLConfiguration settings = Configuration.Settings;

			Title = settings.Application.Property[0].Value;
			ThemeManager.ChangeAppStyle(Application.Current,
					ThemeManager.GetAccent(settings.Application.Property[3].Value),
					ThemeManager.GetAppTheme("Base" + settings.Application.Property[2].Value));
		}
		#endregion


		#region Events
		private void MenuItem_ItemClick(object sender, ItemClickEventArgs e)
		{
			mnuMain.Content = e.ClickedItem;
			mnuMain.IsPaneOpen = false;
		}
		#endregion
	}
}
