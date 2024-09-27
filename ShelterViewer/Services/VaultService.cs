﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ShelterViewer.Models; 

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

    public int Caps
    {
        get
        {
            return _vaultData?.vault.storage.resources.Nuka ?? 0;
        }
    }

    public int Quantums
    {
        get
        {
            return _vaultData?.vault.storage.resources.NukaColaQuantum ?? 0;
        }
    }

    public void InitializeVault(string vaultJsonString)
    {

        try
        {
            var settings = new JsonSerializerSettings();
            _vaultString = vaultJsonString;
            _vaultData = JsonConvert.DeserializeObject<dynamic>(_vaultString, settings);

            NotifyPropertyChanged();
        } 
        catch (Exception ex)
        {
            _vaultString = String.Empty;
            Console.WriteLine("Unable to convert vault string to JSON Object: " + ex.Message);
        }
    }
    public bool IsVaultEmpty()
    {
        return _vaultString == String.Empty;
    }

    public List<Dweller> GetDwellers()
    {
        var settings = new JsonSerializerSettings();
        List<Dweller> dwellers = new();
        if (_vaultData == null)
            return new();

        try
        {
            
            foreach(var dweller in _vaultData.dwellers.dwellers)
            {
                Console.WriteLine(dweller);
                dwellers.Add(JsonConvert.DeserializeObject<Dweller>(dweller.ToString(), settings));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unable to convert dwellers string to JSON Object: " + ex.Message);
        }

        return dwellers;
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
