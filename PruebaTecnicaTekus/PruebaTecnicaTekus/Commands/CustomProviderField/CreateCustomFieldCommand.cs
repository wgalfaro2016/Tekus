using MediatR;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Response.CustomProviderFieldResponse;

namespace PruebaTecnicaTekus.Commands.CustomProviderField
{
    public class CreateCustomFieldCommand : IRequest<CustomProviderFieldResponse>
    {
        public int ProviderId { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string FieldType { get; set; }
    }

    public class CreateCustomFieldCommandHandler : IRequestHandler<CreateCustomFieldCommand, CustomProviderFieldResponse>
    {
        private readonly TekusContext _context;

        public CreateCustomFieldCommandHandler(TekusContext context) {
            _context = context;
        }

        public async Task<CustomProviderFieldResponse> Handle(CreateCustomFieldCommand request, CancellationToken cancellationToken)
        {
            var customProviderField = new Models.CustomProviderField  {
                ProviderId = request.ProviderId,
                FieldName = request.FieldName,
                FieldValue = request.FieldValue,
                FieldType = request.FieldType
            };

            _context.CustomProviderFields.Add(customProviderField);
            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result > 0) {
                return new CustomProviderFieldResponse {
                    IsSuccess = true,
                    Id = customProviderField.Id
                };
            }

            return new CustomProviderFieldResponse {
                IsSuccess = false,
                Id = null
            };
        }
    }
}