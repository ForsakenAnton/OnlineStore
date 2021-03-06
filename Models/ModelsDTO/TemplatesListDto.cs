using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.ModelsDTO
{
	public class TemplatesListDto
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public List<TemplateDto> ListDto { get; set; }
    }
}
