using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gSubber.Core.SubtitleFile
{
    public class SubFileParserResults : ISubFileParserResults
    {
        public ISubFile SubFile { get; set; }

        public IList<SubFileParserMessage> Warnings { get; } = new List<SubFileParserMessage>();

        public IList<SubFileParserMessage> Errors { get; } = new List<SubFileParserMessage>();

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
