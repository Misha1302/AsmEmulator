namespace Commands
{
    using System;

    public sealed class AsmValue
    {
        private readonly Func<int> getNumber;
        private readonly Action<int> setNumber;

        public AsmValue(int number)
        {
            getNumber = () => number;
        }

        public AsmValue(Func<int> getNumber, Action<int> setNumber)
        {
            this.getNumber = getNumber;
            this.setNumber = setNumber;
        }

        public int Value
        {
            get => getNumber();
            set => setNumber(value);
        }
    }
}