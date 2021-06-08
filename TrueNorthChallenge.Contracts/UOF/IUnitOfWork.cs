using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueNorthChallenge.Contracts.Repositories;

namespace TrueNorthChallenge.Contracts.UOF
{
    public interface IUnitOfWork
    {
        void InitDBTransaction(System.Data.IsolationLevel il);
        void CommitDBTransaction();
        void RollbackDBTransaction();
        bool IsInTransaction { get; }
        void SaveChanges();
        void Commit();
        void Dispose();

        #region Repositories
        IPostRepository PostRepository { get; }
        ITagRepository TagRepository { get; }
        IPost_TagsRepository Post_TagsRepository { get; }
        #endregion Repositories
    }
}
