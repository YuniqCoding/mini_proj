using System;
using camp.enums;
using camp.model;
using camp.view;

namespace camp
{
    class Program
    {
        private static StudentService studentService;
        private static SubjectService subjectService;
        private static ScoreService scoreService;


        public static void Main(string[] args)
        {
            CampContext campContext = new CampContext();
            studentService = campContext.StudentServ;
            subjectService = campContext.SubjectServ;
            scoreService = campContext.ScoreServ;

            try
            {
                DisplayMainView();
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nError Log: {e.GetType()}");
                Console.WriteLine($"Error Message: {e.Message}");
                Console.WriteLine("오류 발생!\n프로그램을 종료합니다.");
            }
        }

        private static void DisplayMainView()
        {
            bool flag = true;
            while (flag)
            {
                Display.MainView();
                int input = Convert.ToInt32(Console.ReadLine()); // 입력 받기

                switch (input)
                {
                    case 1:
                        DisplayStudentView(); // 수강생 관리
                        break;
                    case 2:
                        DisplayScoreView(); // 점수 관리
                        break;
                    case 3:
                        flag = false; // 프로그램 종료
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.\n되돌아갑니다!");
                        Thread.Sleep(2000); // 2초 대기
                        break;
                }
            }
            Console.WriteLine("프로그램을 종료합니다.");
        }

        private static void DisplayStudentView()
        {
            bool flag = true;
            while (flag)
            {
                Display.StudentView();
                int input = Convert.ToInt32(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        CreateStudent(); // 수강생 등록
                        break;
                    case 2:
                        InquireStudent(); // 수강생 목록 조회
                        break;
                    case 3:
                        InquireStudentById(); // 수강생 조회
                        break;
                    case 4:
                        UpdateStudentStatus(); // 수강생 정보 수정 (상태)
                        break;
                    case 5:
                        InquireStudentByStatus(); // 상태별 수강생 목록 조회
                        break;
                    case 6:
                        DeleteStudent(); // 수강생 삭제
                        break;
                    case 7:
                        flag = false; // 메인 화면 이동
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.\n메인 화면 이동...");
                        flag = false;
                        break;
                }
            }
        }

        private static void CreateStudent()
        {
            Console.WriteLine("\n수강생을 등록합니다...");
            Console.Write("수강생 이름 입력: ");
            string studentName = Console.ReadLine();

            Display.SubjectList(subjectService.InquireSubjects());

            List<Subject> selectSubjects = null;
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("\n필수 과목 3개, 선택 과목 2개의 과목 ID를 쉼표(,)로 구분지어 입력해주세요!");
                string inputSubjects = Console.ReadLine();
                selectSubjects = subjectService.SelectSubjects(inputSubjects);

                if (subjectService.ValidateSelectSubjects(selectSubjects))
                {
                    flag = false;
                }
                else
                {
                    Console.WriteLine("\n필수 과목 3개 이상, 선택 과목 2개 이상은 필수입니다.");
                }
            }

            Console.WriteLine("\n수강생 등록 중....");
            Student student = studentService.CreateStudent(studentName, selectSubjects);
            Console.WriteLine(student.StudentName + " 수강생 등록 성공!\n");
        }

        private static void InquireStudent()
        {
            Console.WriteLine("\n수강생 목록을 조회합니다...");
            // StudentService를 통해 수강생 목록을 가져와서 Display 클래스의 studentList 메서드로 출력
            Display.StudentList(studentService.InquireStudents());
        }

        private static void InquireStudentById()
        {
            // 관리할 수강생 고유 번호를 입력받습니다.
            string studentId = GetStudentId();
            Student student = studentService.FindStudentById(studentId);

            // 수강생 정보를 조회합니다.
            Console.WriteLine("\n" + student.StudentName + " 수강생 정보를 조회합니다...");
            Display.StudentInfo(student);
        }

        private static void UpdateStudentStatus()
        {
            // 관리할 수강생의 고유 번호를 입력받습니다.
            string studentId = GetStudentId();
            Student student = studentService.FindStudentById(studentId);

            // 수강생의 현재 상태를 출력합니다.
            Console.WriteLine("\n" + student.StudentName + " 수강생의 현재 상태는 :" + student.Status);
            Console.Write("변경할 상태(GREEN, RED, YELLOW)를 입력하시오...");
            string status = Console.ReadLine();
            StudentStatus studentStatus = (StudentStatus)Enum.Parse(typeof(StudentStatus), status, true);

            // 수강생의 상태를 업데이트하고 결과를 출력합니다.
            Console.WriteLine("\n" + student.StudentName + " 수강생 상태를 수정합니다...");
            student.UpdateStatus(studentStatus);
            Console.WriteLine("\n" + student.StudentName + " 수강생 상태 " + student.Status + " 수정 성공!");
        }

