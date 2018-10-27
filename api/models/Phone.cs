using System;
using System.Collections.Generic;

namespace Models
{
    public class Phone
    {
        public long Id { get; set; }
        public string Model { get; set; }
        public string Tech { get; set; }
        public Dictionary<string, string> Bands { get; set; }
        public DateTime? BookedAt { get; set; }
        public string BookedBy { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj == null) return false;
            Phone other = obj as Phone;
            if (other == null) return false;
            return Id == other.Id;
        }
    }
}
