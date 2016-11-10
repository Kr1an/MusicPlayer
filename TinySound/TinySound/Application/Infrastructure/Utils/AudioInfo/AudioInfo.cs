using System;
using TinySound.Application.Infrastructure.Utils.AudioGenres;

namespace TinySound.Application.Infrastructure.Utils.AudioInfo
{
	public class AudioInfo: IEquatable<AudioInfo>
	{

		public AudioInfo()
		{
			Id = 0;
			Title = "";
			Genre = new Genre();
			Duration = 40;
			Rating = 0;
		}
		public int Id { get; set; }
		public string Title { get; set; }
		public Genre Genre { get; set; }
		public int Duration { get; set; }
		public int Rating { get; set; }
		public bool Equals(AudioInfo other)
		{
			if (Id == other.Id && Title == other.Title && Genre == other.Genre &&
			    Duration == other.Duration && Rating == other.Rating)
				return true;
			return false;
		}
		public void SetTitleWithIdChange(string Title)
		{
			this.Title = Title;
			this.Id = (Title.GetHashCode() % 1024 + Duration.GetHashCode() % 1024) % 1024;
		}
	}
}
