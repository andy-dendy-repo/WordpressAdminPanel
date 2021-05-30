using AdminPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WordpressClient.Data;
using WordpressClient.Services.Interfaces;

namespace AdminPanel
{
    public class ServicesForWindow
    {
        protected readonly IOrdersService _ordersService;
        protected readonly IGoodsService _goodsService;

        public IOrdersService OrdersService { get => _ordersService; }
        public IGoodsService GoodsService { get => _goodsService; }

        public ServicesForWindow(IOrdersService ordersService, IGoodsService goodsService)
        {
            _ordersService = ordersService;
            _goodsService = goodsService;
        }

        public async Task<IList<Order>> GetOrders()
        {
            var orders = Mapper.Map<IList<Order>, IList<WpPosts>>(await _ordersService.GetAllAsync());

            foreach (var order in orders)
            {
                order.OrderMeta = await GetOrderMeta(order.Id);
            }

            return orders;
        }

        public async Task<OrderMeta> GetOrderMeta(ulong id)
        {
            var list = await _ordersService.GetMetaByPostId(id);

            OrderMeta orderMeta = null;

            if (list.Count != 0)
            {
                orderMeta = new OrderMeta();
                orderMeta.Ids = list.FirstOrDefault(x => x.MetaKey == "ids")?.MetaValue;
                orderMeta.Name = list.FirstOrDefault(x => x.MetaKey == "name")?.MetaValue;
                orderMeta.SecondName = list.FirstOrDefault(x => x.MetaKey == "second_name")?.MetaValue;
                orderMeta.Phone = list.FirstOrDefault(x => x.MetaKey == "phone")?.MetaValue;
                orderMeta.Address = list.FirstOrDefault(x => x.MetaKey == "address")?.MetaValue;

                var allIds = orderMeta.Ids.Split(',').Where(x => !string.IsNullOrEmpty(x));
                var distinctIds = allIds.Distinct();

                foreach (var item in distinctIds)
                {
                    ulong goodId = ulong.Parse(item);
                    var good = _goodsService.GetByIdAsync(goodId);
                    if (good != null)
                    {
                        int count = allIds.Count(x => x == item);
                        orderMeta.Products.Add(new OrderedProduct(goodId, good.PostTitle, count));
                    }
                }
            }

            return orderMeta;
        }
    }
}
