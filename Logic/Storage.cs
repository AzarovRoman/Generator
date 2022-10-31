using Newtonsoft.Json;

namespace Logic
{
    public class Storage
    {
        public List<Password> Passwords;
        public string Path;

        private static Storage _storageInstance;
        public string FileName;

        public string _pathVariableName = "PwdGeneratorSavePath";

        private Storage()
        {
            Passwords = new List<Password>();
            FileName = "qwe.json";
            Path = Environment.GetEnvironmentVariable(_pathVariableName, EnvironmentVariableTarget.User);
        }


        public static Storage GetStorage()
        {
            if (_storageInstance == null)
            {
                _storageInstance = new Storage();
                return _storageInstance;
            }
            return _storageInstance;
        }

        public void Deserialize()
        {
            try
            {
                using (StreamReader reader = new StreamReader($"{Path}\\{FileName}"))
                {
                    string json = reader.ReadToEnd();
                    if(json != "")
                    {
                        Passwords = JsonConvert.DeserializeObject<List<Password>>(json);
                    }
                }
            }
            catch
            {

            }
        }

        public void Serialize()
        {
            if (Passwords != null || Passwords.Count != 0)
            {
                using (StreamWriter writer = new StreamWriter($"{Path}\\{FileName}"))
                {
                    string json = JsonConvert.SerializeObject(Passwords);
                    writer.WriteLine(json);
                }
            }
        }
    }
}
