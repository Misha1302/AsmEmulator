namespace Commands
{
    using Asm;

    public class CallAsmCommand : AsmCommand
    {
        static CallAsmCommand()
        {
            Init("call", 1, (engine, values) => new CallAsmCommand(engine, values));
        }

        public CallAsmCommand(AsmEngine engine, AsmValue[] values) : base(engine, values, true)
        {
        }

        public override void Execute()
        {
            Engine.Stack.Push(Engine.Ip);
            Engine.Ip = Values[0].Value - 1;
        }
    }
}