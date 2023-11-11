namespace Commands.Jumps
{
    public class JmpAsmCommand : AsmCommand
    {
        static JmpAsmCommand()
        {
            Init("jmp", 1, (engine, values) => new JmpAsmCommand(engine, values));
        }

        public JmpAsmCommand(AsmEngine engine, AsmValue[] values) : base(engine, values, true)
        {
        }

        public override void Execute()
        {
            Engine.Ip = Values[0].Value - 1;
        }
    }
}