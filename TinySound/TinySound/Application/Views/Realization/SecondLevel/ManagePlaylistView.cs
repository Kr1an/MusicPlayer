using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySound.Application.Infrastructure.Entities.PlayList;
using TinySound.Application.Models.Storage;
using TinySound.Application.Views.Abstract;

namespace TinySound.Application.Views
{
	public class ManagePlaylistView : View
	{
		public PlayList ChangedPlayList { get; set; }
		

		public ManagePlaylistView(PlayList ChangedPlayList)
		{
			this.ChangedPlayList = ChangedPlayList;
			
		}
		public void Render()
		{
			Console.Clear();
			Console.WriteLine($"\t{ChangedPlayList.PlayListInfo.Title} Playlist Manager");
			for (int i = 0; i < ChangedPlayList.Audios.Select(x => x.Value.AudioInfo.Title).ToList().Count; i++)
			{
				Console.WriteLine($"{i + 1}){ChangedPlayList.Audios.Select(x => x.Value.AudioInfo.Title).ToList()[i]}");
			}
			Console.WriteLine("\t---------");
			Console.WriteLine("7)Delete playlist");
			Console.WriteLine("8)Delete several");
			Console.WriteLine("9)Add new audio");
			Console.WriteLine("0)Back");
			Console.WriteLine("\tPress 0-9: ");
		}
	}
}
