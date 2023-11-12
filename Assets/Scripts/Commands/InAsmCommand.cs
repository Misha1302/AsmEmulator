namespace Commands
{
    using Asm;

    public class InAsmCommand : AsmCommand
    {
        private bool _waiting;

        static InAsmCommand()
        {
            Init("in", 1, (engine, values) => new InAsmCommand(engine, values));
        }

        public InAsmCommand(AsmEngine engine, AsmValue[] values) : base(engine, values, true)
        {
        }

        public override void Execute()
        {
            if (_waiting) return;

            _waiting = true;
            Engine.In(s =>
            {
                _waiting = false;

                Values[0].Value = int.Parse(s);
                Engine.Ip++;
            });
        }
    }
}