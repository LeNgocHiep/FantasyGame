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


    public void FileVu(string path, string pathFolder)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(pathFolder);
            GhiFile(pathFolder + "/" + path);
        }
        DocFile(pathFolder + "/" + path);
    }

    void DocFile(string path)
    {
        var serializer = new XmlSerializer(typeof(DuLieuNguoiChoi));
        var stream = new FileStream(path, FileMode.Open);
       duLieu = serializer.Deserialize(stream) as DuLieuNguoiChoi;
        stream.Close();
    }

    void GhiFile(string path)
    {
        var serializer = new XmlSerializer(typeof(DuLieuNguoiChoi));
        var stream = new FileStream(path, FileMode.Create);
        serializer.Serialize(stream, this);
        stream.Close();
    }


}

public class DuLieuNguoiChoi
{
    public int[] tuongDaCo = { 1, 0, 0, 0, 0, 1, 1, 1, 0, 1, 0, 0, 1, 1 };
}