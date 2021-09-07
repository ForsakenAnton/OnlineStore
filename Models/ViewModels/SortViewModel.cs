using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public enum SortState
    {
        PriceAsc,
        PriceDesc,
        TopAsc,
        TopDesc,
        DiscountAsc,
        DiscountDesc,
        CountAsc,
        CountDesc,

        ByDate,
        ByUseful
    }

    public class SortViewModel
    {
        //public SortState PriceSort { get; private set; }
        public SortState Current { get; private set; }

        public SortViewModel(SortState sortOrder)
        {
            //PriceSort = sortOrder; //== SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
            Current = sortOrder;
        }
    }
}
