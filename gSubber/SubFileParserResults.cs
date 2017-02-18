using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gSubber
{
    public class SubFileParserResults
    {
        public SubFile SubFile { get; set; }

        private List<SubFileParserMessage> _Warnings = new List<SubFileParserMessage>();
        public List<SubFileParserMessage> Warnings { get { return _Warnings; } }

        private List<SubFileParserMessage> _Errors = new List<SubFileParserMessage>();
        public List<SubFileParserMessage> Erros { get { return _Errors; } }

        public void AddWarning(SubFileParserMessage argWarning)
        {
            _Warnings.Add(argWarning);
        }

        public void AddError(SubFileParserMessage argError)
        {
            _Errors.Add(argError);
        }
    }
}
