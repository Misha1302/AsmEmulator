namespace Commands
{
    using Asm;

    public class PrintAsmCommand : AsmCommand
    {
        static PrintAsmCommand()
        {
            Init("print", 1, (engine, values) => new PrintAsmCommand(engine, values));
        }

        public PrintAsmCommand(AsmEngine engine, AsmValue[] values) : base(engine, values)
        {
        }

        public override void Execute()
        {
            Engine.Out(Values[0].Value);
        }
    }
}