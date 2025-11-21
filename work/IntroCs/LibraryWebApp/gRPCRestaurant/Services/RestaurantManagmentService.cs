using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using gRPCRestaurant;

namespace gRPCRestaurant.Services
{

    public class RestaurantManagmentService : RestoService.RestoServiceBase
    {
        private static readonly List<CustomerDetail> customerDetaiList = new List<CustomerDetail>();

        public RestaurantManagmentService()
        {
        }

        public override Task<CustomerDetailsResponse> AddCustomerDetail(CustomerDetailsRequest request, Grpc.Core.ServerCallContext context)
        {
            CustomerDetail customerDetail = new CustomerDetail
            {
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                TableNumber = request.TableNumber
            };

        customerDetaiList.Add(customerDetail);

        return Task.FromResult(
                new CustomerDetailsResponse
                {
                    Message = "Customer detail added successfully."
                }
            );
        }

        public override Task<CustomerDetailsResponse> GetCustomerDetails(Empty request, ServerCallContext context)
        {
            var customerDetail = customerDetaiList.FirstOrDefault(c => c.TableNumber == request.TableNumber);

            if (customerDetail != null)
            {
                return Task.FromResult(
                    new CustomerDetailsResponse
                    {
                        Message = customerDetail.Name,
                        Email = customerDetail.Email,
                        PhoneNumber = customerDetail.PhoneNumber,
                        TableNumber = customerDetail.TableNumber
                    }
                );
            }
            else
            {
                return Task.FromResult(
                    new GetCustomerDetailsResponse()
                );
            }
        }
    }
}
