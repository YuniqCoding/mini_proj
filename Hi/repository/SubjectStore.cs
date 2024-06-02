using System;
using camp.context;
using camp.enums;
using camp.model;

namespace camp.repository
{
    public class SubjectStore
    {
        private int storeIndex;
public List<Subject> Store { get; }

        public SubjectStore()
        {
            Store = new List<Subject>
        {
            new Subject(
                Sequence(),
                "Java",
                SubjectType.MANDATORY
            ),
            new Subject(
                Sequence(),
                "객체지향",
                SubjectType.MANDATORY
            ),
            new Subject(
                Sequence(),
                "Spring",
                SubjectType.MANDATORY
            ),
            new Subject(
                Sequence(),
                "JPA",
                SubjectType.MANDATORY
            ),
            new Subject(
                Sequence(),
                "MySQL",
                SubjectType.MANDATORY
            ),
            new Subject(
                Sequence(),
                "디자인 패턴",
                SubjectType.CHOICE
            ),
            new Subject(
                Sequence(),
                "Spring Security",
                SubjectType.CHOICE
            ),
            new Subject(
                Sequence(),
                "Redis",
                SubjectType.CHOICE
            ),
            new Subject(
                Sequence(),
                "MongoDB",
                SubjectType.CHOICE
            )
        };
        }

        

        private string Sequence()
        {
            storeIndex++;
            return Constants.IndexType.SUBJECT + storeIndex;
        }
    }
}

