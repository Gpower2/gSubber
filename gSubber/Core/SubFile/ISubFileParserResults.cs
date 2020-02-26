using System.Collections.Generic;

namespace gSubber.Core.SubtitleFile
{
    public interface ISubFileParserResults
    {
        ISubFile SubFile { get; set; }

        IList<SubFileParserMessage> Warnings { get; }
        IList<SubFileParserMessage> Errors { get; }

        void AddWarning(SubFileParserMessage argWarning);
        void AddError(SubFileParserMessage argError);
    }
}