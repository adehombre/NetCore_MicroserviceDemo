using CustomerAPI.Data;
using CustomerAPI.Data.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerAPI.Service.Command
{
    public class UpdateCustomerCommand : IRequest
    {
        public CustomerModel Customer { get; set; }
        public Guid Id { get; set; }
    }
}
