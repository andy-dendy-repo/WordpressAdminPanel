using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordpressClient.Data;
using WordpressClient.Services.Interfaces;

namespace WordpressClient.Services
{
    public class MetaValuesServiceBase : ServiceBase<WpPosts>, IMetaValuesServiceBase
    {
        public MetaValuesServiceBase(AdminDbContext context) : base(context)
        {
        }

        public async Task<IList<WpPostmeta>> GetMetaByPostId(ulong id)
        {
            try
            {
                return await _context.WpPostmeta.Where(x => x.PostId == id).ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task AddMeta(ulong postId, string key, string value)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                return;

            WpPostmeta postmeta = new WpPostmeta()
            {
                PostId = postId,
                MetaKey = key,
                MetaValue = value
            };

            await _context.WpPostmeta.AddAsync(postmeta);
        }

    }
}
