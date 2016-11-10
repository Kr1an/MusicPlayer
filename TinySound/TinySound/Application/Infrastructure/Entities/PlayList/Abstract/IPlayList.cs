using System;
using System.Collections.Generic;
using TinySound.Application.Infrastructure.Utils.PlayListInfo;

namespace TinySound.Application.Infrastructure.Entities.PlayList.Abstract
{
	interface IPlayList
	{
		PlayListInfo PlayListInfo { get; set; }
		Dictionary<Int32, Audio.Audio> Audios { get; set; }	
	}
}
