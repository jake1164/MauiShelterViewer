using Newtonsoft.Json;
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

    private string _vaultString = String.Empty;

    private dynamic? _vaultData = null;

    public string Name 
    { 
        get 
        { 
            return _vaultData?.vault.VaultName ?? String.Empty; 
        } 
    }

    public int DwellerCount
    {
        get
        {
            return _vaultData?.dwellers.dwellers.Count ?? 0;
        }
    }

    public int RoomCount
    {
        get
        {
            return _vaultData?.vault.rooms.Count ?? 0;
        }
    }

    public bool IsVaultEmpty()
    {
        return _vaultString == String.Empty;
    }
    public void InitializeVault(string vaultJsonString)
    {
        try
        {
            _vaultString = vaultJsonString;
            _vaultData = JsonConvert.DeserializeObject<dynamic>(_vaultString);

            NotifyPropertyChanged();
        } 
        catch (Exception ex)
        {
            _vaultString = String.Empty;
            Console.WriteLine("Unable to convert vault string to JSON Object: " + ex.Message);
        }
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
