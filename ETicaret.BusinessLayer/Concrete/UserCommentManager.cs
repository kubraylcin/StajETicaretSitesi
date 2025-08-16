using ETicaret.BusinessLayer.Abstract;
using ETicaret.DataAccessLayer.Abstract;
using ETicaretEntityLayer.Entities;
using System;
using System.Collections.Generic;

namespace ETicaret.BusinessLayer.Concrete
{
    public class UserCommentManager : IUserCommentService
    {
        private readonly IUserCommentDal _userCommentDal;

        public UserCommentManager(IUserCommentDal userCommentDal)
        {
            _userCommentDal = userCommentDal;
        }

        public void TAdd(UserComment entity)
        {
            _userCommentDal.Add(entity);
        }

        public void TDelete(UserComment entity)
        {
            _userCommentDal.Delete(entity);
        }

        public UserComment TGetById(int id)
        {
            return _userCommentDal.GetById(id);
        }

        public List<UserComment> TGetListAll()
        {
            return _userCommentDal.GetListAll();
        }

        public void TUpdate(UserComment entity)
        {
            _userCommentDal.Update(entity);
        }
    }
}
