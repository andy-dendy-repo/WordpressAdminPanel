using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordpressClient.Data;
using WordpressClient.Services.Interfaces;

namespace WordpressClient.Services
{
    public class GoodsService : MetaValuesServiceBase, IGoodsService
    {
        public GoodsService(AdminDbContext context) : base(context)
        {
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

        public async Task AddWithMeta(WpPosts post, string articul, string charasteristics, string description, string discount, string price)
        {
            await Add(post);

            await AddMeta(post.Id, "articul", articul);
            await AddMeta(post.Id, "charasteristics", charasteristics);
            await AddMeta(post.Id, "description", description);
            await AddMeta(post.Id, "discount", discount);
            await AddMeta(post.Id, "price", price);
        }
    }
}
