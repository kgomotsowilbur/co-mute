using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CoMute.Abstractions.Models;

public abstract class BaseEntity : IEquatable<BaseEntity>
{
    public BaseEntity()
    {
        this.Id = Guid.NewGuid();
    }

    [Key]
    [Column("id")]
    public virtual Guid Id { get; set; }

    public override bool Equals(object obj)
    {
        return base.Equals(obj as BaseEntity);
    }

    public bool Equals([AllowNull] BaseEntity other)
    {
        if(other != null && other.Id != Guid.Empty)
        {
            return this.Id.Equals(other.Id);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Id);
    }
}