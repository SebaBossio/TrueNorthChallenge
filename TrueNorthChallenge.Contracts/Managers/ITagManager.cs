using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueNorthChallenge.Contracts.Managers
{
    public interface ITagManager
    {
        void TagPost(int postId, string tag);
        void UntagPost(int postId, string tag);
    }
}
