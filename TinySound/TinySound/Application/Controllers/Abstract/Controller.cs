using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TinySound.Application.Views.Abstract;

namespace TinySound.Application.Controllers.Abstract
{
	public abstract class Controller
	{
		internal View View;
		internal ConsoleKeyInfo UserKey;
		internal Application Context;

		internal void UserInput()
		{
			UserKey = Console.ReadKey();
			Console.Write((char)8);
			EventHandler();
			
		}
		public virtual void Render()
		{
			this.MainLoop();
		}
		

		public virtual void MainLoop()
		{
			while (true)
			{
				Thread.Sleep(100);
				this.View.Render();
				UserInput();	
			}	
		}

		public virtual void EventHandler()
		{
			
		}

	}
}
