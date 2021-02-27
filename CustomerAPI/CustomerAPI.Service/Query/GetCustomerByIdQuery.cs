using CustomerAPI.Data.Models;
using MediatR;
using System;

namespace CustomerAPI.Service.Query
{
    public class GetCustomerByIdQuery : IRequest<CustomerModel>
    {
        public Guid Id { get; set; }
    }
}