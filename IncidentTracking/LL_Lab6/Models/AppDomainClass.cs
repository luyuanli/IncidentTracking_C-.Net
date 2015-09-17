using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LL_Lab6.Models
{
    public class Incident
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public RTO RTO { get; set; }

        [Required]
        public List<Team> Teams { get; set; }

        public Incident()
        {
            this.Teams = new List<Team>();
        }
    }

    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select available time")]
        public Availability Availability { get; set; }

        public List<Incident> Incidents { get; set; }

        public Team()
        {
            this.Incidents = new List<Incident>();
        }
    }

    public enum Availability
    {
        [Display(Name = "On Call")]
        ONCALL = 1,

        [Display(Name= "Business Time")]
        BUSINESS_TIME = 2
    }

    public enum RTO
    {
        [Display(Name = "1 day -- Emergency")]
        EMERGENCY = 1,

        [Display(Name = "3 days -- Important")]
        IMPORTANT = 2,

        [Display(Name = "14 days -- Regular")]
        REGULAR = 3,
    }
}