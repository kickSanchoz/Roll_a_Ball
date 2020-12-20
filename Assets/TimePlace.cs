using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;

//Класс, реализующий сохранение и обновление таблицы рекордов в текстовом файл, а также чтение из этого файла для дальнейшей расстановки
public class TimePlace : MonoBehaviour
{
    public TextMeshProUGUI[] timePlace;
    private int time;
    private int finished;

    private string[] recordsInFileArr = new string[3];
    string path = @"records.txt";

    private void Start()
    {
        time = (int)PlayerPrefs.GetFloat("NeedTime", 0);
        finished = PlayerPrefs.GetInt("isFinished", 0);
        if (finished == 1)
        {
            GetRecordsFromFile();
        }
        ChoosePlace();
        PlayerPrefs.SetInt("isFinished", 0); //Если игра не была пройдена, то установить флаг окончания игры в false
    }

    //Считать все рекорды из файла
    void GetRecordsFromFile()
    {
        using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
        {
            int i = 0;
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                recordsInFileArr[i] = line;                
                i++;                
            }            
        }
        ChoosePlaceInFile();
    }

    //Выбрать подходящую позицию нового времени для записи в файл
    void ChoosePlaceInFile()
    {        
        if(recordsInFileArr[0] == "none" || time < int.Parse(recordsInFileArr[0]))
        {
            recordsInFileArr[2] = recordsInFileArr[1];
            recordsInFileArr[1] = recordsInFileArr[0];
            recordsInFileArr[0] = time.ToString();
        }
        else if(recordsInFileArr[1] == "none" || time < int.Parse(recordsInFileArr[1]))
        {
            recordsInFileArr[2] = recordsInFileArr[1];
            recordsInFileArr[1] = time.ToString();
        }
        else if(recordsInFileArr[2] == "none" || time < int.Parse(recordsInFileArr[2]))
        {
            recordsInFileArr[2] = time.ToString();
        }

        string finalRecords = recordsInFileArr[0]+"\n"+ recordsInFileArr[1] + "\n"+ recordsInFileArr[2];
        try
        {
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(finalRecords);
            }

        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    //Расставить рекорды по местам в таблице
    void ChoosePlace()
    {
        try
        {
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(new FileStream(path, FileMode.OpenOrCreate)))
                {
                    int i = 0;
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        timePlace[i].text = line;
                        i++;
                    }
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.OpenOrCreate)))
                {
                    sw.WriteLine("none\nnone\nnone");
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }       
    }
}
