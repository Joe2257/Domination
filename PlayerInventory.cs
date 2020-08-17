using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InventoryType  {Item, Ammo, Weapon, Utility }
[System.Serializable]
public struct DescriptionPanel
{
    public Text  _itemName;
    public Image _itemImage;
    public Text  _itemDescription;
}

[System.Serializable]
public struct Inventory
{
    public GameObject[] _inventorySlots;
}


public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private GameObject _dropSpawnPosition = null;
    [SerializeField] private GameObject _objectSelected    = null;

    private PlayerCombat               _playerCombat;
    private PlayerController           _playerController;
    [HideInInspector]public UI_Manager uiManager;

    [Header("Weapons Panel")]
    public List<GameObject> weaponsEquipped  = new List<GameObject>();
    public List<GameObject> weaponsCollected = new List<GameObject>();
    public GameObject[]     weaponSlot;

    [Header("Ammo Panel")]
     public List<GameObject> primaryAmmo = new List<GameObject>();
     public GameObject[]     primaryAmmoSlots;
     public float[]          primaryAmmoValues;
    [Space]
     public List<GameObject> specialAmmo = new List<GameObject>();
     public GameObject[]     specialAmmoSlots;
     public float[]          specialAmmoValues;
    [Space]
     public List<GameObject> reserveAmmo = new List<GameObject>();
     public GameObject[]     reserveAmmoSlots;
     public float[]          reserveAmmoValues;

    [Header("Granade")]
    [SerializeField] public List<GameObject> _granadesInInventory = new List<GameObject>();

    [Header("InventoryItems")]
    [SerializeField] public List<GameObject> itemsCollected = new List<GameObject>();
    [SerializeField] public GameObject[]     inventorySlots;

     public bool inventoryisFull = false;
     public bool primaryisFull   = false;
     public bool specialisFull   = false;
     public bool reserveisFull   = false;
     public bool granadeisFull   = false;
     public bool vaultIsActive   = false;

    [Header("Description Tab")]
    public GameObject       description;
    public DescriptionPanel descriptionPanel;

    [Header("Vault")]
    public GameObject vaultScreen;
    public GameObject itemVault;
    public GameObject weaponsVault;

    public List<GameObject> itemsInVault   = new List<GameObject>();
    public GameObject[]     itemsInVaultSlots;

    public List<GameObject> weaponsInVault = new List<GameObject>();
    public GameObject[]     weaponsInVaultSlots;

    private void Start()
    {
        _playerController = GetComponentInParent<PlayerController>();
        _playerCombat     = _playerController.GetComponentInChildren<PlayerCombat>();
        uiManager        = GetComponentInParent<UI_Manager>();

        description.SetActive(false);
        InitializeInventory();

    }

    //Initialize the UI (Inventory, Vault, HUD)
    private void InitializeInventory()
    {

        if (weaponsEquipped[0] != null)
        if (weaponsEquipped[0].GetComponent<FireWeapon>() != null)
        {
              
            FireWeapon _primary    = weaponsEquipped[0].GetComponent<FireWeapon>();
            Weapon_Slot weaponMain = weaponSlot[0].GetComponent<Weapon_Slot>();

             weaponMain._weaponImage.sprite = _primary._image;
             weaponMain._damage.text        = _primary._damage.ToString();
             weaponMain._range.text         = _primary._range.ToString();
             weaponMain._spreadFactor.text  = _primary._spreadFactor.ToString();
             weaponMain._magazine.text      = _primary._magazine.ToString();
             weaponMain._isEmptyWS = false;
        }

        if (weaponsEquipped[1] != null)
        if (weaponsEquipped[1].GetComponent<FireWeapon>() != null)
        {
                 
            FireWeapon _special = weaponsEquipped[1].GetComponent<FireWeapon>();
            Weapon_Slot weaponMain = weaponSlot[1].GetComponent<Weapon_Slot>();

            weaponMain._weaponImage.sprite = _special._image;
            weaponMain._damage.text        = _special._damage.ToString();
            weaponMain._range.text         = _special._range.ToString();
            weaponMain._spreadFactor.text  = _special._spreadFactor.ToString();
            weaponMain._magazine.text      = _special._magazine.ToString();
            weaponMain._isEmptyWS = false;
        }

        if (weaponsEquipped[2] != null)
        if (weaponsEquipped[2].GetComponent<FireWeapon>() != null)
        {

            FireWeapon _reserveW = weaponsEquipped[2].GetComponent<FireWeapon>();
            Weapon_Slot weaponMain = weaponSlot[2].GetComponent<Weapon_Slot>();

             weaponMain._weaponImage.sprite = _reserveW._image;
             weaponMain._damage.text        = _reserveW._damage.ToString();
             weaponMain._range.text         = _reserveW._range.ToString();
             weaponMain._spreadFactor.text  = _reserveW._spreadFactor.ToString();
             weaponMain._magazine.text      = _reserveW._magazine.ToString();
             weaponMain._isEmptyWS = false;
        }


        if (itemsCollected != null)
        {
            for (int i = 0; i < itemsCollected.Count; i++)
            {
                InventorySlot slot = inventorySlots[i].GetComponent<InventorySlot>();
                Item_Main itemMain = itemsCollected[i].GetComponent<Item_Main>();

                if (inventorySlots[i] != null && itemsCollected[i] != null)
                {
                    slot._slotImage.sprite = itemMain._itemImage;
                    slot._slotText.text = null;
                    slot._objectInSlot = itemsCollected[i].gameObject;
                    slot._isEmpty = false;
                }
                else
                    break;
            }
        }

        if (primaryAmmo != null)
        {
            for (int i = 0; i < primaryAmmo.Count; i++)
            {
                if (primaryAmmoSlots[i] != null && primaryAmmo[i] != null)
                {
                    InventorySlot slot = primaryAmmoSlots[i].GetComponent<InventorySlot>();
                    Item_Main itemMain = primaryAmmo[i].GetComponent<Item_Main>();

                    primaryAmmoValues[i] = itemMain._ammoAmount;

                    slot._slotImage.sprite = itemMain._itemImage;
                    slot._slotText.text = primaryAmmoValues[i].ToString();
                    slot._objectInSlot = primaryAmmo[i].gameObject;
                    slot._isEmpty = false;
                }
                else
                    break;
            }
        }

        if (specialAmmo != null)
        {
            for (int i = 0; i < specialAmmo.Count; i++)
            {
                if (specialAmmoSlots[i] != null && specialAmmo[i] != null)
                {
                    InventorySlot slot = specialAmmoSlots[i].GetComponent<InventorySlot>();
                    Item_Main itemMain = specialAmmo[i].GetComponent<Item_Main>();

                    slot._slotImage.sprite = itemMain._itemImage;
                    slot._slotText.text = null;
                    slot._objectInSlot = specialAmmo[i].gameObject;
                    slot._isEmpty = false;
                }
                else
                    break;
            }
        }

        if (reserveAmmo != null)
        {
            for (int i = 0; i < reserveAmmo.Count; i++)
            {
                if (reserveAmmoSlots[i] != null && reserveAmmo[i] != null)
                {
                    InventorySlot slot = reserveAmmoSlots[i].GetComponent<InventorySlot>();
                    Item_Main itemMain = reserveAmmo[i].GetComponent<Item_Main>();

                    slot._slotImage.sprite = itemMain._itemImage;
                    slot._slotText.text = null;
                    slot._objectInSlot = reserveAmmo[i].gameObject;
                    slot._isEmpty = false;
                }
                else
                    break;
            }
        }

        if (itemsInVault.Count > 0)
        {
            for (int i = 0; i < itemsInVault.Count; i++)
            {
                if (itemsInVaultSlots[i] != null && itemsInVault[i] != null)
                {
                    InventorySlot slot = itemsInVaultSlots[i].GetComponent<InventorySlot>();
                    Item_Main itemMain = itemsInVault[i].GetComponent<Item_Main>();

                    slot._slotImage.sprite = itemMain._itemImage;
                    slot._slotText.text = null;
                    slot._objectInSlot = itemsInVault[i].gameObject;
                    slot._isEmpty = false;
                }
                else 
                    break;
            }
        }

        if (weaponsInVault.Count > 0)
        {
            for (int i = 0; i < weaponsInVault.Count; i++)
            {
                if (weaponsInVaultSlots[i] != null && weaponsInVault != null)
                {
                    FireWeapon  weaponScript   = weaponsInVault[i].GetComponent<FireWeapon>();
                    Weapon_Slot weaponMain     = weaponsInVaultSlots[i].GetComponent<Weapon_Slot>();

                    weaponMain._weaponObject       = weaponsInVault[i];
                    weaponMain._weaponImage.sprite = weaponScript._image;
                    weaponMain._damage.text        = weaponScript._damage.ToString();
                    weaponMain._range.text         = weaponScript._range.ToString();
                    weaponMain._spreadFactor.text  = weaponScript._spreadFactor.ToString();
                    weaponMain._magazine.text      = weaponScript._magazine.ToString();

                    weaponMain._isEmptyWS = false;

                }
            }
        }
    }

    //------------Buttons------------\\

    //Empty
    public void OnButtonEnter()
    {

    }

    //Select the itemSlot and the relative Item (from inventory)
    public void OnButtonClick( Button button)
    {
        InventorySlot slot = button.GetComponent<InventorySlot>();
      if (!slot._isEmpty)
      {
          _objectSelected = button.gameObject;
          Item_Main item = slot._objectInSlot.GetComponent<Item_Main>();

          DisplayDescription(item.gameObject.name, item._itemImage, item._itemDescription);
      }
    }

    ////Select the weaponSlot and the relative weapon (from inventory)
    public void OnWeaponButtonClick(Button button)
    {
        Weapon_Slot slot = button.GetComponent<Weapon_Slot>();

        if (!slot._isEmptyWS)
        {
            _objectSelected = button.gameObject;
            FireWeapon weapon = slot._weaponObject.GetComponent<FireWeapon>();

            DisplayDescription(weapon.gameObject.name, weapon._image, weapon._weaponDescription);

            Debug.Log("SlotClicked");
        }
    }

    //Empty
    public void OnButtonExit()
    {

    }

    //Use the selected Item
    public void OnUseButtonClick()
    {
        bool itemUsed = false;

        InventorySlot slot = _objectSelected.GetComponent<InventorySlot>();
        GameObject item = slot._objectInSlot;
        Item_Main _itemMain = item.GetComponent<Item_Main>();

        switch (_itemMain._itemType)
        {
           case InventoryItemType.Med:
                if (_playerController._healthPoints < 100)
                {
                    MedKit(_itemMain._hpAmount);
                    itemUsed = true;
                }
                break;
            case InventoryItemType.Energy:
                if (_playerController._energyPoints < 50)
                {
                    Energy(_itemMain._energyAmount);
                    itemUsed = true;
                }
                break;

        }

        if (itemUsed)
        {
            itemsCollected.Remove(item);
            slot._isEmpty = true;
            slot._objectInSlot = null;
            slot._slotImage.sprite = null;
            slot._slotText.text = "Empty";
            ResetDescription();
        }
           
    }

    //Drop the selected Item
    public void OnDropButtonClick()
    {
        InventorySlot slot = _objectSelected.GetComponent<InventorySlot>();
        GameObject    item = slot._objectInSlot;

        itemsCollected.Remove(item);

        item = Instantiate(item, _dropSpawnPosition.transform.position, _dropSpawnPosition.transform.rotation) as GameObject;
        item.SetActive(true);
        item.name = slot._objectInSlot.name;
        Rigidbody rigidbody = item.gameObject.GetComponent<Rigidbody>();
        rigidbody.AddForce(_playerController._camera.transform.forward * 100);

        Item_Main _item = item.GetComponent<Item_Main>();

        if (_item._itemType == InventoryItemType.Ammo && _item._ammoType == AmmoType.PrimaryAmmo)
        {
            int slotNumber = System.Array.IndexOf(primaryAmmoSlots, _objectSelected);

            primaryAmmo[slotNumber] = null;
            _item._ammoAmount = primaryAmmoValues[slotNumber];
            primaryAmmoValues[slotNumber] = 0;
        }
        else if (_item._itemType == InventoryItemType.Ammo && _item._ammoType == AmmoType.SpecialAmmo)
        {
            int slotNumber = System.Array.IndexOf(specialAmmoSlots, _objectSelected);

            specialAmmo[slotNumber] = null;
            _item._ammoAmount = specialAmmoValues[slotNumber];
            specialAmmoValues[slotNumber] = 0;
        }
        else if (_item._itemType == InventoryItemType.Ammo && _item._ammoType == AmmoType.ReserveAmmo)
        {
            int slotNumber = System.Array.IndexOf(reserveAmmoSlots, _objectSelected);

            reserveAmmo[slotNumber] = null;
            _item._ammoAmount = reserveAmmoValues[slotNumber];
            reserveAmmoValues[slotNumber] = 0;
        }

        slot._isEmpty = true;
        slot._objectInSlot = null;
        slot._slotImage.sprite = null;
        slot._slotText.text = "Empty";
        ResetDescription();
    }

    //Move the Item from inventory to vault
    public void OnSendToVaultButtonClick()
    {
        if (_objectSelected != null)
        {
            InventorySlot itemInSlot = _objectSelected.GetComponent<InventorySlot>();

            GameObject objectToAdd = itemInSlot._objectInSlot;

            AddItemToVault(objectToAdd);

            ResetDescription();
        }
    }

    //Move the Item from vault to inventory
    public void OnSendItemToInventoryButtonClick()
    {
        InventorySlot itemInSlot = _objectSelected.GetComponent<InventorySlot>();

        GameObject objectToAdd = itemInSlot._objectInSlot;

        AddItemToInventory(objectToAdd);

        RemoveItemFromVault(objectToAdd);
    }

    //Move the weapon from inventory to vault
    public void OnSendWeapontoVaultButtonClick()
    {
        Weapon_Slot weaponSlot = _objectSelected.GetComponent<Weapon_Slot>();
        GameObject weapon = weaponSlot._weaponObject;
        FireWeapon weaponScript = weapon.GetComponent<FireWeapon>();


        if (weaponScript._weaponSlotType == WeaponSlotType.Primary)
        {
            weaponsEquipped[0] = null;
            _playerCombat._weaponsEquipped[0] = null;

            foreach (Transform weaponToRemove in _playerCombat._weaponHolder)
            {
                if (weaponToRemove.name == weapon.name)
                {
                    Destroy(weaponToRemove.gameObject);
                }
            }

            ResetWeaponSlot(weaponSlot);

            SetVaultWeaponSlotValues(SlotType.PrimarySlot, weaponScript);


        }
        else if (weaponScript._weaponSlotType == WeaponSlotType.Special)
        {
            weaponsEquipped[1] = null;
            _playerCombat._weaponsEquipped[1] = null;

            foreach (Transform weaponToRemove in _playerCombat._weaponHolder)
            {
                if (weaponToRemove.name == weapon.name)
                {
                    Destroy(weaponToRemove.gameObject);
                }
            }

            ResetWeaponSlot(weaponSlot);

            SetVaultWeaponSlotValues(SlotType.SpecialSlot, weaponScript);


        }
        else if (weaponScript._weaponSlotType == WeaponSlotType.Reserve)
        {
            weaponsEquipped[2] = null;
            _playerCombat._weaponsEquipped[2] = null;

            foreach (Transform weaponToRemove in _playerCombat._weaponHolder)
            {
                if (weaponToRemove.name == weapon.name)
                {
                    Destroy(weaponToRemove.gameObject);
                }
            }

            ResetWeaponSlot(weaponSlot);

            SetVaultWeaponSlotValues(SlotType.ReserveSlot, weaponScript);


        }


    }

    //Move the weapon from vault to inventory
    public void OnEquipWeaponButtonClick()
    {
        if (_objectSelected != null)
        {
            Weapon_Slot weaponSlot = _objectSelected.GetComponent<Weapon_Slot>();
            GameObject weapon = weaponSlot._weaponObject;
            FireWeapon weaponScript = weapon.GetComponent<FireWeapon>();

            if (weaponScript._weaponSlotType == WeaponSlotType.Primary)
            {
                weaponsEquipped[0] = weapon;
                _playerCombat._weaponsEquipped[0] = weapon;
                GameObject weaponInstance = Instantiate(weapon, _playerCombat._weaponHolder.transform.position, _playerCombat._weaponHolder.transform.rotation) as GameObject;
                weaponInstance.transform.parent = _playerCombat._weaponHolder;
                weaponInstance.name = weapon.name;

                ResetWeaponSlot(weaponSlot);
                SetInventoryWeaponSlotValues(SlotType.PrimarySlot, weaponScript);


            }
            else if (weaponScript._weaponSlotType == WeaponSlotType.Special)
            {
                weaponsEquipped[1] = weapon;
                _playerCombat._weaponsEquipped[1] = weapon;
                GameObject weaponInstance = Instantiate(weapon, _playerCombat._weaponHolder.transform.position, _playerCombat._weaponHolder.transform.rotation) as GameObject;
                weaponInstance.transform.parent = _playerCombat._weaponHolder;
                weaponInstance.SetActive(false);

                ResetWeaponSlot(weaponSlot);
                SetInventoryWeaponSlotValues(SlotType.SpecialSlot, weaponScript);

            }
            else if (weaponScript._weaponSlotType == WeaponSlotType.Reserve)
            {
                weaponsEquipped[2] = weapon;
                _playerCombat._weaponsEquipped[2] = weapon;
                GameObject weaponInstance = Instantiate(weapon, _playerCombat._weaponHolder.transform.position, _playerCombat._weaponHolder.transform.rotation) as GameObject;
                weaponInstance.transform.parent = _playerCombat._weaponHolder;
                weaponInstance.SetActive(false);

                ResetWeaponSlot(weaponSlot);
                SetInventoryWeaponSlotValues(SlotType.ReserveSlot, weaponScript);
            }
        }
       
    }

    public void OnCloseVaultButtonClick()
    {
        uiManager._inventoryIsActive = false;
        vaultIsActive = false;

        vaultScreen.SetActive(false);
        uiManager._playerHud.SetActive(true);
        uiManager._playerInventory.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //Activate weapon vault
    public void OnWeaponsButtonClick()
    {
        itemVault.SetActive(false);
        weaponsVault.SetActive(true);
    }

    //Activate items vault
    public void OnItemsButtonClick()
    {
        itemVault.SetActive(true);
        weaponsVault.SetActive(false);
    }


   //------------Description------------\\

    private void DisplayDescription(string name, Sprite image, string text)
    {
        description.SetActive(true);
        descriptionPanel._itemName.text = name;
        descriptionPanel._itemImage.sprite = image;
        descriptionPanel._itemDescription.text = text;
    }

    public void ResetDescription()
    {
         _objectSelected = null;
        descriptionPanel._itemName.text = null;
        descriptionPanel._itemImage.sprite = null;
        descriptionPanel._itemDescription.text = null;
        description.SetActive(false);
    }

    //------------Inventory------------\\

    // Check wich slot is empty between all the slots, the following function
    // add the Item to the relative empty slot.
    public void CheckIfInventoryFull()
    {
        if (itemsCollected.Count == inventorySlots.Length)
        { inventoryisFull = true; }
        else
        { inventoryisFull = false; }

        if (primaryAmmo[3] != null)
        { primaryisFull = true; }
        else 
        { primaryisFull = false; }

        if (specialAmmo[2] != null)
        { specialisFull = true; }
        else
        { specialisFull = false; }

        if (reserveAmmo[2] != null)
        { reserveisFull = true; }
        else
        { reserveisFull = false; }
    }

    public void AddItemToInventory(GameObject _object)
    {
        if (_object.tag == "InteractiveItem" && !inventoryisFull)
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i].GetComponent<InventorySlot>();

                if (slot._isEmpty)
                {
                    Item_Main itemMain = _object.GetComponent<Item_Main>();

                    itemsCollected.Add(_object);
                    slot._slotImage.sprite = itemMain._itemImage;
                    slot._slotText.text = null;
                    slot._objectInSlot = _object;
                    slot._isEmpty = false;

                    break;
                }
            }
        }
        else
        if (_object.tag == "PrimaryAmmo" && !primaryisFull)
        {
            for (int i = 0; i < primaryAmmoSlots.Length; i++)
            {
                InventorySlot slot = primaryAmmoSlots[i].GetComponent<InventorySlot>();

                if (slot._isEmpty)
                {
                    Item_Main itemMain = _object.GetComponent<Item_Main>();

                    primaryAmmoValues[i] = itemMain._ammoAmount;

                    primaryAmmo[i] = _object;
                    slot._slotImage.sprite = itemMain._itemImage;
                    slot._slotText.text = primaryAmmoValues[i].ToString();
                    slot._objectInSlot = _object;
                    slot._isEmpty = false;

                    if (itemMain._ammoAmount > primaryAmmoValues[0])
                    {
                        float tmp = primaryAmmoValues[0];

                        primaryAmmoValues[0] = itemMain._ammoAmount;
                        primaryAmmoValues[i] = tmp;

                        primaryAmmoSlots[0].GetComponent<InventorySlot>()._slotText.text = itemMain._ammoAmount.ToString();
                        primaryAmmoSlots[i].GetComponent<InventorySlot>()._slotText.text = tmp.ToString();
                    }

                    break;
                }
            }
        }
        else
        if (_object.tag == "SpecialAmmo" && !specialisFull)
        {
            for (int i = 0; i < specialAmmoSlots.Length; i++)
            {
                InventorySlot slot = specialAmmoSlots[i].GetComponent<InventorySlot>();

                if (slot._isEmpty)
                {
                    Item_Main itemMain = _object.GetComponent<Item_Main>();

                    specialAmmoValues[i] = itemMain._ammoAmount;

                    specialAmmo[i] = _object;
                    slot._slotImage.sprite = itemMain._itemImage;
                    slot._slotText.text = specialAmmoValues[i].ToString(); ;
                    slot._objectInSlot = _object;
                    slot._isEmpty = false;

                    if (itemMain._ammoAmount > specialAmmoValues[0])
                    {
                        float tmp = specialAmmoValues[0];

                        specialAmmoValues[0] = itemMain._ammoAmount;
                        specialAmmoValues[i] = tmp;

                        specialAmmoSlots[0].GetComponent<InventorySlot>()._slotText.text = itemMain._ammoAmount.ToString();
                        specialAmmoSlots[i].GetComponent<InventorySlot>()._slotText.text = tmp.ToString();
                    }

                    break;
                }
            }
        }
        else
        if (_object.tag == "Reserve" && !reserveisFull)
        {
            Debug.Log("Reserve");

            for (int i = 0; i < reserveAmmoSlots.Length; i++)
            {
                InventorySlot slot = reserveAmmoSlots[i].GetComponent<InventorySlot>();

                if (slot._isEmpty)
                {
                    Item_Main itemMain = _object.GetComponent<Item_Main>();

                    reserveAmmoValues[i] = itemMain._ammoAmount;

                    reserveAmmo[i] = _object;
                    slot._slotImage.sprite = itemMain._itemImage;
                    slot._slotText.text = null;
                    slot._objectInSlot = _object;
                    slot._isEmpty = false;


                    if (itemMain._ammoAmount > reserveAmmoValues[0])
                    {
                        float tmp = reserveAmmoValues[0];

                        reserveAmmoValues[0] = itemMain._ammoAmount;
                        reserveAmmoValues[i] = tmp;

                        reserveAmmoSlots[0].GetComponent<InventorySlot>()._slotText.text = itemMain._ammoAmount.ToString();
                        reserveAmmoSlots[i].GetComponent<InventorySlot>()._slotText.text = tmp.ToString();
                    }

                    break;
                }
            }
        }
        else
        if (_object.tag == "Granade" && !granadeisFull)
        {
            Debug.Log("Reserve");

            for (int i = 0; i < _granadesInInventory.Count; i++)
            {
                if (_granadesInInventory[i] == null)
                {
                    _granadesInInventory[i] = _object;
                    break;
                }
               
                
            }
        }

    }

    public void DropItemFromInventory(InventorySlot slotToClear, Item_Main itemToRemove, int item )
    {

        if (itemToRemove._itemType == InventoryItemType.Ammo && itemToRemove._ammoType == AmmoType.PrimaryAmmo)
        {
            primaryAmmo[item] = null;
            slotToClear._isEmpty = true;
            slotToClear._objectInSlot = null;
            slotToClear._slotImage.sprite = null;
            slotToClear._slotText.text = "Empty";
        }
        else if (itemToRemove._itemType == InventoryItemType.Ammo && itemToRemove._ammoType == AmmoType.SpecialAmmo)
        {
            specialAmmo[item] = null;
            slotToClear._isEmpty = true;
            slotToClear._objectInSlot = null;
            slotToClear._slotImage.sprite = null;
            slotToClear._slotText.text = "Empty";
        }
        else if (itemToRemove._itemType == InventoryItemType.Ammo && itemToRemove._ammoType == AmmoType.ReserveAmmo)
        {
            reserveAmmo[item] = null;
            slotToClear._isEmpty = true;
            slotToClear._objectInSlot = null;
            slotToClear._slotImage.sprite = null;
            slotToClear._slotText.text = "Empty";
        }
    }

    public void RefreshAmmoSlotValues(GameObject[] ammoArray, float[] ammoValues)
    {
        for(int i = 0; i < ammoArray.Length; i++)
        {
            InventorySlot slot = ammoArray[i].GetComponent<InventorySlot>();
            GameObject ammoObj = slot._objectInSlot;

            if (slot._objectInSlot != null)
            slot._slotText.text = ammoValues[i].ToString();
        }
    }


    //------------ItemsEffects&Utility------------\\

    private void MedKit(float amount)
    {
        _playerController._healthPoints += amount;
    }

    private void Energy(float amount)
    {
        _playerController._energyPoints += amount;
    }

    //------------VaultFunctions------------\\

    public void  ActivateVault()
    {
        uiManager._inventoryIsActive = true;
        vaultIsActive = true;

        vaultScreen.SetActive(true);
        uiManager._playerInventory.SetActive(true);
        uiManager._playerHud.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    private void AddItemToVault(GameObject _object)
    {
        for (int i = 0; i < itemsInVaultSlots.Length; i++)
        {
            InventorySlot slot = itemsInVaultSlots[i].GetComponent<InventorySlot>();
            InventorySlot inventorySlot = _objectSelected.GetComponent<InventorySlot>();
            

            if (slot._isEmpty)
            {
                Item_Main itemMain = _object.GetComponent<Item_Main>();

                slot._slotImage.sprite = itemMain._itemImage;
                slot._slotText.text = null;
                slot._objectInSlot = _object;
                slot._isEmpty = false;

                itemsCollected.Remove(_object);
                inventorySlot._isEmpty = true;
                inventorySlot._objectInSlot = null;
                inventorySlot._slotImage.sprite = null;
                inventorySlot._slotText.text = "Empty";
                ResetDescription();

                break;
            }
        }
    }

    private void RemoveItemFromVault(GameObject _object)
    {
        InventorySlot inventorySlot = _objectSelected.GetComponent<InventorySlot>();

        itemsInVault.Remove(_object);
        inventorySlot._isEmpty = true;
        inventorySlot._objectInSlot = null;
        inventorySlot._slotImage.sprite = null;
        inventorySlot._slotText.text = "Empty";
    }

    private void ResetWeaponSlot(Weapon_Slot weaponSlot)
    {
        Weapon_Slot slot = weaponSlot.GetComponent<Weapon_Slot>();

        slot._weaponObject = null;
        slot._weaponImage.sprite = null;
        slot._damage.text = null;
        slot._range.text = null;
        slot._spreadFactor.text = null;
        slot._magazine.text = null;
    }


    //"SlotType" (Primary, Special, Reserve) "Fireweapon" (The script attached to the weapon GO)
    private void SetInventoryWeaponSlotValues(SlotType slotType, FireWeapon weapon)
    {
        if (slotType == SlotType.PrimarySlot)
        {
            Weapon_Slot inventoryWS = weaponSlot[0].GetComponent<Weapon_Slot>();

            if (inventoryWS._slotType == SlotType.PrimarySlot)
            {
                inventoryWS._weaponObject = weapon.gameObject;
                inventoryWS._weaponImage.sprite = weapon._image;
                inventoryWS._damage.text = weapon._damage.ToString();
                inventoryWS._range.text = weapon._range.ToString();
                inventoryWS._spreadFactor.text = weapon._spreadFactor.ToString();
                inventoryWS._magazine.text = weapon._magazine.ToString();
                inventoryWS._isEmptyWS = false;
            }
        }
        else if (slotType == SlotType.SpecialSlot)
        {
            Weapon_Slot inventoryWS = weaponSlot[1].GetComponent<Weapon_Slot>();

            if (inventoryWS._slotType == SlotType.SpecialSlot)
            {
                inventoryWS._weaponObject = weapon.gameObject;
                inventoryWS._weaponImage.sprite = weapon._image;
                inventoryWS._damage.text = weapon._damage.ToString();
                inventoryWS._range.text = weapon._range.ToString();
                inventoryWS._spreadFactor.text = weapon._spreadFactor.ToString();
                inventoryWS._magazine.text = weapon._magazine.ToString();
                inventoryWS._isEmptyWS = false;
            }
        }
        else if (slotType == SlotType.ReserveSlot)
        {
            Weapon_Slot inventoryWS = weaponSlot[2].GetComponent<Weapon_Slot>();

            if (inventoryWS._slotType == SlotType.ReserveSlot)
            {
                inventoryWS._weaponObject = weapon.gameObject;
                inventoryWS._weaponImage.sprite = weapon._image;
                inventoryWS._damage.text = weapon._damage.ToString();
                inventoryWS._range.text = weapon._range.ToString();
                inventoryWS._spreadFactor.text = weapon._spreadFactor.ToString();
                inventoryWS._magazine.text = weapon._magazine.ToString();
                inventoryWS._isEmptyWS = false;
            }
        }


    }

    private void SetVaultWeaponSlotValues(SlotType slotType, FireWeapon weapon)
    {
        if (slotType == SlotType.PrimarySlot)
        {
            Weapon_Slot inventoryWS = weaponsInVaultSlots[0].GetComponent<Weapon_Slot>();

            if (inventoryWS._slotType == SlotType.PrimarySlot)
            {
                inventoryWS._weaponObject = weapon.gameObject;
                inventoryWS._weaponImage.sprite = weapon._image;
                inventoryWS._damage.text = weapon._damage.ToString();
                inventoryWS._range.text = weapon._range.ToString();
                inventoryWS._spreadFactor.text = weapon._spreadFactor.ToString();
                inventoryWS._magazine.text = weapon._magazine.ToString();
                inventoryWS._isEmptyWS = false;
            }
        }
        else if (slotType == SlotType.SpecialSlot)
        {
            Weapon_Slot inventoryWS = weaponsInVaultSlots[1].GetComponent<Weapon_Slot>();

            if (inventoryWS._slotType == SlotType.SpecialSlot)
            {
                inventoryWS._weaponObject = weapon.gameObject;
                inventoryWS._weaponImage.sprite = weapon._image;
                inventoryWS._damage.text = weapon._damage.ToString();
                inventoryWS._range.text = weapon._range.ToString();
                inventoryWS._spreadFactor.text = weapon._spreadFactor.ToString();
                inventoryWS._magazine.text = weapon._magazine.ToString();
                inventoryWS._isEmptyWS = false;
            }
        }
        else if (slotType == SlotType.ReserveSlot)
        {
            Weapon_Slot inventoryWS = weaponsInVaultSlots[2].GetComponent<Weapon_Slot>();

            if (inventoryWS._slotType == SlotType.ReserveSlot)
            {
                inventoryWS._weaponObject = weapon.gameObject;
                inventoryWS._weaponImage.sprite = weapon._image;
                inventoryWS._damage.text = weapon._damage.ToString();
                inventoryWS._range.text = weapon._range.ToString();
                inventoryWS._spreadFactor.text = weapon._spreadFactor.ToString();
                inventoryWS._magazine.text = weapon._magazine.ToString();
                inventoryWS._isEmptyWS = false;
            }
        }

    }
}        
         
         
         