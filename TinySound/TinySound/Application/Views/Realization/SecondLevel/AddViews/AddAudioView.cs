using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySound.Application.Infrastructure.Utils.AudioGenres;
using TinySound.Application.Views.Abstract;

namespace TinySound.Application.Views
{
	public class AddAudioView:View
	{
		public String Title { get; set; }
		public Int32 Duration { get; set; }
		public String AutorName { get; set; }
		public Int32 Rating { get; set; }
		public String Error { get; set; }
		public Genre Genre { get; set; }
		public String FileName { get; set; }

		public AddAudioView()
		{
			Duration = 0;
			AutorName = "unknown";
			Rating = 0;
			Genre = Genres.Undefined;
			Title = "untitled";
			Error = "";
		}
		public void Render()
		{
			Console.Clear();
			Console.WriteLine("\tAdd audio");
			Console.WriteLine($"1)Add new audio");
			Console.WriteLine($"2)Add new audio(Min)");
			Console.WriteLine("\t---------");
			Console.WriteLine("0)Back");
			Console.WriteLine("\tPress 0-2: ");
		}
	}
}
