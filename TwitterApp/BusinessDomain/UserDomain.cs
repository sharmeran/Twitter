using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TwitterApp.Constants;
using TwitterApp.DataAccess;
using TwitterApp.Models;

namespace TwitterApp.BusinessDomain
{
    public class UserDomain
    {
        public ActionState Add(User entity)
        {

            ActionState actionStae = new ActionState();
            UserRepository userRepository = new UserRepository();
            if (userRepository.IsExist(entity, actionStae))
            {
                actionStae.SetFail(ActionStateEnum.AlreadyExist, CommonConstants.IsAlreadyExist); 
            }
            else
            {
                entity.CreatedDate = DateTime.Now;
                userRepository.Insert(entity, actionStae);
            }
            return actionStae;
        }

        public ActionState Update(User entity)
        {
            ActionState actionStae = new ActionState();
            UserRepository userRepository = new UserRepository();
            userRepository.Update(entity, actionStae);
            return actionStae;
        }

        public ActionState Delete(User entity)
        {
            ActionState actionStae = new ActionState();
            UserRepository userRepository = new UserRepository();
            userRepository.Delete(entity, actionStae);
            return actionStae;
        }

        public List<User> FindAll()
        {
            List<User> userList = new List<User>();
            UserRepository userRepository = new UserRepository();
            userList = userRepository.FindAll(new ActionState());
            return userList;
        }
    }
}