using UnityEngine;

// TODO : Ajouter tous vos objets globaux ici.
//        Basez-vous sur le code existant.
public static class Finder
{
    private static EventChannels eventChannels;
    private static ObjectPools objectPools;
    private static GameController gameController;
    private static MusicController musicController;
    private static GameStateController gameStateController;
    private static AudioSource globalAudioSource;
    private static GameObject globalAudioSourceObject;
    
    public static AudioSource GlobalAudioSource
    {
        get
        {
            if (globalAudioSource == null)
                globalAudioSource = GlobalAudioSourceObject.AddComponent<AudioSource>();
            return globalAudioSource;
        }
    }

    public static EventChannels EventChannels
    {
        get
        {
            if (eventChannels == null)
                eventChannels = FindWithTag<EventChannels>("GameController");
            return eventChannels;
        }
    }

    public static ObjectPools ObjectPools
    {
        get
        {
            if (objectPools == null)
                objectPools = FindWithTag<ObjectPools>("GameController");
            return objectPools;
        }
    }

    public static GameController GameController
    {
        get
        {
            if (gameController == null)
                gameController = FindWithTag<GameController>("GameController");
            return gameController;
        }
    }

    public static MusicController MusicController
    {
        get
        {
            if (musicController == null)
                musicController = FindWithTag<MusicController>("GameController");
            return musicController;
        }
    }

    public static GameStateController GameStateController
    {
        get
        {
            if (gameStateController == null)
                gameStateController = FindWithTag<GameStateController>("GameController");
            return gameStateController;
        }
    }

    private static T FindWithTag<T>(string tag) where T : class
    {
        var gameObject = GameObject.FindWithTag(tag);
        if (gameObject == null) return null;
        return gameObject.GetComponent<T>();
    }
    
    private static GameObject GlobalAudioSourceObject
    {
        get
        {
            if (globalAudioSourceObject == null)
                globalAudioSourceObject = new GameObject { name = "GlobalAudioSource" };
            return globalAudioSourceObject;
        }
    }
}