namespace Commands.Jumps
{
    public class JleAsmCommand : AsmCommand
    {
        static JleAsmCommand()
        {
            Init("jle", 1, (engine, values) => new JleAsmCommand(engine, values));
        }

        public JleAsmCommand(AsmEngine engine, AsmValue[] values) : base(engine, values, true)
        {
        }

        public override void Execute()
        {
            if (Engine.Flags.IsLessThanOrEquals)
                Engine.Ip = Values[0].Value - 1;
            else Engine.Ip++;
        }
    }
}