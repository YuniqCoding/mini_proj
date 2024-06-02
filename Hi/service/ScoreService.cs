using System;
using camp.enums;
using camp.model;
using camp.repository;

namespace camp
{
	public class ScoreService
	{
        private readonly ScoreStore scoreStore;
        private readonly IGradePolicy mandatoryPolicy;
        private readonly IGradePolicy choicePolicy;

        public ScoreService(ScoreStore scoreStore, IGradePolicy mandatoryPolicy, IGradePolicy choicePolicy)
        {
            this.scoreStore = scoreStore;
            this.mandatoryPolicy = mandatoryPolicy;
            this.choicePolicy = choicePolicy;
        }

        // 해당 학생, 해당 과목, 해당 회차, 해당 점수를 받아서 등록하는 메소드
        public Score CreateScore(Student student, Subject subject, int round, int score)
        {
            string grade = GetGrade(subject, score);
            return scoreStore.Save(new Score(student.StudentId, subject.SubjectId, round, score, grade));
        }

        public bool IsRegistered(Student student, Subject subject, int round)
        {
            // scoreStore의 모든 점수를 순회하며 조건에 맞는 점수가 있는지 검사합니다.
            foreach (Score score in scoreStore.Store)
            {
                // 학생 ID, 과목 ID, 회차가 모두 일치하는지 확인합니다.
                if (score.StudentId == student.StudentId &&
                    score.SubjectId == subject.SubjectId &&
                    score.Round == round)
                {
                    return true;
                }
            }
            return false; // 일치하는 점수가 없다면 false 반환
        }

        public void UpdateScore(Student student, Subject subject, int round, int updateScore)
        {
            Score findScore = null;
            // scoreStore에서 모든 점수를 검색하여 일치하는 점수를 찾습니다.
            foreach (Score score in scoreStore.Store)
            {
                if (score.StudentId == student.StudentId &&
                    score.SubjectId == subject.SubjectId &&
                    score.Round == round)
                {
                    findScore = score;
                    break;  // 일치하는 점수를 찾았으므로 반복을 중단합니다.
                }
            }

            if (findScore != null)
            {
                // 점수와 등급을 업데이트합니다.
                findScore.Point = updateScore;
                findScore.Grade = GetGrade(subject, updateScore);
            }
            else
            {
                // 일치하는 점수가 없는 경우 예외를 발생시킵니다.
                throw new Exception("Not Found Score");
            }
        }

        public List<Score> InquireGradeByRound(Student student, Subject subject)
        {
            List<Score> scores = new List<Score>();
            // scoreStore의 모든 점수를 순회하면서 학생 ID와 과목 ID가 일치하는 점수를 찾습니다.
            foreach (Score score in scoreStore.Store)
            {
                if (score.StudentId == student.StudentId &&
                    score.SubjectId == subject.SubjectId)
                {
                    scores.Add(score);
                }
            }
            return scores; // 일치하는 모든 점수를 반환합니다.
        }



        public void DeleteScore(Student student)
        {
            // 학생과 관련된 점수를 저장할 리스트를 생성합니다.
            List<Score> deleteScoreList = new List<Score>();

            // scoreStore의 모든 점수를 순회하며 학생의 ID와 일치하는 점수를 찾습니다.
            foreach (Score score in scoreStore.Store)
            {
                if (score.StudentId == student.StudentId)
                {
                    deleteScoreList.Add(score);
                }
            }

            // 일치하는 모든 점수를 삭제합니다.
            scoreStore.DeleteAll(deleteScoreList);
        }

        public Dictionary<Subject, string> InquireAvgGradeBySubject(Student student)
        {
            Dictionary<Subject, string> avgGrades = new Dictionary<Subject, string>();
            // 수강생이 선택한 모든 과목에 대해 반복 처리합니다.
            foreach (Subject selectSubject in student.SelectSubjects)
            {
                List<Score> scores = InquireGradeByRound(student, selectSubject);
                if (scores.Count > 0)
                {
                    float totalScore = GetTotalScore(scores);
                    int avgScore = (int)Math.Round(totalScore / scores.Count);
                    string avgGrade = GetGrade(selectSubject, avgScore);
                    avgGrades.Add(selectSubject, avgGrade);
                }
                else
                {
                    avgGrades.Add(selectSubject, "등록된 점수가 없습니다.");
                }
            }
            return avgGrades;
        }

        public Dictionary<Student, string> InquireAvgGradeByMandatorySubject(List<Student> students)
        {
            Dictionary<Student, string> avgGrades = new Dictionary<Student, string>();
            foreach (Student student in students)
            {
                int size = student.GetMandatorySubjects().Count; // 필수 과목의 수
                int totalScore = 0;
                foreach (Subject mandatorySubject in student.GetMandatorySubjects())
                {
                    List<Score> scores = InquireGradeByRound(student, mandatorySubject);
                    if (scores.Count > 0)
                    {
                        totalScore += (int)Math.Round((float)GetTotalScore(scores) / scores.Count);
                    }
                }
                // 평균 점수를 계산하고 해당 점수를 등급으로 변환합니다.
                string avgGrade = size > 0 ? mandatoryPolicy.ConvertToGrade((int)Math.Round((float)totalScore / size))
                                           : "필수 과목에 등록된 점수가 없습니다.";

                // 학생과 해당 평균 등급을 사전에 추가합니다.
                avgGrades.Add(student, avgGrade);
            }
            return avgGrades;
        }


        // 과목과 점수를 받아서 해당하는 등급을 return해주는 메소드
        private string GetGrade(Subject subject, int score)
        {
            return subject.SubjectOption == SubjectType.MANDATORY
                ? mandatoryPolicy.ConvertToGrade(score)
                : choicePolicy.ConvertToGrade(score);
        }

        private int GetTotalScore(List<Score> scores)
        {
            int totalScore = 0;
            // 점수 리스트에서 각 점수 객체의 점수를 합산합니다.
            foreach (Score score in scores)
            {
                totalScore += score.Point;  // C#에서는 속성 접근 방식을 사용합니다.
            }
            return totalScore;  // 계산된 총점을 반환합니다.
        }

    }
}

