namespace Commands
{
    using Asm;

    public class RetAsmCommand : AsmCommand
    {
        static RetAsmCommand()
        {
            Init("ret", 0, (engine, values) => new RetAsmCommand(engine, values));
        }

        public RetAsmCommand(AsmEngine engine, AsmValue[] values) : base(engine, values, true)
        {
        }

        public override void Execute()
        {
            Engine.Ip = Engine.Stack.Pop() + 1;
        }
    }
}