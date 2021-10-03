using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.ModelsDTO
{
	public class CharacteristicDto
	{
		public string Property { get; set; }
		public string Value { get; set; }
		public bool TakePartInSort { get; set; }
	}
}
