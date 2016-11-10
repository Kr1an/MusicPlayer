using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySound.Application.Controllers.Abstract;
using TinySound.Application.Infrastructure.Utils.UserInputHelper;
using TinySound.Application.Models.Storage;
using TinySound.Application.Views;

namespace TinySound.Application.Controllers
{
	class StartMenuController : Controller
	{
		public StartMenuController(Application Context)
		{
			this.View = new StartMenuView();
			this.Context = Context;
		}

		public override void EventHandler()
		{
			base.EventHandler();
			if (UserInputHelper.IsEqual(UserKey, 0)) SaveAndExit();
			if (UserInputHelper.IsEqual(UserKey, 1)) PlayNewList();
			if (UserInputHelper.IsEqual(UserKey, 2)) ManagePlayingLists();
			if (UserInputHelper.IsEqual(UserKey, 3)) ManagePlaylists();
		}

		private void ManagePlayingLists()
		{
			this.Context.Controller = new PlayerManagerController(Context);
			Context.Controller.Render();
		}

		private void PlayNewList()
		{
			this.Context.Controller = new PlayNewListController(Context);
			Context.Controller.Render();
		}

		public void SaveAndExit()
		{
			Storage.WriteData();
			Environment.Exit(0);
		}

		public void ManagePlaylists()
		{
			this.Context.Controller = new PlaylistsManagerController(Context);
			Context.Controller.Render();
		}
	}
}
