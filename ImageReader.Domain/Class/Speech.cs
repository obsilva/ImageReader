using System.Speech.AudioFormat;
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

            // Configura o audio a ser salvo 
            synth.SetOutputToWaveFile(@"C:\Users\Willyam\AppData\Local\Temp\test.wav",
              new SpeechAudioFormatInfo(32000, AudioBitsPerSample.Sixteen, AudioChannel.Mono));

            // Cria um  SoundPlayer para dar tocar o arquivo de audio.
            System.Media.SoundPlayer m_SoundPlayer =
              new System.Media.SoundPlayer(@"C:\Users\Willyam\AppData\Local\Temp\test.wav");

            // Build a prompt.
            PromptBuilder builder = new PromptBuilder();
            builder.AppendText("This is sample output to a WAVE file.");

            //Fala
            synth.Speak(strTexto.ToString());
            m_SoundPlayer.Play();
        }

		public void Pausa()
		{
			//Inicializa uma nova instancia
			SpeechSynthesizer synth = new SpeechSynthesizer();
			synth.Pause();
		}

	}
}
