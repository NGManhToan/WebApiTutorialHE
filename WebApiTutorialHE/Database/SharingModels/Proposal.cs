using System;
using System.Collections.Generic;

namespace WebApiTutorialHE.Database.SharingModels
{
    public partial class Proposal
    {
        public int ProposalId { get; set; }
        public int? UserId { get; set; }
        public DateTime ProposalDate { get; set; }
        public string ContentProposal { get; set; } = null!;
        public string? ImageProposal { get; set; }
        public string? SubjectProposal { get; set; }

        public virtual User? User { get; set; }
    }
}
