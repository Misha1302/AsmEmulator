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
        gm.InputManager.SaveButtonPressed += () => SaveManager.Save(gm.UiManager.InOutUi.TextInput.text);
        gm.InputManager.LoadButtonPressed += () => SaveManager.Load(code => gm.UiManager.InOutUi.TextInput.text = code);
        gm.OnAppExit += () => SaveManager.OnAppExit(gm.UiManager.InOutUi.TextInput.text);

        gameManager.UiManager.InOutUi.TextInput.text = SaveManager.OnAppStart();
    }

    private void Execute()
    {
        gameManager.UiManager.BlinkUi.Blink(() =>
        {
            gameManager.StopAllCoroutines();
            
            gameManager.UiManager.RamUi.Clear();
            gameManager.UiManager.InOutUi.Clear();
            gameManager.UiManager.RegsUi.Clear();

            MainExecute();
        });
    }

    private void MainExecute()
    {
        var code = gameManager.UiManager.InOutUi.TextInput.text;

        var engine = new AsmEngine(gameManager.UiManager.RamUi.Cells.Select(_ => 0).ToList(),
            new List<AsmCommand>(), gameManager);
        
        var errors = new List<int>();
        engine.Commands = new AsmParser(code).Decode(engine, errors);
        if (errors.Count != 0)
        {
            PrintErrors(errors, engine);
            return;
        }

        engine.OnRamChanged += gameManager.UiManager.RamUi.UpdateRam;
        engine.OnRegChanged += gameManager.UiManager.RegsUi.UpdateRegs;

        gameManager.StartCoroutine(engine.Execute());
    }

    private static void PrintErrors(IEnumerable<int> errors, AsmEngine engine)
    {
        foreach (var e in errors.Distinct())
            engine.Out($"Error: {e}");
    }
}