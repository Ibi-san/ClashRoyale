using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Network : MonoBehaviour
{
    #region Singleton

    public static Network Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion
    

    public void Post(string uri, Dictionary<string, string> data, Action<string> success, Action<string> error = null) => StartCoroutine(PostAsync(uri, data, success, error));

    private IEnumerator PostAsync(string uri, Dictionary<string, string> data, Action<string> sucess, Action<string> error = null)
    {
        //GET
        
        // using (UnityWebRequest www = UnityWebRequest.Get(url))
        // {
        //     yield return www.SendWebRequest();
        //     
        //     if (www.result != UnityWebRequest.Result.Success) error?.Invoke(www.error);
        //     else sucess?.Invoke(www.downloadHandler.text);
        // }
        
        //POST
        
        // WWWForm form = new WWWForm();
        // form.AddField("key-login", "value-password");

        // List<IMultipartFormSection> sections = new();
        // IMultipartFormSection section = new MultipartFormDataSection("key-login", "value-password");
        // sections.Add(section);
        
        using (UnityWebRequest www = UnityWebRequest.Post(uri, data))
        {
            yield return www.SendWebRequest();
            
            if (www.result != UnityWebRequest.Result.Success) error?.Invoke(www.error);
            else sucess?.Invoke(www.downloadHandler.text);
        }
    }
}
