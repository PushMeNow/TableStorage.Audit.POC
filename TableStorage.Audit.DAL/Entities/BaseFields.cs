using System;

namespace TableStorage.Audit.DAL.Entities
{
    public abstract class BaseFields
    {
        public DateTime CreatedDate { get; set; }

        public DateTime LastModified { get; set; }
    }
}