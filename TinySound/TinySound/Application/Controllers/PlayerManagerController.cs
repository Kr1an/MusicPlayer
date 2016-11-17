using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TinySound.Application.Controllers.Abstract;
using TinySound.Application.Infrastructure.Utils.UserInputHelper;
using TinySound.Application.Models.Storage;
using TinySound.Application.Views;

namespace TinySound.Application.Controllers
{
	class PlayerManagerController:Controller
	{
		Thread ControllingThread;
		public PlayerManagerController(Application Context)
		{
			this.View = new PlayerManagerView();
			this.Context = Context;
			ControllingThread = new Thread(GraphicRender);
		}

		public override void EventHandler()
		{
			base.EventHandler();
			if (UserInputHelper.IsEqual(UserKey, 0)) Back();
			if (UserInputHelper.IsInRange(UserKey, 1, Storage.PlayingLists.Count + 1))
			{
				ManagePlayingList(UserKey.KeyChar - '0' - 1);
			}
		}

		private void ManagePlayingList(int i)
		{
			if (Storage.PlayingLists.Count > i)
			{
				this.ControllingThread.Abort();

				this.Context.Controller = new ManagePlayinglistController(Context, Storage.PlayingLists[i]);
				Context.Controller.Render();

			}
			
		}

		private void Back()
		{
			this.ControllingThread.Abort();

			this.Context.Controller = new StartMenuController(Context);
			Context.Controller.Render();
		}

		public override void MainLoop()
		{
			while (true)
			{
				UserInput();
			}
		}

		public override void Render()
		{
			ControllingThread.Start();
			MainLoop();
		}

		public void GraphicRender()
		{
			while (true)
			{
				View.Render();
				Thread.Sleep(100);
			}
		}
	}
}
