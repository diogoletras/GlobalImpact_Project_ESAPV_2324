﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GlobalImpact.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;

namespace GlobalImpact.Models
{
    public class RecyclingBin
    {
        [PersonalData]
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("RecyclingBinTypeId")]
        public virtual RecyclingBinType RecyclingBinType { get; set; }

        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }

        [Display(Name = "Tipo Ecoponto")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Capacidade Total")]
        [Required]
        public int Capacity { get; set; }

        [Display(Name = "Capacidade Atual")]
        [Required]
        public int CurrentCapacity { get; set; }

        [Display(Name = "Disponivel ?")]
        [Required]
        public bool Status { get; set; }

        [NotMapped]
        public List<RecyclingBinType>? RBTList { get; set; }

    }
}
