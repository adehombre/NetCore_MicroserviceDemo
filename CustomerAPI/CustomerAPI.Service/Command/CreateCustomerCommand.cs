using CustomerAPI.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerAPI.Service.Command
{
    public class CreateCustomerCommand : IRequest<Customer>
    {
        public Customer Customer { get; set; }
    }
}
