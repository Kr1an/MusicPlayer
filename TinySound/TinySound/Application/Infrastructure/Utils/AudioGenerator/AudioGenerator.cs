using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCore;
using CSCore.Codecs;
using CSCore.SoundOut;
using NAudio;
using DirectSoundOut = NAudio.Wave.DirectSoundOut;
using WasapiOut = NAudio.Wave.WasapiOut;

namespace TinySound.Application.Infrastructure.Utils.AudioGenerator
{
	class AudioGenerator
	{

		public static ISoundOut GenerateSoundOut(String fileName)
		{
			IWaveSource soundSource = GetSoundSource("relax.mp3");
			var soundOut = GetSoundOut();
			soundOut.Initialize(soundSource);
			return soundOut;
		}
		public static ISoundOut GetSoundOut()
		{
			if (CSCore.SoundOut.WasapiOut.IsSupportedOnCurrentPlatform)
				return new CSCore.SoundOut.WasapiOut();
			else
				return new CSCore.SoundOut.DirectSoundOut();
		}
		public static IWaveSource GetSoundSource(String filename)
		{
			//return any source ... in this example, we'll just play a mp3 file
			return CodecFactory.Instance.GetCodec(Path.Combine(FileHelper.FileHelper.FolderPath, filename));
		}
	}
}
