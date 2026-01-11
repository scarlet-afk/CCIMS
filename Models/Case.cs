using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CCIMS.Models;


namespace CCIMS.Models
{
    public class Case
    {
        [Key]
        public int CaseID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Category { get; set; }

        public string Location { get; set; }

        public DateTime DateField { get; set; }

        public CaseStatus Status { get; set; } = CaseStatus.Open;

        public string Description { get; set; }
        public ICollection<InvestigationLog> InvestigationLogs { get; set; }

    }
}