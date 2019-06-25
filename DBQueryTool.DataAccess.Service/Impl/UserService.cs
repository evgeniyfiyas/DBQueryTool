using System.Collections.Generic;
using System.Linq;
using DBQueryTool.DataAccess.Models;
using DBQueryTool.DataAccess.Service.Interfaces;
using DBQueryTool.Migrations;

namespace DBQueryTool.DataAccess.Service.Impl
{
    public class UserService : IUserService
    {
        public User Add(User entity)
        {
            using (var dbContext = new DBQueryToolDbContext())
            {
                dbContext.Users.Add(entity);
                dbContext.SaveChanges();
                return entity;
            }
        }

        public List<User> GetAll()
        {
            using (var dbContext = new DBQueryToolDbContext())
            {
                return dbContext.Users.ToList();
            }
        }

        public User Update(User entity)
        {
            using (var dbContext = new DBQueryToolDbContext())
            {
                var found = dbContext.Users.Find(entity);
                if (found == null) return null;

                dbContext.Users.Add(entity);
                dbContext.SaveChanges();
                return entity;
            }
        }

        public void Remove(User entity)
        {
            using (var dbContext = new DBQueryToolDbContext())
            {
                dbContext.Users.Remove(entity);
                dbContext.SaveChanges();
            }
        }
    }
}