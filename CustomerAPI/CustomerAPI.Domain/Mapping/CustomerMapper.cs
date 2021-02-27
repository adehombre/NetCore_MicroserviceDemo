using AutoMapper;
using CustomerAPI.Data;
using CustomerAPI.Data.Models;

namespace CustomerAPI.Domain.Mapping
{
    public class CustomerMapper : Profile
    {
        public CustomerMapper()
        {
            CreateMap<Customer, CustomerModel>();
            CreateMap<CustomerModel, Customer>().ForMember(customer => customer.Id, opt => opt.Ignore());
        }
    }
}
