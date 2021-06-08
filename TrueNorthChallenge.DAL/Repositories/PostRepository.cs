using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueNorthChallenge.Contracts.Repositories;
using TrueNorthChallenge.DAL.Repositories.Generic;
using TrueNorthChallenge.DBEntities;

namespace TrueNorthChallenge.DAL.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(TrueNorthContext context) : base(context) { }
    }
}
