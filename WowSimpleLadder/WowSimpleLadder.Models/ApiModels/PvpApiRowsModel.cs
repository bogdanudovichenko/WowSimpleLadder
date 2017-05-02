using System.Collections.Generic;

namespace WowSimpleLadder.Models.ApiModels
{
    public class PvpApiRowsModel
    {
        public IEnumerable<PvpApiRowModel> Rows { get; set; }
    }
}
