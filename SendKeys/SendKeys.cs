namespace SendKeys;

public static class SendKeys
{
    public static void SendWait(string keys)
    {
        System.Windows.Forms.SendKeys.SendWait(keys);
    }
}