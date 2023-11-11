namespace Commands.Jumps
{
    public class JneAsmCommand : AsmCommand
    {
        static JneAsmCommand()
        {
            Init("jne", 1, (engine, values) => new JneAsmCommand(engine, values));
        }

        public JneAsmCommand(AsmEngine engine, AsmValue[] values) : base(engine, values, true)
        {
        }

        public override void Execute()
        {
            if (!Engine.Flags.IsEquals)
                Engine.Ip = Values[0].Value - 1;
            else Engine.Ip++;
        }
    }
}