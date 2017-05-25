using ImageReader.Windows.Helpers;
using MahApps.Metro;
using System.Windows;
using System.Windows.Controls;

namespace ImageReader.Windows.Views
{
	/// <summary>
	/// Interaction logic for OptionsMenu.xaml
	/// </summary>
	public partial class OptionsMenu : UserControl
	{
		#region Globals
		private XMLConfiguration _settings;
		#endregion

		#region Constructors
		/// <summary> Inicializa uma nova instância da classe <see cref="OptionsMenu"/>. </summary>
		public OptionsMenu()
		{
			InitializeComponent();
			_settings = Configuration.Settings;

			cboColors.SelectedItem = ThemeManager.GetAccent(_settings.Application.Property[3].Value);
			txtApplicationTitle.Text = _settings.Application.Property[0].Value;
		}
		#endregion

		#region Methods
		private void SaveConfigurations()
		{ Configuration.Settings = _settings; }
		#endregion

		#region Events
		private void ThemeButton_Click(object sender, RoutedEventArgs e)
		{
			string newTheme = ((Button)sender).Name.Substring(3);
			var currentTheme = ThemeManager.DetectAppStyle(Application.Current);
			ThemeManager.ChangeAppStyle(Application.Current, currentTheme.Item2, ThemeManager.GetAppTheme("Base" + newTheme));

			_settings.Application.Property[2].Value = newTheme;
			SaveConfigurations();
		}

		private void cboThemeColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Accent selectedColor = cboColors.SelectedItem as Accent;
			if (selectedColor != null)
			{
				var currentTheme = ThemeManager.DetectAppStyle(Application.Current);
				ThemeManager.ChangeAppStyle(Application.Current, selectedColor, currentTheme.Item1);

				_settings.Application.Property[3].Value = selectedColor.Name;
				SaveConfigurations();
			}
		}

		private void ConfigurationsTextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			_settings.Application.Property[0].Value = txtApplicationTitle.Text;

			SaveConfigurations();
		}
		#endregion
	}
}
