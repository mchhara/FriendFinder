using System.Security.Claims;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PostsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PostsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet(Name = "GetPosts")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPosts()
        {
            var posts = await _unitOfWork.PostRepository.GetAllPosts();

            if(posts == null) return NoContent();

            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
            
            return Ok(postsDto);
        }

        [HttpPost]
        public async Task<ActionResult<PostDto>> CreatePost([FromBody] CreatePostDto postDto)
        {
            if(postDto == null)
            {
                return BadRequest();
            }

            var userId = User.GetUserId();

            var post = new Post 
            {
                Content = postDto.Content,
                UserId = userId,
                UserName = User.GetUsername()
            };

             _unitOfWork.PostRepository.CreatePost(post);

            if(await _unitOfWork.Complete())
            {
                return CreatedAtRoute("GetPosts", new { id = post.Id },
                     _mapper.Map<PostDto>(post));
            }

            return BadRequest("Problem adding post");
        }
    }
}