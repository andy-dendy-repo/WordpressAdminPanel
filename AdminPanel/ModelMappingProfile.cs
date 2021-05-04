using AdminPanel.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordpressClient.Data;
using WordpressClient.Services.Interfaces;

namespace AdminPanel
{
    public class ModelMappingProfile : Profile
    {
        public ModelMappingProfile()
        {
            CreateMap<WpPosts, Product>();
            CreateMap<Product, WpPosts>();

            CreateMap<WpTerms, Category>();
            CreateMap<Category, WpTerms>();

            CreateMap<WpPosts, Order>();
            CreateMap<Order, WpPosts>();
        }
    }
}
