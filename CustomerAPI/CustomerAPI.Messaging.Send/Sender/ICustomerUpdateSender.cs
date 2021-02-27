using CustomerAPI.Data.Models;

namespace CustomerAPI.Messaging.Send.Sender
{
    public interface ICustomerUpdateSender
    {
        void SendCustomer(CustomerModel customer);
    }
}
