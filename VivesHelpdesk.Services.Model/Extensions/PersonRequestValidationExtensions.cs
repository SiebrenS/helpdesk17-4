using Vives.Services.Model;
using Vives.Services.Model.Extensions;
using VivesHelpdesk.Services.Model.Request;
using VivesHelpdesk.Services.Model.Result;

namespace VivesHelpdesk.Services.Model.Extensions
{
    public static class PersonRequestValidationExtensions
    {
        public static ServiceResult<PersonResult?> Validate(this PersonRequest request)
        {
            var serviceResult = new ServiceResult<PersonResult?>();

            if (string.IsNullOrWhiteSpace(request.FirstName))
            {
                serviceResult = serviceResult.Required(nameof(request.FirstName));
            }
            if (string.IsNullOrWhiteSpace(request.LastName))
            {
                serviceResult = serviceResult.Required(nameof(request.LastName));
            }

            return serviceResult;
        }
    }
}
