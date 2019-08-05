using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField]
    private int maxSize = 0;
    private List<ScriptableProduct> inventario;
    private GameObject actualInteractMenu = null;
    // Start is called before the first frame update
    void Start()
    {
        this.inventario = new List<ScriptableProduct>();
    }
    public void guardarItem(Product item)
    {
        if (inventario.Count < maxSize)
        {
            inventario.Add(item.getProduto());
            Destroy(item.gameObject);
            Debug.Log(inventario.ToArray().ToString());
        }else
        {
            Debug.Log("Container está cheio!");
        }
    }
    public void getItem(int indexValue)
    {
        if (inventario.Count > 0)
        {
            print(inventario[indexValue].ToString());
            GameConfig.actualPlayer.setHand((inventario[indexValue].instancePrefab().GetComponent<Pickable>()));
            inventario.Remove(inventario[indexValue]);
            GameConfig.actualPlayer.closeActualContainer();
        }else
        {
            Debug.Log("Não há itens para retirar!");
        }
    }
    public List<ScriptableProduct> getItemList()
    {
        return this.inventario;
    }
    public void openContainer()
    {
        GameObject tmpInteractMenu = Resources.Load<GameObject>("Prefabs/UI/InteractMenu");
        this.actualInteractMenu = Instantiate<GameObject>(tmpInteractMenu, GameConfig.actualCanvas.transform);
        this.actualInteractMenu.GetComponent<InteractMenu>().setContainer(this);
    }
    public bool hasActualInteractMenu()
    {
        if (actualInteractMenu != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void closeContainer()
    {
        Destroy(actualInteractMenu);
        actualInteractMenu = null;
    }
    public InteractMenu getActualInteractMenu()
    {
        if (hasActualInteractMenu())
        {
            return this.actualInteractMenu.GetComponent<InteractMenu>();
        }
        return null;
    }
    public int getMaxSize()
    {
        return this.maxSize;
    }
}
