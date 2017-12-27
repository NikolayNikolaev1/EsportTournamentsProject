namespace EsportTournaments.Data.Models
{
    public class PlayerTeam
    {
        public string PlayerId { get; set; }

        public User Player { get; set; }

        public int TeamId { get; set; }

        public Team Team { get; set; }
    }
}
