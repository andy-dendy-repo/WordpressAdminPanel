using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordpressClient.Data;

namespace WordpressClient.Services.Interfaces
{
    public interface IMetaValuesServiceBase : IServiceBase<WpPosts>
    {
        Task<IList<WpPostmeta>> GetMetaByPostId(ulong id);

        Task AddMeta(ulong postId, string key, string value);
    }
}
