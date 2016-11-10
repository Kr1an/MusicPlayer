using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySound.Application.Controllers.Abstract;
using TinySound.Application.Infrastructure.Entities.PlayingList;
using TinySound.Application.Infrastructure.Utils.UserInputHelper;
using TinySound.Application.Models.Storage;
using TinySound.Application.Views;

namespace TinySound.Application.Controllers
{
	class PlayNewListController : Controller
	{
		public PlayNewListController(Application Context)
		{
			this.View = new PlayNewListView();
			this.Context = Context;
		}
		public override void EventHandler()
		{
			if (UserInputHelper.IsEqual(UserKey, 0)) Back();
			for (int i = 0; i < Storage.PlayLists.Count; i++)
				if (UserInputHelper.IsEqual(UserKey, i + 1))
					AddNewPlayingList(i);
			base.EventHandler();
		}

		private void AddNewPlayingList(int i)
		{
			Storage.PlayingLists.Add(new PlayingList(Storage.PlayLists[i]));
		}

		private void Back()
		{
			this.Context.Controller = new StartMenuController(Context);
			Context.Controller.Render();
		}
	}
}
