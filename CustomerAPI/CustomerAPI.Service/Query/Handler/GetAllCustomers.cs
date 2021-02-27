using CustomerAPI.Data;
using CustomerAPI.Data.Models;
using CustomerAPI.Data.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace CustomerAPI.Service.Query
{
    public class GetAllCustomers : IRequestHandler<GetAllCustomer, IEnumerable<CustomerModel>>
    {
        private IRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;
        public GetAllCustomers(IRepository<Customer> customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;

        }

        public Task<IEnumerable<CustomerModel>> Handle(GetAllCustomer request, CancellationToken cancellationToken)
        {
            var skipItemsCount = request.ItemByPage * (request.CurrentPage-1);
            var customer = _customerRepository.GetAll().Skip(skipItemsCount).Take(request.ItemByPage);
            return Task.FromResult(_mapper.Map<IEnumerable<CustomerModel>>(customer));
        }
    }
}
