using NK_Back_end_API.Entitiy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NK_Back_end_API.Models
{
    public class GetMemberModel
    {
        public GetMembers[] items { get; set; }
        public int totalItems { get; set; }
    }

    public class GetMembers
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string position { get; set; }
        public IRoleAccount role { get; set; }
        public System.DateTime updated { get; set; }
    }

    public class MemberFilterOptions {
        [Required]
        public int startPage { get; set; }
        [Required]
        public int limitPage { get; set; }
        public string searchType { get; set; }
        public string searchText { get; set; }

        //    searchText?:string;
        //searchType?:string;


    }
}