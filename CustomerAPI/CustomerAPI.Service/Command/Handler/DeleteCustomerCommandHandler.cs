using AutoMapper;
using CustomerAPI.Data;
using CustomerAPI.Data.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerAPI.Service.Command.Handler
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Unit>
    {
        private IRepository<Customer> _customerRepository;

        public DeleteCustomerCommandHandler(IRepository<Customer> customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var entityExist = _customerRepository.GetAll().Any(x => x.Id == request.Id);
            if (!entityExist)
                throw new KeyNotFoundException("Id not Found");
            try
            {
                await _customerRepository.DeleteAsync(request.Id);
                return await Task.FromResult(Unit.Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
