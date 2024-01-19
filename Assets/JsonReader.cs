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

        public int minR=0;
        public int minC=0;
        public  int minNumber=0;
        
    private int[,] matrix;
 private  bool  isPair;


    private void Start()
    {
        string jsonData = ReadJsonFile(filePath);

        
        List<Block>  blocksList = JsonUtility.FromJson<BlocksData>(jsonData).blocks;

        if (blocksList != null)
        {
            
          maxR = blocksList.Max(block => block.R);
          maxC = blocksList.Max(block => block.C);
          maxNumber = blocksList.Max(block => block.number);
             minR = blocksList.Min(block => block.R);
          minC = blocksList.Min(block => block.C);
          minNumber = blocksList.Min(block => block.number);

            Debug.Log("El valor máximo de R es:"+ maxR);
            Debug.Log("El valor máximo de C es:"+maxC);
            Debug.Log("El valor máximo de Number es:"+maxNumber);
            Debug.Log("El valor Min de R es:"+ minR);
            Debug.Log("El valor Min de C es:"+minC);
            Debug.Log("El valor Min de Number es:"+minNumber);


         bool containsBlockWith2 = blocksList.Any(block => block.R == 2 && block.C == 2);
         int mult = maxR*maxC;
            if(mult%2==0){

                isPair = true;
            }


        if (maxNumber <= 9 && minNumber >= 0 && maxC <= 8 && maxR <= 8 && containsBlockWith2 && isPair==true)
        {
            CreateMatrix(blocksList);
        }else{

            Debug.LogError("Cant create matrix");
        }
      

           
    

    
    
        }
        else
        {
            Debug.LogError("List is"+blocksList);
        }
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
        
    // Getters R,C,Matrix called from CreateMatrix.cs
    public int GetMaxR()
    {
        return maxR;
    }


    public int GetMaxC()
    {   
        Debug.Log($"El valor máximo de C"+maxC);
        return maxC;
    }

    public int[,] GetMatrix()
    {
        return matrix;
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
