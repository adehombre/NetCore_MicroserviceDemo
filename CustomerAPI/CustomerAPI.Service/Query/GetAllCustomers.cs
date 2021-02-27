using CustomerAPI.Data.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace CustomerAPI.Service.Query
{
    public class GetAllCustomer : IRequest<IEnumerable<CustomerModel>>
    {
        public string SortBy{ get; set; }
        public int CurrentPage{ get; set; }
        public int ItemByPage{ get; set; }
    }
}