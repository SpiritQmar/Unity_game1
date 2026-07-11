using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public string Username;
    public string PasswordHash;
    public string Email;
}

[System.Serializable]
public class UserDatabase
{
    public List<UserData> Users = new List<UserData>();
}

public class UserAuth : MonoBehaviour
{
    private string filePath;
    private UserDatabase userDatabase = new UserDatabase();

    private void Start()
    {
         
        filePath = Path.Combine(Application.persistentDataPath, "UserDatabase.json");
        LoadUserData();
    }

    private void LoadUserData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            userDatabase = JsonUtility.FromJson<UserDatabase>(json);
        }
    }

    private void SaveUserData()
    {
        string json = JsonUtility.ToJson(userDatabase, true);
        File.WriteAllText(filePath, json);
    }

    public bool Register(string username, string password, string email)
    {
        if (userDatabase.Users.Exists(user => user.Username == username))
        {
            Debug.Log("Username already exists.");
            return false;
        }

        UserData newUser = new UserData
        {
            Username = username,
            PasswordHash = HashPassword(password),
            Email = email
        };

        userDatabase.Users.Add(newUser);
        SaveUserData();
        Debug.Log("Registration successful");
        return true;
    }

    public bool Login(string username, string password)
    {
        UserData user = userDatabase.Users.Find(user => user.Username == username);
        if (user != null && user.PasswordHash == HashPassword(password))
        {
            Debug.Log("Login successful");
            return true;
        }
        else
        {
            Debug.Log("Invalid username or password");
            return false;
        }
    }

    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
