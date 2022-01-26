using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zedcrest.Domain.Entities;

namespace Zedcrest.Service.Query
{
    public class FindUserByEmailQuery : IRequest<User>
    {
        public string EmailAddress { get; set; }
        public string UniqueReference { get; set; }
    }
}
