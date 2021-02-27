using CustomerAPI.Data;
using CustomerAPI.Data.Models;
using CustomerAPI.Data.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerAPI.Service.Command
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private IRepository<Customer> _customerRepository;

        public CreateCustomerCommandHandler(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            return await _customerRepository.AddAsync(request.Customer);
        }

        
    }
}
