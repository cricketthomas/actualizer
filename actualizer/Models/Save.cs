using System;
using System.Collections.Generic;
using actualizer.Models;

namespace actualizer.Models
{


    public class SaveComment
    {
        [Key]
        public string VideoId { get; set; }
        public string UserId { get; set; }

        public List<Comments> Comments { get; set; }
    }


}
