using System;

namespace Net.Extensions
{
    public class Range<T>:IEquatable<Range<T>> where T:struct,IComparable<T>
    {
        public virtual T Start { get; set; } 
        public virtual T End { get; set; } 
        public Range(T start,T end)
        {
            this.Start = start;
            this.End = end;
        }
        public Range()
        {

        }
        public static bool operator ==(Range<T> left,Range<T> right)
        {
            if (left == null && right == null) return true;
            if (left == null || right == null) return false;
            return left.Equals(right);
        }
        public static bool operator !=(Range<T> left, Range<T> right)
        {
            if (left == null && right == null) return false;
            if (left == null || right == null) return true;
            return !left.Equals(right);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Range<T> t)) return false;
            return t.Start.CompareTo(this.Start) == 0 &&
                t.End.CompareTo(this.End) == 0;
        }
        public override int GetHashCode()
        {
            return this.Start.GetHashCode() ^ this.End.GetHashCode();
        }
        public bool IsValid() => this.Start.CompareTo(this.End) <= 0;
        public bool Intersects(Range<T> other)
        {
            var notCross = this.Start.CompareTo(other.End) == 1 ||
                this.End.CompareTo(other.Start) == -1;
            return !notCross;
        }
        public bool SubSetOf(Range<T> other)
        {
            return this.Start.CompareTo(other.Start) >= 0 &&
                this.End.CompareTo(other.End) <= 0;
        }
        public bool TopSetOf(Range<T> other)
        {
            return this.Start.CompareTo(other.Start) <= 0 &&
                this.End.CompareTo(other.End) >= 0;
        }
        public bool Intersects(T start,T end)
        {
            var notCross = this.Start.CompareTo(end) == 1 ||
               this.End.CompareTo(start) == -1;
            return !notCross;
        }
        public  bool Intersects(T other)
        {
            if (this.Start.CompareTo(other) == 1) return false;
            if (this.End.CompareTo(other) == -1) return false;
            return true;
        }

        public bool Equals(Range<T> other)
        {
            return this.Start.CompareTo(other.Start) == 0 &&
                this.End.CompareTo(other.End) == 0;
        }
    }
}
