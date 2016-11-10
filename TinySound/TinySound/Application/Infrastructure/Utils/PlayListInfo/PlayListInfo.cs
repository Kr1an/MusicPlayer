using System.Runtime.Serialization.Formatters;

namespace TinySound.Application.Infrastructure.Utils.PlayListInfo
{
	public class PlayListInfo
	{
		public PlayListInfo()
		{
			Id = 0;
			Rating = 0;
			Duration = 0;
			Title = "";
		}
		public int Id { get; set; }
		public int Rating { get; set; }
		public int Duration { get; set; }
		public string Title { get; set; }

		public void SetTitleWithIdChange(string Title)
		{
			this.Title = Title;
			this.Id = Title.GetHashCode()%1024;
		}


	}
}
