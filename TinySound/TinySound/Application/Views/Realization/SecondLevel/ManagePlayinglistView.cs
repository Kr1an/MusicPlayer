using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySound.Application.Views.Abstract;

namespace TinySound.Application.Views
{
	class ManagePlayinglistView:View
	{
		public void Render()
		{
			Console.Clear();
			Console.WriteLine("\tPlayinglist manager");
			Console.WriteLine("1)Pause");
			Console.WriteLine("2)Resume");
			Console.WriteLine("3)Delete");
			Console.WriteLine("0)Back");
			Console.WriteLine("\tPress 0-3: ");
		}
	}
}
