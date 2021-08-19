﻿namespace EsportsTournaments.Services.Models.Users
{
    using AutoMapper;
    using Core.Mapping;
    using Data.Models;
    using System.Linq;

    public class UserProfileTeamsServiceModel : IMapFrom<Team>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string TeamImageUrl { get; set; }

        public int TournamentsWon { get; set; }

        public int PlayersNumber { get; set; }

        public string CaptainName { get; set; }

        public string Game { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Team, UserProfileTeamsServiceModel>()
                .ForMember(t => t.PlayersNumber, cfg => cfg.MapFrom(t => t.Players.ToList().Count))
                .ForMember(t => t.CaptainName, cfg => cfg.MapFrom(t => t.Captain.UserName))
                .ForMember(t => t.Game, cfg => cfg.MapFrom(t => t.Game.Name));
    }
}