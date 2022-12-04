using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helper
{
    public class GameUrlResolver : IValueResolver<Games, GamesToReturnDto, string>
    {
        private readonly IConfiguration _config;
        public GameUrlResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(Games source, GamesToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }

            return null;
        }
    }
}
