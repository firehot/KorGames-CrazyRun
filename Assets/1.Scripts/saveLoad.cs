using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Text;
using System.Security.Cryptography;

public static class saveLoad {

    public static void SavePlayer(PlayerData data) {

        PlayerPrefs.SetString("udata", Encrypt(JsonUtility.ToJson(data)));

       // Debug.Log("Save Player methoduna gelen data : "+JsonUtility.ToJson(data));
    }

    public static PlayerData LoadPlayer()
    { 

        if (PlayerPrefs.HasKey("udata"))
        {
            //Debug.Log("prefden user data çekildi");
            PlayerData data = JsonUtility.FromJson<PlayerData>(Decrypt(PlayerPrefs.GetString("udata")).ToString());
            //Debug.Log(Decrypt(PlayerPrefs.GetString("udata")).ToString());

            return data;
        }
        else
        {//data yok o yüzden sıfır data girdirip onu döndürüyoruz

            //Debug.Log("sıfırdan data oluşturuldu");

            PlayerData data = new PlayerData();

            PlayerPrefs.SetString("udata", Encrypt(JsonUtility.ToJson(data)));

            return data;
        }

    }


    public static string convertJson(PlayerData data)
    {
        return JsonUtility.ToJson(data);
    }

    public static string hash = "{{crazylabshash!*";

    public static string Encrypt(string input)
    {
        byte[] data = UTF8Encoding.UTF8.GetBytes(input);
        using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        {
            byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            using (TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider() { Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
            {
                ICryptoTransform tr = trip.CreateEncryptor();
                byte[] results = tr.TransformFinalBlock(data, 0, data.Length);
                return Convert.ToBase64String(results, 0, results.Length);
            }
        }
    }

    public static string Decrypt(string input)
    {
        byte[] data = Convert.FromBase64String(input);
        using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        {
            byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            using (TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider() { Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
            {
                ICryptoTransform tr = trip.CreateDecryptor();
                byte[] results = tr.TransformFinalBlock(data, 0, data.Length);
                return UTF32Encoding.UTF8.GetString(results);
            }
        }
    }
}

[Serializable]
public class PlayerData {

    public int currentLevel;
    public int diamondCount;

    public float forwardSpeed;
    public float sideSpeed;

    public float cameraPosX;
    public float cameraPosY;
    public float cameraPosZ;

    public float cameraRotX;
    public float cameraRotY;
    public float cameraRotZ;

    public float millRotationSpeed;
    public float boxMovementSpeed;
    public float groundedRotationSpeed;


    public PlayerData() {
        currentLevel = 0;
        diamondCount = 0;

        forwardSpeed = 10f;
        sideSpeed = 0.5f;

        cameraPosX = 0;
        cameraPosY = 9;
        cameraPosZ = -12;

        cameraRotX = 20;
        cameraRotY = 0;
        cameraRotZ = 0;

        millRotationSpeed = 50;
        boxMovementSpeed = 0.5f;
        groundedRotationSpeed = 50;
    }

}


