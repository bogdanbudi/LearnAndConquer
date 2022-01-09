using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discount.Grpc.Protos;

namespace Cart.API.GrpcServices
{
    public class DisocuntGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;

        public DisocuntGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
        {
            _discountProtoServiceClient = discountProtoServiceClient ?? throw new ArgumentNullException(nameof(discountProtoServiceClient));
        }

        public async Task<CouponModel> GetDiscount(string courseName)
        {
            var discountRequest = new GetDiscountRequest { CourseName = courseName };
            return await _discountProtoServiceClient.GetDiscountAsync(discountRequest);
        }
    }
}
