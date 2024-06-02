using System;
using camp.enums;

namespace camp.model
{
    public class Student
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public StudentStatus Status { get; set; }
        public List<Subject> SelectSubjects { get; }

        public Student(string studentName, StudentStatus status, List<Subject> selectSubjects)
        {
            StudentName = studentName;
            SelectSubjects = selectSubjects;
            Status = status;
        }

        public void UpdateStatus(StudentStatus status)
        {
            Status = status;
        }

        public List<Subject> GetMandatorySubjects()
        {
            List<Subject> mandatorySubjects = new List<Subject>();
            foreach (Subject selectSubject in SelectSubjects)
            {
                if (selectSubject.SubjectOption == SubjectType.MANDATORY)
                {
                    mandatorySubjects.Add(selectSubject);
                }
            }
            return mandatorySubjects;
        }
    }
}

