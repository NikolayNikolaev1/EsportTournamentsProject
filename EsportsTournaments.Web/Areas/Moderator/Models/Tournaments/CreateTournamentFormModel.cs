namespace EsportsTournaments.Web.Areas.Moderator.Models.Tournaments
{
    using Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants;
    using static Common.WebConstants;

    public class CreateTournamentFormModel : IValidatableObject
    {
        [MaxLength(TournamentNameMaxLength,
            ErrorMessage = ErrorMessages.InvalidPropertyMaxLength)]
        [MinLength(TournamentNameMinLength,
            ErrorMessage = ErrorMessages.InvalidPropertyMinLength)]
        [Required]
        public string Name { get; set; }

        public PrizeType Prize { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Game")]
        [Required]
        public int GameId { get; set; }

        public IEnumerable<SelectListItem> Games { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.StartDate < DateTime.UtcNow)
            {
                yield return new ValidationResult("Start date should be in the future.");
            }
        }
    }
}
