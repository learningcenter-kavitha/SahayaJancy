using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Poster.Models.DTO
{
    public class commentDTO
    {
        public int parentId { get; set; }
        public string commentText { get; set; }
        public string username { get; set; }
    }

    public class commentViewModel : commentDTO
    {
        internal object commentID;

        public int CommentID { get; set; }
        
        
    }
}
