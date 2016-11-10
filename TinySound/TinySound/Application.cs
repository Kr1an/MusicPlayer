using TinySound.Application.Abstract;
using TinySound.Application.Controllers;
using TinySound.Application.Models.Storage;


namespace TinySound.Application
{
	public class Application:App
	{
		public Application()
		{
			this.Controller = new StartMenuController(this);
			Storage.ReadData();
		}

		public void Start()
		{
			this.Controller.Render();	
		}


	}
}
