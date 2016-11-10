using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySound.Application.Controllers.Abstract;
using TinySound.Application.Infrastructure.Entities.PlayList;
using TinySound.Application.Infrastructure.Utils.UserInputHelper;
using TinySound.Application.Models.Storage;
using TinySound.Application.Views;

namespace TinySound.Application.Controllers
{
	class ManagePlaylistController:Controller
	{
		private PlayList ChangedPlaylist;

		public ManagePlaylistController(Application Contex, PlayList ChangedPlaylist)
		{
			this.View = new ManagePlaylistView(ChangedPlaylist);
			this.Context = Contex;
			this.ChangedPlaylist = ChangedPlaylist;
		}

		public override void EventHandler()
		{
			base.EventHandler();
			if (UserInputHelper.IsEqual(UserKey, 0)) BackToPlaylistsManager();
			if (UserInputHelper.IsEqual(UserKey, 7)) DeletePlaylist();
			if (UserInputHelper.IsEqual(UserKey, 8)) DeleteSeveral();
			if (UserInputHelper.IsEqual(UserKey, 9)) AddNewAudio();
			
		}

		private void DeletePlaylist()
		{
			Storage.PlayLists.Remove(ChangedPlaylist);
			this.Context.Controller = new PlaylistsManagerController(Context);
			Context.Controller.Render();
		}

		private void AddNewAudio()
		{
			this.Context.Controller = new AddAudioController(Context, ChangedPlaylist);
			Context.Controller.Render();
		}

		private void DeleteSeveral()
		{
			int AudioCount = ChangedPlaylist.Audios.Count;
			for (int i = 0; i < AudioCount; i += 2)
				ChangedPlaylist.Audios.Remove(ChangedPlaylist.Audios.Min(x => x.Key));
		}

		private void BackToPlaylistsManager()
		{
			this.Context.Controller = new PlaylistsManagerController(Context);
			Context.Controller.Render();
		}
	}
}
