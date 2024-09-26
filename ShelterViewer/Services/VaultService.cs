using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShelterViewer.Services;

public class VaultService
{
    public event PropertyChangingEventHandler? OnVaultChanged = null;
    public void InitializeVault(string vaultJsonString)
    {
        VaultString = vaultJsonString;
        NotifyPropertyChanged();
    }
    private string VaultString { get; set; } = String.Empty;

    public string Name { get; set; }

    public bool IsVaultEmpty()
    {
        return VaultString == String.Empty;
    }

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        if (OnVaultChanged == null)
            return;

        if(propertyName != "")
        {
            OnVaultChanged(this, new PropertyChangingEventArgs(propertyName));
        }
    }
}
