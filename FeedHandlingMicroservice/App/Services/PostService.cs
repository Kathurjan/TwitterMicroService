using AutoMapper;
using FeedHandlingMicroservice.DataAccess;
using FeedHandlingMicroservice.Models;

using NetQ;


namespace FeedHandlingMicroservice.App;

public class PostService : IPostService
{
    private readonly IMapper _mapper;
    private readonly IPostRepo _postRepo;

    private readonly MessageClient _messageClient;

    public PostService(IMapper mapper, IPostRepo postRepo, MessageClient messageClient)
    {
        _mapper = mapper;
        _postRepo = postRepo;
        _messageClient = messageClient;
    }
 

    
    public async Task<Post> CreatePost(Post post)

    {
        if (post == null)
        {
            throw new ArgumentNullException(nameof(post), "Provided post is null");
        }
        try
        {
            NotificationDto notificationDto = new NotificationDto
            {
                UserId = post.UserId,
                Message = post.Content,
                Type = "Post"

            };
            post.CreationDate = DateTime.Now;
            
            var createdPost = await _postRepo.CreatePost(post);
            
            _messageClient.Publish(notificationDto, "notificationCreation");
          
            Console.WriteLine(notificationDto.Message);
            Console.WriteLine("Post created and notification sent");

            return createdPost;
        }
        catch (Exception e)
        {
            throw new Exception("CreatePost in Service went wrong: " + e);
        }
    }

    public async Task<Post> GetPostById(int id)
    {
        try
        {
            return await _postRepo.GetPostById(id);
        }
        catch (Exception e)
        {
            throw new Exception("GetPostById in Service went wrong: " + e);
        }
    }

    public async Task<List<Post>> GetAllPost()
    {
        try
        {
            return await _postRepo.GetAllPost();
        }
        catch (Exception e)
        {
            throw new Exception("GetAllPost in Service went wrong: " + e);
        }
    }

    public async Task<List<Post>> GetAllPostByUserId(int userId)
    {
        try
        {
            return await _postRepo.GetAllPostByUserId(userId);
        }
        catch (Exception e)
        {
            throw new Exception("GetAllPostByUserId in Service went wrong: " + e);
        }
    }

    public Task<Post> DeletePost(int id)
    {
        try
        {
           return _postRepo.DeletePost(id);
        }
        catch (Exception e)
        {
            throw new Exception("DeletePost in Service went wrong: " + e);
        }
    }

    public async Task<Post> UpdatePost(PostDto postDto, int postId)
    {
        try
        {
            var validationPost = await _postRepo.GetPostById(postId);
            if (postDto.UserId == validationPost.UserId)
            {
                var post = _mapper.Map<Post>(postDto);
                post.Id = postId;
                return await _postRepo.UpdatePost(post);
            }

            throw new Exception("Deletion not permitted" );
        }
        catch (Exception e)
        {
            throw new Exception("UpdatePost in Service went wrong: " + e);
        }
    }



    public void RebuildDB()
    {
        _postRepo.RebuildDB();
    }
}