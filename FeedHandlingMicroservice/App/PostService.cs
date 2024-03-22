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
    
    
    
    public void RebuildDB()
    {
        _postRepo.RebuildDB();
    }
}