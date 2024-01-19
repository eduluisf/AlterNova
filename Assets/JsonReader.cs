using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class JsonReader : MonoBehaviour
{

    string filePath = "Assets/block.json";


    
        public int maxR=0;
        public int maxC=0;
        public  int maxNumber=0;
        
    private int[,] matrix; // La matriz que almacenará los datos
    private void Start()
    {
        string jsonData = ReadJsonFile(filePath);

        
        var blocksList = JsonUtility.FromJson<BlocksData>(jsonData).blocks;

        if (blocksList != null && blocksList.Any())
        {
            
          maxR = blocksList.Max(block => block.R);
          maxC = blocksList.Max(block => block.C);
          maxNumber = blocksList.Max(block => block.number);

            Debug.Log($"El valor máximo de R es: {maxR}");
            Debug.Log($"El valor máximo de C es: {maxC}");
            Debug.Log($"El valor máximo de C es: {maxNumber}");
         CreateMatrix(blocksList);
    

        // Llenar la matriz con los datos del JSON
    
        }
        else
        {
            Debug.LogError("La lista de bloques está vacía o no se pudo deserializar correctamente.");
        }
    }

    public int GetMaxR()
    {
        return maxR;
    }

    // Método para obtener el valor máximo de C
    public int GetMaxC()
    {   
        Debug.Log($"El valor máximo de C"+maxC);
        return maxC;
    }

    public int[,] GetMatrix()
    {
        return matrix;
    }
    void CreateMatrix(List<Block> blocks)
    {
        // Inicializar la matriz con ceros
        matrix = new int[maxR, maxC];

        // Llenar la matriz con los datos del JSON
        foreach (var block in blocks)
        {
            matrix[block.R - 1, block.C - 1] = block.number;
        }

        // Imprimir la matriz en la consola (solo para verificar)
      
    }

 

    // Función para leer el contenido del archivo JSON
    public string ReadJsonFile(string filePath)
    {
        string jsonData = "";

        try
        {
            // Leer todo el contenido del archivo
            using (StreamReader reader = new StreamReader(filePath))
            {
                jsonData = reader.ReadToEnd();
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error al leer el archivo JSON: {e.Message}");
        }

        return jsonData;
    }
        
    // Clase para deserializar el JSON
    [System.Serializable]
    private class BlocksData
    {
        public List<Block> blocks;
  
    }

    // Clase para deserializar cada bloque
    [System.Serializable]
    private class Block
    {
        public int R;
        public int C;
        public int number;
    }

}
