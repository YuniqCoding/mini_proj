using System;
using camp.enums;
using camp.model;
using camp.repository;

namespace camp.data
{
    public class DummyData
    {
        private readonly SubjectStore subjectStore;
        private readonly StudentStore studentStore;
        private readonly ScoreService scoreService;

        public DummyData(StudentStore studentStore, SubjectStore subjectStore, ScoreService scoreService)
        {
            this.studentStore = studentStore;
            this.subjectStore = subjectStore;
            this.scoreService = scoreService;
        }

        public void InitDummyData()
        {
            foreach (Student student in GetStudents(new List<Subject>
       {
           this.subjectStore.Store[1],
           this.subjectStore.Store[2],
           this.subjectStore.Store[3],
           this.subjectStore.Store[7],
           this.subjectStore.Store[8]
       }))
            {
                studentStore.Save(student);
            }

            GetScores(this.studentStore.Store);
        }

        public List<Student> GetStudents(List<Subject> subjects)
        {
            return new List<Student>
       {
           new Student("Robbie", StudentStatus.GREEN, subjects),
           new Student("Robbert", StudentStatus.RED, subjects),
           new Student("Bob", StudentStatus.YELLOW, subjects),
           new Student("Leo", StudentStatus.YELLOW, subjects),
           new Student("Lio", StudentStatus.GREEN, subjects),
           new Student("Rob", StudentStatus.RED, subjects),
           new Student("Robbin", StudentStatus.GREEN, subjects),
           new Student("Ryan", StudentStatus.GREEN, subjects),
           new Student("Bobby", StudentStatus.GREEN, subjects),
           new Student("Toby", StudentStatus.RED, subjects)
       };
        }

        public void GetScores(List<Student> students)
        {
            foreach (Student student in students)
            {
                foreach (Subject selectSubject in student.SelectSubjects)
                {
                    for (int i = 1; i < 11; i++)
                    {
                        scoreService.CreateScore(student, selectSubject, i, (int)(new Random().NextDouble() * 40) + 60);
                    }
                }
            }
        }
    }
}

