using Common.DomainBase;

namespace Numerology.Domain.Models
{
    public class LetterModel : IBaseEntity<int>
    {
        public virtual int Id { get; set; }
        public virtual char Letter { get; set; }
        public virtual int Value { get; set; }
        public virtual bool Vowel { get; set; }
    }
}
