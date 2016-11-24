using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySound.Application.Controllers.Abstract;
using TinySound.Application.Infrastructure.Entities.PlayingList;
using TinySound.Application.Infrastructure.Utils.UserInputHelper;
using TinySound.Application.Views;

namespace TinySound.Application.Controllers
{
	class ManagePlayinglistController:Controller
	{
		private PlayingList PlayingList;
		public ManagePlayinglistController(Application Context, PlayingList PlayingList)
		{
			this.View = new ManagePlayinglistView();
			this.Context = Context;
			this.PlayingList = PlayingList;
		}

		public override void EventHandler()
		{
			base.EventHandler();

			if (UserInputHelper.IsEqual(UserKey, 0)) BackToPlayinglistsManager();
			if (UserInputHelper.IsEqual(UserKey, 1)) PauseMp3();
			if (UserInputHelper.IsEqual(UserKey, 2)) ResumeMp3();
			if (UserInputHelper.IsEqual(UserKey, 3)) DeleteMp3();
			if (UserInputHelper.IsEqual(UserKey, 4)) NextAudio();
		}

		private void NextAudio()
		{
			this.PlayingList.NextAudio();

		}

		private void DeleteMp3()
		{
			this.PlayingList.Delete();
			this.Context.Controller = new PlayerManagerController(Context);
			Context.Controller.Render();
		}

		private void ResumeMp3()
		{
			this.PlayingList.Resume();
		}

		private void PauseMp3()
		{
			this.PlayingList.Pause();
		}
		private void BackToPlayinglistsManager()
		{
			this.Context.Controller = new PlayerManagerController(Context);
			Context.Controller.Render();
		}
	}
}
 