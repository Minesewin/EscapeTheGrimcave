using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlatforms : MonoBehaviour
{
    public static ResetPlatforms instance;
    public List<GameObject> platforms=new List<GameObject>();
    UnstablePlatform[] unstablePlatforms;
    [SerializeField] int unstablePlatformsLenght;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            Debug.Log(instance);
        }
    }
    public static void addPlatform(GameObject platform)
    {
        Debug.Log(instance);
        instance.AddP(platform);
        Debug.Log(platform);
    }
    void AddP(GameObject gameObject)
    {
        platforms.Add(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        unstablePlatforms = new UnstablePlatform[platforms.Count];
        unstablePlatformsLenght = unstablePlatforms.Length;
        for (int i = 0; i < platforms.Count; i++)
        {
            unstablePlatforms[i] = platforms[i].GetComponent<UnstablePlatform>();
        }
        platforms.Clear();
    }
    public static void Reset()
    {
        instance.ResetAll();
    }
    void ResetAll()
    {
        Debug.Log("Resetting all platforms");
        for (int i = 0; i < unstablePlatforms.Length; i++)
        {
            unstablePlatforms[i].Reset();
        }
    }
    //void ResetAll()
    //{
    //    for (int i = 0; i < unstablePlatforms.Length; i++)
    //    {
    //        unstablePlatforms[i].SendMessage("Reset");
    //    }
    //}
    // Update is called once per frame
    void Update()
    {
        
    }
}
