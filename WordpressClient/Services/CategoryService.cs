using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordpressClient.Data;
using WordpressClient.Services.Interfaces;

namespace WordpressClient.Services
{
    public class CategoryService : ServiceBase<WpTerms>, ICategoryService
    {
        public CategoryService(AdminDbContext context): base(context)
        {
        }

        public override async Task Add(WpTerms entity)
        {
            await base.Add(entity);

            var tax = new WpTermTaxonomy()
            {
                Taxonomy = "category",
                TermId = entity.TermId,
                Description = "product category"
            };

            _context.WpTermTaxonomy.Add(tax);

            await _context.SaveChangesAsync();
        }
    }
}
