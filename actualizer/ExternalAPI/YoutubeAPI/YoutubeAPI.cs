﻿using System;
using System.Threading.Tasks;
using System.Net.Http;
using actualizer.Models;
using System.Web;
using System.Text;
using System.Net.Http.Headers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Okta.Sdk;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Text.Json;

namespace actualizer.ExternalAPI.YoutubeAPI {
    public class YoutubeAPI {
        private static readonly HttpClient client = new HttpClient();

        public YoutubeAPI() {
        }


        public static async Task<List<ReturnJson>> GetCommentsNextPageAsync(string video_id, string search, int lastNumOfComments, string youtube_key, string NextPageToken = null) {
            //void means returns nothing
            string u = "https://www.googleapis.com/youtube/v3/commentThreads?part=snippet&maxResults=100";
            string url = $"{u}&pageToken={NextPageToken}&searchTerms={search}&textFormat=plainText&videoId={video_id}&key={youtube_key}";

            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            RootObject rootobject = JsonSerializer.Deserialize<RootObject>(content);
            List<Comments> commentRows = new List<Comments> { };
            List<ReturnJson> ReturnObject = new List<ReturnJson> { };

            int index = lastNumOfComments;
            foreach (var i in rootobject.items) {
                commentRows.Add(new Comments {
                    id = index,
                    text = i.snippet.topLevelComment.snippet.textDisplay,
                    language = "en",
                    publishedAt = i.snippet.topLevelComment.snippet.publishedAt,
                    likeCount = i.snippet.topLevelComment.snippet.likeCount,
                    commentId = i.snippet.topLevelComment.id
                });
                index++;
            }
            string nextPage = rootobject.nextPageToken;
            string json = JsonSerializer.Serialize(new {
                search,
                //url,
                video_id,
                count = commentRows.Count,
                comments = commentRows,
                nextPage
            });

            ReturnObject.Add(new ReturnJson { search = search, url = url, video_id = video_id, comments = commentRows.ToArray(), count = commentRows.Count, nextPage = nextPage });

            return ReturnObject;

        }


        public static async Task<ReturnJson> SearchComments(string video_id, string search, string lang, int count, string youtube_key) {
            //void means returns nothing
            //List<string> returnObject = new List<string> { };
            string u = "https://www.googleapis.com/youtube/v3/commentThreads?key=";
            string url = $"{u}{youtube_key}&textFormat=plaintext&part=snippet&videoId={video_id}&maxResults={count}&searchTerms={search}";

            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            RootObject rootobject = JsonSerializer.Deserialize<RootObject>(content);
            List<Comments> commentRows = new List<Comments> { };
            int index = 0;
            foreach (var i in rootobject.items) {
                commentRows.Add(new Comments {
                    id = index,
                    text = i.snippet.topLevelComment.snippet.textDisplay,
                    language = "en",
                    publishedAt = i.snippet.topLevelComment.snippet.publishedAt,
                    likeCount = i.snippet.topLevelComment.snippet.likeCount,
                    commentId = i.snippet.topLevelComment.id

                });
                index++;
            }
            //IEnumerable<string> obj = rootobject.items.Select(r => r.snippet.topLevelComment.snippet.textDisplay).Where(r => r == "dog");

            string nextPage = rootobject.nextPageToken;
            string json = JsonSerializer.Serialize(new {
                search,
                video_id,
                //url,
                count = commentRows.Count,
                comments = commentRows,
                nextPage,

            });
            return new ReturnJson { search = search, url = url, video_id = video_id, comments = commentRows.ToArray(), count = commentRows.Count, nextPage = nextPage };

        }
    }
}
