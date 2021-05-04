using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordpressClient.Data;

namespace WordpressClient.Services.Interfaces
{
    public interface IGoodsService : IMetaValuesServiceBase
    {
        Task<IList<WpTerms>> GetCategoriesByPostId(ulong id);

        Task<IList<WpPosts>> GetProductsFilteredByCategoryIds(IList<ulong> ids = null);

        Task AddWithMeta(WpPosts post, string articul, string charasteristics, string description, string discount, string price);

        Task AddWithMeta(WpPosts post, string articul, string charasteristics, string description, string discount, string price, IList<ulong> categoryIds);
    }
}
