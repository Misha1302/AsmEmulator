namespace Commands
{
    using Asm;

    public class CmpAsmCommand : AsmCommand
    {
        static CmpAsmCommand()
        {
            Init("cmp", 2, (engine, values) => new CmpAsmCommand(engine, values));
        }

        public CmpAsmCommand(AsmEngine engine, AsmValue[] values) : base(engine, values)
        {
        }

        public override void Execute()
        {
            var x = Values[0].Value;
            var y = Values[1].Value;
            Engine.Flags = new AsmFlags(x < y, x > y, x == y);
        }
    }
}