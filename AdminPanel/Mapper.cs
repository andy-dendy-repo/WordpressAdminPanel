using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminPanel
{
    public static class Mapper
    {
        private static bool _isInitialized = false;

        private static IMapper _mapper;

        public static R Map<R,T>(T value)
        {
            if(_isInitialized)
            {
                return _mapper.Map<R>(value);
            }
            else
            {
                _isInitialized = true;
                Initialize();
                return Map<R,T>(value);
            }
        }
        public static void Initialize()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ModelMappingProfile());
            });

            _mapper = mappingConfig.CreateMapper();
        }
    }
}
