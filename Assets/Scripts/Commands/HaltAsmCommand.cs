namespace Commands
{
    public class HaltAsmCommand : AsmCommand
    {
        static HaltAsmCommand()
        {
            Init("halt", 0, (engine, values) => new HaltAsmCommand(engine, values));
        }

        public HaltAsmCommand(AsmEngine engine, AsmValue[] values) : base(engine, values)
        {
        }

        public override void Execute()
        {
            Engine.Halt();
        }
    }
}