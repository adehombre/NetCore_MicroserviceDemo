using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerAPI.Service.Command
{
    public class DeleteCustomerCommand: IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
