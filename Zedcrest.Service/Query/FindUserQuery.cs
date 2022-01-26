using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zedcrest.Domain.Entities;

namespace Zedcrest.Service.Query
{
    public class FindUserQuery : IRequest<User>
    {
        public long UserId { get; set; }
    }
}
