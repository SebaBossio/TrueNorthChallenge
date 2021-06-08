using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueNorthChallenge.Contracts.Repositories.Generic;
using TrueNorthChallenge.DBEntities;

namespace TrueNorthChallenge.Contracts.Repositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
    }
}
