using System;
using TrueNorthChallenge.Contracts.UOF;
using TrueNorthChallenge.Contracts.Managers;
using TrueNorthChallenge.Contracts.Repositories;
using System.Linq;
using System.Collections.Generic;
using TrueNorthChallenge.DBEntities;
using TrueNorthChallenge.Common.Exceptions;

namespace TrueNorthChallenge.Managers
{
    public class PostManager : IPostManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Save(Post post)
        {
            if (string.IsNullOrEmpty(post.Title))
            {
                throw new TrueNorthSecureException("Post title is empty");
            }

            if (string.IsNullOrEmpty(post.Content))
            {
                throw new TrueNorthSecureException("Post content is empty");
            }

            if (post.Id > 0)
            {
                var existingPost = _unitOfWork.PostRepository.FindBy(X => X.Id == post.Id).FirstOrDefault();
                if (existingPost == null)
                {
                    throw new TrueNorthSecureException(string.Format("Specified post (id={0}) does not exist", post.Id));
                }

                post.MTS = DateTime.UtcNow;
                _unitOfWork.PostRepository.Edit(post);
            }
            else
            {
                _unitOfWork.PostRepository.Add(post);
            }

            _unitOfWork.SaveChanges();
        }

        public Post Get(int id)
        {
            var post = _unitOfWork.PostRepository.FindByIncluding(x => x.Id == id, x => x.Posts_Tags, x => x.Posts_Tags.Select(y => y.Tag)).FirstOrDefault();
            if (post == null)
            {
                throw new ArgumentException(string.Format("Specified post (id={0}) does not exist", id));
            }

            return post;
        }

        public ICollection<Post> List()
        {
            return _unitOfWork.PostRepository.GetAll();
        }

        public void Remove(int id)
        {
            var post = _unitOfWork.PostRepository.FindBy(X => X.Id == id).FirstOrDefault();
            if (post == null)
            {
                throw new ArgumentException(string.Format("Specified post (id={0}) does not exist", id));
            }

            _unitOfWork.PostRepository.Delete(post);

            _unitOfWork.SaveChanges();
        }
    }
}
