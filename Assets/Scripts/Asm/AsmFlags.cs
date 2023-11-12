namespace Asm
{
    public sealed class AsmFlags
    {
        public readonly bool IsLessThan;
        public readonly bool IsGreaterThan;
        public readonly bool IsEquals;

        public AsmFlags(bool isLessThan, bool isGreaterThan, bool isEquals)
        {
            IsLessThan = isLessThan;
            IsGreaterThan = isGreaterThan;
            IsEquals = isEquals;
        }

        public bool IsLessThanOrEquals => IsLessThan || IsEquals;
        public bool IsGreaterThanOrEquals => IsGreaterThan || IsEquals;
    }
}