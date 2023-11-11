namespace Commands.Jumps
{
    public class JgtAsmCommand : AsmCommand
    {
        static JgtAsmCommand()
        {
            Init("jgt", 1, (engine, values) => new JgtAsmCommand(engine, values));
        }

        public JgtAsmCommand(AsmEngine engine, AsmValue[] values) : base(engine, values, true)
        {
        }

        public override void Execute()
        {
            if (Engine.Flags.IsGreaterThan)
                Engine.Ip = Values[0].Value - 1;
            else Engine.Ip++;
        }
    }
}