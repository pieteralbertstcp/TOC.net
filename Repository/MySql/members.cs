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
    
    public partial class members
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string cell { get; set; }
        public string id_number { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public System.DateTime date_created { get; set; }
        public System.DateTime date_updated { get; set; }
        public byte[] profile_image { get; set; }
        public System.DateTime started_woodworking_on { get; set; }
        public string address { get; set; }
        public Nullable<float> lng { get; set; }
        public Nullable<float> lat { get; set; }
    }
}
