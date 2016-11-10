using System;

namespace TinySound.Application.Infrastructure.Utils.AudioGenres
{
	public class Genre: IEquatable<Genre>
	{
		public Int32 Id;
		public String Name;

		public Genre()
		{
			Id = 0;
			Name = "";
		}
		public bool Equals(Genre other)
		{
			return (Id == other.Id && Name == other.Name);
		}
	}
}
