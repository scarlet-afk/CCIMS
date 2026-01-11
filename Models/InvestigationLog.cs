using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CCIMS.Models
{
    public class InvestigationLog
    {
        [Key]
        public int LogID { get; set; }

        [Required]
        public int CaseID { get; set; }
        [MaybeNull]
        public Case Case { get; set; }
        [AllowNull]
        public string ActionTaken { get; set; }
        [AllowNull]
        public string Notes { get; set; }
        [AllowNull]
        public string InvestigatorId { get; set; }
        [AllowNull]
        public string InvestigatorName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}