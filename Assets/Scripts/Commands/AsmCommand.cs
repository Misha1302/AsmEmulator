namespace Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public abstract class AsmCommand
    {
        public static readonly List<(string name, int argsCount, Func<AsmEngine, AsmValue[], AsmCommand> cmd)>
            Commands = new();

        public readonly bool IsJumpCommand;

        protected readonly AsmEngine Engine;
        private readonly AsmValue[] values;

        protected AsmCommand(AsmEngine engine, AsmValue[] values, bool isJumpCommand = false)
        {
            Engine = engine;
            this.values = values;
            IsJumpCommand = isJumpCommand;
        }

        protected IReadOnlyList<AsmValue> Values => values;

        public static void CallStaticConstructorsOfChildren()
        {
            Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsSubclassOf(typeof(AsmCommand))).ToList()
                .ForEach(x => x.TypeInitializer.Invoke(null, null));
        }

        protected static void Init(string name, int argsCount, Func<AsmEngine, AsmValue[], AsmCommand> cmd)
        {
            if (Commands.All(x => x.name != name))
                Commands.Add((name, argsCount, cmd));
        }

        public abstract void Execute();
    }
}