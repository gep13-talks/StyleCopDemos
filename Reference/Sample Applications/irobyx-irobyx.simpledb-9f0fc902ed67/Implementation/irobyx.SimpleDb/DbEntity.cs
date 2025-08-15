using System;

namespace irobyx.SimpleDb
{
    public abstract class DbEntity: IEquatable<DbEntity> 
    {
        // TODO: refactor to only allow setting the Id using the ctor
        public Guid Id { get; set; }

        //protected DbEntity()
        //{
        //    Id = Guid.NewGuid();
        //}

        //internal DbEntity(Guid id)
        //{
        //    Id = id;
        //}

        public override bool Equals(object obj)
        {
            if (!(obj is DbEntity)) return false;
            return this.Equals((DbEntity)obj);
        }
    
        public bool Equals(DbEntity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id.Equals(this.Id);
        }
 
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public static bool operator ==(DbEntity left, DbEntity right)
        {
            if (ReferenceEquals(left, null)) return false;
            return left.Equals(right);
        }

        public static bool operator !=(DbEntity left, DbEntity right)
        {
            return !(left == right);
        }
    }
}