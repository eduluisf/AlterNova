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
        private int mult = 0;
        [SerializeField] private GameObject UICanvas;

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

          
            // Here we check the condition: 
            // R y C need two position as minimum and eighth as maximum
            // R xC must be pair 
            //Number need 0 as minimun value and 9 as maximun

         bool containsBlockWith2 = blocksList.Any(block => block.R == 2 && block.C == 2);
         mult = maxR*maxC;
           
            if(mult%2==0){
                isPair = true;
            }
        if (maxNumber <= 9 && minNumber >= 0 && maxC <= 8 && maxR <= 8 && containsBlockWith2 && isPair==true)
        {
            //IF we are available we create the matrix, this matrix it'll be read in Creatematrix.cs
            CreateMatrix(blocksList);
        }else{

            Debug.LogError("Cant create matrix");
            UICanvas.SetActive(true);
        }
      
        }
        else
        {
            Debug.LogError("List is"+blocksList);
        }
    }

    void CreateMatrix(List<Block> blocks)
    {
    //Start the matrix
        matrix = new int[maxR, maxC];

        // fill the matrix with the data
        foreach (var block in blocks)
        {
            matrix[block.R - 1, block.C - 1] = block.number;
        }

   
      
    }

 

    // Read the json 
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
            Debug.LogError($"cant read json: {e.Message}");
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

   public int GetPair()
    {   
        Debug.Log($"El valor máximo de C"+maxC);
        return (mult/2);
    }



    // Class to  deserialize JSON
    [System.Serializable]
    private class BlocksData
    {
        public List<Block> blocks;
  
    }

    // Class to deserialize each block
    [System.Serializable]
    private class Block
    {
        public int R;
        public int C;
        public int number;
    }

}
