using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueNorthChallenge.DBEntities;

namespace TrueNorthChallenge.Contracts.Managers
{
    public interface IPostManager
    {
        void Save(Post post);
        Post Get(int id);
        ICollection<Post> List();
        void Remove(int id);
    }
}
