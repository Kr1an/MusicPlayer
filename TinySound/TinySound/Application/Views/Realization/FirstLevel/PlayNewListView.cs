using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySound.Application.Models.Storage;
using TinySound.Application.Views.Abstract;

namespace TinySound.Application.Views
{
	class PlayNewListView:View
	{
		public void Render()
		{
			Console.Clear();

			Console.WriteLine("\tPlay new playlist");
			for (int i = 0; i < Storage.PlayLists.Count; i++)
			{
				Console.WriteLine($"{i + 1}){Storage.PlayLists[i].PlayListInfo.Title}");
			}
			Console.WriteLine("\t---------");
			Console.WriteLine("0)Back");
			Console.WriteLine("\t---------");
			Console.WriteLine("##Choose playlist to play");
		}
	}
}
