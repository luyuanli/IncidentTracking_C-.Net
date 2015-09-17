using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using LL_Lab6.Models;
using System.Web.Mvc;

namespace LL_Lab6.ViewModels
{
    public class IncidentName
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class IncidentShort : IncidentName
    {
        [Required]
        public RTO RTO { get; set; }
    }

    public class IncidentForHttpGet
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public RTO RTO { get; set; }

        [Display(Name = "Teams Assigned")]
        public MultiSelectList TeamSelectList { get; set; }

        public void Clear()
        {
            Name = Description = string.Empty;
        }
    }

    public class IncidentForHttpPost
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Description { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select available time")]
        public RTO RTO { get; set; }

        [Required(ErrorMessage = "Incident should be assigned to at least one team.")]
        public virtual ICollection<int> TeamIds { get; set; }
    }
}