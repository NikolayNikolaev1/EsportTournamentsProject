namespace EsportsTournaments.Test.Fixtures
{
    using AutoMapper;
    using Core.Mapping;

    public class MapperFixture
    {
        public MapperFixture()
        {
            this.InitializeMapper();
        }

        public IMapper Mapper { get; set; }

        private void InitializeMapper()
        {
            this.Mapper = new MapperConfiguration(
                cfg => cfg.AddProfile<AutoMapperProfile>())
                .CreateMapper();
        }
    }
}
