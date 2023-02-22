using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public static class DatabaseManager
{
    private static MonoBehaviour m_MonoBehaviour = new MonoBehaviour();

    private static string m_BaseUrl = "http://localhost:80/Database/";

    private static WWWForm m_DataForm;
    private static UnityWebRequest m_WebRequest;

    public static void RemoveFormAndRequest()
    {
        m_DataForm = null;
        m_WebRequest = null;
    }

    public static UnityWebRequest GetWebRequest()
    {
        return m_WebRequest;
    }

    private static int GetPrimitiveType<T>(T variable)
    {
        // WWWForm only supports string and integer
        if (variable.GetType() == typeof(string))
            return 1;
        else if (variable.GetType() == typeof(int))
            return 2;

        return -1;
    }

    public static void AddData<T>(string fieldName, T fieldData)
    {
        if (m_DataForm == null)
            m_DataForm = new WWWForm();

        switch (GetPrimitiveType(fieldData))
        {
            // String
            case 1: m_DataForm.AddField(fieldName, fieldData.ToString()); break;
            // Integer
            case 2: m_DataForm.AddField(fieldName, int.Parse(fieldData.ToString())); break;
        }
    }

    private static IEnumerator SendRequestAndWait()
    {
        yield return m_WebRequest;
    }

    public static bool SaveData(string phpUrl, string method)
    {
        if (m_DataForm == null)
            return false;

        // UnityWebRequest.Put method is not supported for now, will add if need
        if (method.ToLower() == "get")
            m_WebRequest = UnityWebRequest.Get(m_BaseUrl + phpUrl);
        else if (method.ToLower() == "post")
            m_WebRequest = UnityWebRequest.Post(m_BaseUrl + phpUrl, m_DataForm);
        else
            return false;

        m_MonoBehaviour.StartCoroutine(SendRequestAndWait());
        return true;
    }
}