namespace Infrastructure.Management
{
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Abstractions;
    using System.Runtime.Serialization.Formatters.Binary;

    public static class Loader
    {
        public static T Load<T>(FileInfoBase file)
        {
            if (!file.Exists)
            {
                return default;
            }

            using (var stream = file.Open(FileMode.Open))
            {
                var binaryFormatter = new BinaryFormatter();
                return (T) binaryFormatter.Deserialize(stream);
            }
        }

        public static IEnumerable<T> LoadAll<T>(DirectoryInfoBase directory)
        {
            if (!directory.Exists)
            {
                yield break;
            }

            foreach (var file in directory.GetFiles())
            {
                yield return Load<T>(file);
            }
        }
    }
}