        private static void InquireStudentByStatus()
        {
            Console.Write("조회할 수강생 상태(GREEN, RED, YELLOW)를 입력하시오...");
            string status = Console.ReadLine();
            StudentStatus studentStatus;

            // Enum.TryParse 메서드를 사용하여 문자열을 열거형으로 변환합니다.
            if (Enum.TryParse(status, true, out studentStatus))
            {
                Console.WriteLine("\n" + studentStatus + " 상태의 수강생 목록 조회 중...");
                Display.StudentList(studentService.InquireStudentByStatus(studentStatus));
            }
            else
            {
                Console.WriteLine("잘못된 상태 입력입니다. 올바른 상태(GREEN, RED, YELLOW)를 입력해주세요.");
            }
        }

        private static void DeleteStudent()
        {
            string studentId = GetStudentId(); // 관리할 수강생 고유 번호를 입력받습니다.
            Student student = studentService.FindStudentById(studentId);

            Console.Write($"\n{student.StudentName} 수강생을 정말 삭제하시겠습니까? (Y/N)");
            string input = Console.ReadLine().ToUpper(); // 대문자로 변환하여 입력 받습니다.

            if (input == "Y")
            {
                if (studentService.DeleteStudent(student))
                {
                    Console.WriteLine($"\n{student.StudentName} 수강생 삭제 성공!");
                    scoreService.DeleteScore(student); // 관련 점수 데이터도 삭제
                }
                else
                {
                    throw new Exception($"{student.StudentName} 수강생 삭제 실패!");
                }
            }
            else
            {
                Console.WriteLine($"{student.StudentName} 수강생 삭제를 취소합니다.");
            }
        }

        private static void DisplayScoreView()
        {
            bool flag = true;
            while (flag)
            {
                Display.ScoreView();
                int input = int.Parse(Console.ReadLine()); // 사용자 입력을 정수로 변환

                switch (input)
                {
                    case 1:
                        CreateScore(); // 수강생의 과목별 시험 회차 및 점수 등록
                        break;
                    case 2:
                        UpdateRoundScoreBySubject(); // 수강생의 과목별 회차 점수 수정
                        break;
                    case 3:
                        InquireRoundGradeBySubject(); // 수강생의 특정 과목 회차별 등급 조회
                        break;
                    case 4:
                        InquireGradeBySubject(); // 특정 수강생 과목별 등급 조회
                        break;
                    case 5:
                        InquireAvgGradeByStatusStudent(); // 특정 상태 수강생들의 필수 과목 평균 등급 조회
                        break;
                    case 6:
                        flag = false; // 메인 화면 이동
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.\n메인 화면 이동...");
                        flag = false; // 잘못된 입력 처리
                        break;
                }
            }
        }

        private static void CreateScore()
        {
            string studentId = GetStudentId(); // 관리할 수강생 고유 번호를 입력받습니다.
            Student student = studentService.FindStudentById(studentId);

            Display.SubjectList(student.SelectSubjects);
            string subjectId = GetSubjectId(); // 관리할 과목 고유 번호를 입력받습니다.
            Subject subject = subjectService.FindSubjectById(subjectId);

            int round = 1;
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("\n등록할 회차를 입력하시오...");
                round = int.Parse(Console.ReadLine());
                if (round <= 10 && round >= 1)
                {
                    if (!scoreService.IsRegistered(student, subject, round))
                    {
                        flag = false;
                    }
                    else
                    {
                        Console.WriteLine("이미 등록된 회차입니다.");
                    }
                }
                else
                {
                    Console.WriteLine("회차는 1 ~ 10");
                }
            }

            int score = 0;
            flag = true;
            while (flag)
            {
                Console.WriteLine("\n등록할 점수를 입력하시오...");
                score = int.Parse(Console.ReadLine());
                if (score <= 100 && score >= 0)
                {
                    flag = false;
                }
                else
                {
                    Console.WriteLine("점수는 0 ~ 100");
                }
            }

