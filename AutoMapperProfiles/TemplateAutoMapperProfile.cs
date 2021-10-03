using AutoMapper;
using OnlineStore.Models;
using OnlineStore.Models.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineStore.AutomapperProfiles
{
    public class TemplateAutoMapperProfile : Profile
    {
        public TemplateAutoMapperProfile()
        {
			CreateMap<Template, TemplatesListDto>()
					.ForMember(t => t.TemplatesDto,
						opt => opt.MapFrom(
							src => JsonSerializer.Deserialize<List<TemplateDto>>(src.SerializedClassesListToString, new JsonSerializerOptions { })));

			CreateMap<TemplatesListDto, Template>()
				.ForMember(t => t.SerializedClassesListToString,
				opt => opt.MapFrom(
					src => JsonSerializer.Serialize(src.TemplatesDto, new JsonSerializerOptions { WriteIndented = true })));
		}
    }
}
