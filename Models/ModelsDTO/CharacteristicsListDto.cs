using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.ModelsDTO
{
	public class CharacteristicsListDto
	{
        public int Id { get; set; }
        public List<CharacteristicDto> ListDto { get; set; }
	}
}
