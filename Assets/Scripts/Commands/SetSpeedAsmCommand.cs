﻿namespace Commands
{
    using Asm;

    public class SetSpeedAsmCommand : AsmCommand
    {
        static SetSpeedAsmCommand()
        {
            Init("stspd", 1, (engine, values) => new SetSpeedAsmCommand(engine, values));
        }

        public SetSpeedAsmCommand(AsmEngine engine, AsmValue[] values) : base(engine, values)
        {
        }

        public override void Execute()
        {
            Engine.Speed = Values[0].Value;
        }
    }
}