using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Preload : MonoBehaviour {

    public static Preload Global;
    public Sprite[] listImageTuong;
    public GameObject[] listTuong;
    //public List<GameObject> lstTuongDangCo;
    public DuLieuNguoiChoi duLieu;
    public List<int> vitriTuongDangCo;


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    // Use this for initialization
    void Start () {
        duLieu = new DuLieuNguoiChoi();
        Global = this;
        duLieu = (DuLieuNguoiChoi)XuLyFile<DuLieuNguoiChoi>(Application.streamingAssetsPath, "DuLieuNguoiChoi.xml", duLieu);

        for(int i = 0; i < duLieu.tuongDaCo.Length; i++)
        {
            if (duLieu.tuongDaCo[i] == 1)
                vitriTuongDangCo.Add(i);
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

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

    public static string GetXMLFromObject(object o)
    {
        StringWriter sw = new StringWriter();
        XmlTextWriter tw = null;
        try
        {
            XmlSerializer serializer = new XmlSerializer(o.GetType());
            tw = new XmlTextWriter(sw);
            serializer.Serialize(tw, o);
        }
        catch (Exception ex)
        {
            //Handle Exception Code
        }
        finally
        {
            sw.Close();
            if (tw != null)
            {
                tw.Close();
            }
        }
        return sw.ToString();
    }
    public static System.Object ObjectToXML(string xml, Type objectType)
    {
        StringReader strReader = null;
        XmlSerializer serializer = null;
        XmlTextReader xmlReader = null;
        System.Object obj = null;
        try
        {
            strReader = new StringReader(xml);
            serializer = new XmlSerializer(objectType);
            xmlReader = new XmlTextReader(strReader);
            obj = serializer.Deserialize(xmlReader);
        }
        catch (Exception exp)
        {
            //Handle Exception Code
        }
        finally
        {
            if (xmlReader != null)
            {
                xmlReader.Close();
            }
            if (strReader != null)
            {
                strReader.Close();
            }
        }
        return obj;
    }


}

public class DuLieuNguoiChoi
{
    public int[] tuongDaCo = { 1, 0, 0, 0, 0, 1, 1, 1, 0, 1, 0, 0, 1, 1 };
}