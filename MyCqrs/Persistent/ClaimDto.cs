using System;
using System.Collections.Generic;
using System.Text;

namespace MyCqrs.Persistent
{
    public class ClaimDto
    {
        public string ClaimNo { get; set; }
        public DateTime ClaimFiledDate { get; set; }
        public DateTime ClaimAmendDate { get; set; }
        public decimal ClaimAmount { get; set; }
        public string ClaimStatus { get; set; }
        public string ClaimReviewedBy { get; set; }

        public Guid ClaimId { get; set; }
    }
}
