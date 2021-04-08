using AdminPanel.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WordpressClient.Data;

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
        }
    }
}
