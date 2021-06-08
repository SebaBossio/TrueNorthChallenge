using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueNorthChallenge.Common.DTO;
using TrueNorthChallenge.Common.DTO.Generic;
using TrueNorthChallenge.Contracts.Managers;

namespace TrueNorthChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ILogger<TagController> _logger;
        private readonly ITagManager _tagManager;

        public TagController(ILogger<TagController> logger, ITagManager tagManager)
        {
            _logger = logger;
            _tagManager = tagManager;
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ResponseDTO<Object>))]
        public void Post([FromBody] PostTagModel pt)
        {
            _tagManager.TagPost(pt.PostId, pt.Tag);
        }

        [HttpDelete("{postId}/{tag}")]
        [ProducesResponseType(200, Type = typeof(ResponseDTO<Object>))]
        public void Delete(int postId, string tag)
        {
            _tagManager.UntagPost(postId, tag);
        }
    }
}
