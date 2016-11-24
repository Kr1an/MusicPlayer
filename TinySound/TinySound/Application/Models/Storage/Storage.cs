using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SObjectApplication.Repository.Serializer;
using TinySound.Application.Infrastructure.Entities.Audio;
using TinySound.Application.Infrastructure.Entities.PlayingList;
using TinySound.Application.Infrastructure.Entities.PlayList;
using TinySound.Application.Infrastructure.Utils.AudioGenres;
using TinySound.Application.Infrastructure.Utils.AudioInfo;
using TinySound.Application.Infrastructure.Utils.FileHelper;
using TinySound.Application.Infrastructure.Utils.PlayListInfo;

namespace TinySound.Application.Models.Storage
{
	public static class Storage
	{
		public class Lib
		{
			public List<PlayList> PlayLists;
			public List<PlayingList> PlayingLists;
		}
		public static List<PlayList> PlayLists;
		public static List<PlayingList> PlayingLists;

		static public String FileName = "TinySoundDB.xml";//can be changed
		static public String FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TinySound");
		static public String FullPath = Path.Combine(FolderPath, FileName);


		static Storage()
		{

			


		}

		public static void WriteData()
		{
			XmlSerializer mySerializer = new XmlSerializer(typeof(List<PlayList>));
			StreamWriter myWriter = new StreamWriter(FullPath);

			mySerializer.Serialize(myWriter, Storage.PlayLists);
			myWriter.Close();
		}

		public static void AddTestRecords()
		{
			PlayLists = new List<PlayList>();
			PlayingLists = new List<PlayingList>();
			
			//PlayLists.Add(new PlayList() { PlayListInfo = new PlayListInfo() { Duration = 134, Id = 1, Rating = 9, Title = "Yellow Bobles"} });
			//PlayLists.Add(new PlayList() { PlayListInfo = new PlayListInfo() { Duration = 200, Id = 2, Rating = 6, Title = "New Music Age"} });
			//PlayLists.Add(new PlayList() { PlayListInfo = new PlayListInfo() { Duration = 200, Id = 2, Rating = 6, Title = "Dream"} });
			//PlayLists[0].Audios.Add(1 ,new Audio() {FileHelper = new FileHelper() {FilePath = "rel1.mp3"}, AudioInfo = new AudioInfo() { Duration = 40, Genre = Genres.Asian,      Id = 1, Title = "rel1.mp3" , Rating = 9} });
			//PlayLists[0].Audios.Add(2, new Audio() {FileHelper = new FileHelper() {FilePath = "rel2.mp3"}, AudioInfo = new AudioInfo() { Duration = 40, Genre = Genres.AvantGarde, Id = 2, Title = "rel2.mp3", Rating = 9 } });
			//PlayLists[0].Audios.Add(3, new Audio() {FileHelper = new FileHelper() {FilePath = "rel3.mp3"}, AudioInfo = new AudioInfo() { Duration = 54, Genre = Genres.Electronic, Id = 3, Title = "rel3.mp3", Rating = 9 } });

			//PlayLists[1].Audios.Add(4, new Audio() {FileHelper = new FileHelper() {FilePath = "rel4.mp3"}, AudioInfo = new AudioInfo() { Duration = 80, Genre = Genres.Asian,      Id = 1, Title = "rel4.mp3",  Rating = 4 } });
			//PlayLists[1].Audios.Add(5, new Audio() {FileHelper = new FileHelper() {FilePath = "rel1.mp3"}, AudioInfo = new AudioInfo() { Duration = 80, Genre = Genres.AvantGarde, Id = 2, Title = "rel1.mp3", Rating = 8 } });
			//PlayLists[1].Audios.Add(6, new Audio() {FileHelper = new FileHelper() {FilePath = "rel0.mp3"}, AudioInfo = new AudioInfo() { Duration = 40, Genre = Genres.Electronic, Id = 3, Title = "rel0.mp3",   Rating = 6 } });

			//PlayLists[2].Audios.Add(7, new Audio() { FileHelper = new FileHelper() { FilePath = "relax_min.mp3" }, AudioInfo = new AudioInfo() { Duration = 40, Genre = Genres.Electronic, Id = 3, Title = "relax_min.mp3", Rating = 6 } });


		}
		static public void ReadData()
		{
			if (!File.Exists(FullPath))
			{
				if (!Directory.Exists(FolderPath))
					Directory.CreateDirectory(FolderPath);
				if (!File.Exists(FullPath))
					File.Delete(FullPath);
				AddTestRecords();
			}
			else
			{
				XmlSerializer mySerializer = new XmlSerializer(typeof(List<PlayList>));
				FileStream myFileStream = new FileStream(FullPath, FileMode.Open);
				Storage.PlayLists = (List<PlayList>)mySerializer.Deserialize(myFileStream);
				Storage.PlayingLists = new List<PlayingList>();
				myFileStream.Close();
			}
		}
	}
}
