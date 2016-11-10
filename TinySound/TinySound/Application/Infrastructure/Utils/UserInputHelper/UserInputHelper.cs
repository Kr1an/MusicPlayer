using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinySound.Application.Infrastructure.Utils.UserInputHelper
{
	public static class UserInputHelper
	{
		public static bool IsEqual(ConsoleKeyInfo UserKey, int ExpectedChar)
		{
			return UserKey.Key == (ConsoleKey) ('0' + ExpectedChar);
		}

		public static bool IsInRange(ConsoleKeyInfo UserKey, int from, int toWithout)
		{
			int obj = (UserKey.KeyChar - '0');

			return obj >= from && obj < toWithout;
		}
	}
}
