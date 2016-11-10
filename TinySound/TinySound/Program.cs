using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;

using System.Threading.Tasks;
using TinySound.Application;
using System.Timers;

using TinySound.Application.Infrastructure.Utils.PlayListInfo;
using Bonsai.Player;
using Bonsai;
using CSCore;
using CSCore.Codecs;
using CSCore.SoundOut;
using TinySound.Application.Infrastructure.Entities.Audio;
using TinySound.Application.Infrastructure.Entities.PlayingList;
using TinySound.Application.Infrastructure.Utils.AudioGenerator;
using TinySound.Application.Infrastructure.Utils.FileHelper;
using TinySound.Application.Models.Storage;

namespace TinySound
{
	class Program
	{
		public static String FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

		static void Main(string[] args)
		{
			
			new Application.Application().Start();
		}

	
	}
}
