using System.Collections.Generic;

namespace WowSimpleLadder.Models.ApiModels
{
    public class PvpApiRowsModel
    {
        public IReadOnlyList<PvpApiRowModel> Rows { get; set; }
    }
}
