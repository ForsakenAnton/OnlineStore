using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.ModelsDTO
{
	public class TemplateDto
	{
		public string Property { get; set; }
		public bool TakePartInSort { get; set; }
	}
}
