using System;
using camp.enums; 

namespace camp.model
{
    public class Subject
    {
        public string SubjectId { get; }
        public string SubjectName { get; }
        public SubjectType SubjectOption { get; }

        public Subject(string subjectId, string subjectName, SubjectType subjectType)
        {
            SubjectId = subjectId;
            SubjectName = subjectName;
            SubjectOption = subjectType;
        }

        public bool IsMandatory()
        {
            return SubjectOption == SubjectType.MANDATORY;
        }
    }
}
