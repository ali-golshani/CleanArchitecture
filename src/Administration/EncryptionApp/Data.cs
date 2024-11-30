using Framework.Security.Encryption;

namespace EncryptionApp;

internal class Data : BindableBase
{
    public event EventHandler<Exception>? Error;

    private string? plainText;
    private string? cipherText;
    private string? eKey;

    public Data()
    {
        EncryptCommand = new DelegateCommand(Encrypt);
        DecryptCommand = new DelegateCommand(Decrypt);
        CopyPlainTextCommand = new DelegateCommand(CopyPlainText);
        CopyCipherTextCommand = new DelegateCommand(CopyCipherText);
        ClearPlainTextCommand = new DelegateCommand(ClearPlainText);
        ClearCipherTextCommand = new DelegateCommand(ClearCipherText);
        PasteEKeyCommand = new DelegateCommand(PasteEKey);
    }

    public DelegateCommand EncryptCommand { get; }
    public DelegateCommand DecryptCommand { get; }
    public DelegateCommand CopyPlainTextCommand { get; }
    public DelegateCommand ClearPlainTextCommand { get; }
    public DelegateCommand CopyCipherTextCommand { get; }
    public DelegateCommand ClearCipherTextCommand { get; }
    public DelegateCommand PasteEKeyCommand { get; }

    public string? PlainText
    {
        get
        {
            return plainText;
        }
        set
        {
            if (plainText != value)
            {
                plainText = value;
                RaisePropertyChanged(nameof(PlainText));
            }
        }
    }

    public string? CipherText
    {
        get
        {
            return cipherText;
        }
        set
        {
            if (cipherText != value)
            {
                cipherText = value;
                RaisePropertyChanged(nameof(CipherText));
            }
        }
    }

    public string? EKey
    {
        get
        {
            return eKey;
        }
        set
        {
            if (eKey != value)
            {
                eKey = value;
                RaisePropertyChanged(nameof(EKey));
            }
        }
    }

    private StringEncryptor? Encryptor
    {
        get
        {
            return string.IsNullOrEmpty(EKey) ? null : new StringEncryptor(EKey);

        }
    }

    private void Encrypt()
    {
        var text = PlainText;

        if (string.IsNullOrEmpty(text))
        {
            return;
        }

        CipherText = Encryptor?.Encrypt(text);
    }

    private void Decrypt()
    {
        var text = CipherText;

        if (string.IsNullOrEmpty(text))
        {
            return;
        }

        try
        {
            PlainText = Encryptor?.Decrypt(text);
        }
        catch (Exception exp)
        {
            Error?.Invoke(this, exp);
        }
    }

    private void CopyPlainText()
    {
        Copy(PlainText);
    }

    private void CopyCipherText()
    {
        Copy(CipherText);
    }

    private void ClearPlainText()
    {
        PlainText = string.Empty;
    }

    private void ClearCipherText()
    {
        CipherText = string.Empty;
    }

    private void PasteEKey()
    {
        try
        {
            if (System.Windows.Clipboard.ContainsText())
            {
                EKey = System.Windows.Clipboard.GetText();
            }
        }
        catch { DoNothings(); }
    }

    private static void Copy(string? text)
    {
        try
        {
            if (!string.IsNullOrEmpty(text))
            {
                System.Windows.Clipboard.SetText(text);
            }
        }
        catch { DoNothings(); }
    }

    private static void DoNothings()
    {
        /* DoNothings */
    }
}
