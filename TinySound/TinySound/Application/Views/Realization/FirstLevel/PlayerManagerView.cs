using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySound.Application.Models.Storage;
using TinySound.Application.Views.Abstract;

namespace TinySound.Application.Views
{
	class PlayerManagerView:View
	{
		public void Render()
		{
			Console.Clear();

			Console.WriteLine("\tPlaying Playlists");
			for (int i = 0; i < Storage.PlayingLists.Count; i++)
			{
				Console.WriteLine($"{i + 1}){Storage.PlayingLists[i].Title}");
			}
			Console.WriteLine("\t---------");
			Console.WriteLine("0)Back");
			Console.WriteLine("\t---------");
			Console.WriteLine("##Choose playlist to play");
		}
	}
}
