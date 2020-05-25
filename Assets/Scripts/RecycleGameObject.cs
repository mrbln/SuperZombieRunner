using System.Collections.Generic;
using UnityEngine;

public interface IRecycle
{
    void Restart();
    void ShutDown();
}

public class RecycleGameObject : MonoBehaviour
{
    private List<IRecycle> recycleComponents;

    private void Awake()
    {
        var components = GetComponents<MonoBehaviour>();
        recycleComponents = new List<IRecycle>();

        foreach (var component in components)
        {
            if (component is IRecycle)
            {
                recycleComponents.Add(component as IRecycle);
            }
        }
    }

    // So when we attach this script to a game object, 
    // and we attempt to reinstantiate a game object 
    // that already exists, we're going to call restart.
    public void Restart()
    {
        gameObject.SetActive(true);

        foreach (var component in recycleComponents)
        {
            component.Restart();
        }
    }

    // Likewise, when we're trying to delete one, 
    // we're going to call shut down instead of 
    // actually using the full destroy call. 
    public void ShutDown()
    {
        gameObject.SetActive(false);

        foreach (var component in recycleComponents)
        {
            component.ShutDown();
        }
    }
}
