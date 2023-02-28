﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Domain
{
    public class AuditableEntity
    {
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
