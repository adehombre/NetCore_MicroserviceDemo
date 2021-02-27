using CustomerAPI.Data;
using CustomerAPI.Data.Models;
using CustomerAPI.Data.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using System;

namespace CustomerAPI.Service.Query
{
    public class GetCustomerByIdHandle : IRequestHandler<GetCustomerByIdQuery, CustomerModel>
    {
        private IRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;
        public GetCustomerByIdHandle(IRepository<Customer> customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;

        }

        public async Task<CustomerModel> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _customerRepository.GetByIdAsync(request.Id);
                return _mapper.Map<CustomerModel>(customer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
    }
}
