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
    
    public partial class SearchJob
    {
        public SearchJob()
        {
            this.TrackedFiles = new HashSet<TrackedFile>();
        }
    
        public int Id { get; set; }
        public string Alias { get; set; }
        public SearchStatus Status { get; set; }
        public string RequestorComments { get; set; }
        public string AdminComments { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual ICollection<TrackedFile> TrackedFiles { get; set; }
    }
}