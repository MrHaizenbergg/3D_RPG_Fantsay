using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Debug.LogWarning("EquipManager more one");
    }
    #endregion

    public Equipment[] defaultItems;
    public SkinnedMeshRenderer[] currentMeshes;

    public GameObject leftHand, rightHand;

    public GameObject currentWeapon = null;
    GameObject currentShield = null;

    public bool shieldEquip = false;
    public bool weaponTwoHand = false;

    Equipment[] currentEquipment;
    //SkinnedMeshRenderer[] curentMeshes;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    InventoryRPG inventory;

    private void Start()
    {
        inventory = InventoryRPG.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        //curentMeshes = new SkinnedMeshRenderer[numSlots];

        //EquipDefailtItems();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        Equipment oldItem = Unequip(slotIndex);

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        Debug.Log("SlotIndex: " + slotIndex);
        if (slotIndex == 3) //WeaponSlot
        {
            currentEquipment[3] = newItem;
            currentWeapon = Instantiate(newItem.itemObject);

            if (newItem.twoHand)
            {
                weaponTwoHand = true;
                currentWeapon.transform.rotation = Quaternion.Euler(21, 39, -95);
                //currentWeapon.transform.localScale = new Vector3(100, 100, 100);
            }
            else
            {
                weaponTwoHand = false;
                currentWeapon.transform.rotation = Quaternion.Euler(-180, -90, 90);
            }

            currentWeapon.transform.SetParent(rightHand.transform, false);
            currentWeapon.GetComponent<MeshFilter>().sharedMesh.RecalculateBounds();
            currentWeapon.GetComponent<MeshFilter>().sharedMesh.RecalculateNormals();
            currentWeapon.GetComponent<MeshFilter>().sharedMesh.RecalculateTangents();

            if (newItem.twoHand && !shieldEquip)
            {
                Unequip(4);
                Debug.Log("ShieldUnequip");
            }

            Debug.Log("SpawnSword");
            return;
        }
        if (slotIndex == 4) //ShieldSlot
        {
            currentEquipment[4] = newItem;
            currentShield = Instantiate(newItem.itemObject);
            currentShield.transform.SetParent(leftHand.transform, false);
            currentShield.GetComponent<MeshFilter>().sharedMesh.RecalculateBounds();
            currentShield.GetComponent<MeshFilter>().sharedMesh.RecalculateNormals();
            currentShield.GetComponent<MeshFilter>().sharedMesh.RecalculateTangents();
            shieldEquip = true;

            if (weaponTwoHand)
            {
                Unequip(4);
            }
            Debug.Log("SpawnShield");
            return;
        }

        //SetEquipmentBlendShapes(newItem, 100); //BlendShape

        currentEquipment[slotIndex] = newItem;

        currentMeshes[slotIndex].sharedMesh = newItem.mesh.sharedMesh; //First Realization

        //SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh); //Second Realization
        //newMesh.transform.parent = targetMeshes[slotIndex].transform.parent;

        //newMesh.bones = targetMeshes[slotIndex].bones;
        //newMesh.rootBone = targetMeshes[slotIndex].rootBone;
        //curentMeshes[slotIndex] = newMesh;
    }
    public Equipment Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            if (currentMeshes[slotIndex] != null && !currentMeshes[3])
            {
                currentMeshes[slotIndex].sharedMesh = null;
                //Destroy(currentMeshes[slotIndex].gameObject);
            }

            Equipment oldItem = currentEquipment[slotIndex];

            Debug.Log("CurrentEquipment: " + currentEquipment[slotIndex]);
            if (slotIndex == 3)
            {
                weaponTwoHand = false;
                Destroy(currentWeapon);
                shieldEquip = false;
                Destroy(currentShield);


                if (currentEquipment[3] == null)
                {
                    currentEquipment[3] = oldItem;
                    currentWeapon = Instantiate(oldItem.itemObject);

                    Debug.Log("CurrentEquipment2: " + currentEquipment[slotIndex]);

                    if (oldItem.twoHand)
                    {
                        weaponTwoHand = true;
                        currentWeapon.transform.rotation = Quaternion.Euler(-214, -133, 87);
                    }
                    else
                    {
                        weaponTwoHand = false;
                        currentWeapon.transform.rotation = Quaternion.Euler(-180, -90, 90);
                    }
                    //Debug.Log("Twohand: " + weaponTwoHand);

                    //if (oldItem.twoHand && shieldEquip)
                    //{
                    //    //currentEquipment[4] = oldItem;
                    //    shieldEquip = false;
                    //    Destroy(currentShield);
                    //    Unequip(4);
                    //    Debug.Log("ShieldUnequip");
                    //}

                    //currentWeapon.transform.rotation = Quaternion.Euler(-180, -90, 90);
                    currentWeapon.transform.SetParent(rightHand.transform, false);
                    Debug.Log("CurrentSword: " + weaponTwoHand);
                }
            }
            else if (slotIndex == 4)
            {
                shieldEquip = false;
                Destroy(currentShield);
                //weaponSlotPlayer.twoHandSword = null;

                if (currentEquipment[4] == null)
                {
                    currentEquipment[4] = oldItem;
                    currentShield = Instantiate(oldItem.itemObject);
                    //weaponSlotPlayer.twoHandSword = currentWeapon;
                    //go.transform.localScale = new Vector3(100,100, 100);
                    currentShield.transform.rotation = Quaternion.Euler(-180, -90, 90);
                    currentShield.transform.SetParent(rightHand.transform, false);
                    shieldEquip = true;
                    Debug.Log("SpawnSwordOld");
                }
            }
            else
            {
                currentMeshes[slotIndex].sharedMesh = oldItem.mesh.sharedMesh;
            }

            //SetEquipmentBlendShapes(oldItem, 0); //BlendShape
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

            return oldItem;
        }

        return null;
    }
    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }

        EquipDefailtItems();
    }

    private void EquipDefailtItems()
    {
        foreach (Equipment item in defaultItems)
        {
            Equip(item);
        }
    }

    //private void SetEquipmentBlendShapes(Equipment item,int weight)
    //{
    //    foreach(EquipmentMeshRegion blendShape in item.coveredMeshRegions)
    //    {
    //        //for(int i = 0;i<currentMeshes.Length; i++)
    //        //{
    //            currentMeshes[0].SetBlendShapeWeight((int)blendShape,weight);
    //        //}
    //    }
    //}
}
