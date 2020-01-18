using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gSubber.Core.SubtitleFile
{
    public class SubFileParserResults
    {
        public SubFile SubFile { get; set; }
        
        public List<SubFileParserMessage> Warnings { get; } = new List<SubFileParserMessage>();
        
        public List<SubFileParserMessage> Errors { get; } = new List<SubFileParserMessage>();

        public void AddWarning(SubFileParserMessage argWarning)
        {
            Warnings.Add(argWarning);
        }

        public void AddError(SubFileParserMessage argError)
        {
            Errors.Add(argError);
        }
    }
}
