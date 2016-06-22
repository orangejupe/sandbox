namespace App
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class FilesEnumerator : IEnumerable<FileInfo>
    {
        private readonly string directoryName;
        public FilesEnumerator(string directoryName)
        {
            this.directoryName = directoryName;
        }

        public IEnumerator<FileInfo> GetEnumerator()
        {
            return Directory.GetFiles(directoryName, "Engine*.log", SearchOption.TopDirectoryOnly)
                .Select(x => new FileInfo(x))
                .OrderBy(x => x.CreationTime)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}