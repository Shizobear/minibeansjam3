using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private List<GameBehaviour> behaviours;


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void CreateGameManager()
    {
        if (instance == null)
        {
            GameObject gameManager_object = new GameObject("GameManager");
            instance = gameManager_object.AddComponent<GameManager>();
            instance.Initialize();
        }
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public void Initialize()
    {
        behaviours = new List<GameBehaviour>();
    }

    public void Subscribe(GameBehaviour _behaviour)
    {
        if (!behaviours.Contains(_behaviour))
            behaviours.Add(_behaviour);
    }

    public void Unsubscribe(GameBehaviour _behaviour)
    {
        behaviours.Remove(_behaviour);
    }
    // Update is called once per frame
    void Update()
    {
        foreach (GameBehaviour behaviour in behaviours.ToArray())
            behaviour.UpdateBehaviour();

    }
}
