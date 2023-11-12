namespace Commands.Jumps
{
    using Asm;

    public class JltAsmCommand : AsmCommand
    {
        static JltAsmCommand()
        {
            Init("jlt", 1, (engine, values) => new JltAsmCommand(engine, values));
        }

        public JltAsmCommand(AsmEngine engine, AsmValue[] values) : base(engine, values, true)
        {
        }

        public override void Execute()
        {
            if (Engine.Flags.IsLessThan)
                Engine.Ip = Values[0].Value - 1;
            else Engine.Ip++;
        }
    }
}