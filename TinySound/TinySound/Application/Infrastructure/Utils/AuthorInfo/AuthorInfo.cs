using System;

namespace TinySound.Application.Infrastructure.Utils.AuthorInfo
{
	public class AuthorInfo:IEquatable<AuthorInfo>
	{
		public AuthorInfo()
		{
			AuthorName = "";
		}
		public string AuthorName { get; set; }
		public bool Equals(AuthorInfo other)
		{
			return AuthorName == other.AuthorName;
		}
	}
}
