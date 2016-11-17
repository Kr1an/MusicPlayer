using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySound.Application.Controllers.Abstract;
using TinySound.Application.Infrastructure.Entities.Audio;
using TinySound.Application.Infrastructure.Entities.PlayList;
using TinySound.Application.Infrastructure.Utils.AudioGenres;
using TinySound.Application.Infrastructure.Utils.FileHelper;
using TinySound.Application.Infrastructure.Utils.UserInputHelper;
using TinySound.Application.Infrastructure.Utils.Validators;
using TinySound.Application.Infrastructure.Utils.Validators.Abstract;
using TinySound.Application.Models.Storage;
using TinySound.Application.Views;

namespace TinySound.Application.Controllers
{
	class AddAudioController:Controller
	{
		private Audio Audio { get; set; }
		private PlayList ChangedPlaylist { get; set; }
		public AddAudioController(Application Context, PlayList ChangedPlaylist)
		{
			this.View = new AddAudioView();
			this.Context = Context;
			this.ChangedPlaylist = ChangedPlaylist;
			Audio = new Audio();
		}
		public override void EventHandler()
		{
			base.EventHandler();
			if (UserInputHelper.IsEqual(UserKey, 0)) BackToPlaylistManager();
			if (UserInputHelper.IsEqual(UserKey, 1)) AddAudio();
			if (UserInputHelper.IsEqual(UserKey, 2)) AddAudioMin();
			

		}

		private void AddAudioMin()
		{
			Console.Clear();
			Console.WriteLine("File Name");
			Console.WriteLine("-file format is mp3");
			Console.WriteLine("-enter name with extention");
			Console.WriteLine("-file should locate on Desktop");
			Console.WriteLine("\t---------");
			Console.Write("Enter filename: ");
			this.Audio.AudioInfo.SetTitleWithIdChange(Console.ReadLine());
			this.Audio.AudioInfo.Duration = 70;
			this.Audio.AudioInfo.Genre = Genres.Country;
			this.Audio.AudioInfo.Rating = 5;
			this.Audio.FileHelper.FilePath = Path.Combine(FileHelper.FolderPath, Audio.AudioInfo.Title);

			SaveAndAdd();
			BackToPlaylistManager();
		}

		private void AddAudio()
		{

			AddAudioMin();
		}

		private void BackToPlaylistManager()
		{
			this.Context.Controller = new ManagePlaylistController(Context, ChangedPlaylist);
			Context.Controller.Render();
		}

		private void SaveAndAdd()
		{

			if (AudioTitleValidator.IsValid(Audio, ChangedPlaylist))
			{
				ChangedPlaylist.Audios.Add(Audio.AudioInfo.Id, Audio);
			}


		}

		private void TitleEnter()
		{
			Console.Clear();
			Console.Write("Enter Name: ");
			((AddAudioView)View).Title = Console.ReadLine();
			((AddAudioView)View).Error = "";
		}
	}
}
