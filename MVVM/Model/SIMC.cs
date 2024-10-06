using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complaint_Portal.Model
{
    public class SIMC
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string Date { get; set; }
        public string Time { get; set; }
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
        public string Department { get; set; }
        public string Block { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string WorkRelated { get; set; }
        public string ComplaintDetails { get; set; }
    }

}
