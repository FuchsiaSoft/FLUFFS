//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class ShrinkJob
    {
        public ShrinkJob()
        {
            this.TrackedFiles = new HashSet<TrackedFile>();
            this.ReductionLogs = new HashSet<ReductionLog>();
        }
    
        public int Id { get; set; }
        public string Alias { get; set; }
        public SearchStatus Status { get; set; }
        public string ShrinkJpegs { get; set; }
        public int JpegTarget { get; set; }
        public string ShrinkPdfs { get; set; }
        public PdfVersion PdfTarget { get; set; }
        public Nullable<int> PdfJpegTarget { get; set; }
        public bool ShrinkWord { get; set; }
        public string UpgradeWord { get; set; }
        public int WordJpegTarget { get; set; }
    
        public virtual ICollection<TrackedFile> TrackedFiles { get; set; }
        public virtual ICollection<ReductionLog> ReductionLogs { get; set; }
        public virtual User User { get; set; }
    }
}
