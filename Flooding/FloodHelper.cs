using System.Collections.Generic;
using System.Linq;
using Windows.System;
using Windows.UI.Input.Preview.Injection;

namespace Flooding;

public static class FloodHelper
{
    private static readonly InputInjector Injector = InputInjector.TryCreate();

    public static void InjectString(string value)
    {
        foreach (var character in value)
        {
            Injector.InjectKeyboardInput(new[]
            {
                new InjectedInputKeyboardInfo
                {
                    ScanCode = character,
                    KeyOptions = InjectedInputKeyOptions.Unicode
                },
                new InjectedInputKeyboardInfo
                {
                    ScanCode = character,
                    KeyOptions = InjectedInputKeyOptions.KeyUp
                }
            });
        }
    }

    public static void InjectMessage(string message, IEnumerable<VirtualKey> sendKeys)
    {
        InjectString(message);

        Injector.InjectKeyboardInput(
            from sendKey in sendKeys
            select new InjectedInputKeyboardInfo {VirtualKey = (ushort) sendKey}
        );
        Injector.InjectKeyboardInput(
            from sendKey in sendKeys
            select new InjectedInputKeyboardInfo 
            {
                VirtualKey = (ushort) sendKey,
                KeyOptions = InjectedInputKeyOptions.KeyUp
            }
        );
    }
}