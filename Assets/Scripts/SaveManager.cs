using System;
using System.IO;
using SimpleFileBrowser;
using UnityEngine;

public static class SaveManager
{
    public static void Save(string code)
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Text Files", ".txt"));

        FileBrowser.SetDefaultFilter(".txt");

        FileBrowser.AddQuickLink("Current", Directory.GetCurrentDirectory());

        FileBrowser.ShowSaveDialog(
            path => FileBrowserHelpers.WriteTextToFile(path[0], code),
            null,
            FileBrowser.PickMode.Files);
    }

    public static void Load(Action<string> callback)
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Text Files", ".txt"));

        FileBrowser.SetDefaultFilter(".txt");

        FileBrowser.AddQuickLink("Current", Directory.GetCurrentDirectory());

        FileBrowser.ShowLoadDialog(
            path => callback(FileBrowserHelpers.ReadTextFromFile(path[0])),
            null,
            FileBrowser.PickMode.Files);
    }

    public static void OnAppExit(string code) => PlayerPrefs.SetString("code", code);

    public static string OnAppStart() => PlayerPrefs.GetString("code");
}