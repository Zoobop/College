using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, ILoader<Inventory, InventoryDATA>, IDataManagement
{
    #region Fields and Properties

    [SerializeField] private Dictionary<Consumable, int> _consumables = new Dictionary<Consumable, int>();
    [SerializeField] private Dictionary<Craftable, int> _craftables = new Dictionary<Craftable, int>();
    [SerializeField] private List<KeyItem> _keyItems = new List<KeyItem>();

    public Dictionary<Consumable, int> Consumables { get { return _consumables; } }
    public Dictionary<Craftable, int> Craftables { get { return _craftables; } }
    public List<KeyItem> KeyItems { get { return _keyItems; } }

    public Action<Inventory> OnInventoryChanged;

    #endregion

    #region Unity Functions

    public void Start()
    {
        OnInventoryChanged?.Invoke(this);
    }

    #endregion

    #region Inventory Modifiers
    public void Add(Craftable craftable)
    {
        TryAddToItemStack(craftable);

        OnInventoryChanged?.Invoke(this);
    }

    public void Add(KeyItem keyItem) 
    { 
        _keyItems.Add(keyItem);

        OnInventoryChanged?.Invoke(this);
    }

    public void Add(Consumable consumable)
    {
        TryAddToItemStack(consumable);

        OnInventoryChanged?.Invoke(this);
    }

    private void TryAddToItemStack(Craftable craftable)
    {
        if (!_craftables.ContainsKey(craftable))
        {
            _craftables.Add(craftable, 1);
            return;
        }

        if (craftable.IsStackable && _craftables[craftable] < craftable.MaxStack)
        {
            _craftables[craftable]++;
            return;
        }
    }

    private void TryAddToItemStack(Consumable consumable)
    {
        if (!_consumables.ContainsKey(consumable))
        {
            _consumables.Add(consumable, 1);
            return;
        }

        if (consumable.IsStackable && _consumables[consumable] < consumable.MaxStack)
        {
            _consumables[consumable]++;
            return;
        }
    }

    public void RemoveItem(Craftable craftable)
    {
        if (!_craftables.ContainsKey(craftable))
            return;

        if (_craftables[craftable] > 1)
            _craftables[craftable]--;
        else
            _craftables.Remove(craftable);

        OnInventoryChanged?.Invoke(this);
    }

    public void RemoveItem(Consumable consumable)
    {
        if (!_consumables.ContainsKey(consumable))
            return;

        if (_consumables[consumable] > 1)
            _consumables[consumable]--;
        else
        {
            _consumables.Remove(consumable);
        }

        OnInventoryChanged?.Invoke(this);
    }

    public void RemoveItem(KeyItem keyItem)
    {
        _keyItems.Remove(keyItem);

        OnInventoryChanged?.Invoke(this);
    }
    #endregion

    #region ILoader
    public void Convert(Inventory classToConvert) { }

    public void SetMemberValues(InventoryDATA dataClass)
    {
        _craftables = dataClass.craftables;
        _keyItems = dataClass.keyItems;
    }

    public InventoryDATA ToData() => new InventoryDATA { craftables = _craftables, keyItems = _keyItems };
    #endregion

    #region IDataManagement
    public void SaveData()
    {
        
    }

    public void LoadData()
    {
        
    }
    #endregion
}

[Serializable]
public class InventoryDATA
{
    public Dictionary<Consumable, int> consumables;
    public Dictionary<Craftable, int> craftables;
    public List<KeyItem> keyItems;
}