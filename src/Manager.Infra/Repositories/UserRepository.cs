using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manager.Domain.Entities;
using Manager.Infra.Context;
using Manager.Infra.Interfaces;
using Manager.Infra.Repositiries;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infra.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ManagerContext _context;
        public UserRepository(ManagerContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetByEmail(string email)
        {
            var student = await _context.Users
                            .Where
                            (
                                x => x.Email.ToLower() == email.ToLower()
                            )
                            .AsNoTracking()
                            .ToListAsync();

            return student.FirstOrDefault();
        }

        public Task<List<User>> SearchByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> SearchByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}