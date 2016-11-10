using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinySound.Application.Infrastructure.Utils.Validators.Abstract
{
	abstract class WordValidator
	{
		internal static bool IsChar(char ch)
		{
			return Char.IsLetter(ch);
		}

		internal static bool IsDigit(char ch)
		{
			return Char.IsDigit(ch);
		}

		internal static bool IsSpace(char ch)
		{
			return Char.IsWhiteSpace(ch);
		}
	}
}
