using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordpressClient.Data;
using WordpressClient.Services.Interfaces;

namespace WordpressClient.Services
{
    public class GoodsService : ServiceBase<WpPosts>, IGoodsService
    {
        public GoodsService(AdminDbContext context) : base(context)
        {
        }

        public async Task<IList<WpPostmeta>> GetMetaByProductId(ulong id)
        {
            return await _context.WpPostmeta.Where(x => x.PostId == id).ToListAsync();
        }

        public override async Task<List<WpPosts>> GetAllAsync()
        {
            return await _context.WpPosts.Where(x => x.PostType == "product").ToListAsync();
        }

        public async Task<IList<WpPosts>> GetProductsFilteredByCategoryIds(IList<ulong> ids = null)
        {
            if (ids == null || ids.Count==0)
                return await GetAllAsync();

            var termRels = await _context.WpTermTaxonomy.Where(x => ids.Contains(x.TermId))
                .Select(x => _context.WpTermRelationships.FirstOrDefault(y => y.TermTaxonomyId == x.TermTaxonomyId))
                .Select(x => _context.WpPosts.FirstOrDefault(p => p.Id == x.ObjectId)).ToListAsync();

            return termRels;
        }
    }
}
