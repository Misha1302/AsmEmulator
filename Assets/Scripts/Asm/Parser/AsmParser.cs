﻿using System.Collections.Generic;
using System.Linq;
using Commands;
using JetBrains.Annotations;

namespace Asm.Parser
{
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

            // need to fix!
            AsmCommand.CallStaticConstructorsOfChildren();


            for (var i = 0; i < code.Length; i++)
            {
                var curText = code[i..];

                if (curText.IsLineOfSpaces() || curText.IsComment())
                {
                    commands.Add(new NopAsmCommand(engine, null));
                    continue;
                }

                var cmd = AsmCommand.Commands.FirstOrDefault(cmd => curText.StartsWith(cmd.name));
                if (cmd == default)
                {
                    errors.Add(code.CountLines(i));
                    continue;
                }

                i += cmd.name.Length;

                var asmValues = ParseArgs(engine, cmd.argsCount, ref i, out var success);
                if (!success)
                {
                    errors.Add(code.CountLines(i));
                    continue;
                }

                commands.Add(cmd.cmd(engine, asmValues));
            }

            return commands;
        }

        private AsmValue[] ParseArgs(AsmEngine engine, int argsCount, ref int i, out bool success)
        {
            success = true;

            var asmValues = new AsmValue[argsCount];
            for (var j = 0; j < argsCount; j++)
            {
                asmValues[j] = GetValue(ref i, engine, out success);

                if (!success || asmValues[j] == null)
                {
                    success = false;
                    return asmValues;
                }
            }

            // if extra arg
            if (GetValue(ref i, engine, out _) != null)
                success = false;

            return asmValues;
        }

        [CanBeNull]
        private AsmValue GetValue(ref int i, AsmEngine engine, out bool success)
        {
            success = true;
            AsmValue result = null;

            SkipSeparators(ref i);

            if (code[i].IsNewLine() || code[i].IsEof())
                return null;

            if (code[i].IsNumber())
            {
                result = new AsmValue(ParseNumber(ref i));
            }
            else if (code[i].IsMem())
            {
                i++;
                if (code[i].IsNumber())
                {
                    var number = ParseNumber(ref i);
                    result = new AsmValue(() => engine.GetRam(number), regValue => engine.SetRam(number, regValue));
                }
                else if (code[i].IsReg())
                {
                    i++;
                    var number = ParseReg(ref i, out success);
                    result = new AsmValue(() => engine.GetRam(engine.GetReg(number)),
                        regValue => engine.SetRam(engine.GetReg(number), regValue));
                }
                else
                {
                    success = false;
                }
            }
            else if (code[i].IsReg())
            {
                i++;
                var number = ParseReg(ref i, out success);
                result = new AsmValue(() => engine.GetReg(number), regValue => engine.SetReg(number, regValue));
            }
            else
            {
                success = false;
            }

            return result;
        }

        private void SkipSeparators(ref int i)
        {
            while (code[i] is ',' or ' ') i++;
        }

        private int ParseReg(ref int i, out bool success)
        {
            success = true;

            var reg = ParseNumber(ref i);
            if (reg is < 0 or >= AsmEngine.RegsCount)
                success = false;

            return reg;
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
}