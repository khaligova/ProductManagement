using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Persistence.Repositories.Users
{
    public interface IUserWriteRepository:IWriteRepository<User>
    {
    }
}
