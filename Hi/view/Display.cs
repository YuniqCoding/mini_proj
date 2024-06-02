using System;
using camp.model;

namespace camp.view
{
	public class Display
	{
        public static void MainView()
        {
            Console.WriteLine("\n==================================");
            Console.WriteLine("캠 수강생 관리 프로그램 실행 중...");
            Console.WriteLine("1. 수강생 관리");
            Console.WriteLine("2. 점수 관리");
            Console.WriteLine("3. 프로그램 종료");
            Console.Write("관리 항목을 선택하세요...");
        }

        public static void StudentView()
        {
            Console.WriteLine("==================================");
            Console.WriteLine("수강생 관리 실행 중...");
            Console.WriteLine("1. 수강생 등록");
            Console.WriteLine("2. 수강생 목록 조회");
            Console.WriteLine("3. 수강생 조회");
            Console.WriteLine("4. 수강생 상태 수정");
            Console.WriteLine("5. 상태별 수강생 목록 조회");
            Console.WriteLine("6. 수강생 삭제");
            Console.WriteLine("7. 메인 화면 이동");
            Console.Write("관리 항목을 선택하세요...");
        }

        public static void ScoreView()
        {
            Console.WriteLine("==================================");
            Console.WriteLine("점수 관리 실행 중...");
            Console.WriteLine("1. 수강생의 과목별 시험 회차 및 점수 등록");
            Console.WriteLine("2. 수강생의 과목별 회차 점수 수정");
            Console.WriteLine("3. 수강생의 특정 과목 회차별 등급 조회");
            Console.WriteLine("4. 특정 수강생 과목별 등급 조회");
            Console.WriteLine("5. 특정 상태 수강생들의 필수 과목 평균 등급 조회");
            Console.WriteLine("6. 메인 화면 이동");
            Console.Write("관리 항목을 선택하세요...");
        }


        public static void SubjectList(List<Subject> subjects)
        {
            Console.WriteLine("과목 목록 조회 중...\n");
            // subjects 리스트의 각 과목에 대해 반복
            foreach (Subject subject in subjects)
            {
                Console.WriteLine("==================================");
                Console.WriteLine("과목 ID = " + subject.SubjectId); // 과목 ID 출력
                Console.WriteLine("과목 명 = " + subject.SubjectName); // 과목 이름 출력
                Console.WriteLine("과목 타입 = " + subject.SubjectOption); // 과목 타입 출력
            }
        }

        public static void StudentList(List<Student> students)
        {
            // 주어진 학생 목록을 순회하며 각 학생의 정보 출력
            foreach (Student student in students)
            {
                Console.WriteLine("==================================");
                Console.WriteLine("수강생 ID = " + student.StudentId); // 학생 ID 출력
                Console.WriteLine("수강생 이름 = " + student.StudentName); // 학생 이름 출력
            }
        }

        public static void ScoreListByRound(List<Score> scores)
        {
            foreach (Score score in scores)
            {
                Console.WriteLine("==================================");
                Console.WriteLine("회차 = " + score.Round);  // 회차 정보 출력
                Console.WriteLine("등급 = " + score.Grade);  // 등급 정보 출력
            }
        }


        public static void StudentInfo(Student student)
        {
            Console.WriteLine("==================================");
            Console.WriteLine("수강생 ID = " + student.StudentId); // 수강생의 ID 출력
            Console.WriteLine("수강생 이름 = " + student.StudentName); // 수강생의 이름 출력
            Console.WriteLine("수강생 상태 = " + student.Status); // 수강생의 상태 출력

            // 수강생이 선택한 과목들을 출력
            foreach (Subject selectSubject in student.SelectSubjects)
            {
                Console.WriteLine("선택한 과목명: " + selectSubject.SubjectName);
            }
        }

        public static void StudentAvgGradeBySubject(Dictionary<Subject, string> avgGrades)
        {
            foreach (KeyValuePair<Subject, string> entry in avgGrades)
            {
                Console.WriteLine("==================================");
                Console.WriteLine("과목 명 = " + entry.Key.SubjectName); // 과목 이름 출력
                Console.WriteLine("과목 타입 = " + entry.Key.SubjectOption); // 과목 타입 출력
                Console.WriteLine("과목 평균 등급 = " + entry.Value); // 과목의 평균 등급 출력
            }
        }

        public static void StudentAvgGradeByMandatorySubject(Dictionary<Student, string> avgGrades)
        {
            foreach (KeyValuePair<Student, string> entry in avgGrades)
            {
                Console.WriteLine("==================================");
                Console.WriteLine("수강생 ID = " + entry.Key.StudentId); // 수강생의 ID를 출력합니다.
                Console.WriteLine("수강생 이름 = " + entry.Key.StudentName); // 수강생의 이름을 출력합니다.
                Console.WriteLine("수강생 필수 과목 평균 등급 = " + entry.Value); // 수강생의 평균 등급을 출력합니다.
            }
        }


    }
}

