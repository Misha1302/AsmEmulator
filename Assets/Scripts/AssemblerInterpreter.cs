using System.Collections.Generic;
using System.Linq;
using Commands;

public sealed class AssemblerInterpreter
{
    private GameManager gameManager;

    public void Init(GameManager gm)
    {
        gameManager = gm;
        gm.InputManager.StartButtonPressed += Execute;
        gm.InputManager.SaveButtonPressed += () => SaveManager.Save(gm.UiManager.TextInput.text);
        gm.InputManager.LoadButtonPressed += () => SaveManager.Load(code => gm.UiManager.TextInput.text = code);
        gm.OnAppExit += () => SaveManager.OnAppExit(gm.UiManager.TextInput.text);

        gameManager.UiManager.TextInput.text = SaveManager.OnAppStart();
    }

    private void Execute()
    {
        gameManager.UiManager.Blink(() =>
        {
            gameManager.UiManager.RamFiller.Clear();
            gameManager.UiManager.Clear();
            
            MainExecute();
        });
    }

    private void MainExecute()
    {
        var code = gameManager.UiManager.TextInput.text;

        var engine = new AsmEngine(gameManager.UiManager.RamFiller.Cells.Select(_ => 0).ToList(),
            new List<AsmCommand>(), gameManager);
        var errors = new List<int>();
        engine.Commands = new AsmParser(code).Decode(engine, errors);
        if (errors.Count != 0)
        {
            PrintErrors(errors, engine);
            return;
        }

        engine.OnRamChanged += gameManager.UiManager.RamFiller.UpdateRam;
        engine.OnRegChanged += gameManager.UiManager.UpdateRegs;

        gameManager.StopAllCoroutines();
        gameManager.StartCoroutine(engine.Execute());
    }

    private void PrintErrors(List<int> errors, AsmEngine engine)
    {
        foreach (var e in errors.Distinct())
            engine.Print($"Ошибка {e}");
    }
}