using AutoMapper;

namespace EsportsTournaments.Core.Mapping
{
    public interface IHaveCustomMapping
    {
        void ConfigureMapping(Profile mapper);
    }
}
