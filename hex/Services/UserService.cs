using hex.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hex.Services
{
    public class UserService
    {
        public static List<User> Users() 
        {
            using (var db = new Context())
            {
                var users = db.Users.ToList();
                return users;
            }
        }
        public static User Add(User user)
        {
            using (var db = new Context())
            {
                db.Users.Add(user);
                db.SaveChanges();
                return user;
            }
        }

        public static User GetUser(Guid id)
        {
            using (var db = new Context())
            {
                var user = db.Users.Find(id);
                if (user == null)
                    throw new ArgumentException();
                else
                    return user;
            }
        }
        public static User GetUser(String username)
        {
            using (var db = new Context())
            {
                var users = db.Users.Where(x => x.UserName.Equals(username)).ToList();
                if (users.Count == 0)
                {
                    return null;
                }
                else return users.First();
            }
        }

        public static int GetVisitCount(String username)
        {
            using (var db = new Context())
            {
                var users = db.Users.Where(x => x.UserName.Equals(username)).ToList();
                if (users.Count == 0)
                {
                    throw new ArgumentException();
                }
                else return users.First().VisitCount;
            }
        }

        public static bool IsUserAuthentic(string username , string password)
        {
            using (var db = new Context())
            {
                var users = db.Users.Where((x => x.UserName.Equals(username) && x.Password.Equals(password))).ToList();
                if (users.Count == 0)
                {
                    return false;
                }
                else return true;
            }
        }
        public static void IncreaseVisitCount(String username)
        {        
            using (var db = new Context())
            {
                var users = db.Users.Where(x => x.UserName.Equals(username)).ToList();
                if (users.Count == 0)
                {
                    throw new ArgumentException();
                }
                else
                {
                    users.First().VisitCount++;
                }
                db.SaveChanges();
            }
        }

        public static int GetFollowersCount(String username)
        { 
            using(var db = new Context())
            {
                var users = db.Users.Where(x=>x.UserName.Equals(username)).ToList();
                if(users.Count == 0)
                    throw new ArgumentException();
                return users.First().Followers.Count;
            }
        }

        public static int GetOwnedBlogCount(String username)
        { 
            using(var db = new Context())
            {
                var users = db.Users.Where(x=>x.UserName.Equals(username)).ToList();
                if(users.Count == 0)
                    throw new ArgumentException();
                return users.First().OwnedBlogs.Count;
            }
        }

        public static int GetFollowingCount(String username)
        { 
            using(var db = new Context())
            {
                var users = db.Users.Where(x=>x.UserName.Equals(username)).ToList();
                if(users.Count == 0)
                    throw new ArgumentException();
                return users.First().Following.Count;
            }
        }
        public static int GetSubscriedBlogCount(String username)
        {
            using (var db = new Context())
            {
                var users = db.Users.Where(x => x.UserName.Equals(username)).ToList();
                if (users.Count == 0)
                    throw new ArgumentException();
                var user = users[0];
                return user.SubscribedBlogs.Count;
            }
        }
        public static IEnumerable<Blog> GetOwnedBlogs(string username)
        {
            using (var db = new Context())
            {
                var users = db.Users.Where(x => x.UserName.Equals(username)).ToList();
                if (users.Count == 0)
                    throw new ArgumentException();
                var user = users[0];
                return user.OwnedBlogs;
            }
        }

    }    
}