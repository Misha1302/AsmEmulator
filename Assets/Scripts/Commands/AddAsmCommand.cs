namespace Commands
{
    using Asm;

    public class AddAsmCommand : AsmCommand
    {
        static AddAsmCommand()
        {
            Init("add", 2, (engine, values) => new AddAsmCommand(engine, values));
        }

        public AddAsmCommand(AsmEngine engine, AsmValue[] values) : base(engine, values)
        {
        }

        public override void Execute()
        {
            Values[0].Value += Values[1].Value;
        }
    }
}