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
	class PlaylistsManagerController : Controller
	{
		public PlaylistsManagerController(Application Context)
		{
			this.View = new PlaylistsManagerView();
			this.Context = Context;
		}

		public override void EventHandler()
		{
			base.EventHandler();
			if(UserInputHelper.IsEqual(UserKey, 0)) Back();
			if(UserInputHelper.IsEqual(UserKey, 9)) AddPlaylist();

			for (int i = 0; i < Storage.PlayLists.Count; i++)
				if (UserInputHelper.IsEqual(UserKey, i + 1))
					ChoosePlaylist(i);






		}

		private void ChoosePlaylist(int Index)
		{
			this.Context.Controller = new ManagePlaylistController(Context, Storage.PlayLists[Index]);
			Context.Controller.Render();
		}

		private void AddPlaylist()
		{
			this.Context.Controller = new AddPlaylistController(Context);
			Context.Controller.Render();
		}

		private void Back()
		{
			this.Context.Controller = new StartMenuController(Context);
			Context.Controller.Render();
		}
	}
}
