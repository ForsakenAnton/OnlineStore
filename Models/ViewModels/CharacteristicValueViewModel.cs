using OnlineStore.Models.ModelsDTO;
using System;
using System.Collections;
using System.Collections.Generic;

namespace OnlineStore.Models.ViewModels
{
    public class CharacteristicValueViewModel
    {
        public int Count { get; set; }
        public string Value { get; set; }
    }

    class CharacteristicValueViewModelComparer : IEqualityComparer<CharacteristicValueViewModel>
    {
        public bool Equals(CharacteristicValueViewModel x, CharacteristicValueViewModel y)
        {
            if (Object.ReferenceEquals(x, y))
                return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.Value == y.Value;
        }

        public int GetHashCode(CharacteristicValueViewModel characteristicValue)
        {
            if (Object.ReferenceEquals(characteristicValue, null))
                return 0;

            int hashCharacteristicValue = characteristicValue.Value == null ? 0 : characteristicValue.Value.GetHashCode();

            return hashCharacteristicValue;
        }
    }
}