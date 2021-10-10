using OnlineStore.Models.ModelsDTO;
using System.Collections;
using System.Collections.Generic;

namespace OnlineStore.Models.ViewModels
{
    public class CharacteristicsListViewModel: IEqualityComparer
    {
        public int Count { get; set; }
        public string Value { get; set; }

        public new bool Equals(object x, object y)
        {
            throw new System.NotImplementedException();
        }

        public int GetHashCode(object obj)
        {
            throw new System.NotImplementedException();
        }
    }
}