using System.Collections.Generic;
using System.Linq;
using Commands;
using JetBrains.Annotations;

public sealed class AsmParser
{
    private readonly string code;
    private List<AsmCommand> allCommands;

    public AsmParser(string code)
    {
        this.code = code + "\0";
    }


    public List<AsmCommand> Decode(AsmEngine engine, List<int> errors)
    {
        var commands = new List<AsmCommand>();

        AsmCommand.CallStaticConstructorsOfChildren();


        for (var i = 0; i < code.Length; i++)
        {
            var curText = code[i..];

            if (curText.Contains('\n') && (curText[..curText.IndexOf('\n')].All(char.IsWhiteSpace) ||
                                           curText.Trim().StartsWith(';')))
            {
                commands.Add(new NopAsmCommand(engine, null));
                continue;
            }

            var cmd = AsmCommand.Commands.FirstOrDefault(cmd => curText.StartsWith(cmd.name));
            if (cmd == default)
            {
                errors.Add(code[..i].Count(x => x == '\n'));
                continue;
            }

            i += cmd.name.Length;

            var asmValues = new AsmValue[3];
            asmValues[0] = GetValue(ref i, engine);
            asmValues[1] = GetValue(ref i, engine);
            asmValues[2] = GetValue(ref i, engine);


            commands.Add(cmd.cmd(engine, asmValues));
        }

        return commands;
    }

    [CanBeNull]
    private AsmValue GetValue(ref int i, AsmEngine engine)
    {
        AsmValue result = null;

        while (code[i] is ',' or ' ') i++;

        if (char.IsDigit(code[i]) || code[i] == '-')
        {
            result = new AsmValue(ParseNumber(ref i));
        }
        else if (code[i] == '$')
        {
            i++;
            if (char.IsDigit(code[i]) || code[i] == '-')
            {
                var number = ParseNumber(ref i);
                result = new AsmValue(() => engine.GetRam(number), regValue => engine.SetRam(number, regValue));
            }
            else // reg
            {
                i++;
                var number = ParseNumber(ref i);
                result = new AsmValue(() => engine.GetRam(engine.GetReg(number)),
                    regValue => engine.SetRam(engine.GetReg(number), regValue));
            }
        }
        else if (code[i] == 'r')
        {
            i++;
            var number = ParseNumber(ref i);
            result = new AsmValue(() => engine.GetReg(number), regValue => engine.SetReg(number, regValue));
        }

        return result;
    }

    private int ParseNumber(ref int i)
    {
        var integer = "";
        while (char.IsDigit(code[i]) || code[i] == '-')
        {
            integer += code[i];
            i++;
        }

        return int.Parse(integer);
    }
}