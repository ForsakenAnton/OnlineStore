using AutoMapper;
using OnlineStore.Models;
using OnlineStore.Models.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineStore.AutoMapperProfiles
{
    public class CharactetisticAutoMapperProfile : Profile
    {
		public CharactetisticAutoMapperProfile()
		{
			CreateMap<Charactetistic, CharacteristicsListDto>()
						.ForMember(c => c.ListDto,
							opt => opt.MapFrom(
								src => JsonSerializer.Deserialize<List<CharacteristicDto>>(src.SerializedClassesListToString, new JsonSerializerOptions { }))); //ToCreateClassList(src.SerializedClassesListToString)));

			CreateMap<CharacteristicsListDto, Charactetistic>()
				.ForMember(p => p.SerializedClassesListToString,
				opt => opt.MapFrom(
					src => JsonSerializer.Serialize(src.ListDto, new JsonSerializerOptions { WriteIndented = true }))); //ToCreateJoinString(src.ListDto)));
		}
    }
}
