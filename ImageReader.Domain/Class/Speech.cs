using System.Media;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;

namespace ImageReader.Domain
{
	public class Speech
	{
        SoundPlayer soundPlayer;
         
        public void FromText(string text)
		{
			string audioFilePath = System.IO.Path.GetFullPath(@"..\files") + "\\test.wav";
			var synthesizer = new SpeechSynthesizer();

			// Synthesizer configuration
			synthesizer.SetOutputToDefaultAudioDevice();
			synthesizer.SetOutputToWaveFile(audioFilePath,
				new SpeechAudioFormatInfo(32000, AudioBitsPerSample.Sixteen, AudioChannel.Stereo));

            // Cria um  SoundPlayer para dar tocar o arquivo de audio.
            soundPlayer = new SoundPlayer(audioFilePath);

            //Fala
            synthesizer.Speak(text.ToString());
			soundPlayer.Play();
		}

		public void Pause()
		{
            soundPlayer.Stop();
        }
	}
}
