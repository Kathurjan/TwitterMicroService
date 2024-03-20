namespace FeedHandlingMicroservice.Models;

public class Post
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Content { get; set; }
    public DateTime CreationDate { get; set; }
    public ICollection<PostHashtag> Hashtags { get; set; } = new List<PostHashtag>();
}
public class Hashtag
{
    public int Id { get; set; }
    public string Tag { get; set; } 
    public ICollection<PostHashtag> Posts { get; set; } = new List<PostHashtag>();
}

public class PostHashtag
{
    public int PostId { get; set; }
    public Post Post { get; set; } // Establishes link to Post
    
    public int HashtagId { get; set; }
    public Hashtag Hashtag { get; set; } // Establishes link to Hashtag
}
