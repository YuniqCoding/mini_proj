using System;
using camp.context;
using camp.model;

namespace camp.repository
{
    public class StudentStore
    {
        public List<Student> Store { get; } 
        private int storeIndex;

        public StudentStore()
        {
            Store = new List<Student>();
        }

        private string Sequence()
        {
            storeIndex++;
            return Constants.IndexType.STUDENT + storeIndex;
        }

        public Student Save(Student student)
        {
            student.StudentId = Sequence();
            Store.Add(student);
            return student;
        }

        public bool Delete(Student student)
        {
            return Store.Remove(student);
        }
    }
}

