﻿namespace FeedHandlingMicroservice.Models;


public class PostDto
    {
        public int UserId { get; set; }
        public string Content { get; set; }
        public List<string> Hashtags { get; set; } = new List<string>();
    }

public class HashtagDto
{
    public string Tag { get; set; } 
}