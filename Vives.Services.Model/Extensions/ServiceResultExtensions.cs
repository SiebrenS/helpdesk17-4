namespace Vives.Services.Model.Extensions
{
    public static class ServiceResultExtensions
    {
        public static ServiceResult<T> Required<T>(this ServiceResult<T> serviceResult, string propertyName)
        {
            serviceResult.Messages.Add(
                new ServiceMessage
                {
                    Code = "Required",
                    Message = $"{propertyName} is required",
                    Type = ServiceMessageType.Error
                });

            return serviceResult;
        }

        public static ServiceResult<T> NotFound<T>(this ServiceResult<T> serviceResult, string entityName)
        {
            serviceResult.Messages.Add(
                new ServiceMessage
                {
                    Code = "NotFound",
                    Message = $"Could not find {entityName}",
                    Type = ServiceMessageType.Error
                });

            return serviceResult;
        }

        public static ServiceResult NotFound(this ServiceResult serviceResult, string entityName)
        {
            serviceResult.Messages.Add(
                new ServiceMessage
                {
                    Code = "NotFound",
                    Message = $"Could not find {entityName}",
                    Type = ServiceMessageType.Error
                });

            return serviceResult;
        }

        public static ServiceResult<T> Unauthorized<T>(this ServiceResult<T> serviceResult)
        {
            serviceResult.Messages.Add(
                new ServiceMessage
                {
                    Code = "Unauthorized",
                    Message = "You are not authorized",
                    Type = ServiceMessageType.Error
                });

            return serviceResult;
        }
    }
}
