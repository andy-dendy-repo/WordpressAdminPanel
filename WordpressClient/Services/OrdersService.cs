using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordpressClient.Data;
using WordpressClient.Services.Interfaces;

namespace WordpressClient.Services
{
    public class OrdersService : MetaValuesServiceBase, IOrdersService
    {
        public OrdersService(AdminDbContext context) : base(context)
        {
        }

        public override async Task<List<WpPosts>> GetAllAsync()
        {
            return await _context.WpPosts.Where(x => x.PostType == "order").ToListAsync();
        }
    }
}
