using TinySound.Application.Infrastructure.Utils.AudioInfo;
using TinySound.Application.Infrastructure.Utils.AuthorInfo;
using TinySound.Application.Infrastructure.Utils.FileHelper;

namespace TinySound.Application.Infrastructure.Entities.Audio.Abstract
{
	interface IAudio
	{
		//Interface needed to represent audio track
		//
		//entity in TinySound MediaPlayer
		AuthorInfo AutorInfo { get; set; }	//Autor Info
		AudioInfo AudioInfo { get; set; }	//Audio Track Info
		FileHelper FileHelper { get; set; }	//Fil  Info and ...
	}
}
