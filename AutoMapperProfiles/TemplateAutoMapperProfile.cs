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
					.ForMember(t => t.ListDto,
						opt => opt.MapFrom(
							src =>  JsonSerializer.Deserialize<List<TemplateDto>>(src.SerializedTemplates, new JsonSerializerOptions { }))); // DeserializeToTemplate(src.SerializedTemplates, src.Id, src.Title)));

			CreateMap<TemplatesListDto, Template>()
				.ForMember(t => t.SerializedTemplates,
				opt => opt.MapFrom(
					src => JsonSerializer.Serialize(src.ListDto, new JsonSerializerOptions { WriteIndented = true })));
		}

		//private List<TemplateDto> DeserializeToTemplate(string serializedTemplates, int id, string title)
  //      {
		//	if(String.IsNullOrEmpty(serializedTemplates))
  //          {
		//		return new List<TemplateDto>();
  //          }

		//	var templatesListDto = JsonSerializer.Deserialize<List<CharacteristicDto>>(serializedTemplates);	
		//}
    }
}
