using System;
using System.Collections.Generic;
using Z.EntityFramework.Plus;

namespace TableStorage.Audit.BLL.Responses
{
    public class AuditEntryResponse
    {
        public Guid AuditEntryID { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string EntityTypeName { get; set; }

        public AuditEntryState State { get; set; }

        public AuditEntryPropertyResponse[] Properties { get; set; }
    }
}