            Console.WriteLine("시험 점수를 등록합니다...");
            Score saveScore = scoreService.CreateScore(student, subject, round, score);
            Console.WriteLine("\n" +
                $"{student.StudentName} 수강생 " +
                $"{subject.SubjectName} 과목 " +
                $"{saveScore.Round}회차 " +
                $"{saveScore.Point}점 등록 성공!"
            );
        }

        private static void UpdateRoundScoreBySubject()
        {
            string studentId = GetStudentId(); // 관리할 수강생 고유 번호를 입력받습니다.
            Student student = studentService.FindStudentById(studentId);

            Display.SubjectList(student.SelectSubjects);
            string subjectId = GetSubjectId(); // 관리할 과목 고유 번호를 입력받습니다.
            Subject subject = subjectService.FindSubjectById(subjectId);

            int round = 1;
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("\n수정할 회차를 입력하시오...");
                round = int.Parse(Console.ReadLine());
                if (round <= 10 && round >= 1)
                {
                    if (scoreService.IsRegistered(student, subject, round))
                    {
                        flag = false;
                    }
                    else
                    {
                        Console.WriteLine("등록 되어있지 않은 회차입니다.");
                    }
                }
                else
                {
                    Console.WriteLine("회차는 1 ~ 10 사이여야 합니다.");
                }
            }

            int updateScore = 0;
            flag = true;
            while (flag)
            {
                Console.WriteLine("\n수정할 점수를 입력하시오...");
                updateScore = int.Parse(Console.ReadLine());
                if (updateScore <= 100 && updateScore >= 0)
                {
                    flag = false;
                }
                else
                {
                    Console.WriteLine("점수는 0 ~ 100 사이여야 합니다.");
                }
            }

            Console.WriteLine("시험 점수를 수정합니다...");
            scoreService.UpdateScore(student, subject, round, updateScore);
        }

        private static void InquireRoundGradeBySubject()
        {
            string studentId = GetStudentId(); // 관리할 수강생 고유 번호를 입력받습니다.
            Student student = studentService.FindStudentById(studentId);

            Display.SubjectList(student.SelectSubjects);
            string subjectId = GetSubjectId(); // 관리할 과목 고유 번호를 입력받습니다.
            Subject subject = subjectService.FindSubjectById(subjectId);

            Console.WriteLine($"{subject.SubjectName} 과목 회차별 등급을 조회합니다...");
            List<Score> scoreList = scoreService.InquireGradeByRound(student, subject);
            Display.ScoreListByRound(scoreList);
        }

        private static void InquireGradeBySubject()
        {
            string studentId = GetStudentId(); // 관리할 수강생 고유 번호를 입력받습니다.
            Student student = studentService.FindStudentById(studentId);

            Console.WriteLine($"\n{student.StudentName} 수강생의 과목별 평균 등급 조회 중...");
            Dictionary<Subject, string> avgGrades = scoreService.InquireAvgGradeBySubject(student);
            Display.StudentAvgGradeBySubject(avgGrades);
        }

        private static void InquireAvgGradeByStatusStudent()
        {
            Console.Write("조회할 수강생 상태(GREEN, RED, YELLOW)를 입력하시오...");
            string status = Console.ReadLine();  // 사용자 입력 받기
            StudentStatus studentStatus;

            // 문자열을 StudentStatus 열거형으로 변환합니다. 변환에 실패하면 메시지를 출력합니다.
            if (!Enum.TryParse(status, true, out studentStatus))
            {
                Console.WriteLine("입력한 상태가 유효하지 않습니다.");
                return;
            }

            Console.WriteLine($"\n{studentStatus} 상태 수강생들의 필수 과목 평균 등급 조회 중...");
            List<Student> students = studentService.InquireStudentByStatus(studentStatus);
            Dictionary<Student, string> avgGrades = scoreService.InquireAvgGradeByMandatorySubject(students);
            Display.StudentAvgGradeByMandatorySubject(avgGrades);
        }


        private static string GetStudentId()
        {
            // 사용자에게 관리할 수강생의 번호를 입력받습니다.
            Console.Write("\n관리할 수강생의 번호를 입력하시오...");
            return Console.ReadLine();
        }

        private static string GetSubjectId()
        {
            Console.Write("\n관리할 과목의 번호를 입력하시오...");
            return Console.ReadLine();  // 사용자로부터 입력받은 과목 번호를 반환합니다.
        }

    }



}
