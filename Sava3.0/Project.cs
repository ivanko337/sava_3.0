//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sava3._0
{
    using System;
    using System.Collections.Generic;
    
    public partial class Project
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Project()
        {
            this.ProjectEmployees = new HashSet<ProjectEmployee>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int PlatformId { get; set; }
        public int TypeId { get; set; }
        public int CustomerId { get; set; }
        public string TaskDescription { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Platform Platform { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectEmployee> ProjectEmployees { get; set; }
        public virtual ProjectType ProjectType { get; set; }
    }
}
