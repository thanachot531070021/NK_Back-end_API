//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NK_Back_end_API.Entitiy
{
    using System;
    using System.Collections.Generic;
    
    public partial class AccessTokens
    {
        public int id { get; set; }
        public string token { get; set; }
        public System.DateTime exprise { get; set; }
        public int memberID { get; set; }
    
        public virtual Member Member { get; set; }
    }
}
