using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordpressClient.Data;

namespace WordpressClient.Services.Interfaces
{
    public interface IGoodsService : IServiceBase<WpPosts>
    {
        Task<IList<WpPostmeta>> GetMetaByProductId(ulong id);

        Task<IList<WpPosts>> GetProductsFilteredByCategoryIds(IList<ulong> ids = null);
    }
}
