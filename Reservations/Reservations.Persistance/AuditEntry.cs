using Microsoft.EntityFrameworkCore.ChangeTracking;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Reservations.Persistance
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }
        public string TableName { get; set; }
        public string Action { get; set; }
        public string UserId { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();

        public bool HasTemporaryProperties => TemporaryProperties.Any();

        public Audit ToAudit()
        {
            var audit = new Audit();
            audit.TableName = TableName;
            audit.DateTime = DateTime.UtcNow;
            audit.KeyValues = JsonSerializer.Serialize(KeyValues);
            audit.OldValues = OldValues.Count == 0 ? null : JsonSerializer.Serialize(OldValues);
            audit.NewValues = NewValues.Count == 0 ? null : JsonSerializer.Serialize(NewValues);
            audit.Action = Action;
            audit.UserId = UserId;
            return audit;
        }
    }
}
