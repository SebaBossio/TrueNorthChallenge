using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueNorthChallenge.Common.DTO;
using TrueNorthChallenge.Common.DTO.Generic;
using TrueNorthChallenge.Contracts.Managers;
using TrueNorthChallenge.DBEntities;
using TrueNorthChallenge.Managers;

namespace TrueNorthChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;
        private readonly IMapper _mapper;
        private readonly IPostManager _postManager;

        public PostController(ILogger<PostController> logger, IMapper mapper, IPostManager postManager)
        {
            _logger = logger;
            _mapper = mapper;
            _postManager = postManager;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ResponseDTO<IEnumerable<PostListItemModel>>))]
        public IEnumerable<PostListItemModel> GetAll()
        {
            var entityList = _postManager.List();
            return _mapper.Map<List<PostListItemModel>>(entityList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ResponseDTO<PostDetailsModel>))]
        public PostDetailsModel Get(int id)
        {
            var post = _postManager.Get(id);
            return _mapper.Map<PostDetailsModel>(post);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ResponseDTO<Int32>))]
        public int Create([FromBody] PostDetailsModel post)
        {
            var entity = _mapper.Map<Post>(post);
            _postManager.Save(entity);

            return entity.Id;
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ResponseDTO<Object>))]
        public void Update([FromBody] PostDetailsModel post)
        {
            var entity = _mapper.Map<Post>(post);
            _postManager.Save(entity);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(ResponseDTO<Object>))]
        public void Delete(int id)
        {
            _postManager.Remove(id);
        }
    }
}
