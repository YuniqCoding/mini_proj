using System;
using camp.enums;
using camp.model;
using camp.repository;

namespace camp
{
    public class StudentService
    {
        private readonly StudentStore studentStore;

        public StudentService(StudentStore studentStore)
        {
            this.studentStore = studentStore;
        }

        public Student CreateStudent(string studentName, List<Subject> selectSubjects)
        {
            // 새로운 학생 객체 생성
            Student student = new Student(studentName, StudentStatus.GREEN, selectSubjects);

            // 학생 저장소에 학생 저장 후 저장된 학생 반환
            return studentStore.Save(student);
        }

        public List<Student> InquireStudents()
        {
            // studentStore에서 저장된 모든 학생 객체를 반환
            return studentStore.Store;
        }

        public Student FindStudentById(string studentId)
        {
            // 학생 저장소에서 모든 학생을 순회하며 ID를 비교합니다.
            foreach (Student student in studentStore.Store)
            {
                // 입력받은 studentId와 학생의 ID가 같은 경우 해당 학생을 반환합니다.
                if (student.StudentId == studentId)
                {
                    return student;
                }
            }

            // 해당 ID의 학생을 찾지 못한 경우 예외를 발생시킵니다.
            throw new Exception("Not Found Student");
        }

        public List<Student> InquireStudentByStatus(StudentStatus studentStatus)
        {
            List<Student> students = new List<Student>();
            // studentStore의 모든 학생을 순회하며 상태를 확인합니다.
            foreach (Student student in studentStore.Store)
            {
                // 학생의 상태가 입력받은 상태와 동일한 경우 리스트에 추가합니다.
                if (student.Status == studentStatus)
                {
                    students.Add(student);
                }
            }
            return students;
        }

        public bool DeleteStudent(Student student)
        {
            // studentStore에서 주어진 student 객체를 삭제하고 성공 여부를 반환합니다.
            return studentStore.Delete(student);
        }


    }

}

