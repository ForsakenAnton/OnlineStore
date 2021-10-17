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

    class CharacteristicDtoComparer : IEqualityComparer<CharacteristicDto>
    {
        public bool Equals(CharacteristicDto x, CharacteristicDto y)
        {
            if (Object.ReferenceEquals(x, y))
                return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.Property == y.Property && x.Value == y.Value && x.TakePartInSort == y.TakePartInSort;
        }

        public int GetHashCode(CharacteristicDto characteristicDto)
        {
            if (Object.ReferenceEquals(characteristicDto, null))
                return 0;

            int hashCharacteristicProperty = characteristicDto.Property == null ? 0 : characteristicDto.Property.GetHashCode();
            int hashCharacteristicValue = characteristicDto.Value == null ? 0 : characteristicDto.Value.GetHashCode();
            int hashCharacteristicTakePartInSort = characteristicDto.TakePartInSort.GetHashCode();

            return hashCharacteristicProperty ^ hashCharacteristicValue ^ hashCharacteristicTakePartInSort;
        }
    }
}
