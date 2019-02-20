using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Poster.Models.DTO
{
    public class CommentRepository
    {
        ChatterEntities5 context = new ChatterEntities5();

        public IQueryable<Commend> GetAll()
        {
            return context.Commends.OrderBy(x => x.CommentDate);
        }

        public commentViewModel AddComment(commentDTO comment)
        {
            var _comment = new Commend()
            {
                ParentId = comment.parentId,
                CommandText = comment.commentText,
                Username = comment.username,
                
        };

            context.Commends.Add(_comment);
            context.SaveChanges();
            return context.Commends.Where(x => x.CommentID == _comment.CommentID)
                    .Select(x => new commentViewModel
                    {
                        commentID = x.CommentID,
                        commentText = x.CommandText,
                        
                        
                        
                        username = x.Username

                    }).FirstOrDefault();
        }

        
    }
}
    
