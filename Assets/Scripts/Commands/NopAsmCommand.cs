namespace Commands
{
    public sealed class NopAsmCommand : AsmCommand
    {
        static NopAsmCommand()
        {
            Init("nop", 0, (engine, values) => new NopAsmCommand(engine, values));
        }

        public NopAsmCommand(AsmEngine engine, AsmValue[] values) : base(engine, values)
        {
        }

        public override void Execute()
        {
        }
    }
}