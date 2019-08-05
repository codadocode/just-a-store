using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractMenu : MonoBehaviour
{
    private Container actualContainer;
    private Dropdown menuDropdown;
    public void getDropdownComponent()
    {
        this.menuDropdown = GetComponent<Dropdown>();
        this.menuDropdown.onValueChanged.AddListener(instanceProduct);
    }

    public void instanceProduct(int arg0)
    {
        print(menuDropdown.value);
        this.actualContainer.getItem((menuDropdown.value - 1));
    }


    public void setContainer(Container container)
    {
        getDropdownComponent();
        this.actualContainer = container;
        List<string> tmpStringList = new List<string>();
        tmpStringList.Add("Container:" + container.getItemList().Count.ToString() + "/" + container.getMaxSize());
        foreach (ScriptableProduct product in container.getItemList())   {
            tmpStringList.Add(product.getNome());
        }
        this.menuDropdown.AddOptions(tmpStringList);
    }
    public int getIndexValue()
    {
        return menuDropdown.value;
    }

    public Dropdown getDropdown()
    {
        return this.menuDropdown;
    }
}
