using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueNorthChallenge.Contracts.UOF;
using TrueNorthChallenge.Contracts.Repositories;
using TrueNorthChallenge.DAL;

namespace TrueNorthChallenge.DAL.UOF
{
    public class UnitOfWork : IUnitOfWork
    {
        private TrueNorthContext _context;

        public UnitOfWork(TrueNorthContext context, IPostRepository postRepository, ITagRepository tagRepository)
        {
            _context = context;
            _postRepository = postRepository;
            _tagRepository = tagRepository;
        }

        #region GeneralMethods
        public void InitDBTransaction(System.Data.IsolationLevel il)
        {
            this._context.InitDBTransaction(il);
        }

        public void CommitDBTransaction()
        {
            this._context.CommitDBTransaction();
        }

        public void RollbackDBTransaction()
        {
            this._context.RollbackDBTransaction();
        }

        public bool IsInTransaction
        {
            get { return this._context.IsInTransaction; }
        }

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        public void Commit()
        {
            this._context.Commit();
        }

        public void Dispose()
        {
            this._context.Dispose();
        }
        #endregion GeneralMethods

        ~UnitOfWork()
        {
            Dispose();
        }

        #region Repositories

        private IPostRepository _postRepository;
        public IPostRepository PostRepository
        {
            get
            {
                return this._postRepository;
            }
        }

        private ITagRepository _tagRepository;
        public ITagRepository TagRepository
        {
            get
            {
                return this._tagRepository;
            }
        }

        private IPost_TagsRepository _post_TagsRepository;
        public IPost_TagsRepository Post_TagsRepository
        {
            get
            {
                return this._post_TagsRepository;
            }
        }
        #endregion Repositories
    }
}
