//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Repository.MySql
{
    using System;
    using System.Collections.Generic;
    
    public partial class suppliers
    {
        public suppliers()
        {
            this.suppliers_locations = new HashSet<suppliers_locations>();
        }
    
        public string id { get; set; }
        public string supplier_name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string image_url { get; set; }
        public string supplier_marker_color { get; set; }
        public System.DateTime date_created { get; set; }
        public System.DateTime date_updated { get; set; }
    
        public virtual ICollection<suppliers_locations> suppliers_locations { get; set; }
    }
}
