﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFinderPro.Library.TextExtraction.FileProviders
{
    public abstract class FileProvider
    {
        public FileProviderType FileProviderType { get; }

        protected FileProvider(FileProviderType fileProviderType)
        {
            FileProviderType = fileProviderType;
        }

        public FileProvider() { }

        
        protected abstract string TryGetTextFromFile(string filePath);

        public virtual ExtractedFileText GetTextFromFile(string filePath)
        {
            try
            {
                return new ExtractedFileText(TryGetTextFromFile(filePath) , filePath , true);
            }
            catch (Exception ex)
            {
                return FailFileText.FromFile(filePath , ex.Message);
            }
        }
        public virtual IEnumerable<ExtractedFileText> GetTextFromFiles(IEnumerable<string> filePathCollection)
        {
            return filePathCollection.AsParallel().Select(GetTextFromFile);
        }
    }
}
