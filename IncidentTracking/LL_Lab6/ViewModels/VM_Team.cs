using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LL_Lab6.ViewModels
{
    public class TeamName
    {
        [Key]
        public int Id { get; set; }

        public String Name { get; set; }
    }
}