using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySound.Application.Controllers;
using TinySound.Application.Controllers.Abstract;

namespace TinySound.Application.Abstract
{
	public abstract class App
	{
		internal Controller Controller;

		public void CreatePage(Controller Controller)
		{
			this.Controller = Controller;
			this.Controller.Render();
		}
		
	}
}
