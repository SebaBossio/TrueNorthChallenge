using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueNorthChallenge.Contracts.UOF;
using TrueNorthChallenge.Contracts.Managers;
using TrueNorthChallenge.Contracts.Repositories;
using TrueNorthChallenge.DBEntities;
using TrueNorthChallenge.Common.Exceptions;

namespace TrueNorthChallenge.Managers
{
    public class TagManager : ITagManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void TagPost(int postId, string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                throw new ArgumentException("Tag name is invalid");
            }

            var post = _unitOfWork.PostRepository.FindBy(X => X.Id == postId).FirstOrDefault();
            if (post == null)
            {
                throw new ArgumentException(string.Format("Specified post (id={0}) does not exist", postId));
            }

            var tagEntity = _unitOfWork.TagRepository.FindBy(t => t.Name == tag).FirstOrDefault();
            if (!post.Posts_Tags.Any(pt => pt.TagId == tagEntity.Id))
            {
                _unitOfWork.InitDBTransaction(System.Data.IsolationLevel.ReadUncommitted);

                try
                {
                    if (tagEntity == null)
                    {
                        _unitOfWork.TagRepository.Add(new Tag { Name = tag });
                        _unitOfWork.SaveChanges();
                    }

                    _unitOfWork.Post_TagsRepository.Add(new Posts_Tags
                    {
                        PostId = post.Id,
                        TagId = tagEntity.Id
                    });

                    _unitOfWork.SaveChanges();
                    _unitOfWork.CommitDBTransaction();
                }
                catch(Exception ex)
                {
                    _unitOfWork.RollbackDBTransaction();
                    throw;
                }
            }
        }

        public void UntagPost(int postId, string tag)
        {

            if (string.IsNullOrEmpty(tag))
            {
                throw new TrueNorthSecureException("Tag name is invalid");
            }

            var post = _unitOfWork.PostRepository.FindBy(X => X.Id == postId).FirstOrDefault();
            if (post == null)
            {
                throw new TrueNorthSecureException(string.Format("Specified post (id={0}) does not exist", postId));
            }

            var tagEntity = _unitOfWork.TagRepository.FindBy(t => t.Name == tag).FirstOrDefault();
            if (tagEntity == null)
            {
                throw new TrueNorthSecureException(string.Format("Specified tag (id={0}) does not exist", tag));
            }
            else if (!post.Posts_Tags.Any(pt => pt.TagId == tagEntity.Id))
            {
                throw new TrueNorthSecureException(string.Format("Specified tag (id={0}) isn't related to the specified post (id={1}).", tag, postId));
            }
            else
            {
                _unitOfWork.Post_TagsRepository.Delete(post.Posts_Tags.Single());
            }
        }
    }
}
