using System;
using TinySound.Application.Infrastructure.Entities.Audio.Abstract;
using TinySound.Application.Infrastructure.Utils.AudioInfo;
using TinySound.Application.Infrastructure.Utils.AuthorInfo;
using TinySound.Application.Infrastructure.Utils.FileHelper;

namespace TinySound.Application.Infrastructure.Entities.Audio
{
	public class Audio:IAudio, IEquatable<Audio>
	{
		public Audio()
		{
			AutorInfo = new AuthorInfo();
			AudioInfo = new AudioInfo();
			FileHelper = new FileHelper();
			
		}

		public AuthorInfo AutorInfo{ get; set; }
		public AudioInfo AudioInfo { get; set; }
		public FileHelper FileHelper { get; set; }
		
		
		public bool Equals(Audio other)
		{
			if (this.AutorInfo == other.AutorInfo &&
			    this.AudioInfo == other.AudioInfo)
				return true;
			return false;
		}
	}
}
