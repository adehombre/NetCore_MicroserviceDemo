using AutoMapper;
using CustomerAPI.Data;
using CustomerAPI.Data.Models;
using CustomerAPI.Data.Repositories;
using CustomerAPI.Messaging.Send.Sender;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerAPI.Service.Command
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Unit>
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;
        private readonly ICustomerUpdateSender _customerUpdateSender;

        public UpdateCustomerCommandHandler(IRepository<Customer> customerRepository, IMapper mapper, ICustomerUpdateSender customerUpdateSender)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _customerUpdateSender = customerUpdateSender;
        }

        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = _mapper.Map<Customer>(request.Customer);
                var customerResult = await _customerRepository.UpdateAsync(customer, request.Id);
                _customerUpdateSender.SendCustomer(_mapper.Map<CustomerModel>(customerResult));
                return await Task.FromResult(Unit.Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
