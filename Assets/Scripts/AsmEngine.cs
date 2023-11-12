using System;
using System.Collections;
using System.Collections.Generic;
using Commands;
using JetBrains.Annotations;
using UnityEngine;

public sealed class AsmEngine
{
    public const int RegsCount = 4;

    public int Speed = 1000;

    public int Ip;
    public readonly Stack<int> Stack = new();
    public List<AsmCommand> Commands;
    private readonly GameManager gameManager;
    private readonly List<int> ram;
    private readonly int[] reg = new int[RegsCount];

    [CanBeNull] public Action<List<int>> OnRamChanged = null;
    [CanBeNull] public Action<int[]> OnRegChanged = null;
    public AsmFlags Flags = new(false, false, false);

    public AsmEngine(List<int> ram, List<AsmCommand> commands, GameManager gameManager)
    {
        this.ram = ram;
        Commands = commands;
        this.gameManager = gameManager;
    }

    public int GetRam(int index) => ram[index];

    public void SetRam(int index, int value)
    {
        ram[index] = value;
        OnRamChanged?.Invoke(ram);
    }

    public int GetReg(int index) => reg[index];

    public void SetReg(int index, int value)
    {
        reg[index] = value;
        OnRegChanged?.Invoke(reg);
    }

    public IEnumerator Execute()
    {
        var count = 0;

        for (Ip = 0; Ip < Commands.Count;)
        {
            var command = Commands[Ip];
            command.Execute();

            if (!command.IsJumpCommand) Ip++;

            if (count++ % Math.Min(Speed, 1000) != 0) continue;

            var seconds = 1f / Speed;
            if (seconds < 0.01f)
                yield return new WaitForFixedUpdate();
            else yield return new WaitForSeconds(seconds);
        }
    }

    public void Halt()
    {
        Ip = Commands.Count;
    }

    public void Out<T>(T value)
    {
        gameManager.UiManager.InOutUi.Out(value.ToString());
    }
}