using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySound.Application.Views.Abstract;

namespace TinySound.Application.Views
{
	public class AddPlaylistView:View
	{
		public String Title { get; set; }
		public String Error { get; set; }

		public AddPlaylistView()
		{
			Title = "untitled";
			Error = "";
		}
		public void Render()
		{
			Console.Clear();
			Console.WriteLine("\tAdd playlist");
			Console.WriteLine($"1)Title enter ({Title})");
			Console.WriteLine($"2)Save and add {Error}");
			Console.WriteLine("0)Back");
			
			Console.WriteLine("\tPress 0-2: ");
		}
	}
}
