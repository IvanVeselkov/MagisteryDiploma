using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
/// <summary>
/// Задача скрипта следующая:
///     Забить в программу данные из файла для дальнейшей обработки.
///     Показатели гироскопа считаются как радианы на сек
///     Следовательно исходя из времени работы я могу определить угол поворота
///     Задержка между данными состовляет 20мс 5 строчек - 1 секунда    
/// 
/// </summary>
public class ProcessingOfIndications : MonoBehaviour
{
   


    [SerializeField] private bool GoProcess;

    string path = "D:/Programs/UnityProject/InformationFromPorts/Assets/Documents/Test.txt";

    List<string> FileData = new List<string>();

    int count = 0;
    //List<float> Accel = new List<float>();
    float[,] Accel;//= new float[910, 3];//число 910 нужно определять из файла автоматически
    float[,] OptAccel;
    //List<float> Gyro = new List<float>();
    float[,] Gyro;// = new float[910, 3];
    float[,] OptGyro;
    //List<float> Marnito = new List<float>();
    float[,] Magnito;// = new float[910, 3];
    float[,] OptMagnito;
    void Start()
    {
        FullFileData();
        ProcessingDataFromFileData();
        OptimizeData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    #region WORK with FILES
    /// <summary>
    /// Формирование списка всех данных из файла
    /// </summary>
    void FullFileData()
    {
        foreach(string i in File.ReadAllLines(path))
        {
            FileData.Add(i);
            count++;
        }
        Accel = new float[count, 3];
        Gyro = new float[count, 3];
        Magnito = new float[count, 3];

        OptAccel = new float[count / 5, 3];
        OptGyro = new float[count / 5, 3];
        OptMagnito = new float[count / 5, 3];
    }

    /// <summary>
    /// Формирование массивов данных показателей из файла.
    /// </summary>
    void ProcessingDataFromFileData()
    {
        for (int i = 0; i < count; i++)
        {
            string[] arr_str = System.Text.RegularExpressions.Regex.Split(FileData[i], "\\t");
            Console.WriteLine(float.TryParse(System.Text.RegularExpressions.Regex.Split(FileData[i], "\\t")[0], System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out float c));
            //float a = float.Parse(System.Text.RegularExpressions.Regex.Split(FileData[i], "\\t")[0], System.Globalization.NumberStyles.Float);
            
            
            for(int j=0;j<3;j++)
            {
                Accel[i, j] = float.Parse(arr_str[j], System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo);
                Gyro[i,j]= float.Parse(arr_str[j+3], System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo);
                Magnito[i,j]= float.Parse(arr_str[j+6], System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo);
            }
            //Accel[i, 0] = float.Parse(System.Text.RegularExpressions.Regex.Split(FileData[i], "\\t")[0]);
            //Accel[i, 1] = float.Parse(System.Text.RegularExpressions.Regex.Split(FileData[i], "\\t")[1]);
            //Accel[i, 2] = float.Parse(System.Text.RegularExpressions.Regex.Split(FileData[i], "\\t")[2]);

            //Gyro[i,0] = float.Parse(System.Text.RegularExpressions.Regex.Split(FileData[i], "\\t")[3]);
            //Gyro[i,1] = float.Parse(System.Text.RegularExpressions.Regex.Split(FileData[i], "\\t")[4]);
            //Gyro[i,2] = float.Parse(System.Text.RegularExpressions.Regex.Split(FileData[i], "\\t")[5]);

            //Magnito[i,0] = float.Parse(System.Text.RegularExpressions.Regex.Split(FileData[i], "\\t")[6]);
            //Magnito[i,1] = float.Parse(System.Text.RegularExpressions.Regex.Split(FileData[i], "\\t")[7]);
            //Magnito[i,2] = float.Parse(System.Text.RegularExpressions.Regex.Split(FileData[i], "\\t")[8]);
        }
    }

    /// <summary>
    /// Метод оптимазации данных.
    /// В дальнейшем будет дорабатываться
    /// варианты: 1) просто беру среднее среди 5 последовательно идущих значений
    /// </summary>
    void OptimizeData()
    {
        for(int i=0;i<count/5;i++)
        {
            for(int j=0;j<count;j+=5)
            {
                OptAccel[i, 0] = (Accel[j, 0] + Accel[j + 1, 0] + Accel[j + 2, 0] + Accel[j + 3, 0] + Accel[j + 4, 0]) / 5;
                OptAccel[i, 1] = (Accel[j, 1] + Accel[j + 1, 1] + Accel[j + 2, 1] + Accel[j + 3, 1] + Accel[j + 4, 1]) / 5;
                OptAccel[i, 2] = (Accel[j, 2] + Accel[j + 1, 2] + Accel[j + 2, 2] + Accel[j + 3, 2] + Accel[j + 4, 2]) / 5;

                OptGyro[i, 0] = (Gyro[j, 0] + Gyro[j + 1, 0] + Gyro[j + 2, 0] + Gyro[j + 3, 0] + Gyro[j + 4, 0]) / 5;
                OptGyro[i, 1] = (Gyro[j, 1] + Gyro[j + 1, 1] + Gyro[j + 2, 1] + Gyro[j + 3, 1] + Gyro[j + 4, 1]) / 5;
                OptGyro[i, 2] = (Gyro[j, 2] + Gyro[j + 1, 2] + Gyro[j + 2, 2] + Gyro[j + 3, 2] + Gyro[j + 4, 2]) / 5;

            }
        }
    }
    #endregion


    #region GETER and SETER
    //public bool GetProcessState()
    //{
    //    return GoProcess;
    //}

    //public void SetProcessState(bool g)
    //{
    //    GoProcess = g;
    //}

    public string GetFilePath()
    {
        return path;
    }

    public void SetFilePath(string p)
    {
        path = p;
    }

    public int GetCountDataInFile()
    {
        return count;
    }
    #endregion

    #region MATHEMATICS_METHODS

    public float[,] GetGyroAngle()
    {
        float[,] gyroangle = new float[count / 5, 3];
        for (int i = 0; i < count / 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                gyroangle[i, j] = OptGyro[i, j] * i;
            }
        }
        return gyroangle;
    }

    public float[,] GetAccelData()
    {
        float[,] acceldata = new float[count / 5, 3];
        for(int i=0;i<count/5;i++)
        {
            for(int j=0;j<3;j++)
            {
                acceldata[i, j] = OptAccel[i, j] * i * i;
            }
        }
        return acceldata;
    }
    #endregion
}
