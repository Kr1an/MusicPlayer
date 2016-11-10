using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySound.Application.Infrastructure.Utils.Validators.Abstract;
using TinySound.Application.Models.Storage;

namespace TinySound.Application.Infrastructure.Utils.Validators
{
	class PlayListTitleValidator:WordValidator
	{
		public static bool IsValid(String word)
		{
			return
				word.Length <= 15 && word.Length >= 3 && word != "untitled" &&
				Storage.PlayLists.Select(x => x.PlayListInfo.Title).All(title => title != word) &&
				word.ToCharArray().All(ch => (IsDigit(ch) || IsChar(ch) || IsSpace(ch) || @"-/\|_".ToCharArray().Contains(ch)));
		}
	}
}
