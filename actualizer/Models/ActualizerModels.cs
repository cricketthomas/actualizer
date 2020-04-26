using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using actualizer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace actualizer.Models {
    public class ActualizerModels {
        public ActualizerModels() {
        }

        //public class SavedObjects {
        //    [Key]
        //    public string VideoId { get; set; }
        //    public string UserId { get; set; }
        //    public string Object { get; set; }
        //    public DateTime Date { get; set; }
        //    public string Source { get; set; }
        //    public List<Comments> Documents { get; set; }
        //    public int JSONLength { get; set; }
        //    public Metadata metadata { get; set; }

        //}


        //public class RemainingRequests {
        //    [Key]
        //    public string Resource { get; set; }
        //    public int Count { get; set; }
        //    public DateTime Date { get; set; }

        //}

        public class SearchResultsMetadata {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public int totalSearches { get; set; }
            public int totalCommentsSearched { get; set; }
            public int keywordsExtracted { get; set; }
            public int sentimentAPIRequests { get; set; }

        }



    }
}
