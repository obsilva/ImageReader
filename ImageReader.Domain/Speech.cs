using System.Speech.Synthesis;

namespace ImageReader.Domain
{
	public class Speech
	{
		public void SoletraTexto(string strTexto)
		{
			//Inicializa uma nova instancia
			SpeechSynthesizer synth = new SpeechSynthesizer();

			//Configura a saida de audio
			synth.SetOutputToDefaultAudioDevice();

			//
			synth.Speak(strTexto.ToString());
		}
	}
}
