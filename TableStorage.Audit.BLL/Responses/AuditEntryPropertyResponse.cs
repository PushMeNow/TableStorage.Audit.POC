using System;

namespace TableStorage.Audit.BLL.Responses
{
    public class AuditEntryPropertyResponse
    {
        public Guid AuditEntryPropertyID { get; set; }

        public Guid AuditEntryID { get; set; }

        public string PropertyName { get; set; }

        public string RelationName { get; set; }

        public bool IsValueSet { get; set; }

        public string InternalPropertyName { get; set; }

        public bool IsKey { get; set; }

        public string OldValueFormatted { get; set; }

        public string NewValueFormatted { get; set; }
    }
}