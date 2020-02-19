using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gSubber.Core.SubtitleFile
{
    public class SubFileParserResults
    {
        public ISubFile SubFile { get; set; }
        
        public IEnumerable<SubFileParserMessage> Warnings { get; } = new List<SubFileParserMessage>();
        
        public IEnumerable<SubFileParserMessage> Errors { get; } = new List<SubFileParserMessage>();

        public void AddWarning(SubFileParserMessage argWarning)
        {
            (Warnings as IList).Add(argWarning);
        }

        public void AddError(SubFileParserMessage argError)
        {
            (Errors as IList).Add(argError);
        }
    }
}
