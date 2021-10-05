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
    public class CharacteristicAutoMapperProfile : Profile
    {
		public CharacteristicAutoMapperProfile()
		{
			CreateMap<Characteristic, CharacteristicsListDto>()
						.ForMember(c => c.ListDto,
							opt => opt.MapFrom(
								src => JsonSerializer.Deserialize<List<CharacteristicDto>>(src.SerializedCharactetistics, new JsonSerializerOptions { }))); //ToCreateClassList(src.SerializedClassesListToString)));

			CreateMap<CharacteristicsListDto, Characteristic>()
				.ForMember(p => p.SerializedCharactetistics,
				opt => opt.MapFrom(
					src => JsonSerializer.Serialize(src.ListDto, new JsonSerializerOptions { WriteIndented = true }))); //ToCreateJoinString(src.ListDto)));
		}
    }
}
