﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFinderPro.Library.TextExtraction.FileProviders;

namespace TextFinderPro.Library.TextExtraction
{
    public class TextExtractor
    {
        public bool AllFilesNeedToExtract { get; set; }
        public ICollection<string> ExtentionsForExtracting { get; set; }
        public ICollection<string> ExcludedExtentions { get; set; }

        public TextExtractor()
        {
            ExtentionsForExtracting = new List<string>();
            ExcludedExtentions = new List<string>();
        }

        public bool AreSettingsCorrect()
        {
            if (ExtentionsForExtracting.Intersect(ExcludedExtentions).Count() != 0) return false;
            if (AllFilesNeedToExtract && ExtentionsForExtracting.Count() != 0) return false;
            if (AllFilesNeedToExtract && ExcludedExtentions.Count() != 0) return false;
            return true;
        }

        public IEnumerable<ExtractedFileText> GetTextFromFiles(IEnumerable<string> filePathsCollection)
        {
            if (!AreSettingsCorrect())
                throw new InvalidOperationException("Extracting settings are not correct");
            if (!AllFilesNeedToExtract)
                filePathsCollection = filePathsCollection.Where(file => ExtentionsForExtracting.Contains(Path.GetExtension(file)) && !ExcludedExtentions.Contains(Path.GetExtension(file)));
            return filePathsCollection.GroupBy(file => FileProviderFactory.GetTextProvider(file))
                                      .SelectMany((groupedFiles) => groupedFiles.Key.GetTextFromFiles(groupedFiles));
        }

    }
}
