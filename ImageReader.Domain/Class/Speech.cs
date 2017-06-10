using System.Speech.AudioFormat;
using System.Speech.Synthesis;

namespace ImageReader.Domain
{
	public class Speech
	{
		public void FromText(string text)
		{
			string audioFilePath = System.IO.Path.GetTempPath() + "test.wav";
			var synthesizer = new SpeechSynthesizer();

			// Synthesizer configuration
			synthesizer.SetOutputToDefaultAudioDevice();
			synthesizer.SetOutputToWaveFile(audioFilePath,
				new SpeechAudioFormatInfo(32000, AudioBitsPerSample.Sixteen, AudioChannel.Stereo));

			// Cria um  SoundPlayer para dar tocar o arquivo de audio.
			var soundPlayer = new System.Media.SoundPlayer(audioFilePath);

			//Fala
			synthesizer.Speak(text.ToString());
			soundPlayer.Play();
		}

		public void Pause()
		{
			//Inicializa uma nova instancia
			SpeechSynthesizer synth = new SpeechSynthesizer();
			synth.Pause();
		}
	}
}
