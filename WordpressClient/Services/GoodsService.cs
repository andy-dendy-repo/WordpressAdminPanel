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

        private async Task AddCategoryToPost(ulong postId, ulong categoryId)
        {
            var tax = _context.WpTermTaxonomy.FirstOrDefault(x=>x.TermId==categoryId);

            WpTermRelationships relationship = new WpTermRelationships()
            {
                ObjectId = postId,
                TermOrder = 0,
                TermTaxonomyId = tax.TermTaxonomyId
            };

            await _context.WpTermRelationships.AddAsync(relationship);

            await _context.SaveChangesAsync();
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

        public async Task<IList<WpTerms>> GetCategoriesByPostId(ulong id)
        {
            var rels = _context.WpTermRelationships.Where(x=>x.ObjectId==id).Select(x=>x.TermTaxonomyId);

            var tax = _context.WpTermTaxonomy.Where(t => rels.Contains(t.TermTaxonomyId)).Select(x=>x.TermId);

            var terms = _context.WpTerms.Where(x=>tax.Contains(x.TermId));

            return await terms.ToListAsync();
        }

        public override async Task Add(WpPosts entity)
        {
            entity.PostType = "product";
            entity.MenuOrder = 0;
            entity.PostParent = 0;
            entity.PostContentFiltered = string.Empty;
            entity.ToPing = string.Empty;
            entity.Pinged = string.Empty;
            entity.PostExcerpt = string.Empty;

            await base.Add(entity);
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

        public async Task AddWithMeta(WpPosts post, string articul, string charasteristics, string description, string discount, string price, IList<ulong> categoryIds)
        {
            await AddWithMeta(
                post,
                articul,
                charasteristics,
                description,
                discount,
                price
                );

            foreach(var id in categoryIds)
            {
                await AddCategoryToPost(post.Id, id);
            }
        }
    }
}
