using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class FileDoc : MonoBehaviour
{

    public Setting setting = new Setting();
    public HIHI hihi = new HIHI();

    private void Start()
    {
        setting = (Setting)XuLyFile<Setting>(Application.streamingAssetsPath, "demofile.xml", setting);
        hihi = (HIHI)XuLyFile<HIHI>(Application.streamingAssetsPath, "hihi.xml", hihi);
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(setting.name + " --- " + setting.sott + " --- " + setting.vt + " --- " + setting.color);
        }
    }

    public T XuLyFile<T>(string pathFolder, string nameFile, object obj)
    {
        T classobject;
        string xmlString = null;
        string path = pathFolder + "/" + nameFile;
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(pathFolder);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, obj);
                memoryStream.Position = 0;
                xmlString = new StreamReader(memoryStream).ReadToEnd();
            }
            XElement xElement = XElement.Parse(xmlString);
            xElement.Save(path);
        }
        using (StringReader stringReader = new StringReader(xmlString))
        {
            classobject = (T)xmlSerializer.Deserialize(stringReader);
        }
        return classobject;
    }

}

public class Setting
{
    public string name = "Vu";
    public int sott = 1;
    public Vector3 vt = Vector3.zero;
    public Color color = Color.red;
}

public class HIHI
{
    public int so = 1;
}
