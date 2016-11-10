using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySound.Application.Controllers.Abstract;
using TinySound.Application.Infrastructure.Entities.PlayList;
using TinySound.Application.Infrastructure.Utils.UserInputHelper;
using TinySound.Application.Infrastructure.Utils.Validators;
using TinySound.Application.Infrastructure.Utils.Validators.Abstract;
using TinySound.Application.Models.Storage;
using TinySound.Application.Views;

namespace TinySound.Application.Controllers
{
	class AddPlaylistController:Controller
	{
		public AddPlaylistController(Application Context)
		{
			this.View = new AddPlaylistView();
			this.Context = Context;
		}
		public override void EventHandler()
		{
			base.EventHandler();
			if (UserInputHelper.IsEqual(UserKey, 0)) BackToPlaylistsManager();
			if (UserInputHelper.IsEqual(UserKey, 1)) TitleEnter();
			if (UserInputHelper.IsEqual(UserKey, 2)) SaveAndAdd();
			

		}

		private void BackToPlaylistsManager()
		{
			this.Context.Controller = new PlaylistsManagerController(Context);
			Context.Controller.Render();
		}

		private void SaveAndAdd()
		{
			if (PlayListTitleValidator.IsValid(((AddPlaylistView) View).Title) && Storage.PlayLists.Count <= 7)
			{
				PlayList NewPlayList = new PlayList();
				NewPlayList.PlayListInfo.SetTitleWithIdChange(((AddPlaylistView)View).Title);

				Storage.PlayLists.Add(NewPlayList);
				
				BackToPlaylistsManager();
			}
			else
			{
				((AddPlaylistView)View).Title = "untitled";
				if(Storage.PlayLists.Count > 7)
					((AddPlaylistView)View).Error = @"(Error: Max audios - 8)";
				else
					((AddPlaylistView)View).Error = @"(Error: a-z, A-Z, 0-9, ' ', -, _, \, |, / Chose Another Title)";
			}

			
		}

		private void TitleEnter()
		{
			Console.Clear();
			Console.Write("Enter Name: ");
			((AddPlaylistView)View).Title = Console.ReadLine();
			((AddPlaylistView)View).Error = "";
		}
	}
}
