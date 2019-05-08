namespace Infrastructure.Management
{
    using System.IO;
    using System.IO.Abstractions;
    using System.Runtime.Serialization.Formatters.Binary;

    public static class Saver
    {
        public static void Save<T>(T obj, FileInfoBase file)
        {
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }

            using (var stream = file.Open(FileMode.Create))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, obj);
            }
        }
    }
}