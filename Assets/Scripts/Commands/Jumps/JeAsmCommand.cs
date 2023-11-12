namespace Commands.Jumps
{
    using Asm;

    public class JeAsmCommand : AsmCommand
    {
        static JeAsmCommand()
        {
            Init("je", 1, (engine, values) => new JeAsmCommand(engine, values));
        }

        public JeAsmCommand(AsmEngine engine, AsmValue[] values) : base(engine, values, true)
        {
        }

        public override void Execute()
        {
            if (Engine.Flags.IsEquals)
                Engine.Ip = Values[0].Value - 1;
            else Engine.Ip++;
        }
    }
}