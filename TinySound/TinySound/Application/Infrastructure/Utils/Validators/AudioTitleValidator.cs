using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySound.Application.Infrastructure.Entities.Audio;
using TinySound.Application.Infrastructure.Entities.PlayList;
using TinySound.Application.Infrastructure.Utils.Validators.Abstract;

namespace TinySound.Application.Infrastructure.Utils.Validators
{
	class AudioTitleValidator:WordValidator
	{
		public static bool IsValid(Audio audio, PlayList currentPlayList)

		{
			return audio.AudioInfo.Title.Length >= 4 && audio.AudioInfo.Title.Length <= 20 &&
			       !currentPlayList.Audios.Values.Select(x=>x.AudioInfo.Title).Contains(audio.AudioInfo.Title) && File.Exists(audio.FileHelper.FilePath);
		}
	}
}
