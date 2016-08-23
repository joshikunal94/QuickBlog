using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hex.Entites
{
    public class File
    {
        public Guid FileId { get; set; }
        public string Path { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset LastAccesedOn { get; set; }
        public FileType Type { get; set; }
        public virtual User UserOwner { get; set; }
        public virtual Blog BlogOwner { get; set; }
        public virtual Post PostOwner { get; set; }
       
    }
    
}