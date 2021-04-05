namespace Customer.Api.Infrastructure.ResponseModels
{
    public class OkResponse : ApiResponse
    {
        public object Data { get; set; }

        public OkResponse(object data) : base(200)
        {
            Data = data;
        }
    }
}