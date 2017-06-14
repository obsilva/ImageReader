using System.Speech.AudioFormat;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace ImageReader.Domain
{
	public class Speech
	{
		#region Properties
		private SpeechSynthesizer Synthesizer { get; set; }
		#endregion


		#region Constructors
		public Speech() => Synthesizer = new SpeechSynthesizer();
		#endregion


		#region Methods
		public void FromText(string text)
		{
			if (Synthesizer.State == SynthesizerState.Paused)
			{ Synthesizer.Resume(); }
			else if (Synthesizer.State == SynthesizerState.Ready)
			{ Synthesizer.Speak(text); }
		}

		public void Pause()
		{
			if (Synthesizer.State == SynthesizerState.Speaking)
			{ Synthesizer.Pause(); }
		}

		public void ExportAudio(string text)
		{
			var synth = new SpeechSynthesizer();
			var saveFileDialog = new SaveFileDialog()
			{
				FileName = "imageReader",
				Filter = "Audio Files | *.wav"
			};

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				// Now here's our save folder
				string audioFilePath = saveFileDialog.FileName;
				audioFilePath = audioFilePath.Replace(".wav", "");
				audioFilePath = audioFilePath + ".wav";

				synth.SetOutputToDefaultAudioDevice();
				synth.SetOutputToWaveFile(audioFilePath,
					new SpeechAudioFormatInfo(32000, AudioBitsPerSample.Sixteen, AudioChannel.Stereo));

				synth.Volume = 100;
				synth.Speak(text);
				synth.Dispose();
			}
		}
		#endregion
	}
}
