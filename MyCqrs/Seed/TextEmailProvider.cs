using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace MyCqrs.Seed
{
    public class TextEmailProvider : IEmail
    {
        private static ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();
        private readonly string path;
        public TextEmailProvider(string path)
        {
            this.path = path;
        }
        public void Send(string from, string to, string cc, string subject, string emailBody)
        {
            var emailContent = string.Format("[At]: {5} [From]:{0} [To]:{1} [CC]:{2} [Subject]: {3} [Body]: {4}", from, to, cc, subject, emailBody, DateTime.Now);

            // Set Status to Locked
            _readWriteLock.EnterWriteLock();
            try
            {
                // Append text to the file
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(emailContent);
                    sw.Close();
                }
            }
            finally
            {
                // Release lock
                _readWriteLock.ExitWriteLock();
            }

        }
    }
}

