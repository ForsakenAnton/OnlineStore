using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class GroupCharacteristicsViewModel
    {
        public string Property { get; set; }
        public List<CharacteristicsListViewModel> CharacteristicsListViewModel  { get; set; }
    }
}
