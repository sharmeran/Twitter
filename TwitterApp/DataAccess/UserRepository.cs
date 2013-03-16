using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using TwitterApp.Constants;
using TwitterApp.Models;

namespace TwitterApp.DataAccess
{
    public class UserRepository
    {
        public void Insert(TwitterApp.Models.User entity, ActionState actionState)
        {
            MongoServer server = null;
            MongoDatabase database = null;
            try
            {
                server = MongoServer.Create(ConfigurationManager.AppSettings[CommonConstants.ConnictionString]);
                database = server.GetDatabase(CommonConstants.DatabaseName);
                var dataCollection = database.CollectionExists(UserConstants.UserCollection);
                if (Convert.ToBoolean(dataCollection) == false)
                {
                    database.CreateCollection(UserConstants.UserCollection);
                }
                else
                {
                    MongoCollection<BsonDocument> user = database.GetCollection<BsonDocument>(UserConstants.UserCollection);
                    BsonDocument userEntity = new BsonDocument {
                { UserConstants.Name, entity.Name },
                { UserConstants.Username, entity.Username },
                { UserConstants.Password, entity.Password },
                { UserConstants.CreatedDate, entity.CreatedDate }
                };
                    user.Insert(userEntity);
                    actionState.SetSuccess();

                }
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStateEnum.Exception, ex.Message);
            }
            finally
            {
                server.Disconnect();
                server = null;
                database = null;

            }
        }

        public void Update(TwitterApp.Models.User entity, ActionState actionState)
        {
             MongoServer server = null;
            MongoDatabase database = null;
            try
            {
                server = MongoServer.Create(ConfigurationManager.AppSettings[CommonConstants.ConnictionString]);
                database = server.GetDatabase(CommonConstants.DatabaseName);
                MongoCollection<BsonDocument> user = database.GetCollection<BsonDocument>(UserConstants.UserCollection);
                var userEntity = user.FindOneById(ObjectId.Parse(entity.ID));
                userEntity[UserConstants.Name] = entity.Name;
                userEntity[UserConstants.Password] = entity.Password;
                user.Save(userEntity);
                actionState.SetSuccess();

            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStateEnum.Exception, ex.Message);
            }
            finally
            {
                server.Disconnect();
                server = null;
                database = null;
            }
        }

        public void Delete(TwitterApp.Models.User entity, ActionState actionState)
        {
             MongoServer server = null;
            MongoDatabase database = null;
            try
            {
                server = MongoServer.Create(ConfigurationManager.AppSettings[CommonConstants.ConnictionString]);
                database = server.GetDatabase(CommonConstants.DatabaseName);
                MongoCollection<BsonDocument> user = database.GetCollection<BsonDocument>(UserConstants.UserCollection);
                user.Remove(new QueryDocument(UserConstants.ID, ObjectId.Parse(entity.ID)));
                actionState.SetSuccess();
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStateEnum.Exception, ex.Message);
            }
            finally
            {
                server.Disconnect();
                server = null;
                database = null;
            }
        }

        public List<TwitterApp.Models.User> FindAll(ActionState actionState)
        {
            List<TwitterApp.Models.User> userList;
            MongoServer server = null;
            MongoDatabase database = null;
            TwitterApp.Models.User entity;


            userList = new List<User>();
            entity = null;
            try
            {
                server = MongoServer.Create(ConfigurationManager.AppSettings[CommonConstants.ConnictionString]);
                database = server.GetDatabase(CommonConstants.DatabaseName);
                MongoCollection<BsonDocument> user = database.GetCollection<BsonDocument>(UserConstants.UserCollection);
                foreach (BsonDocument userEntity in user.FindAll())
                {
                    entity = new User();
                    entity.ID = userEntity[UserConstants.ID].ToString();
                    entity.Name = userEntity[UserConstants.Name].AsString;
                    entity.Password = userEntity[UserConstants.Password].AsString;
                    entity.CreatedDate = userEntity[UserConstants.CreatedDate].AsDateTime;
                    entity.Username = userEntity[UserConstants.Username].AsString;
                    userList.Add(entity);
                }
                actionState.SetSuccess();
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStateEnum.Exception, ex.Message);
            }
            finally
            {
                server.Disconnect();
                server = null;
                database = null;
            }
            return userList;
        }

       

        public bool IsExist(TwitterApp.Models.User entity, ActionState actionState)
        {
            MongoServer server = null;
            MongoDatabase database = null;
            bool isExist = false;
            try
            {
                server = MongoServer.Create(ConfigurationManager.AppSettings[CommonConstants.ConnictionString]);
                database = server.GetDatabase(CommonConstants.DatabaseName);
                MongoCollection<BsonDocument> user = database.GetCollection<BsonDocument>(UserConstants.UserCollection);
                MongoCursor<BsonDocument> userEntity = user.Find(new QueryDocument(UserConstants.Username, entity.Username));
                if (userEntity.Count() == 0)
                {
                    isExist = false;
                }
                else
                {
                    isExist = true;
                }
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStateEnum.Exception, ex.Message);
            }
            finally
            {
                server.Disconnect();
                server = null;
                database = null;
            }
            return isExist;
        }

    }
}