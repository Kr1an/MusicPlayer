using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySound.Application.Views.Abstract;

namespace TinySound.Application.Views
{
	class StartMenuView:View
	{
		public void Render()
		{
			Console.Clear();
			Console.WriteLine("\tMain Menu");
			Console.WriteLine("1)Play new list");
			Console.WriteLine("2)Manage plaing lists");
			Console.WriteLine("3)Manage playlists");
			Console.WriteLine("0)Save and exit");
			Console.WriteLine("\tPress 0-3: ");
		}
	}
}
