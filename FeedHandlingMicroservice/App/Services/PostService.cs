using AutoMapper;
using FeedHandlingMicroservice.DataAccess;
using FeedHandlingMicroservice.Models;
using FeedHandlingMicroservice.RabbitMq.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FeedHandlingMicroservice.App;

public class PostService : IPostService
{
    private readonly IMapper _mapper;
    private readonly IPostRepo _postRepo;
    private readonly IRabbitMqSender _rabbitMqSender;
    public PostService(IMapper mapper, IPostRepo postRepo, IRabbitMqSender rabbitMqSender)
    {
        _mapper = mapper;
        _postRepo = postRepo;
        _rabbitMqSender = rabbitMqSender;
    }

    
    public async Task<Post> CreatePost(Post post)

    {
        if (post == null)
        {
            throw new ArgumentNullException(nameof(post), "Provided post is null");
        }
        try
        {
            post.CreationDate = DateTime.Now;
            
            var createdPost = await _postRepo.CreatePost(post);
            
            _rabbitMqSender.SendUserId(post.UserId);

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