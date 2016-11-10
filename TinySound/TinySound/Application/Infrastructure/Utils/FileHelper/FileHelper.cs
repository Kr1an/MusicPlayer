using System;

namespace TinySound.Application.Infrastructure.Utils.FileHelper
{
	public class FileHelper:IEquatable<FileHelper>
	{
		public static String FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		public FileHelper()
		{
			FilePath = "";
		}
		public String FilePath { get; set; }
		public bool Equals(FileHelper other)
		{
			return this.FilePath == other.FilePath;
		}
	}
}
