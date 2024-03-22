namespace FeedHandlingMicroservice.Models;


public class PostDto
    {
        public int UserId { get; set; }
        public string Content { get; set; }
       
    }

public class PostHashtagDto
{
    public int HashtagId { get; set; }
}