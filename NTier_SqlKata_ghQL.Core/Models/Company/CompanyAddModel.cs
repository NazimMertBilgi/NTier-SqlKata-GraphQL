using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier_SqlKata_ghQL.Core.Models.Company
{
    public class CompanyAddModel
    {
        [Required(ErrorMessage = "Lütfen Name alanını doldurunuz.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Lütfen IsActive alanını doldurunuz.")]
        public bool IsActive { get; set; } 

    }
}
