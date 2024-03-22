using AutoMapper;
using FeedHandlingMicroservice.DataAccess;
using FeedHandlingMicroservice.Models;
using Microsoft.AspNetCore.Mvc;

namespace FeedHandlingMicroservice.App;

public class PostService : IPostService
{
    private readonly IMapper _mapper;
    private readonly IPostRepo _postRepo;
    public PostService(IMapper mapper, IPostRepo postRepo)
    {
        _mapper = mapper;
        _postRepo = postRepo;
    }


    public void CreatePost(PostDto postDto)
    {
        try
        {
            var post = _mapper.Map<Post>(postDto);
            post.CreationDate = DateTime.Now;
            _postRepo.CreatePost(post);
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

    public void DeletePost(int id)
    {
        try
        {
            _postRepo.DeletePost(id);
        }
        catch (Exception e)
        {
            throw new Exception("DeletePost in Service went wrong: " + e);
        }
    }

    public async Task<Post> UpdatePost(PostDto postDto)
    {
        try
        {
            var post = _mapper.Map<Post>(postDto);
            return await _postRepo.UpdatePost(post);
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