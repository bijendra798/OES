//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OESApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserLogin
    {
        public int Loginid { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Nullable<int> RoleId { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public Nullable<int> LoginAttempt { get; set; }
    
        public virtual UserRole UserRole { get; set; }
    }
}
