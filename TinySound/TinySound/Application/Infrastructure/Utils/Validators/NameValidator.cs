using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TinySound.Application.Infrastructure.Utils.Validators.Abstract;
using TinySound.Application.Models.Storage;

namespace TinySound.Application.Infrastructure.Utils.Validators
{
	class NameValidator:WordValidator
	{
		public static bool IsValid(String _name)
		{
			return
				_name.Length >= 4 && _name.Length <= 20 &&
				_name.ToCharArray().All(ch => (IsChar(ch) || IsSpace(ch)));
		}
	}
}
