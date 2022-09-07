using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting_Moq.Unit
{
    public class DontTestMicrosoftApi
    {
        private IFiles _files;

        public DontTestMicrosoftApi(IFiles files)
        {
            _files = files;
        }

        public Task SaveFile(string path,Stream file)
        {
            var fileStream = _files.OpenWriteStreamTo(path);

            return file.CopyToAsync(fileStream);
        }
    }

    public interface IFiles
    {
        MemoryStream OpenWriteStreamTo(string path);
    }
}
