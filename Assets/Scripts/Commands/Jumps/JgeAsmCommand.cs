namespace Commands.Jumps
{
    public class JgeAsmCommand : AsmCommand
    {
        static JgeAsmCommand()
        {
            Init("jge", 1, (engine, values) => new JgeAsmCommand(engine, values));
        }

        public JgeAsmCommand(AsmEngine engine, AsmValue[] values) : base(engine, values, true)
        {
        }

        public override void Execute()
        {
            if (Engine.Flags.IsGreaterThanOrEquals)
                Engine.Ip = Values[0].Value - 1;
            else Engine.Ip++;
        }
    }
}