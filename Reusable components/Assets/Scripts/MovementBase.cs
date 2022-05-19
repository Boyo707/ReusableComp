using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBase : MonoBehaviour
{


    protected float borders = 5;

    //list 
    //private List<Component> components;
    //private List<IMovements> _movementComponents;

    void Start()
    {


        /*components = new List<Component>();
        foreach (var component in GetComponents<Component>())
        {
            if (component != this)
                if (component is IMovements updateComponents)
                    _movementComponents.Add(updateComponents);
        }
        for (int i = 0; i < components.Count; i++)
        {
            Debug.Log(components[i]);
        }*/
        
    }

    public KeyboardInput KeyboardInput
    {
        get { if (GetComponent<KeyboardInput>() != null) { return GetComponent<KeyboardInput>(); } return null; }
    }

    public Rigidbody2D rigidbody
    {
        get { return GetComponent<Rigidbody2D>(); }
    }
    


    // Update is called once per frame
    private void Update()
    {
        
        //UpdateComp();
    }

    protected virtual void UpdateComp() 
    {
        Debug.Log("sharted");
    }

}
