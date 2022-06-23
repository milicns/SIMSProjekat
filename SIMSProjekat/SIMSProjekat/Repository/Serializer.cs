using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SIMSProjekat.Model;

namespace SIMSProjekat.Repository
{
    public class Serializer<T>
    {

        public void JsonSerialize(object data, string filePath)
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            StreamWriter sw = new StreamWriter(filePath);
            JsonWriter jsonWriter = new JsonTextWriter(sw);

            jsonSerializer.Serialize(jsonWriter, data);
            jsonWriter.Close();
            sw.Close();
        }

        public object JsonDeserialize(string filePath)
        {
           
            List<T> outputList;
            

            using (Stream Stream = new FileStream(filePath, FileMode.OpenOrCreate))
            using (StreamReader sr = new StreamReader(Stream))
            using (JsonReader jsonReader = new JsonTextReader(sr))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                outputList = jsonSerializer.Deserialize<List<T>>(jsonReader);
            }

            if (outputList == null)
            {
                outputList = new List<T>();
            }
            return outputList;

        }

    }
}
