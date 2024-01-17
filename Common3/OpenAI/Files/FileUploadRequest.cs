using System;
using System.IO;

namespace OpenAI.Files
{
    public sealed class FileUploadRequest : IDisposable
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="filePath">
        /// Local file path to upload.
        /// </param>
        /// <param name="purpose">
        /// The intended purpose of the uploaded documents.
        /// If the purpose is set to "fine-tune", each line is a JSON record with "prompt" and "completion"
        /// fields representing your training examples.
        /// </param>
        /// <exception cref="FileNotFoundException"></exception>
        public FileUploadRequest(string filePath, string purpose)
        {
            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException(string.Format("Could not find the {0)} file located at {filePath}","filePath"));
            }

            File = System.IO.File.OpenRead(filePath);
            FileName = Path.GetFileName(filePath);

            Purpose = purpose;
        }

        ~FileUploadRequest()
        {
            Dispose(false);
        }

        public Stream File { get; private set; }

        public string FileName { get; private set; }

        public string Purpose { get; private set; }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (File != null) File.Close();
                if (File != null) File.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
