using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TestSkyori
{
    public class FileGenerator
    {
        /// <summary>
        /// Записать результат проверки в файл
        /// </summary>
        /// <param name="checkResult"></param>
        /// <param name="path"></param>
        public void WriteJsonFile(Result checkResult, string path)
        {
            if (checkResult == null || string.IsNullOrEmpty(path))
                return;

            var listResult = new List<Result>();
            
            
            //Если файл не существует то создаём и записываем туда результат
            //Если существует то считываем информацию из фаайла
            //Конвертируем в List<Result>, дополняем новой записью и записываем снова
            if (!File.Exists(path))
            {
                listResult.Add(checkResult);
                var json = JsonConvert.SerializeObject(listResult, Formatting.Indented);
                File.WriteAllText(path, json);
            }
            else
            {
                string objectJsonFile = File.ReadAllText(path); //данные считываются в строку 
                listResult = JsonConvert.DeserializeObject<List<Result>>(objectJsonFile);
                listResult.Add(checkResult);
                var json = JsonConvert.SerializeObject(listResult, Formatting.Indented);
                File.WriteAllText(path, json);
            }
        }
    }
}