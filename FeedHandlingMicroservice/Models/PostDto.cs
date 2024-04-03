namespace FeedHandlingMicroservice.Models;


public class PostDto
    {
        public int? UserId { get; set; }
        public string Content { get; set; }
        public ICollection<PostHashtag>? Hashtags { get; set; } = new List<PostHashtag>();
    }

public class HashtagDto
{
    public string Tag { get; set; } 
}