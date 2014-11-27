using Exam.Data;
using Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Microsoft.AspNet.Identity;

namespace Exam.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UsersService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UsersService.svc or UsersService.svc.cs at the Solution Explorer and start debugging.
    public class UsersService : IUsersService
    {
        private IBullsAndCowsData data;

        public UsersService()
            : this(new BullsAndCowsEFData(new BullsAndCowsDbContext()))
        {
        }

        public UsersService(IBullsAndCowsData data)
        {
            this.data = data;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var allusers = this.data.Users.All()
                .OrderBy(u => u.UserName)
                .Take(10);

            return allusers;
        }

        public object GetById(string id)
        {
            var user = this.data.Users.Find(id);
            if (user == null)
            {
                throw new ArgumentException();
            }

            return new
            {
                Id = user.Id,
                Username = user.UserName,
                WonGames = user.WinsCount,
                LostGames = user.LossesCount,
            };
        }

        public IEnumerable<User> GetAllUsers(int page)
        {
            var result = this.data.Users.All()
                .OrderBy(u => u.UserName)
                .Skip(page * 10)
                .Take(10);

            return result;
        }
    }
}
