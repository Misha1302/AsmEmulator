namespace Commands
{
    public class CopyAsmCommand : AsmCommand
    {
        static CopyAsmCommand()
        {
            Init("copy", 2, (engine, values) => new CopyAsmCommand(engine, values));
        }

        public CopyAsmCommand(AsmEngine engine, AsmValue[] values) : base(engine, values)
        {
        }

        public override void Execute()
        {
            Values[0].Value = Values[1].Value;
        }
    }
}