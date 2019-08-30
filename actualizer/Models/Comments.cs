using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Linq;
using System.Collections.Generic;
namespace actualizer.Models
{


    public class AuthorChannelId
    {
        public string value { get; set; }
    }

    public class Snippet2
    {
        public string authorDisplayName { get; set; }
        public string authorProfileImageUrl { get; set; }
        public string authorChannelUrl { get; set; }
        public AuthorChannelId authorChannelId { get; set; }
        public string videoId { get; set; }
        public string textDisplay { get; set; }
        public string textOriginal { get; set; }
        public bool canRate { get; set; }
        public string viewerRating { get; set; }
        public int likeCount { get; set; }
        public DateTime publishedAt { get; set; }
        public DateTime updatedAt { get; set; }
    }

    public class TopLevelComment
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string id { get; set; }
        public Snippet2 snippet { get; set; }
    }

    public class Snippet
    {
        public string videoId { get; set; }
        public TopLevelComment topLevelComment { get; set; }
        public bool canReply { get; set; }
        public int totalReplyCount { get; set; }
        public bool isPublic { get; set; }
    }

    public class Item
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string id { get; set; }
        public Snippet snippet { get; set; }
    }

    public class RootObjectj
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public PageInfo pageInfo { get; set; }
        public List<Item> items { get; set; }
    }





}
