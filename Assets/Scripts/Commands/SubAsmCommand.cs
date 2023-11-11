namespace Commands
{
    public class SubAsmCommand : AsmCommand
    {
        static SubAsmCommand()
        {
            Init("sub", 2, (engine, values) => new SubAsmCommand(engine, values));
        }

        public SubAsmCommand(AsmEngine engine, AsmValue[] values) : base(engine, values)
        {
        }

        public override void Execute()
        {
            Values[0].Value -= Values[1].Value;
        }
    }
}