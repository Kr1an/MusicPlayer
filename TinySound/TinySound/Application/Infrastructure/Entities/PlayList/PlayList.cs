using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using TinySound.Application.Infrastructure.Entities.PlayList.Abstract;
using TinySound.Application.Infrastructure.Utils.PlayListInfo;

namespace TinySound.Application.Infrastructure.Entities.PlayList
{
	public class PlayList:IPlayList, IEquatable<PlayList>
	{
		public PlayList()
		{
			Audios = new Dictionary<int, Audio.Audio>();
			PlayListInfo = new PlayListInfo();
		}
		public PlayListInfo PlayListInfo { get; set; }

		[XmlElement("Dictionary")]
		public List<KeyValuePair<int, Audio.Audio>> XMLDictionaryProxy
		{
			get
			{
				return new List<KeyValuePair<int, Audio.Audio>>(this.Audios);
			}
			set
			{
				this.Audios = new Dictionary<int, Audio.Audio>();
				foreach (var pair in value)
					this.Audios[pair.Key] = pair.Value;
			}
		}


		[XmlIgnore]
		public Dictionary<Int32, Audio.Audio> Audios { get; set; }

		public bool Equals(PlayList other)
		{
			if (Audios == null && other.Audios == null)
				return true;
			if (Audios != null && other.Audios == null)
				return false;
			if (Audios == null && other.Audios != null)
				return false;
			if (Audios.Count != other.Audios.Count)
				return false;
			
			foreach (var item in this.Audios)
				if (!other.Audios.Contains(item))
					return false;
			return true;
		}	
	}
}
