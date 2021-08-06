using System;

namespace TableStorage.Audit.BLL.Responses
{
    public abstract class BaseFieldsResponse
    {
        public DateTime CreatedDate { get; set; }

        public DateTime LastModified { get; set; }
    }
}